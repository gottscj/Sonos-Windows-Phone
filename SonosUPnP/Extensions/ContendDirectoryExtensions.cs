using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class ContendDirectoryExtensions
    {
        #region Methods

        /// <summary>
        ///     The string value for the allowed value BrowseMetadata of the A_ARG_TYPE_BrowseFlag state variable.
        /// </summary>
        private const string CsAllowedValAargtypeBrowseFlagBrowseMetadata = "BrowseMetadata";

        /// <summary>
        ///     The string value for the allowed value BrowseDirectChildren of the A_ARG_TYPE_BrowseFlag state variable.
        /// </summary>
        private const string CsAllowedValAargtypeBrowseFlagBrowseDirectChildren = "BrowseDirectChildren";

        /// <summary>
        ///     The string value for the allowed value NOTRUN of the ShareListRefreshState state variable.
        /// </summary>
        private const string CsAllowedValShareListRefreshStateNotrun = "NOTRUN";

        /// <summary>
        ///     The string value for the allowed value RUNNING of the ShareListRefreshState state variable.
        /// </summary>
        private const string CsAllowedValShareListRefreshStateRunning = "RUNNING";

        /// <summary>
        ///     The string value for the allowed value DONE of the ShareListRefreshState state variable.
        /// </summary>
        private const string CsAllowedValShareListRefreshStateDone = "DONE";

        /// <summary>
        ///     Parses a string value from the AARGTYPEBrowseFlag state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPEBrowseFlagEnum.Invalid if not parsable.</returns>
        public static BrowseFlag ParseAargtypeBrowseFlag(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeBrowseFlagBrowseMetadata:
                    return BrowseFlag.BrowseMetadata;
                case CsAllowedValAargtypeBrowseFlagBrowseDirectChildren:
                    return BrowseFlag.BrowseDirectChildren;
                default:
                    return BrowseFlag.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPEBrowseFlag state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPEBrowseFlagEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeBrowseFlag(BrowseFlag value)
        {
            switch (value)
            {
                case BrowseFlag.BrowseMetadata:
                    return CsAllowedValAargtypeBrowseFlagBrowseMetadata;
                case BrowseFlag.BrowseDirectChildren:
                    return CsAllowedValAargtypeBrowseFlagBrowseDirectChildren;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the ShareListRefreshState state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or ShareListRefreshStateEnum.Invalid if not parsable.</returns>
        public static ShareListRefreshStateEnum ParseShareListRefreshState(string value)
        {
            switch (value)
            {
                case CsAllowedValShareListRefreshStateNotrun:
                    return ShareListRefreshStateEnum.Notrun;
                case CsAllowedValShareListRefreshStateRunning:
                    return ShareListRefreshStateEnum.Running;
                case CsAllowedValShareListRefreshStateDone:
                    return ShareListRefreshStateEnum.Done;
                default:
                    return ShareListRefreshStateEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the ShareListRefreshState state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if ShareListRefreshStateEnum.Invalid or out of range.</returns>
        public static string ToStringShareListRefreshState(ShareListRefreshStateEnum value)
        {
            switch (value)
            {
                case ShareListRefreshStateEnum.Notrun:
                    return CsAllowedValShareListRefreshStateNotrun;
                case ShareListRefreshStateEnum.Running:
                    return CsAllowedValShareListRefreshStateRunning;
                case ShareListRefreshStateEnum.Done:
                    return CsAllowedValShareListRefreshStateDone;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}