using System.Linq;
using Sonos.Models;
using Sonos.Services;
using UPnP;

namespace Sonos.Devices
{
    /// <summary>
    ///     Encapsulates a specific Device class for the Sonos PLAY:x device (urn:schemas-upnp-org:device:ZonePlayer:1).
    /// </summary>
    public class ZonePlayer : ZoneDevice
    {
        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the Sonos PLAY:x device from a base device.
        /// </summary>
        /// <param name="device">The base device to create the Sonos PLAY:x device from.</param>
        public ZonePlayer(Device device)
            : base(device)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a new AlarmClock1 (urn:schemas-upnp-org:service:AlarmClock:1) child Service for the device.
        /// </summary>
        private AlarmClock1 _aClock;

        /// <summary>
        ///     Gets a new DeviceProperties1 (urn:schemas-upnp-org:service:DeviceProperties:1) child Service for the device.
        /// </summary>
        private DeviceProperties1 _devProp;

        /// <summary>
        ///     Gets a new GroupManagement1 (urn:schemas-upnp-org:service:GroupManagement:1) child Service for the device.
        /// </summary>
        private GroupManagement1 _grpmgnt;

        /// <summary>
        ///     Gets a new MediaRenderer (urn:schemas-upnp-org:device:MediaRenderer:1) child Device for the device.
        /// </summary>
        private MediaRenderer _mRenderer;

        /// <summary>
        ///     Gets a new MusicServices1 (urn:schemas-upnp-org:service:MusicServices:1) child Service for the device.
        /// </summary>
        private MusicServices1 _mServ;

        /// <summary>
        ///     Gets a new MediaServer (urn:schemas-upnp-org:device:MediaServer:1) child Device for the device.
        /// </summary>
        private MediaServer _mServer;

        /// <summary>
        ///     Gets a new SystemProperties1 (urn:schemas-upnp-org:service:SystemProperties:1) child Service for the device.
        /// </summary>
        private SystemProperties1 _sysProp;

        /// <summary>
        ///     Gets a new ZoneGroupTopology1 (urn:schemas-upnp-org:service:ZoneGroupTopology:1) child Service for the device.
        /// </summary>
        private ZoneGroupTopology1 _zGrp;

        public MediaServer MediaServer
        {
            get
            {
                if (_mServer != null) return _mServer;

                var device = DeviceList.FirstOrDefault(d => d.DeviceType == DeviceTypes.MediaServer);
                _mServer = device != null ? new MediaServer(device) : null;
                return _mServer;
            }
        }

        public MediaRenderer MediaRenderer
        {
            get
            {
                if (_mRenderer != null) return _mRenderer;
                var device = DeviceList.FirstOrDefault(d => d.DeviceType == DeviceTypes.MediaRenderer);

                if (device != null)
                    _mRenderer = new MediaRenderer(device);

                return _mRenderer;
            }
        }

        public AlarmClock1 AlarmClock1
        {
            get
            {
                if (_aClock != null) return _aClock;
                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.AlarmClock);

                if (service != null)
                    _aClock = new AlarmClock1(service);

                return _aClock;
            }
        }

        public MusicServices1 MusicServices1
        {
            get
            {
                if (_mServ != null) return _mServ;
                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.MusicServices1);

                if (service != null)
                    _mServ = new MusicServices1(service);

                return _mServ;
            }
        }

        public DeviceProperties1 DeviceProperties1
        {
            get
            {
                if (_devProp != null) return _devProp;
                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.DeviceProperties1);

                if (service != null)
                    _devProp = new DeviceProperties1(service);

                return _devProp;
            }
        }

        public SystemProperties1 SystemProperties1
        {
            get
            {
                if (_sysProp != null) return _sysProp;

                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.SystemProperties1);

                if (service != null)
                    _sysProp = new SystemProperties1(service);

                return _sysProp;
            }
        }

        public ZoneGroupTopology1 ZoneGroupTopology1
        {
            get
            {
                if (_zGrp != null) return _zGrp;
                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.ZoneGroupTopology1);

                if (service != null)
                    _zGrp = new ZoneGroupTopology1(service);

                return _zGrp;
            }
        }

        public GroupManagement1 GroupManagement1
        {
            get
            {
                if (_grpmgnt != null) return _grpmgnt;

                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.GroupManagement1);

                if (service != null)
                    _grpmgnt = new GroupManagement1(service);

                return _grpmgnt;
            }
        }

        #endregion
    }
}