using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class DeviceProperties
    {
        #region Extension Methods

        /// <summary>
        ///     The string value for the allowed value On of the LEDState state variable.
        /// </summary>
        private const string CsAllowedValLedStateOn = "On";

        /// <summary>
        ///     The string value for the allowed value Off of the LEDState state variable.
        /// </summary>
        private const string CsAllowedValLedStateOff = "Off";

        /// <summary>
        ///     Parses a string value from the LEDState state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or LEDStateEnum.Invalid if not parsable.</returns>
        public static LedStateEnum ParseLedState(string value)
        {
            switch (value)
            {
                case CsAllowedValLedStateOn:
                    return LedStateEnum.On;
                case CsAllowedValLedStateOff:
                    return LedStateEnum.Off;
                default:
                    return LedStateEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the LEDState state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if LEDStateEnum.Invalid or out of range.</returns>
        public static string ToStringLedState(LedStateEnum value)
        {
            switch (value)
            {
                case LedStateEnum.On:
                    return CsAllowedValLedStateOn;
                case LedStateEnum.Off:
                    return CsAllowedValLedStateOff;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}