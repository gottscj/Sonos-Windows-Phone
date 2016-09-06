using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class ConnectionManagerExtensions
    {
        #region Protected Methods

        /// <summary>
        ///     The string value for the allowed value OK of the A_ARG_TYPE_ConnectionStatus state variable.
        /// </summary>
        private const string CsAllowedValAargtypeConnectionStatusOk = "OK";

        /// <summary>
        ///     The string value for the allowed value ContentFormatMismatch of the A_ARG_TYPE_ConnectionStatus state variable.
        /// </summary>
        private const string CsAllowedValAargtypeConnectionStatusContentFormatMismatch = "ContentFormatMismatch";

        /// <summary>
        ///     The string value for the allowed value InsufficientBandwidth of the A_ARG_TYPE_ConnectionStatus state variable.
        /// </summary>
        private const string CsAllowedValAargtypeConnectionStatusInsufficientBandwidth = "InsufficientBandwidth";

        /// <summary>
        ///     The string value for the allowed value UnreliableChannel of the A_ARG_TYPE_ConnectionStatus state variable.
        /// </summary>
        private const string CsAllowedValAargtypeConnectionStatusUnreliableChannel = "UnreliableChannel";

        /// <summary>
        ///     The string value for the allowed value Unknown of the A_ARG_TYPE_ConnectionStatus state variable.
        /// </summary>
        private const string CsAllowedValAargtypeConnectionStatusUnknown = "Unknown";

        /// <summary>
        ///     The string value for the allowed value Input of the A_ARG_TYPE_Direction state variable.
        /// </summary>
        private const string CsAllowedValAargtypeDirectionInput = "Input";

        /// <summary>
        ///     The string value for the allowed value Output of the A_ARG_TYPE_Direction state variable.
        /// </summary>
        private const string CsAllowedValAargtypeDirectionOutput = "Output";

        /// <summary>
        ///     Parses a string value from the AARGTYPEConnectionStatus state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEConnectionStatusEnum.Invalid if not parsable.</returns>
        public static AargtypeConnectionStatusEnum ParseAargtypeConnectionStatus(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeConnectionStatusOk:
                    return AargtypeConnectionStatusEnum.Ok;
                case CsAllowedValAargtypeConnectionStatusContentFormatMismatch:
                    return AargtypeConnectionStatusEnum.ContentFormatMismatch;
                case CsAllowedValAargtypeConnectionStatusInsufficientBandwidth:
                    return AargtypeConnectionStatusEnum.InsufficientBandwidth;
                case CsAllowedValAargtypeConnectionStatusUnreliableChannel:
                    return AargtypeConnectionStatusEnum.UnreliableChannel;
                case CsAllowedValAargtypeConnectionStatusUnknown:
                    return AargtypeConnectionStatusEnum.Unknown;
                default:
                    return AargtypeConnectionStatusEnum.Invalid;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEConnectionStatus state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>
        ///     The string value for the enumeration, or string.empty if AARGTYPEConnectionStatusEnum.Invalid or out of
        ///     range.
        /// </returns>
        public static string ToStringAargtypeConnectionStatus(AargtypeConnectionStatusEnum value)
        {
            switch (value)
            {
                case AargtypeConnectionStatusEnum.Ok:
                    return CsAllowedValAargtypeConnectionStatusOk;
                case AargtypeConnectionStatusEnum.ContentFormatMismatch:
                    return CsAllowedValAargtypeConnectionStatusContentFormatMismatch;
                case AargtypeConnectionStatusEnum.InsufficientBandwidth:
                    return CsAllowedValAargtypeConnectionStatusInsufficientBandwidth;
                case AargtypeConnectionStatusEnum.UnreliableChannel:
                    return CsAllowedValAargtypeConnectionStatusUnreliableChannel;
                case AargtypeConnectionStatusEnum.Unknown:
                    return CsAllowedValAargtypeConnectionStatusUnknown;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the AARGTYPEDirection state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEDirectionEnum.Invalid if not parsable.</returns>
        public static AargtypeDirectionEnum ParseAargtypeDirection(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeDirectionInput:
                    return AargtypeDirectionEnum.Input;
                case CsAllowedValAargtypeDirectionOutput:
                    return AargtypeDirectionEnum.Output;
                default:
                    return AargtypeDirectionEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEDirection state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPEDirectionEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeDirection(AargtypeDirectionEnum value)
        {
            switch (value)
            {
                case AargtypeDirectionEnum.Input:
                    return CsAllowedValAargtypeDirectionInput;
                case AargtypeDirectionEnum.Output:
                    return CsAllowedValAargtypeDirectionOutput;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}