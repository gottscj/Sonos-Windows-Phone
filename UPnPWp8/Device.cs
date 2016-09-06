using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using UPnP.Extensions;
using UPnP.Models;

namespace UPnP
{
    public class Device
    {
        private readonly XElement _description;

        private readonly Uri _uri;
        private readonly List<Device> _deviceList;
        private readonly List<Service> _services;
        private string _uuid = "";

        /// <summary>
        ///     Creates a new device from an already created device.
        /// </summary>
        /// <param name="device">The device to create from.</param>
        public Device(Device device)
            : this(device.Uri, device.Description)
        {
        }

        public Device()
        {
        }

        public Device(Uri uri, XElement description)
        {
            _uri = uri;
            _description = description;
            _deviceList = new List<Device>();
            _services = new List<Service>();

            // get all services in device
            var serviceList = Description.TryGetElement("serviceList");
            if (serviceList != null)
            {
                foreach (var serviceDescription in serviceList.GetDescendants("service"))
                {
                    _services.Add(new Service(serviceDescription, BaseAddress));
                }
            }


            // get all devices in devicelist
            // get all services in device
            var deviceList = Description.TryGetElement("deviceList");
            if (deviceList == null) return;
            foreach (var device in deviceList.GetDescendants("device"))
            {
                _deviceList.Add(new Device(uri, device));
            }
        }

        protected XElement Description
        {
            get { return _description; }
        }

        protected Uri Uri
        {
            get { return _uri; }
        }

        protected string BaseAddress
        {
            get { return Uri.Scheme + "://" + Uri.Authority; }
        }

        protected ReadOnlyCollection<Device> DeviceList
        {
            get
            {
                return new ReadOnlyCollection<Device>(_deviceList);
            }
        }

        protected ReadOnlyCollection<Service> Services
        {
            get
            {
                return new ReadOnlyCollection<Service>(_services);
            }
        }

        public string DeviceId
        {
            get { return Description.TryGetValue("serialNum"); }
        }

        public string FriendlyName
        {
            get { return Description.TryGetValue("friendlyName"); }
        }

        public string DeviceType
        {
            get { return Description.TryGetValue("deviceType"); }
        }

        public string ModelName
        {
            get { return Description.TryGetValue("modelName"); }
        }


        public string ImageUrl
        {
            get
            {
                var localPath = Description.TryGetValue("url");
                if (string.IsNullOrEmpty(localPath))
                {
                    return string.Empty;
                }
                return BaseAddress + localPath;
            }
        }

        public string Uuid
        {
            get
            {
                if (!string.IsNullOrEmpty(_uuid)) return _uuid;

                _uuid = Description.TryGetValue("UDN");
                if (_uuid.StartsWith("uuid:"))
                {
                    _uuid = _uuid.Substring(5);
                }
                return _uuid;
            }
        }

        public static async Task<Device> CreateAsync(string uri)
        {
            var dom = await Utility.DownloadXmlAsync(uri).ConfigureAwait(false);
            return dom.GetDefaultNamespace() != "urn:schemas-upnp-org:device-1-0" ? default(Device) : new Device(new Uri(uri), dom);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public DeviceInfo DeviceInfo
        {
            get
            {
                return new DeviceInfo(Uri, Description);     
            }
        }
    }
}