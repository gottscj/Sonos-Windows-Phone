using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class AlarmClock
    {
        #region Protected Methods

        /// <summary>
        ///     The string value for the allowed value ONCE of the A_ARG_TYPE_Recurrence state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRecurrenceOnce = "ONCE";

        /// <summary>
        ///     The string value for the allowed value WEEKDAYS of the A_ARG_TYPE_Recurrence state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRecurrenceWeekdays = "WEEKDAYS";

        /// <summary>
        ///     The string value for the allowed value WEEKENDS of the A_ARG_TYPE_Recurrence state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRecurrenceWeekends = "WEEKENDS";

        /// <summary>
        ///     The string value for the allowed value DAILY of the A_ARG_TYPE_Recurrence state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRecurrenceDaily = "DAILY";

        /// <summary>
        ///     The string value for the allowed value NORMAL of the A_ARG_TYPE_AlarmPlayMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeAlarmPlayModeNormal = "NORMAL";

        /// <summary>
        ///     The string value for the allowed value REPEAT_ALL of the A_ARG_TYPE_AlarmPlayMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeAlarmPlayModeRepeatall = "REPEAT_ALL";

        /// <summary>
        ///     The string value for the allowed value SHUFFLE_NOREPEAT of the A_ARG_TYPE_AlarmPlayMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeAlarmPlayModeShufflenorepeat = "SHUFFLE_NOREPEAT";

        /// <summary>
        ///     The string value for the allowed value SHUFFLE of the A_ARG_TYPE_AlarmPlayMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeAlarmPlayModeShuffle = "SHUFFLE";

        /// <summary>
        ///     Parses a string value from the AARGTYPERecurrence state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPERecurrenceEnum.Invalid if not parsable.</returns>
        public static AargtypeRecurrenceEnum ParseAargtypeRecurrence(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeRecurrenceOnce:
                    return AargtypeRecurrenceEnum.Once;
                case CsAllowedValAargtypeRecurrenceWeekdays:
                    return AargtypeRecurrenceEnum.Weekdays;
                case CsAllowedValAargtypeRecurrenceWeekends:
                    return AargtypeRecurrenceEnum.Weekends;
                case CsAllowedValAargtypeRecurrenceDaily:
                    return AargtypeRecurrenceEnum.Daily;
                default:
                    return AargtypeRecurrenceEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPERecurrence state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPERecurrenceEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeRecurrence(AargtypeRecurrenceEnum value)
        {
            switch (value)
            {
                case AargtypeRecurrenceEnum.Once:
                    return CsAllowedValAargtypeRecurrenceOnce;
                case AargtypeRecurrenceEnum.Weekdays:
                    return CsAllowedValAargtypeRecurrenceWeekdays;
                case AargtypeRecurrenceEnum.Weekends:
                    return CsAllowedValAargtypeRecurrenceWeekends;
                case AargtypeRecurrenceEnum.Daily:
                    return CsAllowedValAargtypeRecurrenceDaily;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the AARGTYPEAlarmPlayMode state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEAlarmPlayModeEnum.Invalid if not parsable.</returns>
        public static AargtypeAlarmPlayModeEnum ParseAargtypeAlarmPlayMode(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeAlarmPlayModeNormal:
                    return AargtypeAlarmPlayModeEnum.Normal;
                case CsAllowedValAargtypeAlarmPlayModeRepeatall:
                    return AargtypeAlarmPlayModeEnum.Repeatall;
                case CsAllowedValAargtypeAlarmPlayModeShufflenorepeat:
                    return AargtypeAlarmPlayModeEnum.Shufflenorepeat;
                case CsAllowedValAargtypeAlarmPlayModeShuffle:
                    return AargtypeAlarmPlayModeEnum.Shuffle;
                default:
                    return AargtypeAlarmPlayModeEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEAlarmPlayMode state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPEAlarmPlayModeEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeAlarmPlayMode(AargtypeAlarmPlayModeEnum value)
        {
            switch (value)
            {
                case AargtypeAlarmPlayModeEnum.Normal:
                    return CsAllowedValAargtypeAlarmPlayModeNormal;
                case AargtypeAlarmPlayModeEnum.Repeatall:
                    return CsAllowedValAargtypeAlarmPlayModeRepeatall;
                case AargtypeAlarmPlayModeEnum.Shufflenorepeat:
                    return CsAllowedValAargtypeAlarmPlayModeShufflenorepeat;
                case AargtypeAlarmPlayModeEnum.Shuffle:
                    return CsAllowedValAargtypeAlarmPlayModeShuffle;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}