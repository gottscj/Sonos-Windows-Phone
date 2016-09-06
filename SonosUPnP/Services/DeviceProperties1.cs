using System;
using System.Threading.Tasks;
using Sonos.Extensions;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the DeviceProperties1 (urn:schemas-upnp-org:service:DeviceProperties:1)
    ///     service.
    /// </summary>
    public class DeviceProperties1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the SettingsReplicationState state variable.
        /// </summary>
        protected const string CsStateVarSettingsReplicationState = "SettingsReplicationState";

        /// <summary>
        ///     The name for the ZoneName state variable.
        /// </summary>
        protected const string CsStateVarZoneName = "ZoneName";

        /// <summary>
        ///     The name for the Icon state variable.
        /// </summary>
        protected const string CsStateVarIcon = "Icon";

        /// <summary>
        ///     The name for the Invisible state variable.
        /// </summary>
        protected const string CsStateVarInvisible = "Invisible";

        /// <summary>
        ///     The name for the IsZoneBridge state variable.
        /// </summary>
        protected const string CsStateVarIsZoneBridge = "IsZoneBridge";

        /// <summary>
        ///     The name for the ChannelMapSet state variable.
        /// </summary>
        protected const string CsStateVarChannelMapSet = "ChannelMapSet";


        /// <summary>
        ///     The name for the SetLEDState action.
        /// </summary>
        protected const string CsActionSetLedState = "SetLEDState";

        /// <summary>
        ///     The name for the GetLEDState action.
        /// </summary>
        protected const string CsActionGetLedState = "GetLEDState";

        /// <summary>
        ///     The name for the SetInvisible action.
        /// </summary>
        protected const string CsActionSetInvisible = "SetInvisible";

        /// <summary>
        ///     The name for the GetInvisible action.
        /// </summary>
        protected const string CsActionGetInvisible = "GetInvisible";

        /// <summary>
        ///     The name for the AddBondedZones action.
        /// </summary>
        protected const string CsActionAddBondedZones = "AddBondedZones";

        /// <summary>
        ///     The name for the RemoveBondedZones action.
        /// </summary>
        protected const string CsActionRemoveBondedZones = "RemoveBondedZones";

        /// <summary>
        ///     The name for the CreateStereoPair action.
        /// </summary>
        protected const string CsActionCreateStereoPair = "CreateStereoPair";

        /// <summary>
        ///     The name for the SeparateStereoPair action.
        /// </summary>
        protected const string CsActionSeparateStereoPair = "SeparateStereoPair";

        /// <summary>
        ///     The name for the SetZoneAttributes action.
        /// </summary>
        protected const string CsActionSetZoneAttributes = "SetZoneAttributes";

        /// <summary>
        ///     The name for the GetZoneAttributes action.
        /// </summary>
        protected const string CsActionGetZoneAttributes = "GetZoneAttributes";

        /// <summary>
        ///     The name for the GetHouseholdID action.
        /// </summary>
        protected const string CsActionGetHouseholdId = "GetHouseholdID";

        /// <summary>
        ///     The name for the GetZoneInfo action.
        /// </summary>
        protected const string CsActionGetZoneInfo = "GetZoneInfo";

        /// <summary>
        ///     The name for the SetAutoplayLinkedZones action.
        /// </summary>
        protected const string CsActionSetAutoplayLinkedZones = "SetAutoplayLinkedZones";

        /// <summary>
        ///     The name for the GetAutoplayLinkedZones action.
        /// </summary>
        protected const string CsActionGetAutoplayLinkedZones = "GetAutoplayLinkedZones";

        /// <summary>
        ///     The name for the SetAutoplayRoomUUID action.
        /// </summary>
        protected const string CsActionSetAutoplayRoomUuid = "SetAutoplayRoomUUID";

        /// <summary>
        ///     The name for the GetAutoplayRoomUUID action.
        /// </summary>
        protected const string CsActionGetAutoplayRoomUuid = "GetAutoplayRoomUUID";

        /// <summary>
        ///     The name for the SetAutoplayVolume action.
        /// </summary>
        protected const string CsActionSetAutoplayVolume = "SetAutoplayVolume";

        /// <summary>
        ///     The name for the GetAutoplayVolume action.
        /// </summary>
        protected const string CsActionGetAutoplayVolume = "GetAutoplayVolume";

        /// <summary>
        ///     The name for the ImportSetting action.
        /// </summary>
        protected const string CsActionImportSetting = "ImportSetting";

        /// <summary>
        ///     The name for the SetUseAutoplayVolume action.
        /// </summary>
        protected const string CsActionSetUseAutoplayVolume = "SetUseAutoplayVolume";

        /// <summary>
        ///     The name for the GetUseAutoplayVolume action.
        /// </summary>
        protected const string CsActionGetUseAutoplayVolume = "GetUseAutoplayVolume";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the DeviceProperties1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the DeviceProperties1 service from.</param>
        public DeviceProperties1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Public Static Methods

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the SettingsReplicationState state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> SettingsReplicationStateChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ZoneName state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> ZoneNameChanged;

        /// <summary>
        ///     Occurs when the service notifies that the Icon state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> IconChanged;

        /// <summary>
        ///     Occurs when the service notifies that the Invisible state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<Boolean> InvisibleChanged;

        /// <summary>
        ///     Occurs when the service notifies that the IsZoneBridge state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<Boolean> IsZoneBridgeChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ChannelMapSet state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> ChannelMapSetChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the SettingsReplicationStateChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnSettingsReplicationStateChanged(StateVariableChangedEventArgs e)
        {
            if (SettingsReplicationStateChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ZoneNameChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnZoneNameChanged(StateVariableChangedEventArgs e)
        {
            if (ZoneNameChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the IconChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnIconChanged(StateVariableChangedEventArgs e)
        {
            if (IconChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the InvisibleChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnInvisibleChanged(StateVariableChangedEventArgs e)
        {
            if (InvisibleChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the IsZoneBridgeChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnIsZoneBridgeChanged(StateVariableChangedEventArgs e)
        {
            if (IsZoneBridgeChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ChannelMapSetChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnChannelMapSetChanged(StateVariableChangedEventArgs e)
        {
            if (ChannelMapSetChanged != null)
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
                case CsStateVarSettingsReplicationState:
                    // Raise the event for the SettingsReplicationState state variable
                    OnSettingsReplicationStateChanged(e);
                    break;

                case CsStateVarZoneName:
                    // Raise the event for the ZoneName state variable
                    OnZoneNameChanged(e);
                    break;

                case CsStateVarIcon:
                    // Raise the event for the Icon state variable
                    OnIconChanged(e);
                    break;

                case CsStateVarInvisible:
                    // Raise the event for the Invisible state variable
                    OnInvisibleChanged(e);
                    break;

                case CsStateVarIsZoneBridge:
                    // Raise the event for the IsZoneBridge state variable
                    OnIsZoneBridgeChanged(e);
                    break;

                case CsStateVarChannelMapSet:
                    // Raise the event for the ChannelMapSet state variable
                    OnChannelMapSetChanged(e);
                    break;
            }


            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the SetLEDState action.
        /// </summary>
        /// <param name="desiredLedState">In value for the DesiredLEDState action parameter.</param>
        public async Task SetLedState(LedStateEnum desiredLedState)
        {
            var loIn = new object[1];

            loIn[0] = DeviceProperties.ToStringLedState(desiredLedState);
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredLEDState"},
                Name = CsActionSetLedState,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetLEDState action.
        /// </summary>
        /// <returns>Out value for the CurrentLEDState action parameter.</returns>
        public async Task<ActionResult> GetLedState()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetLedState,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetInvisible action.
        /// </summary>
        /// <param name="desiredInvisible">In value for the DesiredInvisible action parameter.</param>
        public async Task SetInvisible(Boolean desiredInvisible)
        {
            var loIn = new object[1];

            loIn[0] = desiredInvisible;
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredInvisible"},
                Name = CsActionSetInvisible,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetInvisible action.
        /// </summary>
        /// <returns>Out value for the CurrentInvisible action parameter.</returns>
        public async Task<ActionResult> GetInvisible()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetInvisible,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the AddBondedZones action.
        /// </summary>
        /// <param name="channelMapSet">In value for the ChannelMapSet action parameter.</param>
        public async Task AddBondedZones(String channelMapSet)
        {
            var loIn = new object[1];

            loIn[0] = channelMapSet;
            var action = new SoapAction
            {
                ArgNames = new[] {"ChannelMapSet"},
                Name = CsActionAddBondedZones,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RemoveBondedZones action.
        /// </summary>
        /// <param name="channelMapSet">In value for the ChannelMapSet action parameter.</param>
        public async Task RemoveBondedZones(String channelMapSet)
        {
            var loIn = new object[1];

            loIn[0] = channelMapSet;
            var action = new SoapAction
            {
                ArgNames = new[] {"ChannelMapSet"},
                Name = CsActionRemoveBondedZones,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the CreateStereoPair action.
        /// </summary>
        /// <param name="channelMapSet">In value for the ChannelMapSet action parameter.</param>
        public async Task CreateStereoPair(String channelMapSet)
        {
            var loIn = new object[1];

            loIn[0] = channelMapSet;
            var action = new SoapAction
            {
                ArgNames = new[] {"ChannelMapSet"},
                Name = CsActionCreateStereoPair,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SeparateStereoPair action.
        /// </summary>
        /// <param name="channelMapSet">In value for the ChannelMapSet action parameter.</param>
        public async Task SeparateStereoPair(String channelMapSet)
        {
            var loIn = new object[1];

            loIn[0] = channelMapSet;
            var action = new SoapAction
            {
                ArgNames = new[] {"ChannelMapSet"},
                Name = CsActionSeparateStereoPair,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetZoneAttributes action.
        /// </summary>
        /// <param name="desiredZoneName">In value for the DesiredZoneName action parameter.</param>
        /// <param name="desiredIcon">In value for the DesiredIcon action parameter.</param>
        public async Task SetZoneAttributes(String desiredZoneName, String desiredIcon)
        {
            var loIn = new object[2];

            loIn[0] = desiredZoneName;
            loIn[1] = desiredIcon;
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredZoneName", "DesiredIcon"},
                Name = CsActionSetZoneAttributes,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetZoneAttributes action.
        /// </summary>
        public async Task<ActionResult> GetZoneAttributes()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetZoneAttributes,
                ExpectedReplyCount = 2
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetHouseholdID action.
        /// </summary>
        /// <returns>Out value for the CurrentHouseholdID action parameter.</returns>
        public async Task<ActionResult> GetHouseholdId()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetHouseholdId,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetZoneInfo action.
        /// </summary>
        public async Task<ActionResult> GetZoneInfo()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetZoneInfo,
                ExpectedReplyCount = 8
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetAutoplayLinkedZones action.
        /// </summary>
        /// <param name="includeLinkedZones">In value for the IncludeLinkedZones action parameter.</param>
        public async Task SetAutoplayLinkedZones(Boolean includeLinkedZones)
        {
            var loIn = new object[1];

            loIn[0] = includeLinkedZones;
            var action = new SoapAction
            {
                ArgNames = new[] {"IncludeLinkedZones"},
                Name = CsActionSetAutoplayLinkedZones,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetAutoplayLinkedZones action.
        /// </summary>
        /// <returns>Out value for the IncludeLinkedZones action parameter.</returns>
        public async Task<ActionResult> GetAutoplayLinkedZones()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetAutoplayLinkedZones,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetAutoplayRoomUUID action.
        /// </summary>
        /// <param name="roomUuid">In value for the RoomUUID action parameter.</param>
        public async Task SetAutoplayRoomUuid(String roomUuid)
        {
            var loIn = new object[1];

            loIn[0] = roomUuid;
            var action = new SoapAction
            {
                ArgNames = new[] {"RoomUUID"},
                Name = CsActionSetAutoplayRoomUuid,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetAutoplayRoomUUID action.
        /// </summary>
        /// <returns>Out value for the RoomUUID action parameter.</returns>
        public async Task<ActionResult> GetAutoplayRoomUuid()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetAutoplayRoomUuid,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetAutoplayVolume action.
        /// </summary>
        /// <param name="volume">In value for the Volume action parameter. With range of 0 to 100. Increment of 1.</param>
        public async Task SetAutoplayVolume(UInt16 volume)
        {
            var loIn = new object[1];

            loIn[0] = volume;
            var action = new SoapAction
            {
                ArgNames = new[] {"Volume"},
                Name = CsActionSetAutoplayVolume,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetAutoplayVolume action.
        /// </summary>
        /// <returns>Out value for the CurrentVolume action parameter. With range of 0 to 100. Increment of 1.</returns>
        public async Task<ActionResult> GetAutoplayVolume()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetAutoplayVolume,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the ImportSetting action.
        /// </summary>
        /// <param name="settingId">In value for the SettingID action parameter. With range of 0 to 65535.</param>
        /// <param name="settingUri">In value for the SettingURI action parameter.</param>
        public async Task ImportSetting(UInt32 settingId, String settingUri)
        {
            var loIn = new object[2];

            loIn[0] = settingId;
            loIn[1] = settingUri;
            var action = new SoapAction
            {
                ArgNames = new[] {"SettingID", "SettingURI"},
                Name = CsActionImportSetting,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetUseAutoplayVolume action.
        /// </summary>
        /// <param name="useVolume">In value for the UseVolume action parameter.</param>
        public async Task SetUseAutoplayVolume(Boolean useVolume)
        {
            var loIn = new object[1];

            loIn[0] = useVolume;
            var action = new SoapAction
            {
                ArgNames = new[] {"UseVolume"},
                Name = CsActionSetUseAutoplayVolume,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetUseAutoplayVolume action.
        /// </summary>
        /// <returns>Out value for the UseVolume action parameter.</returns>
        public async Task<ActionResult> GetUseAutoplayVolume()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetUseAutoplayVolume,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        #endregion
    }
}