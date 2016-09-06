using System;
using System.Threading.Tasks;
using Sonos.Extensions;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the AlarmClock1 (urn:schemas-upnp-org:service:AlarmClock:1) service.
    /// </summary>
    public class AlarmClock1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the TimeZone state variable.
        /// </summary>
        protected const string CsStateVarTimeZone = "TimeZone";

        /// <summary>
        ///     The name for the TimeServer state variable.
        /// </summary>
        protected const string CsStateVarTimeServer = "TimeServer";

        /// <summary>
        ///     The name for the TimeGeneration state variable.
        /// </summary>
        protected const string CsStateVarTimeGeneration = "TimeGeneration";

        /// <summary>
        ///     The name for the AlarmListVersion state variable.
        /// </summary>
        protected const string CsStateVarAlarmListVersion = "AlarmListVersion";

        /// <summary>
        ///     The name for the DailyIndexRefreshTime state variable.
        /// </summary>
        protected const string CsStateVarDailyIndexRefreshTime = "DailyIndexRefreshTime";

        /// <summary>
        ///     The name for the TimeFormat state variable.
        /// </summary>
        protected const string CsStateVarTimeFormat = "TimeFormat";

        /// <summary>
        ///     The name for the DateFormat state variable.
        /// </summary>
        protected const string CsStateVarDateFormat = "DateFormat";

        /// <summary>
        ///     The name for the SetFormat action.
        /// </summary>
        protected const string CsActionSetFormat = "SetFormat";

        /// <summary>
        ///     The name for the GetFormat action.
        /// </summary>
        protected const string CsActionGetFormat = "GetFormat";

        /// <summary>
        ///     The name for the SetTimeZone action.
        /// </summary>
        protected const string CsActionSetTimeZone = "SetTimeZone";

        /// <summary>
        ///     The name for the GetTimeZone action.
        /// </summary>
        protected const string CsActionGetTimeZone = "GetTimeZone";

        /// <summary>
        ///     The name for the GetTimeZoneAndRule action.
        /// </summary>
        protected const string CsActionGetTimeZoneAndRule = "GetTimeZoneAndRule";

        /// <summary>
        ///     The name for the GetTimeZoneRule action.
        /// </summary>
        protected const string CsActionGetTimeZoneRule = "GetTimeZoneRule";

        /// <summary>
        ///     The name for the SetTimeServer action.
        /// </summary>
        protected const string CsActionSetTimeServer = "SetTimeServer";

        /// <summary>
        ///     The name for the GetTimeServer action.
        /// </summary>
        protected const string CsActionGetTimeServer = "GetTimeServer";

        /// <summary>
        ///     The name for the SetTimeNow action.
        /// </summary>
        protected const string CsActionSetTimeNow = "SetTimeNow";

        /// <summary>
        ///     The name for the GetHouseholdTimeAtStamp action.
        /// </summary>
        protected const string CsActionGetHouseholdTimeAtStamp = "GetHouseholdTimeAtStamp";

        /// <summary>
        ///     The name for the GetTimeNow action.
        /// </summary>
        protected const string CsActionGetTimeNow = "GetTimeNow";

        /// <summary>
        ///     The name for the CreateAlarm action.
        /// </summary>
        protected const string CsActionCreateAlarm = "CreateAlarm";

        /// <summary>
        ///     The name for the UpdateAlarm action.
        /// </summary>
        protected const string CsActionUpdateAlarm = "UpdateAlarm";

        /// <summary>
        ///     The name for the DestroyAlarm action.
        /// </summary>
        protected const string CsActionDestroyAlarm = "DestroyAlarm";

        /// <summary>
        ///     The name for the ListAlarms action.
        /// </summary>
        protected const string CsActionListAlarms = "ListAlarms";

        /// <summary>
        ///     The name for the SetDailyIndexRefreshTime action.
        /// </summary>
        protected const string CsActionSetDailyIndexRefreshTime = "SetDailyIndexRefreshTime";

        /// <summary>
        ///     The name for the GetDailyIndexRefreshTime action.
        /// </summary>
        protected const string CsActionGetDailyIndexRefreshTime = "GetDailyIndexRefreshTime";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the AlarmClock1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the AlarmClock1 service from.</param>
        public AlarmClock1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the TimeZone state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> TimeZoneChanged;

        /// <summary>
        ///     Occurs when the service notifies that the TimeServer state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> TimeServerChanged;

        /// <summary>
        ///     Occurs when the service notifies that the TimeGeneration state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<UInt32> TimeGenerationChanged;

        /// <summary>
        ///     Occurs when the service notifies that the AlarmListVersion state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> AlarmListVersionChanged;

        /// <summary>
        ///     Occurs when the service notifies that the DailyIndexRefreshTime state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> DailyIndexRefreshTimeChanged;

        /// <summary>
        ///     Occurs when the service notifies that the TimeFormat state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> TimeFormatChanged;

        /// <summary>
        ///     Occurs when the service notifies that the DateFormat state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> DateFormatChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the TimeZoneChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnTimeZoneChanged(StateVariableChangedEventArgs e)
        {
            if (TimeZoneChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the TimeServerChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnTimeServerChanged(StateVariableChangedEventArgs e)
        {
            if (TimeServerChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the TimeGenerationChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnTimeGenerationChanged(StateVariableChangedEventArgs e)
        {
            if (TimeGenerationChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the AlarmListVersionChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnAlarmListVersionChanged(StateVariableChangedEventArgs e)
        {
            if (AlarmListVersionChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the DailyIndexRefreshTimeChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDailyIndexRefreshTimeChanged(StateVariableChangedEventArgs e)
        {
            if (DailyIndexRefreshTimeChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the TimeFormatChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnTimeFormatChanged(StateVariableChangedEventArgs e)
        {
            if (TimeFormatChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the DateFormatChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDateFormatChanged(StateVariableChangedEventArgs e)
        {
            if (DateFormatChanged != null)
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
                case CsStateVarTimeZone:
                    // Raise the event for the TimeZone state variable
                    OnTimeZoneChanged(e);
                    break;

                case CsStateVarTimeServer:
                    // Raise the event for the TimeServer state variable
                    OnTimeServerChanged(e);
                    break;

                case CsStateVarTimeGeneration:
                    // Raise the event for the TimeGeneration state variable
                    OnTimeGenerationChanged(e);
                    break;

                case CsStateVarAlarmListVersion:
                    // Raise the event for the AlarmListVersion state variable
                    OnAlarmListVersionChanged(e);
                    break;

                case CsStateVarDailyIndexRefreshTime:
                    // Raise the event for the DailyIndexRefreshTime state variable
                    OnDailyIndexRefreshTimeChanged(e);
                    break;

                case CsStateVarTimeFormat:
                    // Raise the event for the TimeFormat state variable
                    OnTimeFormatChanged(e);
                    break;

                case CsStateVarDateFormat:
                    // Raise the event for the DateFormat state variable
                    OnDateFormatChanged(e);
                    break;
            }

            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the SetFormat action.
        /// </summary>
        /// <param name="desiredTimeFormat">In value for the DesiredTimeFormat action parameter.</param>
        /// <param name="desiredDateFormat">In value for the DesiredDateFormat action parameter.</param>
        public async Task SetFormat(String desiredTimeFormat, String desiredDateFormat)
        {
            var loIn = new object[2];

            loIn[0] = desiredTimeFormat;
            loIn[1] = desiredDateFormat;
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredTimeFormat", "DesiredDateFormat"},
                Name = CsActionSetFormat,
                ExpectedReplyCount = 0
            };
            
            
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetFormat action.
        /// </summary>
        public async Task<ActionResult> GetFormat()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetFormat,
                ExpectedReplyCount = 2
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetTimeZone action.
        /// </summary>
        /// <param name="index">In value for the Index action parameter.</param>
        /// <param name="autoAdjustDst">In value for the AutoAdjustDst action parameter.</param>
        public async Task SetTimeZone(Int32 index, Boolean autoAdjustDst)
        {
            var loIn = new object[2];

            loIn[0] = index;
            loIn[1] = autoAdjustDst;
            var action = new SoapAction
            {
                ArgNames = new[] {"Index", "AutoAdjustDst"},
                Name = CsActionSetTimeZone,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetTimeZone action.
        /// </summary>
        public async Task<ActionResult> GetTimeZone()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetTimeZone,
                ExpectedReplyCount = 2
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetTimeZoneAndRule action.
        /// </summary>
        /// <param>Out value for the CurrentTimeZone action parameter.
        ///     <name>currentTimeZone</name>
        /// </param>
        public async Task<ActionResult> GetTimeZoneAndRule()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetTimeZoneAndRule,
                ExpectedReplyCount = 3
            };

            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetTimeZoneRule action.
        /// </summary>
        /// <param name="index">In value for the Index action parameter.</param>
        /// <returns>Out value for the TimeZone action parameter.</returns>
        public async Task<ActionResult> GetTimeZoneRule(Int32 index)
        {
            var loIn = new object[1];

            loIn[0] = index;
            var action = new SoapAction
            {
                ArgNames = new[] {"Index"},
                Name = CsActionGetTimeZoneRule,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetTimeServer action.
        /// </summary>
        /// <param name="desiredTimeServer">In value for the DesiredTimeServer action parameter.</param>
        public async Task SetTimeServer(String desiredTimeServer)
        {
            var loIn = new object[1];

            loIn[0] = desiredTimeServer;
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredTimeServer"},
                Name = CsActionSetTimeServer,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetTimeServer action.
        /// </summary>
        /// <returns>Out value for the CurrentTimeServer action parameter.</returns>
        public async Task<ActionResult> GetTimeServer()
        {
            var loIn = new object[0];


            var action = new SoapAction
            {
                ArgNames = new string[] {},
                Name = CsActionGetTimeServer,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetTimeNow action.
        /// </summary>
        /// <param name="desiredTime">In value for the DesiredTime action parameter.</param>
        /// <param name="timeZoneForDesiredTime">In value for the TimeZoneForDesiredTime action parameter.</param>
        public async Task SetTimeNow(String desiredTime, String timeZoneForDesiredTime)
        {
            var loIn = new object[2];

            loIn[0] = desiredTime;
            loIn[1] = timeZoneForDesiredTime;
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredTime", "TimeZoneForDesiredTime"},
                Name = CsActionSetTimeNow,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetHouseholdTimeAtStamp action.
        /// </summary>
        /// <param name="timeStamp">In value for the TimeStamp action parameter.</param>
        /// <returns>Out value for the HouseholdUTCTime action parameter.</returns>
        public async Task<ActionResult> GetHouseholdTimeAtStamp(String timeStamp)
        {
            var loIn = new object[1];

            loIn[0] = timeStamp;
            var action = new SoapAction
            {
                ArgNames = new[] {"TimeStamp"},
                Name = CsActionGetHouseholdTimeAtStamp,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetTimeNow action.
        /// </summary>
        public async Task<ActionResult> GetTimeNow()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetTimeNow,
                ExpectedReplyCount = 4
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the CreateAlarm action.
        /// </summary>
        /// <param name="startLocalTime">In value for the StartLocalTime action parameter.</param>
        /// <param name="duration">In value for the Duration action parameter.</param>
        /// <param name="recurrence">In value for the Recurrence action parameter.</param>
        /// <param name="enabled">In value for the Enabled action parameter.</param>
        /// <param name="roomUuid">In value for the RoomUUID action parameter.</param>
        /// <param name="programUri">In value for the ProgramURI action parameter.</param>
        /// <param name="programMetaData">In value for the ProgramMetaData action parameter.</param>
        /// <param name="playMode">In value for the PlayMode action parameter. Default value of NORMAL.</param>
        /// <param name="volume">In value for the Volume action parameter.</param>
        /// <param name="includeLinkedZones">In value for the IncludeLinkedZones action parameter.</param>
        /// <returns>Out value for the AssignedID action parameter.</returns>
        public async Task<ActionResult> CreateAlarm(String startLocalTime, String duration,
            AargtypeRecurrenceEnum recurrence, Boolean enabled, String roomUuid, String programUri,
            String programMetaData, AargtypeAlarmPlayModeEnum playMode, UInt16 volume, Boolean includeLinkedZones)
        {
            var loIn = new object[10];

            loIn[0] = startLocalTime;
            loIn[1] = duration;
            loIn[2] = AlarmClock.ToStringAargtypeRecurrence(recurrence);
            loIn[3] = enabled;
            loIn[4] = roomUuid;
            loIn[5] = programUri;
            loIn[6] = programMetaData;
            loIn[7] = AlarmClock.ToStringAargtypeAlarmPlayMode(playMode);
            loIn[8] = volume;
            loIn[9] = includeLinkedZones;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "StartLocalTime", "Duration", "Recurrence", "Enabled", "RoomUUID", "ProgramURI", "ProgramMetaData",
                        "PlayMode", "Volume", "IncludeLinkedZones", "AssignedID"
                    },
                Name = CsActionCreateAlarm,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the UpdateAlarm action.
        /// </summary>
        /// <param name="iD">In value for the ID action parameter.</param>
        /// <param name="startLocalTime">In value for the StartLocalTime action parameter.</param>
        /// <param name="duration">In value for the Duration action parameter.</param>
        /// <param name="recurrence">In value for the Recurrence action parameter.</param>
        /// <param name="enabled">In value for the Enabled action parameter.</param>
        /// <param name="roomUuid">In value for the RoomUUID action parameter.</param>
        /// <param name="programUri">In value for the ProgramURI action parameter.</param>
        /// <param name="programMetaData">In value for the ProgramMetaData action parameter.</param>
        /// <param name="playMode">In value for the PlayMode action parameter. Default value of NORMAL.</param>
        /// <param name="volume">In value for the Volume action parameter.</param>
        /// <param name="includeLinkedZones">In value for the IncludeLinkedZones action parameter.</param>
        public async Task UpdateAlarm(UInt32 iD, String startLocalTime, String duration,
            AargtypeRecurrenceEnum recurrence, Boolean enabled, String roomUuid, String programUri,
            String programMetaData, AargtypeAlarmPlayModeEnum playMode, UInt16 volume, Boolean includeLinkedZones)
        {
            var loIn = new object[11];

            loIn[0] = iD;
            loIn[1] = startLocalTime;
            loIn[2] = duration;
            loIn[3] = AlarmClock.ToStringAargtypeRecurrence(recurrence);
            loIn[4] = enabled;
            loIn[5] = roomUuid;
            loIn[6] = programUri;
            loIn[7] = programMetaData;
            loIn[8] = AlarmClock.ToStringAargtypeAlarmPlayMode(playMode);
            loIn[9] = volume;
            loIn[10] = includeLinkedZones;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "ID", "StartLocalTime", "Duration", "Recurrence", "Enabled", "RoomUUID", "ProgramURI",
                        "ProgramMetaData", "PlayMode", "Volume", "IncludeLinkedZones"
                    },
                Name = CsActionUpdateAlarm,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the DestroyAlarm action.
        /// </summary>
        /// <param name="iD">In value for the ID action parameter.</param>
        public async Task DestroyAlarm(UInt32 iD)
        {
            var loIn = new object[1];

            loIn[0] = iD;
            var action = new SoapAction
            {
                ArgNames = new[] {"ID"},
                Name = CsActionDestroyAlarm,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ListAlarms action.
        /// </summary>
        public async Task<ActionResult> ListAlarms()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionListAlarms,
                ExpectedReplyCount = 2
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetDailyIndexRefreshTime action.
        /// </summary>
        /// <param name="desiredDailyIndexRefreshTime">In value for the DesiredDailyIndexRefreshTime action parameter.</param>
        public async Task SetDailyIndexRefreshTime(String desiredDailyIndexRefreshTime)
        {
            var loIn = new object[1];

            loIn[0] = desiredDailyIndexRefreshTime;
            var action = new SoapAction
            {
                ArgNames = new[] {"DesiredDailyIndexRefreshTime"},
                Name = CsActionSetDailyIndexRefreshTime,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetDailyIndexRefreshTime action.
        /// </summary>
        /// <returns>Out value for the CurrentDailyIndexRefreshTime action parameter.</returns>
        public async Task<ActionResult> GetDailyIndexRefreshTime()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetDailyIndexRefreshTime,
                ExpectedReplyCount = 1
            };

            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        #endregion
    }
}