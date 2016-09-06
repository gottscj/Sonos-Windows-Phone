using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sonos.Extensions;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the RenderingControl1 (urn:schemas-upnp-org:service:RenderingControl:1)
    ///     service.
    /// </summary>
    public class RenderingControl1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the LastChange state variable.
        /// </summary>
        protected const string CsStateVarLastChange = "LastChange";

        /// <summary>
        ///     The name for the GetMute action.
        /// </summary>
        protected const string CsActionGetMute = "GetMute";

        /// <summary>
        ///     The name for the SetMute action.
        /// </summary>
        protected const string CsActionSetMute = "SetMute";

        /// <summary>
        ///     The name for the ResetBasicEQ action.
        /// </summary>
        protected const string CsActionResetBasicEq = "ResetBasicEQ";

        /// <summary>
        ///     The name for the ResetExtEQ action.
        /// </summary>
        protected const string CsActionResetExtEq = "ResetExtEQ";

        /// <summary>
        ///     The name for the GetVolume action.
        /// </summary>
        protected const string CsActionGetVolume = "GetVolume";

        /// <summary>
        ///     The name for the SetVolume action.
        /// </summary>
        protected const string CsActionSetVolume = "SetVolume";

        /// <summary>
        ///     The name for the SetRelativeVolume action.
        /// </summary>
        protected const string CsActionSetRelativeVolume = "SetRelativeVolume";

        /// <summary>
        ///     The name for the GetVolumeDB action.
        /// </summary>
        protected const string CsActionGetVolumeDb = "GetVolumeDB";

        /// <summary>
        ///     The name for the SetVolumeDB action.
        /// </summary>
        protected const string CsActionSetVolumeDb = "SetVolumeDB";

        /// <summary>
        ///     The name for the GetVolumeDBRange action.
        /// </summary>
        protected const string CsActionGetVolumeDbRange = "GetVolumeDBRange";

        /// <summary>
        ///     The name for the GetBass action.
        /// </summary>
        protected const string CsActionGetBass = "GetBass";

        /// <summary>
        ///     The name for the SetBass action.
        /// </summary>
        protected const string CsActionSetBass = "SetBass";

        /// <summary>
        ///     The name for the GetTreble action.
        /// </summary>
        protected const string CsActionGetTreble = "GetTreble";

        /// <summary>
        ///     The name for the SetTreble action.
        /// </summary>
        protected const string CsActionSetTreble = "SetTreble";

        /// <summary>
        ///     The name for the GetEQ action.
        /// </summary>
        protected const string CsActionGetEq = "GetEQ";

        /// <summary>
        ///     The name for the SetEQ action.
        /// </summary>
        protected const string CsActionSetEq = "SetEQ";

        /// <summary>
        ///     The name for the GetLoudness action.
        /// </summary>
        protected const string CsActionGetLoudness = "GetLoudness";

        /// <summary>
        ///     The name for the SetLoudness action.
        /// </summary>
        protected const string CsActionSetLoudness = "SetLoudness";

        /// <summary>
        ///     The name for the GetSupportsOutputFixed action.
        /// </summary>
        protected const string CsActionGetSupportsOutputFixed = "GetSupportsOutputFixed";

        /// <summary>
        ///     The name for the GetOutputFixed action.
        /// </summary>
        protected const string CsActionGetOutputFixed = "GetOutputFixed";

        /// <summary>
        ///     The name for the SetOutputFixed action.
        /// </summary>
        protected const string CsActionSetOutputFixed = "SetOutputFixed";

        /// <summary>
        ///     The name for the GetHeadphoneConnected action.
        /// </summary>
        protected const string CsActionGetHeadphoneConnected = "GetHeadphoneConnected";

        /// <summary>
        ///     The name for the RampToVolume action.
        /// </summary>
        protected const string CsActionRampToVolume = "RampToVolume";

        /// <summary>
        ///     The name for the RestoreVolumePriorToRamp action.
        /// </summary>
        protected const string CsActionRestoreVolumePriorToRamp = "RestoreVolumePriorToRamp";

        /// <summary>
        ///     The name for the SetChannelMap action.
        /// </summary>
        protected const string CsActionSetChannelMap = "SetChannelMap";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the RenderingControl1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the RenderingControl1 service from.</param>
        public RenderingControl1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the LastChange state variable has changed its value.
        /// </summary>
        private event StateVariableChangedEventHandler<XElement> LastChangeChangedInternal;

        public event StateVariableChangedEventHandler<XElement> LastChangeChanged
        {
            add
            {
                EventSemaphore++;
                LastChangeChangedInternal += value;
            }
            remove
            {
                EventSemaphore--;
                LastChangeChangedInternal -= value;
            }
        }

        #endregion

        #region Event Callers

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

        /// <summary>
        ///     Raises the LastChangeChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnLastChangeChanged(StateVariableChangedEventArgs e)
        {
            if (LastChangeChangedInternal != null)
                LastChangeChangedInternal(this, e.StateVarValue);
        }

        /// <summary>
        ///     Raises the StateVariableChanged event.
        /// </summary>
        protected override void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
            // Determine state variable that is changing
            switch (e.StateVarName)
            {
                case CsStateVarLastChange:
                    // Raise the event for the LastChange state variable
                    OnLastChangeChanged(e);
                    break;
            }


            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the GetMute action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <returns>Out value for the CurrentMute action parameter.</returns>
        public async Task<ActionResult> GetMute(UInt32 instanceId, AargtypeMuteChannelEnum channel)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeMuteChannel(channel);

            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel"},
                Name = CsActionGetMute,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetMute action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <param name="desiredMute">In value for the DesiredMute action parameter.</param>
        public async Task SetMute(UInt32 instanceId, AargtypeMuteChannelEnum channel, Boolean desiredMute)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeMuteChannel(channel);
            loIn[2] = desiredMute;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel", "DesiredMute"},
                Name = CsActionSetMute,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ResetBasicEQ action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> ResetBasicEq(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionResetBasicEq,
                ExpectedReplyCount = 5
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the ResetExtEQ action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="eQType">In value for the EQType action parameter.</param>
        public async Task ResetExtEq(UInt32 instanceId, String eQType)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = eQType;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "EQType"},
                Name = CsActionResetExtEq,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetVolume action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <returns>Out value for the CurrentVolume action parameter. With range of 0 to 100. Increment of 1.</returns>
        public async Task<ActionResult> GetVolume(UInt32 instanceId, AargtypeChannelEnum channel)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel"},
                Name = CsActionGetVolume,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetVolume action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <param name="desiredVolume">In value for the DesiredVolume action parameter. With range of 0 to 100. Increment of 1.</param>
        public async Task SetVolume(UInt32 instanceId, AargtypeChannelEnum channel, UInt16 desiredVolume)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            loIn[2] = desiredVolume;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel", "DesiredVolume"},
                Name = CsActionSetVolume,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetRelativeVolume action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <param name="adjustment">In value for the Adjustment action parameter.</param>
        /// <returns>Out value for the NewVolume action parameter. With range of 0 to 100. Increment of 1.</returns>
        public async Task<ActionResult> SetRelativeVolume(UInt32 instanceId, AargtypeChannelEnum channel,
            Int32 adjustment)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            loIn[2] = adjustment;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel", "Adjustment"},
                Name = CsActionSetRelativeVolume,
                ExpectedReplyCount = 1
            };

            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetVolumeDB action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <returns>Out value for the CurrentVolume action parameter.</returns>
        public async Task<ActionResult> GetVolumeDb(UInt32 instanceId, AargtypeChannelEnum channel)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel"},
                Name = CsActionGetVolumeDb,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetVolumeDB action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <param name="desiredVolume">In value for the DesiredVolume action parameter.</param>
        public async Task SetVolumeDb(UInt32 instanceId, AargtypeChannelEnum channel, Int16 desiredVolume)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            loIn[2] = desiredVolume;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel", "DesiredVolume"},
                Name = CsActionSetVolumeDb,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetVolumeDBRange action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        public async Task<ActionResult> GetVolumeDbRange(UInt32 instanceId, AargtypeChannelEnum channel)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel"},
                Name = CsActionGetVolumeDbRange,
                ExpectedReplyCount = 2
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetBass action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the CurrentBass action parameter. With range of -10 to 10. Increment of 1.</returns>
        public async Task<ActionResult> GetBass(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetBass,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetBass action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="desiredBass">In value for the DesiredBass action parameter. With range of -10 to 10. Increment of 1.</param>
        public async Task SetBass(UInt32 instanceId, Int16 desiredBass)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = desiredBass;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "DesiredBass"},
                Name = CsActionSetBass,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetTreble action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the CurrentTreble action parameter. With range of -10 to 10. Increment of 1.</returns>
        public async Task<ActionResult> GetTreble(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetTreble,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetTreble action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="desiredTreble">In value for the DesiredTreble action parameter. With range of -10 to 10. Increment of 1.</param>
        public async Task SetTreble(UInt32 instanceId, Int16 desiredTreble)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = desiredTreble;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "DesiredTreble"},
                Name = CsActionSetTreble,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetEQ action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="eQType">In value for the EQType action parameter.</param>
        /// <returns>Out value for the CurrentValue action parameter.</returns>
        public async Task<ActionResult> GetEq(UInt32 instanceId, String eQType)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = eQType;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "EQType"},
                Name = CsActionGetEq,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetEQ action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="eQType">In value for the EQType action parameter.</param>
        /// <param name="desiredValue">In value for the DesiredValue action parameter.</param>
        public async Task SetEq(UInt32 instanceId, String eQType, Int16 desiredValue)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = eQType;
            loIn[2] = desiredValue;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "EQType", "DesiredValue"},
                Name = CsActionSetEq,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetLoudness action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <returns>Out value for the CurrentLoudness action parameter.</returns>
        public async Task<ActionResult> GetLoudness(UInt32 instanceId, AargtypeChannelEnum channel)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel"},
                Name = CsActionGetLoudness,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetLoudness action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <param name="desiredLoudness">In value for the DesiredLoudness action parameter.</param>
        public async Task SetLoudness(UInt32 instanceId, AargtypeChannelEnum channel, Boolean desiredLoudness)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            loIn[2] = desiredLoudness;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel", "DesiredLoudness"},
                Name = CsActionSetLoudness,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetSupportsOutputFixed action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the CurrentSupportsFixed action parameter.</returns>
        public async Task<ActionResult> GetSupportsOutputFixed(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetSupportsOutputFixed,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetOutputFixed action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the CurrentFixed action parameter.</returns>
        public async Task<ActionResult> GetOutputFixed(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetOutputFixed,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetOutputFixed action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="desiredFixed">In value for the DesiredFixed action parameter.</param>
        public async Task SetOutputFixed(UInt32 instanceId, Boolean desiredFixed)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = desiredFixed;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "DesiredFixed"},
                Name = CsActionSetOutputFixed,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetHeadphoneConnected action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the CurrentHeadphoneConnected action parameter.</returns>
        public async Task<ActionResult> GetHeadphoneConnected(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetHeadphoneConnected,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the RampToVolume action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        /// <param name="rampType">In value for the RampType action parameter.</param>
        /// <param name="desiredVolume">In value for the DesiredVolume action parameter. With range of 0 to 100. Increment of 1.</param>
        /// <param name="resetVolumeAfter">In value for the ResetVolumeAfter action parameter.</param>
        /// <param name="programUri">In value for the ProgramURI action parameter.</param>
        /// <returns>Out value for the RampTime action parameter.</returns>
        public async Task<ActionResult> RampToVolume(UInt32 instanceId, AargtypeChannelEnum channel,
            AargtypeRampTypeEnum rampType, UInt16 desiredVolume, Boolean resetVolumeAfter, String programUri)
        {
            var loIn = new object[6];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            loIn[2] = RenderingControlExtensions.ToStringAargtypeRampType(rampType);
            loIn[3] = desiredVolume;
            loIn[4] = resetVolumeAfter;
            loIn[5] = programUri;
            var action = new SoapAction
            {
                ArgNames =
                    new[] {"InstanceID", "Channel", "RampType", "DesiredVolume", "ResetVolumeAfter", "ProgramURI"},
                Name = CsActionRampToVolume,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the RestoreVolumePriorToRamp action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channel">In value for the Channel action parameter.</param>
        public async Task RestoreVolumePriorToRamp(UInt32 instanceId, AargtypeChannelEnum channel)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = RenderingControlExtensions.ToStringAargtypeChannel(channel);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Channel"},
                Name = CsActionRestoreVolumePriorToRamp,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetChannelMap action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="channelMap">In value for the ChannelMap action parameter.</param>
        public async Task SetChannelMap(UInt32 instanceId, String channelMap)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = channelMap;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "ChannelMap"},
                Name = CsActionSetChannelMap,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}