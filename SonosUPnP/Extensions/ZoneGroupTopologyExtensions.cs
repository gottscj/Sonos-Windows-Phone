using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class ZoneGroupTopologyExtensions
    {
        #region public extensions Methods

        /// <summary>
        ///     The string value for the allowed value All of the A_ARG_TYPE_UpdateType state variable.
        /// </summary>
        private const string CsAllowedValAargtypeUpdateTypeAll = "All";

        /// <summary>
        ///     The string value for the allowed value Software of the A_ARG_TYPE_UpdateType state variable.
        /// </summary>
        private const string CsAllowedValAargtypeUpdateTypeSoftware = "Software";

        /// <summary>
        ///     The string value for the allowed value Remove of the A_ARG_TYPE_UnresponsiveDeviceActionType state variable.
        /// </summary>
        private const string CsAllowedValAargtypeUnresponsiveDeviceActionTypeRemove = "Remove";

        /// <summary>
        ///     The string value for the allowed value VerifyThenRemoveSystemwide of the A_ARG_TYPE_UnresponsiveDeviceActionType
        ///     state variable.
        /// </summary>
        private const string CsAllowedValAargtypeUnresponsiveDeviceActionTypeVerifyThenRemoveSystemwide =
            "VerifyThenRemoveSystemwide";

        /// <summary>
        ///     Parses a string value from the AARGTYPEUpdateType state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEUpdateTypeEnum.Invalid if not parsable.</returns>
        public static AargtypeUpdateTypeEnum ParseAargtypeUpdateType(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeUpdateTypeAll:
                    return AargtypeUpdateTypeEnum.All;
                case CsAllowedValAargtypeUpdateTypeSoftware:
                    return AargtypeUpdateTypeEnum.Software;
                default:
                    return AargtypeUpdateTypeEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEUpdateType state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPEUpdateTypeEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeUpdateType(AargtypeUpdateTypeEnum value)
        {
            switch (value)
            {
                case AargtypeUpdateTypeEnum.All:
                    return CsAllowedValAargtypeUpdateTypeAll;
                case AargtypeUpdateTypeEnum.Software:
                    return CsAllowedValAargtypeUpdateTypeSoftware;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the AARGTYPEUnresponsiveDeviceActionType state var and returns the enumeration value for
        ///     it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEUnresponsiveDeviceActionTypeEnum.Invalid if not parsable.</returns>
        public static AargtypeUnresponsiveDeviceActionTypeEnum ParseAargtypeUnresponsiveDeviceActionType(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeUnresponsiveDeviceActionTypeRemove:
                    return AargtypeUnresponsiveDeviceActionTypeEnum.Remove;
                case CsAllowedValAargtypeUnresponsiveDeviceActionTypeVerifyThenRemoveSystemwide:
                    return AargtypeUnresponsiveDeviceActionTypeEnum.VerifyThenRemoveSystemwide;
                default:
                    return AargtypeUnresponsiveDeviceActionTypeEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEUnresponsiveDeviceActionType state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>
        ///     The string value for the enumeration, or string.empty if AARGTYPEUnresponsiveDeviceActionTypeEnum.Invalid or
        ///     out of range.
        /// </returns>
        public static string ToStringAargtypeUnresponsiveDeviceActionType(AargtypeUnresponsiveDeviceActionTypeEnum value)
        {
            switch (value)
            {
                case AargtypeUnresponsiveDeviceActionTypeEnum.Remove:
                    return CsAllowedValAargtypeUnresponsiveDeviceActionTypeRemove;
                case AargtypeUnresponsiveDeviceActionTypeEnum.VerifyThenRemoveSystemwide:
                    return CsAllowedValAargtypeUnresponsiveDeviceActionTypeVerifyThenRemoveSystemwide;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}