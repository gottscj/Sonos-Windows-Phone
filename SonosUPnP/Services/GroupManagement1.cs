using System;
using System.Threading.Tasks;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the GroupManagement1 (urn:schemas-upnp-org:service:GroupManagement:1)
    ///     service.
    /// </summary>
    public class GroupManagement1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the GroupCoordinatorIsLocal state variable.
        /// </summary>
        protected const string CsStateVarGroupCoordinatorIsLocal = "GroupCoordinatorIsLocal";

        /// <summary>
        ///     The name for the LocalGroupUUID state variable.
        /// </summary>
        protected const string CsStateVarLocalGroupUuid = "LocalGroupUUID";

        /// <summary>
        ///     The name for the ResetVolumeAfter state variable.
        /// </summary>
        protected const string CsStateVarResetVolumeAfter = "ResetVolumeAfter";

        /// <summary>
        ///     The name for the VolumeAVTransportURI state variable.
        /// </summary>
        protected const string CsStateVarVolumeAvTransportUri = "VolumeAVTransportURI";

        /// <summary>
        ///     The name for the AddMember action.
        /// </summary>
        protected const string CsActionAddMember = "AddMember";

        /// <summary>
        ///     The name for the RemoveMember action.
        /// </summary>
        protected const string CsActionRemoveMember = "RemoveMember";

        /// <summary>
        ///     The name for the ReportTrackBufferingResult action.
        /// </summary>
        protected const string CsActionReportTrackBufferingResult = "ReportTrackBufferingResult";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the GroupManagement1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the GroupManagement1 service from.</param>
        public GroupManagement1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the GroupCoordinatorIsLocal state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<Boolean> GroupCoordinatorIsLocalChanged;

        /// <summary>
        ///     Occurs when the service notifies that the LocalGroupUUID state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> LocalGroupUuidChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ResetVolumeAfter state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<Boolean> ResetVolumeAfterChanged;

        /// <summary>
        ///     Occurs when the service notifies that the VolumeAVTransportURI state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> VolumeAvTransportUriChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the GroupCoordinatorIsLocalChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnGroupCoordinatorIsLocalChanged(StateVariableChangedEventArgs e)
        {
            if (GroupCoordinatorIsLocalChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the LocalGroupUUIDChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnLocalGroupUuidChanged(StateVariableChangedEventArgs e)
        {
            if (LocalGroupUuidChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ResetVolumeAfterChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnResetVolumeAfterChanged(StateVariableChangedEventArgs e)
        {
            if (ResetVolumeAfterChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the VolumeAVTransportURIChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnVolumeAvTransportUriChanged(StateVariableChangedEventArgs e)
        {
            if (VolumeAvTransportUriChanged != null)
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
                case CsStateVarGroupCoordinatorIsLocal:
                    // Raise the event for the GroupCoordinatorIsLocal state variable
                    OnGroupCoordinatorIsLocalChanged(e);
                    break;

                case CsStateVarLocalGroupUuid:
                    // Raise the event for the LocalGroupUUID state variable
                    OnLocalGroupUuidChanged(e);
                    break;

                case CsStateVarResetVolumeAfter:
                    // Raise the event for the ResetVolumeAfter state variable
                    OnResetVolumeAfterChanged(e);
                    break;

                case CsStateVarVolumeAvTransportUri:
                    // Raise the event for the VolumeAVTransportURI state variable
                    OnVolumeAvTransportUriChanged(e);
                    break;
            }

            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the AddMember action.
        /// </summary>
        /// <param name="memberId">In value for the MemberID action parameter.</param>
        public async Task<ActionResult> AddMember(String memberId)
        {
            var loIn = new object[1];

            loIn[0] = memberId;
            var action = new SoapAction
            {
                ArgNames = new[] {"MemberID"},
                Name = CsActionAddMember,
                ExpectedReplyCount = 3
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the RemoveMember action.
        /// </summary>
        /// <param name="memberId">In value for the MemberID action parameter.</param>
        public async Task RemoveMember(String memberId)
        {
            var loIn = new object[1];

            loIn[0] = memberId;
            var action = new SoapAction
            {
                ArgNames = new[] {"MemberID"},
                Name = CsActionRemoveMember,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ReportTrackBufferingResult action.
        /// </summary>
        /// <param name="memberId">In value for the MemberID action parameter.</param>
        /// <param name="resultCode">In value for the ResultCode action parameter.</param>
        public async Task ReportTrackBufferingResult(String memberId, Int32 resultCode)
        {
            var loIn = new object[2];

            loIn[0] = memberId;
            loIn[1] = resultCode;
            var action = new SoapAction
            {
                ArgNames = new[] {"MemberID", "ResultCode"},
                Name = CsActionRemoveMember,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}