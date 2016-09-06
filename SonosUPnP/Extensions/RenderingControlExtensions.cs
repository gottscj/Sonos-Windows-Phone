using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class RenderingControlExtensions
    {
        #region Protected Methods

        /// <summary>
        ///     The string value for the allowed value Master of the A_ARG_TYPE_Channel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeChannelMaster = "Master";

        /// <summary>
        ///     The string value for the allowed value LF of the A_ARG_TYPE_Channel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeChannelLf = "LF";

        /// <summary>
        ///     The string value for the allowed value RF of the A_ARG_TYPE_Channel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeChannelRf = "RF";

        /// <summary>
        ///     The string value for the allowed value Master of the A_ARG_TYPE_MuteChannel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeMuteChannelMaster = "Master";

        /// <summary>
        ///     The string value for the allowed value LF of the A_ARG_TYPE_MuteChannel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeMuteChannelLf = "LF";

        /// <summary>
        ///     The string value for the allowed value RF of the A_ARG_TYPE_MuteChannel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeMuteChannelRf = "RF";

        /// <summary>
        ///     The string value for the allowed value SpeakerOnly of the A_ARG_TYPE_MuteChannel state variable.
        /// </summary>
        private const string CsAllowedValAargtypeMuteChannelSpeakerOnly = "SpeakerOnly";

        /// <summary>
        ///     The string value for the allowed value SLEEP_TIMER_RAMP_TYPE of the A_ARG_TYPE_RampType state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRampTypeSleeptimerramptype = "SLEEP_TIMER_RAMP_TYPE";

        /// <summary>
        ///     The string value for the allowed value ALARM_RAMP_TYPE of the A_ARG_TYPE_RampType state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRampTypeAlarmramptype = "ALARM_RAMP_TYPE";

        /// <summary>
        ///     The string value for the allowed value AUTOPLAY_RAMP_TYPE of the A_ARG_TYPE_RampType state variable.
        /// </summary>
        private const string CsAllowedValAargtypeRampTypeAutoplayramptype = "AUTOPLAY_RAMP_TYPE";

        /// <summary>
        ///     Parses a string value from the AARGTYPEChannel state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEChannelEnum.Invalid if not parsable.</returns>
        public static AargtypeChannelEnum ParseAargtypeChannel(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeChannelMaster:
                    return AargtypeChannelEnum.Master;
                case CsAllowedValAargtypeChannelLf:
                    return AargtypeChannelEnum.Lf;
                case CsAllowedValAargtypeChannelRf:
                    return AargtypeChannelEnum.Rf;
                default:
                    return AargtypeChannelEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEChannel state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPEChannelEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeChannel(AargtypeChannelEnum value)
        {
            switch (value)
            {
                case AargtypeChannelEnum.Master:
                    return CsAllowedValAargtypeChannelMaster;
                case AargtypeChannelEnum.Lf:
                    return CsAllowedValAargtypeChannelLf;
                case AargtypeChannelEnum.Rf:
                    return CsAllowedValAargtypeChannelRf;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the AARGTYPEMuteChannel state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEMuteChannelEnum.Invalid if not parsable.</returns>
        public static AargtypeMuteChannelEnum ParseAargtypeMuteChannel(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeMuteChannelMaster:
                    return AargtypeMuteChannelEnum.Master;
                case CsAllowedValAargtypeMuteChannelLf:
                    return AargtypeMuteChannelEnum.Lf;
                case CsAllowedValAargtypeMuteChannelRf:
                    return AargtypeMuteChannelEnum.Rf;
                case CsAllowedValAargtypeMuteChannelSpeakerOnly:
                    return AargtypeMuteChannelEnum.SpeakerOnly;
                default:
                    return AargtypeMuteChannelEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEMuteChannel state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPEMuteChannelEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeMuteChannel(AargtypeMuteChannelEnum value)
        {
            switch (value)
            {
                case AargtypeMuteChannelEnum.Master:
                    return CsAllowedValAargtypeMuteChannelMaster;
                case AargtypeMuteChannelEnum.Lf:
                    return CsAllowedValAargtypeMuteChannelLf;
                case AargtypeMuteChannelEnum.Rf:
                    return CsAllowedValAargtypeMuteChannelRf;
                case AargtypeMuteChannelEnum.SpeakerOnly:
                    return CsAllowedValAargtypeMuteChannelSpeakerOnly;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the AARGTYPERampType state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPERampTypeEnum.Invalid if not parsable.</returns>
        public static AargtypeRampTypeEnum ParseAargtypeRampType(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeRampTypeSleeptimerramptype:
                    return AargtypeRampTypeEnum.SleepTimerRampType;
                case CsAllowedValAargtypeRampTypeAlarmramptype:
                    return AargtypeRampTypeEnum.AlarmRampType;
                case CsAllowedValAargtypeRampTypeAutoplayramptype:
                    return AargtypeRampTypeEnum.AutoplayRampType;
                default:
                    return AargtypeRampTypeEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPERampType state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPERampTypeEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeRampType(AargtypeRampTypeEnum value)
        {
            switch (value)
            {
                case AargtypeRampTypeEnum.SleepTimerRampType:
                    return CsAllowedValAargtypeRampTypeSleeptimerramptype;
                case AargtypeRampTypeEnum.AlarmRampType:
                    return CsAllowedValAargtypeRampTypeAlarmramptype;
                case AargtypeRampTypeEnum.AutoplayRampType:
                    return CsAllowedValAargtypeRampTypeAutoplayramptype;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}