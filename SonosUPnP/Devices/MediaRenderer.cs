using System.Linq;
using Sonos.Models;
using Sonos.Services;
using UPnP;

namespace Sonos.Devices
{
    /// <summary>
    ///     Encapsulates a specific Device class for the Sonos PLAY:3 device (urn:schemas-upnp-org:device:MediaRenderer:1).
    /// </summary>
    public class MediaRenderer : Device
    {
        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the Sonos PLAY:3 device from a base device.
        /// </summary>
        /// <param name="device">The base device to create the Sonos PLAY:3 device from.</param>
        public MediaRenderer(Device device)
            : base(device)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a new AVTransport1 (urn:schemas-upnp-org:service:AVTransport:1) child Service for the device.
        /// </summary>
        private AvTransport1 _avTransport1;

        /// <summary>
        ///     Gets a new ConnectionManager1 (urn:schemas-upnp-org:service:ConnectionManager:1) child Service for the device.
        /// </summary>
        private ConnectionManager1 _connectionManager1;

        /// <summary>
        ///     Gets a new RenderingControl1 (urn:schemas-upnp-org:service:RenderingControl:1) child Service for the device.
        /// </summary>
        private RenderingControl1 _renderingControl1;

        public RenderingControl1 RenderingControl1Service
        {
            get
            {
                Service service = Services.FirstOrDefault(x => x.ServiceType == ServiceTypes.RenderingControl);
                if (service != null)
                    _renderingControl1 = new RenderingControl1(service);

                return _renderingControl1;
            }
        }

        public ConnectionManager1 ConnectionManager1Service
        {
            get
            {
                if (_connectionManager1 != null) return _connectionManager1;
                Service service = Services.FirstOrDefault(x => x.ServiceType == ServiceTypes.ConnectionManager);

                if (service != null)
                    _connectionManager1 = new ConnectionManager1(service);

                return _connectionManager1;
            }
        }

        public AvTransport1 AvTransport1Service
        {
            get
            {
                if (_avTransport1 != null) return _avTransport1;

                Service service = Services.FirstOrDefault(x => x.ServiceType == ServiceTypes.AvTransport);

                if (service != null)
                    _avTransport1 = new AvTransport1(service);
                return _avTransport1;
            }
        }

        #endregion
    }
}