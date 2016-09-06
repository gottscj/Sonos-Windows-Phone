using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the MusicServices1 (urn:schemas-upnp-org:service:MusicServices:1)
    ///     service.
    /// </summary>
    public class MusicServices1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the A_ARG_TYPE_ServiceDescriptorList state variable.
        /// </summary>
        protected const string CsStateVarAargtypeServiceDescriptorList = "A_ARG_TYPE_ServiceDescriptorList";

        /// <summary>
        ///     The name for the A_ARG_TYPE_ServiceTypeList state variable.
        /// </summary>
        protected const string CsStateVarAargtypeServiceTypeList = "A_ARG_TYPE_ServiceTypeList";

        /// <summary>
        ///     The name for the ServiceId state variable.
        /// </summary>
        protected const string CsStateVarServiceId = "ServiceId";

        /// <summary>
        ///     The name for the ServiceListVersion state variable.
        /// </summary>
        protected const string CsStateVarServiceListVersion = "ServiceListVersion";

        /// <summary>
        ///     The name for the SessionId state variable.
        /// </summary>
        protected const string CsStateVarSessionId = "SessionId";

        /// <summary>
        ///     The name for the Username state variable.
        /// </summary>
        protected const string CsStateVarUsername = "Username";

        /// <summary>
        ///     The name for the GetSessionId action.
        /// </summary>
        protected const string CsActionGetSessionId = "GetSessionId";

        /// <summary>
        ///     The name for the ListAvailableServices action.
        /// </summary>
        protected const string CsActionListAvailableServices = "ListAvailableServices";

        /// <summary>
        ///     The name for the UpdateAvailableServices action.
        /// </summary>
        protected const string CsActionUpdateAvailableServices = "UpdateAvailableServices";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the MusicServices1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the MusicServices1 service from.</param>
        public MusicServices1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Public Static Methods

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the ServiceListVersion state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<XElement> ServiceListVersionChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the ServiceListVersionChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnServiceListVersionChanged(StateVariableChangedEventArgs e)
        {
            if (ServiceListVersionChanged != null)
                ServiceListVersionChanged(this, e.StateVarValue);
        }

        /// <summary>
        ///     Raises the StateVariableChanged event.
        /// </summary>
        protected override void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
            // Determine state variable that is changing
            switch (e.StateVarName)
            {
                case CsStateVarServiceListVersion:
                    // Raise the event for the ServiceListVersion state variable
                    OnServiceListVersionChanged(e);
                    break;
            }


            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the GetSessionId action.
        /// </summary>
        /// <param name="serviceId">In value for the ServiceId action parameter.</param>
        /// <param name="username">In value for the Username action parameter.</param>
        /// <returns>Out value for the SessionId action parameter.</returns>
        public async Task<ActionResult> GetSessionId(Int16 serviceId, String username)
        {
            var loIn = new object[2];

            loIn[0] = serviceId;
            loIn[1] = username;

            var action = new SoapAction
            {
                ArgNames = new[] {CsStateVarServiceId, CsStateVarUsername},
                Name = CsActionGetSessionId,
                ExpectedReplyCount = 1
            };

            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            Exception ex = null;
            if (result.Error != null)
            {
                ex = result.Error.Exception;
            }

            return new ActionResult(result.XElement, ex);
        }

        /// <summary>
        ///     Executes the ListAvailableServices action.
        /// </summary>
        public async Task<ActionResult> ListAvailableServices()
        {
            var loIn = new object[0];

            //object[] loOut = InvokeAction(csAction_ListAvailableServices, loIn);
            var action = new SoapAction
            {
                ArgNames = new[] {CsStateVarServiceListVersion},
                Name = CsActionListAvailableServices,
                ExpectedReplyCount = 3
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the UpdateAvailableServices action.
        /// </summary>
        public async Task UpdateAvailableServices()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionUpdateAvailableServices,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}