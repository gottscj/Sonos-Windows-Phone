using System;
using System.Threading.Tasks;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the ConnectionManager1 (urn:schemas-upnp-org:service:ConnectionManager:1)
    ///     service.
    /// </summary>
    public class ConnectionManager1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the SourceProtocolInfo state variable.
        /// </summary>
        protected const string CsStateVarSourceProtocolInfo = "SourceProtocolInfo";

        /// <summary>
        ///     The name for the SinkProtocolInfo state variable.
        /// </summary>
        protected const string CsStateVarSinkProtocolInfo = "SinkProtocolInfo";

        /// <summary>
        ///     The name for the CurrentConnectionIDs state variable.
        /// </summary>
        protected const string CsStateVarCurrentConnectionIDs = "CurrentConnectionIDs";


        /// <summary>
        ///     The name for the GetProtocolInfo action.
        /// </summary>
        protected const string CsActionGetProtocolInfo = "GetProtocolInfo";

        /// <summary>
        ///     The name for the GetCurrentConnectionIDs action.
        /// </summary>
        protected const string CsActionGetCurrentConnectionIDs = "GetCurrentConnectionIDs";

        /// <summary>
        ///     The name for the GetCurrentConnectionInfo action.
        /// </summary>
        protected const string CsActionGetCurrentConnectionInfo = "GetCurrentConnectionInfo";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the ConnectionManager1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the ConnectionManager1 service from.</param>
        public ConnectionManager1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the SourceProtocolInfo state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> SourceProtocolInfoChanged;

        /// <summary>
        ///     Occurs when the service notifies that the SinkProtocolInfo state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> SinkProtocolInfoChanged;

        /// <summary>
        ///     Occurs when the service notifies that the CurrentConnectionIDs state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> CurrentConnectionIDsChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the SourceProtocolInfoChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnSourceProtocolInfoChanged(StateVariableChangedEventArgs e)
        {
            if (SourceProtocolInfoChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the SinkProtocolInfoChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnSinkProtocolInfoChanged(StateVariableChangedEventArgs e)
        {
            if (SinkProtocolInfoChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the CurrentConnectionIDsChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnCurrentConnectionIDsChanged(StateVariableChangedEventArgs e)
        {
            if (CurrentConnectionIDsChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the StateVariableChanged event.
        /// </summary>
        protected override void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
            // Determine state variable that is changing
            switch (e.StateVarName)
            {
                case CsStateVarSourceProtocolInfo:
                    // Raise the event for the SourceProtocolInfo state variable
                    OnSourceProtocolInfoChanged(e);
                    break;

                case CsStateVarSinkProtocolInfo:
                    // Raise the event for the SinkProtocolInfo state variable
                    OnSinkProtocolInfoChanged(e);
                    break;

                case CsStateVarCurrentConnectionIDs:
                    // Raise the event for the CurrentConnectionIDs state variable
                    OnCurrentConnectionIDsChanged(e);
                    break;
            }


            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the GetProtocolInfo action.
        /// </summary>
        public async Task<ActionResult> GetProtocolInfo()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetProtocolInfo,
                ExpectedReplyCount = 2
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetCurrentConnectionIDs action.
        /// </summary>
        /// <returns>Out value for the ConnectionIDs action parameter.</returns>
        public async Task<ActionResult> GetCurrentConnectionIDs()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetCurrentConnectionIDs,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetCurrentConnectionInfo action.
        /// </summary>
        /// <param name="connectionId">In value for the ConnectionID action parameter.</param>
        public async Task<ActionResult> GetCurrentConnectionInfo(Int32 connectionId)
        {
            var loIn = new object[1];

            loIn[0] = connectionId;
            var action = new SoapAction
            {
                ArgNames = new[] {"ConnectionID"},
                Name = CsActionGetCurrentConnectionInfo,
                ExpectedReplyCount = 7
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        #endregion
    }
}