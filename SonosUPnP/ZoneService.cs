using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Messaging;
using Sonos.Devices;
using Sonos.Extensions;
using Sonos.Models;
using Sonos.Models.MessageBus;
using UPnP.Extensions;

namespace Sonos
{
    /// <summary>
    ///     Encapsulates Current Zone player services and caching.
    /// </summary>
    public class ZoneService
    {
        private readonly IMessenger _messageBus;
        private string _address;
        private ZonePlayer _coordinator;
        private ZoneVolume _volume;
        private PositionInfo _positionInfo;
        private Task _pollTask;

        private List<ZoneDevice> _zones;

        public ZoneService(IMessenger messageBus)
        {
            CurrentTransportState = TransportState.Unknown;
            _messageBus = messageBus;
        }

        public async Task<bool> Initialize()
        {
            _zones = await FindZoneDevices();

            if (_zones.Count == 0) return false;

            // use first found zoneplayer which supports zonetopology to get topology
            var tmpZonePlayer = new ZonePlayer(_zones.First());

            Topology = await GetTopology(tmpZonePlayer);
            
            var coordinator = (from member in Topology.Members
                               from zone in _zones
                               where member.IsCoordinator && member.Uuid == zone.Uuid
                               select zone)
                .FirstOrDefault();

            CurrentCoordinator = new ZonePlayer(coordinator);

            // poll sonos for state changes.
            _pollTask = Poll();

            return true;
        }
        public string CoordinatorName
        {
            get { return CurrentCoordinator != null ? CurrentCoordinator.ZoneName : ""; }
        }

        public string CoordinatorUuid
        {
            get { return CurrentCoordinator != null ? CurrentCoordinator.Uuid : ""; }
        }

        public string CoordinatorDeviceId
        {
            get
            {
                return CurrentCoordinator != null ? CurrentCoordinator.DeviceId : "";
            }
        }

        public string CoordinatorAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(_address)) return _address;
                if (CurrentCoordinator == null) return string.Empty;

