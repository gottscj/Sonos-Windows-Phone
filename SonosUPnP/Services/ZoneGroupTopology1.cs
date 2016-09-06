using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sonos.Extensions;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the ZoneGroupTopology1 (urn:schemas-upnp-org:service:ZoneGroupTopology:1)
    ///     service.
    /// </summary>
    public class ZoneGroupTopology1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the AvailableSoftwareUpdate state variable.
        /// </summary>
        protected const string CsStateVarAvailableSoftwareUpdate = "AvailableSoftwareUpdate";

        /// <summary>
        ///     The name for the ZoneGroupState state variable.
        /// </summary>
        protected const string CsStateVarZoneGroupState = "ZoneGroupState";

        /// <summary>
        ///     The name for the ThirdPartyMediaServersX state variable.
        /// </summary>
        protected const string CsStateVarThirdPartyMediaServersX = "ThirdPartyMediaServersX";

        /// <summary>
        ///     The name for the AlarmRunSequence state variable.
        /// </summary>
        protected const string CsStateVarAlarmRunSequence = "AlarmRunSequence";

        /// <summary>
        ///     The name for the CheckForUpdate action.
        /// </summary>
        protected const string CsActionCheckForUpdate = "CheckForUpdate";

        /// <summary>
        ///     The name for the BeginSoftwareUpdate action.
        /// </summary>
        protected const string CsActionBeginSoftwareUpdate = "BeginSoftwareUpdate";

        /// <summary>
        ///     The name for the ReportUnresponsiveDevice action.
        /// </summary>
        protected const string CsActionReportUnresponsiveDevice = "ReportUnresponsiveDevice";

        /// <summary>
        ///     The name for the ReportAlarmStartedRunning action.
        /// </summary>
        protected const string CsActionReportAlarmStartedRunning = "ReportAlarmStartedRunning";

        /// <summary>
        ///     The name for the SubmitDiagnostics action.
        /// </summary>
        protected const string CsActionSubmitDiagnostics = "SubmitDiagnostics";

        /// <summary>
        ///     The name for the RegisterMobileDevice action.
        /// </summary>
        protected const string CsActionRegisterMobileDevice = "RegisterMobileDevice";

        protected const string CsActionZoneGroupName = "ZoneGroupName";
        protected const string CsActionZoneGroupId = "ZoneGroupID";
        protected const string CsActionZonePlayerUuiDsInGroup = "ZonePlayerUUIDsInGroup";

        private int _eventSemaphore;

        private int EventSemaphore
        {
            get { return _eventSemaphore; }
            set
            {
                if (_eventSemaphore == 0 && value == 1)
                {
                    SubcribeToEvents();
                }
                else if (_eventSemaphore > 0 && value == 0)
                {
                    UnsubscribeToEvents();
                }
                _eventSemaphore = value;
            }
        }

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the ZoneGroupTopology1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the ZoneGroupTopology1 service from.</param>
        public ZoneGroupTopology1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the AvailableSoftwareUpdate state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<XElement> AvailableSoftwareUpdateChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ZoneGroupState state variable has changed its value.
        /// </summary>
        private event StateVariableChangedEventHandler<XElement> ZoneGroupStateChangedInternal;

        public event StateVariableChangedEventHandler<XElement> ZoneGroupStateChanged
        {
            add
            {
                EventSemaphore++;
                ZoneGroupStateChangedInternal += value;
            }
            remove
            {
                EventSemaphore--;
                ZoneGroupStateChangedInternal -= value;
            }
        }

        /// <summary>
        ///     Occurs when the service notifies that the ThirdPartyMediaServersX state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<XElement> ThirdPartyMediaServersXChanged;

        /// <summary>
        ///     Occurs when the service notifies that the AlarmRunSequence state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<XElement> AlarmRunSequenceChanged;

        public event StateVariableChangedEventHandler<XElement> ZoneGroupNameChanged;
        public event StateVariableChangedEventHandler<XElement> ZoneGroupIdChanged;
        public event StateVariableChangedEventHandler<XElement> ZonePlayerUuiDsInGroupChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the AvailableSoftwareUpdateChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnAvailableSoftwareUpdateChanged(StateVariableChangedEventArgs e)
        {
            if (AvailableSoftwareUpdateChanged != null)
            {
                AvailableSoftwareUpdateChanged(this, e.StateVarValue);
            }
        }

        /// <summary>
        ///     Raises the ZoneGroupStateChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnZoneGroupStateChanged(StateVariableChangedEventArgs e)
        {
            if (ZoneGroupStateChangedInternal != null)
            {
                ZoneGroupStateChangedInternal(this, e.StateVarValue);
            }
        }

        /// <summary>
        ///     Raises the ThirdPartyMediaServersXChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnThirdPartyMediaServersXChanged(StateVariableChangedEventArgs e)
        {
            if (ThirdPartyMediaServersXChanged != null)
            {
                XElement element = e.StateVarValue.Descendants(XName.Get(e.StateVarName)).FirstOrDefault();

                ThirdPartyMediaServersXChanged(this, element);
            }
        }

        /// <summary>
        ///     Raises the AlarmRunSequenceChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnAlarmRunSequenceChanged(StateVariableChangedEventArgs e)
        {
            if (AlarmRunSequenceChanged != null)
            {
                XElement element = e.StateVarValue.Descendants(XName.Get(e.StateVarName)).FirstOrDefault();
                AlarmRunSequenceChanged(this, element);
            }
        }

        protected virtual void OnZoneGroupNameChanged(StateVariableChangedEventArgs e)
        {
            if (ZoneGroupNameChanged != null)
            {
                ZoneGroupNameChanged(this, e.StateVarValue);
            }
        }

        protected virtual void OnZoneGroupIdChanged(StateVariableChangedEventArgs e)
        {
            if (ZoneGroupIdChanged != null)
            {
                ZoneGroupIdChanged(this, e.StateVarValue);
            }
        }

        protected virtual void OnZonePlayerUuiDsInGroupChanged(StateVariableChangedEventArgs e)
        {
            if (ZonePlayerUuiDsInGroupChanged != null)
            {
                ZonePlayerUuiDsInGroupChanged(this, e.StateVarValue);
            }
        }

        /// <summary>
        ///     Raises the StateVariableChanged event.
        /// </summary>
        protected override void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
            // Determine state variable that is changing
            switch (e.StateVarName)
            {
                default:
                    Debug.WriteLine(e.StateVarValue.ToString());
                    break;

                case CsStateVarAvailableSoftwareUpdate:
                    // Raise the event for the AvailableSoftwareUpdate state variable
                    OnAvailableSoftwareUpdateChanged(e);
                    break;

                case CsStateVarZoneGroupState:
                    // Raise the event for the ZoneGroupState state variable
                    OnZoneGroupStateChanged(e);
                    break;

                case CsStateVarThirdPartyMediaServersX:
                    // Raise the event for the ThirdPartyMediaServersX state variable
                    OnThirdPartyMediaServersXChanged(e);
                    break;

                case CsStateVarAlarmRunSequence:
                    // Raise the event for the AlarmRunSequence state variable
                    OnAlarmRunSequenceChanged(e);
                    break;
                case CsActionZoneGroupName:
                    OnZoneGroupNameChanged(e);
                    break;
                case CsActionZoneGroupId:
                    OnZoneGroupIdChanged(e);
                    break;
                case CsActionZonePlayerUuiDsInGroup:
                    OnZonePlayerUuiDsInGroupChanged(e);
                    break;
            }

            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the CheckForUpdate action.
        /// </summary>
        /// <param name="updateType">In value for the UpdateType action parameter.</param>
        /// <param name="cachedOnly">In value for the CachedOnly action parameter.</param>
        /// <param name="version">In value for the Version action parameter.</param>
        /// <returns>Out value for the UpdateItem action parameter.</returns>
        public async Task<ActionResult> CheckForUpdate(AargtypeUpdateTypeEnum updateType, Boolean cachedOnly,
            String version)
        {
            var loIn = new object[3];

            loIn[0] = ZoneGroupTopologyExtensions.ToStringAargtypeUpdateType(updateType);
            loIn[1] = cachedOnly;
            loIn[2] = version;
            var action = new SoapAction
            {
                ArgNames = new[] {"UpdateType", "CachedOnly", "Version"},
                Name = CsActionCheckForUpdate,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the BeginSoftwareUpdate action.
        /// </summary>
        /// <param name="updateUrl">In value for the UpdateURL action parameter.</param>
        /// <param name="flags">In value for the Flags action parameter.</param>
        public async Task BeginSoftwareUpdate(String updateUrl, UInt32 flags)
        {
            var loIn = new object[2];

            loIn[0] = updateUrl;
            loIn[1] = flags;
            var action = new SoapAction
            {
                ArgNames = new[] {"UpdateURL", "Flags"},
                Name = CsActionBeginSoftwareUpdate,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ReportUnresponsiveDevice action.
        /// </summary>
        /// <param name="deviceUuid">In value for the DeviceUUID action parameter.</param>
        /// <param name="desiredAction">In value for the DesiredAction action parameter.</param>
        public async Task ReportUnresponsiveDevice(String deviceUuid,
            AargtypeUnresponsiveDeviceActionTypeEnum desiredAction)
        {
            var loIn = new object[2];

            loIn[0] = deviceUuid;
            loIn[1] = ZoneGroupTopologyExtensions.ToStringAargtypeUnresponsiveDeviceActionType(desiredAction);
            var action = new SoapAction
            {
                ArgNames = new[] {"DeviceUUID", "DesiredAction"},
                Name = CsActionReportUnresponsiveDevice,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ReportAlarmStartedRunning action.
        /// </summary>
        public async Task ReportAlarmStartedRunning()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionReportAlarmStartedRunning,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SubmitDiagnostics action.
        /// </summary>
        /// <returns>Out value for the DiagnosticID action parameter.</returns>
        public async Task<ActionResult> SubmitDiagnostics()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionSubmitDiagnostics,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the RegisterMobileDevice action.
        /// </summary>
        /// <param name="mobileDeviceName">In value for the MobileDeviceName action parameter.</param>
        /// <param name="mobileDeviceUdn">In value for the MobileDeviceUDN action parameter.</param>
        /// <param name="mobileIpAndPort">In value for the MobileIPAndPort action parameter.</param>
        public async Task RegisterMobileDevice(String mobileDeviceName, String mobileDeviceUdn, String mobileIpAndPort)
        {
            var loIn = new object[3];

            loIn[0] = mobileDeviceName;
            loIn[1] = mobileDeviceUdn;
            loIn[2] = mobileIpAndPort;
            var action = new SoapAction
            {
                ArgNames = new[] {"MobileDeviceName", "MobileDeviceUDN", "MobileIPAndPort"},
                Name = CsActionRegisterMobileDevice,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}