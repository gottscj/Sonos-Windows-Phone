using System.Linq;
using Sonos.Models;
using Sonos.Services;
using UPnP;

namespace Sonos.Devices
{
    /// <summary>
    ///     Encapsulates a specific Device class for the Sonos PLAY:3 device (urn:schemas-upnp-org:device:MediaServer:1).
    /// </summary>
    public class MediaServer : Device
    {
        #region Protected Constants

        /// <summary>
        ///     Stores the constant ID value for child service ContentDirectory1.
        /// </summary>
        protected const string CsServiceContentDirectory1Id = "urn:upnp-org:serviceId:ContentDirectory";

        /// <summary>
        ///     Stores the constant ID value for child service ConnectionManager1.
        /// </summary>
        protected const string CsServiceConnectionManager1Id = "urn:upnp-org:serviceId:ConnectionManager";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the Sonos PLAY:3 device from a base device.
        /// </summary>
        /// <param name="device">The base device to create the Sonos PLAY:3 device from.</param>
        public MediaServer(Device device)
            : base(device)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a new ConnectionManager1 (urn:schemas-upnp-org:service:ConnectionManager:1) child Service for the device.
        /// </summary>
        private ConnectionManager1 _connectionManager1;

        /// <summary>
        ///     Gets a new ContentDirectory1 (urn:schemas-upnp-org:service:ContentDirectory:1) child Service for the device.
        /// </summary>
        private ContentDirectory1Service _contentDirectory1Service;

        public ContentDirectory1Service ContentDirectory1Service
        {
            get
            {
                if (_contentDirectory1Service != null) return _contentDirectory1Service;

                var service = Services.FirstOrDefault(x => x.ServiceType == ServiceTypes.ContentDirectory);
                if (service != null)
                    _contentDirectory1Service = new ContentDirectory1Service(service);

                return _contentDirectory1Service;
            }
        }

        public ConnectionManager1 ConnectionManager1Service
        {
            get
            {
                if (_connectionManager1 != null) return _connectionManager1;
                var service = Services.FirstOrDefault(x => x.ServiceType == ServiceTypes.ConnectionManager);

                if (service != null)
                    _connectionManager1 = new ConnectionManager1(service);

                return _connectionManager1;
            }
        }

        #endregion
    }
}