                _address = CurrentCoordinator.DeviceInfo.BaseAddress;
                return _address;
            }
        }

        private ZoneTopology _zoneTopology;

        public ZoneTopology Topology
        {
            get { return _zoneTopology; }
            set
            {
                _zoneTopology = value;
                _messageBus.Send(new ZoneTopologyChangedMessage() {ZoneTopology = _zoneTopology});
            }
        }

        private ZonePlayer CurrentCoordinator
        {
            get
            {
                return _coordinator;
            }
            set
            {
                if (_coordinator != null)
                {
                    //_coordinator.ZoneGroupTopology1.ZoneGroupStateChanged -= OnTopologyChange;
                    _coordinator.MediaServer.ContentDirectory1Service.ContainerUpdateIDsChanged -=
                    ContainerUpdateIDsChanged;
                }
                
                _coordinator = value;

                var deviceInfo = _coordinator.DeviceInfo;
                _address = deviceInfo.BaseAddress;

                
                //_coordinator.ZoneGroupTopology1.ZoneGroupStateChanged += OnTopologyChange;
                _coordinator.MediaServer.ContentDirectory1Service.ContainerUpdateIDsChanged +=
                    ContainerUpdateIDsChanged;
                _messageBus.Send(new CoordinatorChangedMessage {Coordinator = _coordinator});
            }
        }

        public TransportState CurrentTransportState { get; private set; }

        public ZoneVolume CurrentVolume
        {
            get { return _volume; }
        }

        private async Task Poll()
        {
            while (CurrentCoordinator != null)
            {
                var sw = new Stopwatch();
                sw.Start();

                var volume = await GetVolume();
                var positionInfo = await GetPositionInfo();
                var transportState = await GetTransportInfo();

                if (_volume == null)
                    Debug.WriteLine("Volume = null");
                else
                    Debug.WriteLine("volume: {0}, ismuted: {1}", volume.Volume, volume.IsMuted);
                // volume
                if (_volume == null ||
                    _volume.Volume != volume.Volume ||
                    _volume.IsMuted != volume.IsMuted)
                {
                    
                    _volume = volume;
                    _messageBus.Send(new VolumeChangedMessage { ZoneVolume = volume });
                }

                // position info
                if (_positionInfo == null ||
                    _positionInfo.Track.Title != positionInfo.Track.Title ||
                    _positionInfo.Track.AlbumTitle != positionInfo.Track.Artist ||
                    _positionInfo.Track.ImageUri != positionInfo.Track.ImageUri)
                {
                    _positionInfo = positionInfo;
                    _messageBus.Send(new PositionInfoChangedMessage {PositionInfo = positionInfo});
                }
               
                // transport info
                if (transportState.TransportState != CurrentTransportState)
                {
                    CurrentTransportState = transportState.TransportState;
                    _messageBus.Send(new TransportStateChangedMessage {TransportState = transportState.TransportState});
                }
                
                sw.Stop();
                Debug.WriteLine("ZoneService poll {0} ms", sw.ElapsedMilliseconds);
                var timeout = 1000 - (int) sw.ElapsedMilliseconds;
                if (timeout < 0) timeout = 1000;

                await Task.Delay(timeout)
                    .ConfigureAwait(false);
            }
        }

        public async Task<PositionInfo> GetPositionInfo()
        {
            if (CurrentCoordinator == null) return null;
            var positionInfoResult = await CurrentCoordinator
                .MediaRenderer
                .AvTransport1Service
                .GetPositionInfo(0)
                .ConfigureAwait(false);

            if (positionInfoResult.Exception != null) return null;

            var positionInfo = new PositionInfo(positionInfoResult.Result, CoordinatorAddress);
            return positionInfo;
        }
        public async Task<TransportInfo> GetTransportInfo()
        {
            if (CurrentCoordinator == null) return null;

            var actionResult = await CurrentCoordinator
                .MediaRenderer
                .AvTransport1Service
                .GetTransportInfo(0)
                .ConfigureAwait(false);

            if (actionResult.Exception != null) return null;

            var transportInfo = new TransportInfo(actionResult.Result);
            return transportInfo;
        }

        public void ChangeCoordinator(string uuid)
        {
            var newCurrent = _zones.FirstOrDefault(x => x.Uuid == uuid);
            if (newCurrent != null)
            {
                CurrentCoordinator = new ZonePlayer(newCurrent);
            }
        }

        public async Task<List<ZoneDevice>> FindZoneDevices()
        {
            List<ZoneDevice> zones = null;
            var zoneCount = 0;
            // try three times to find zones
            for (var i = 0; i < 3; i++)
            {
                zones = await ZoneDevice.FindAndCreateAsync(true);
                zoneCount = zones.Count();
                if (zoneCount > 0)
                    break;
            }
            // TODO: Create error if no zones found!
            if (zones == null || zoneCount <= 0) return zones;
            return zones;
        }

        public async Task<ZoneTopology> GetTopology(ZoneDevice zoneDevice)
        {
            var target = zoneDevice.DeviceInfo.BaseAddress + "/status/topology";
            var topologyXml = await new WebClient().DownloadStringAsync(target)
                .ConfigureAwait(false);

            var topologyElement = XElement.Parse(topologyXml);

            var members = topologyElement
                .Descendants(XName.Get("ZonePlayer"))
                .Select(zonePlayerElement => new ZoneMember(zonePlayerElement))
                .OrderBy(z => z.Title)
                .ToList();

            return new ZoneTopology(members);
        }

        public async Task<ZoneVolume> GetVolume()
        {
            var isMuteActionResult =
                await
                    CurrentCoordinator.MediaRenderer.RenderingControl1Service.GetMute(0, AargtypeMuteChannelEnum.Master).ConfigureAwait(false);

            if (isMuteActionResult.Exception != null) 
                return new ZoneVolume(null, new ZoneMute(null));

            var zoneMute = new ZoneMute(isMuteActionResult.Result);

            ActionResult volumeActionResult =
                await CurrentCoordinator.MediaRenderer.RenderingControl1Service.GetVolume(0,
                    AargtypeChannelEnum.Master);

            if (volumeActionResult.Exception != null) 
                return new ZoneVolume(null, new ZoneMute(null));

            var zoneVolume = new ZoneVolume(volumeActionResult.Result, zoneMute);

            return zoneVolume;
        }

        public Task<ActionResult> Browse(string objectId, uint startingIndex, uint requestedCount, BrowseFlag browseFlag = BrowseFlag.BrowseDirectChildren)
        {
            const string filter = "dc:title,res,dc:creator,upnp:artist,upnp:album,upnp:albumArtURI";

            return CurrentCoordinator
                .MediaServer
                .ContentDirectory1Service
                .Browse(objectId, browseFlag, filter, startingIndex, requestedCount, "");
        }

        public Task SetVolume(int volume)
        {
            return CurrentCoordinator
                .MediaRenderer
                .RenderingControl1Service
                .SetVolume(0, AargtypeChannelEnum.Master, (UInt16) volume);
        }

        public Task SetMute(bool desiredMute)
        {
            return CurrentCoordinator
                .MediaRenderer
                .RenderingControl1Service
                .SetMute(0, AargtypeMuteChannelEnum.Master, desiredMute);
        }

        public async Task Play(Container container)
        {
            var addUriReponseXelement = await CurrentCoordinator
                .MediaRenderer
                .AvTransport1Service
                .AddUriToQueue(0, container.Resource, "", 0, true);

            var addResponse = new AddUriToQueueResponse(addUriReponseXelement.Result);

            await CurrentCoordinator
                .MediaRenderer
                .AvTransport1Service
                .Seek(0, AargtypeSeekModeEnum.Tracknr, addResponse.FirstTrackNumberEnqueued);

            await CurrentCoordinator.MediaRenderer.AvTransport1Service.Play(0, PlaySpeed.Normal);
        }

        public Task Play()
        {
            return CurrentCoordinator.MediaRenderer.AvTransport1Service.Play(0, PlaySpeed.Normal);
        }
        public Task Next()
        {
            return CurrentCoordinator.MediaRenderer.AvTransport1Service.Next(0);
        }

        public Task Previous()
        {
            return CurrentCoordinator.MediaRenderer.AvTransport1Service.Previous(0);
        }
        public async Task PlayQueueIndex(int index)
        {
            await CurrentCoordinator
                .MediaRenderer
                .AvTransport1Service
                .Seek(0, AargtypeSeekModeEnum.Tracknr, index.ToString(CultureInfo.InvariantCulture));

            await CurrentCoordinator.MediaRenderer.AvTransport1Service.Play(0, PlaySpeed.Normal);
        }

        public Task Pause()
        {
            return CurrentCoordinator.MediaRenderer.AvTransport1Service.Pause(0);
        }

        public Task<ActionResult> GetAvailableServices()
        {
            return CurrentCoordinator.MusicServices1.ListAvailableServices();
        }

        private string _houseHoldId = string.Empty;
        public async Task<string> GetHouseholdId()
        {
            if (_houseHoldId != null) return _houseHoldId;
            var actionResult = await CurrentCoordinator.DeviceProperties1.GetHouseholdId();
            if (actionResult.Exception != null)
            {
                Debug.WriteLine("Error getting house hold id: " + actionResult.Exception.Message);
                return "";
            }

            _houseHoldId = actionResult.Result.TryGetElementValue("CurrentHouseholdID");
            return _houseHoldId;
        }

        private string _sessionId = string.Empty;

        public async Task<string> GetSessionId(short id, string username)
        {
            if (_sessionId != null) return _sessionId;
            var actionResult = await CurrentCoordinator.MusicServices1.GetSessionId(id, username);

            if (actionResult.Exception != null)
            {
                Debug.WriteLine("Error getting sessionId: " + actionResult.Exception.Message);
                return "";
            }

            if (actionResult.Result == null) return "";

            _sessionId = actionResult.Result.TryGetElementValue("SessionId");
            return _sessionId;
        }

        private readonly Dictionary<string, string> _strings = new Dictionary<string, string>(); 
        public async Task<string> GetString(string identifier)
        {
            lock (_strings)
            {
                string wantedString;
                if (_strings.TryGetValue(identifier, out wantedString))
                    return wantedString;
            }
            var actionResult = await CurrentCoordinator.SystemProperties1.GetString(identifier);

            if (actionResult.Exception != null)
            {
                Debug.WriteLine("Error getting string '"+identifier+"': "+actionResult.Exception.Message);
                return "";
            }
            
            var result = actionResult.Result.TryGetElementValue("StringValue");
            if (result == "") return result;

            lock (_strings)
            {
                _strings.Add(identifier, result);
            }
            return result;
        }

        public async Task<string> GetRadioLocation()
        {
            return await GetString("R_RadioLocation");
        }

        private void ContainerUpdateIDsChanged(object sender, string queueId)
        {
            if (queueId.StartsWith("Q:0"))
            {
                _messageBus.Send(new QueueChangedMessage {QueueId = queueId});
            }
        }

#if false
        private void OnTopologyChange(object sender, XElement topologychangedXElement)
        {
            if (Topology != null)
            {
                var newTopology = topologychangedXElement.ToZoneGroups();
                var noUpdateNeeded = newTopology.Any(nz => Topology.Any(oz => oz.Id == nz.Id));
                if (noUpdateNeeded) return;
            }

            _topology = topologychangedXElement;
            _storage.Save(Tokens.ZoneGroupTopology, _topology);
            _messageBus.Send(new EventArgs(), Message.TopologyChanged);

            var query = topologychangedXElement.GetTopologyCoordinators();

            // if we have no coordinator or the coordinator isnt present in the new topology update the coordinator
            if (CurrentCoordinator == null || query.SingleOrDefault(x => x.Uuid == CurrentCoordinator.Uuid) == null)
            {
                if (query.Any())
                {
                    var activeZoneMember = query.First();

                    // for now just take the first found.
                    var z = Zones.FirstOrDefault(x => x.ZoneName == activeZoneMember.Title);
                    CurrentCoordinator = new ZonePlayer(z);
                }
            }

            if (_pollTask == null)
            {
                _pollTask = Poll();
            }
        } 
#endif
    }
}