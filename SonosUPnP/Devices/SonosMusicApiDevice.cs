using System;
using System.Linq;
using System.Xml.Linq;
using Sonos.Models;
using Sonos.Services;
using UPnP;

namespace Sonos.Devices
{
    public class SonosMusicApiDevice : Device
    {
        private readonly string _deviceId;
        private readonly ZoneService _zoneService;
        private readonly ISonosMusicApi _sonosMusicApi;


        private MusicApiService _service;

        public SonosMusicApiDevice(ISonosMusicApi sonosMusicApi, string deviceId,
            MusicServiceContainer musicServiceContainer, Uri uri, ZoneService zoneService)
            : base(uri,
                CreateSmapiDevice(deviceId, musicServiceContainer.Title, musicServiceContainer.ImageUri,
                    musicServiceContainer.ServiceId, uri.LocalPath))
        {
            _deviceId = deviceId;
            _zoneService = zoneService;
            _sonosMusicApi = sonosMusicApi;
            MusicServiceContainer = musicServiceContainer;
        }

        private MusicServiceContainer MusicServiceContainer { get; set; }

        /// <summary>
        ///     Gets a new Sonos Music API service for the device.
        /// </summary>
        public MusicApiService MusicApiService
        {
            get
            {
                if (_service != null) return _service;

                var service = Services.FirstOrDefault(s => s.ServiceType == ServiceTypes.SonosServices);

                if (service != null)
                {
                    _service = new MusicApiService(_sonosMusicApi, _deviceId, service, MusicServiceContainer, _zoneService);
                }

                return _service;
            }
        }

        private static XElement CreateSmapiDevice(string deviceId, string title, string imageUrl,
            string serviceId, string controlUrl)
        {
            var s = "<root xmlns=\"urn:schemas-upnp-org:device-1-0\">" +
                        "<device>" +
                        "<deviceType />" +
                        "<friendlyName>" + title + "</friendlyName>" +
                        "<modelNumber />" +
                        "<modelName />" +
                        "<serialNum>" + deviceId + "</serialNum>" +
                        "<UDN />" +
                        "<url>" + imageUrl + "</url>" +
                        "<serviceList>" +
                        "<service>" +
                        "<serviceType>" + ServiceTypes.SonosServices + "</serviceType>" +
                        "<serviceId>" + serviceId + "</serviceId>" +
                        "<controlURL>" + controlUrl + "</controlURL>" +
                        "<eventSubURL />" +
                        "<SCPDURL />" +
                        "</service>" +
                        "</serviceList>" +
                        "<deviceList />" +
                        "</device>" +
                    "</root>";

            return XElement.Parse(s);
        }
    }
}