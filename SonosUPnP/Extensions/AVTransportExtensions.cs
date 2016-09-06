using System;
using Sonos.Models;

namespace Sonos.Extensions
{
    public static class AvTransport
    {
        #region AVTransport Methods

        /// <summary>
        ///     The string value for the allowed value STOPPED of the TransportState state variable.
        /// </summary>
        private const string CsAllowedValTransportStateStopped = "STOPPED";

        /// <summary>
        ///     The string value for the allowed value PLAYING of the TransportState state variable.
        /// </summary>
        private const string CsAllowedValTransportStatePlaying = "PLAYING";

        /// <summary>
        ///     The string value for the allowed value PAUSED_PLAYBACK of the TransportState state variable.
        /// </summary>
        private const string CsAllowedValTransportStatePausedplayback = "PAUSED_PLAYBACK";

        /// <summary>
        ///     The string value for the allowed value TRANSITIONING of the TransportState state variable.
        /// </summary>
        private const string CsAllowedValTransportStateTransitioning = "TRANSITIONING";

        /// <summary>
        ///     The string value for the allowed value NONE of the PlaybackStorageMedium state variable.
        /// </summary>
        private const string CsAllowedValPlaybackStorageMediumNone = "NONE";

        /// <summary>
        ///     The string value for the allowed value NETWORK of the PlaybackStorageMedium state variable.
        /// </summary>
        private const string CsAllowedValPlaybackStorageMediumNetwork = "NETWORK";

        /// <summary>
        ///     The string value for the allowed value NONE of the RecordStorageMedium state variable.
        /// </summary>
        private const string CsAllowedValRecordStorageMediumNone = "NONE";

        /// <summary>
        ///     The string value for the allowed value NORMAL of the CurrentPlayMode state variable.
        /// </summary>
        private const string CsAllowedValCurrentPlayModeNormal = "NORMAL";

        /// <summary>
        ///     The string value for the allowed value REPEAT_ALL of the CurrentPlayMode state variable.
        /// </summary>
        private const string CsAllowedValCurrentPlayModeRepeatall = "REPEAT_ALL";

        /// <summary>
        ///     The string value for the allowed value SHUFFLE_NOREPEAT of the CurrentPlayMode state variable.
        /// </summary>
        private const string CsAllowedValCurrentPlayModeShufflenorepeat = "SHUFFLE_NOREPEAT";

        /// <summary>
        ///     The string value for the allowed value SHUFFLE of the CurrentPlayMode state variable.
        /// </summary>
        private const string CsAllowedValCurrentPlayModeShuffle = "SHUFFLE";

        /// <summary>
        ///     The string value for the allowed value 1 of the TransportPlaySpeed state variable.
        /// </summary>
        private const string CsAllowedValTransportPlaySpeed1 = "1";

        /// <summary>
        ///     The string value for the allowed value TRACK_NR of the A_ARG_TYPE_SeekMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeSeekModeTracknr = "TRACK_NR";

        /// <summary>
        ///     The string value for the allowed value REL_TIME of the A_ARG_TYPE_SeekMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeSeekModeReltime = "REL_TIME";

        /// <summary>
        ///     The string value for the allowed value SECTION of the A_ARG_TYPE_SeekMode state variable.
        /// </summary>
        private const string CsAllowedValAargtypeSeekModeSection = "SECTION";

        /// <summary>
        ///     Parses a string value from the TransportState state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or TransportState.Invalid if not parsable.</returns>
        public static TransportState ParseTransportState(string value)
        {
            switch (value)
            {
                case CsAllowedValTransportStateStopped:
                    return TransportState.Stopped;
                case CsAllowedValTransportStatePlaying:
                    return TransportState.Playing;
                case CsAllowedValTransportStatePausedplayback:
                    return TransportState.Pausedplayback;
                case CsAllowedValTransportStateTransitioning:
                    return TransportState.Transitioning;
                default:
                    return TransportState.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the TransportState state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if TransportState.Invalid or out of range.</returns>
        public static string ToStringTransportState(TransportState value)
        {
            switch (value)
            {
                case TransportState.Stopped:
                    return CsAllowedValTransportStateStopped;
                case TransportState.Playing:
                    return CsAllowedValTransportStatePlaying;
                case TransportState.Pausedplayback:
                    return CsAllowedValTransportStatePausedplayback;
                case TransportState.Transitioning:
                    return CsAllowedValTransportStateTransitioning;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the PlaybackStorageMedium state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or PlaybackStorageMediumEnum.Invalid if not parsable.</returns>
        public static PlaybackStorageMediumEnum ParsePlaybackStorageMedium(string value)
        {
            switch (value)
            {
                case CsAllowedValPlaybackStorageMediumNone:
                    return PlaybackStorageMediumEnum.None;
                case CsAllowedValPlaybackStorageMediumNetwork:
                    return PlaybackStorageMediumEnum.Network;
                default:
                    return PlaybackStorageMediumEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the PlaybackStorageMedium state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if PlaybackStorageMediumEnum.Invalid or out of range.</returns>
        public static string ToStringPlaybackStorageMedium(PlaybackStorageMediumEnum value)
        {
            switch (value)
            {
                case PlaybackStorageMediumEnum.None:
                    return CsAllowedValPlaybackStorageMediumNone;
                case PlaybackStorageMediumEnum.Network:
                    return CsAllowedValPlaybackStorageMediumNetwork;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the RecordStorageMedium state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or RecordStorageMediumEnum.Invalid if not parsable.</returns>
        public static RecordStorageMediumEnum ParseRecordStorageMedium(string value)
        {
            switch (value)
            {
                case CsAllowedValRecordStorageMediumNone:
                    return RecordStorageMediumEnum.None;
                default:
                    return RecordStorageMediumEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the RecordStorageMedium state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if RecordStorageMediumEnum.Invalid or out of range.</returns>
        public static string ToStringRecordStorageMedium(RecordStorageMediumEnum value)
        {
            switch (value)
            {
                case RecordStorageMediumEnum.None:
                    return CsAllowedValRecordStorageMediumNone;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the CurrentPlayMode state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or CurrentPlayModeEnum.Invalid if not parsable.</returns>
        public static CurrentPlayModeEnum ParseCurrentPlayMode(string value)
        {
            switch (value)
            {
                case CsAllowedValCurrentPlayModeNormal:
                    return CurrentPlayModeEnum.Normal;
                case CsAllowedValCurrentPlayModeRepeatall:
                    return CurrentPlayModeEnum.Repeatall;
                case CsAllowedValCurrentPlayModeShufflenorepeat:
                    return CurrentPlayModeEnum.Shufflenorepeat;
                case CsAllowedValCurrentPlayModeShuffle:
                    return CurrentPlayModeEnum.Shuffle;
                default:
                    return CurrentPlayModeEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the CurrentPlayMode state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if CurrentPlayModeEnum.Invalid or out of range.</returns>
        public static string ToStringCurrentPlayMode(CurrentPlayModeEnum value)
        {
            switch (value)
            {
                case CurrentPlayModeEnum.Normal:
                    return CsAllowedValCurrentPlayModeNormal;
                case CurrentPlayModeEnum.Repeatall:
                    return CsAllowedValCurrentPlayModeRepeatall;
                case CurrentPlayModeEnum.Shufflenorepeat:
                    return CsAllowedValCurrentPlayModeShufflenorepeat;
                case CurrentPlayModeEnum.Shuffle:
                    return CsAllowedValCurrentPlayModeShuffle;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the TransportPlaySpeed state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or TransportPlaySpeedEnum.Invalid if not parsable.</returns>
        public static PlaySpeed ParseTransportPlaySpeed(string value)
        {
            switch (value)
            {
                case CsAllowedValTransportPlaySpeed1:
                    return PlaySpeed.Normal;
                default:
                    return PlaySpeed.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the TransportPlaySpeed state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if TransportPlaySpeedEnum.Invalid or out of range.</returns>
        public static string ToStringTransportPlaySpeed(PlaySpeed value)
        {
            switch (value)
            {
                case PlaySpeed.Normal:
                    return CsAllowedValTransportPlaySpeed1;
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        ///     Parses a string value from the AARGTYPESeekMode state var and returns the enumeration value for it.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed value or AARGTYPESeekModeEnum.Invalid if not parsable.</returns>
        public static AargtypeSeekModeEnum ParseAargtypeSeekMode(string value)
        {
            switch (value)
            {
                case CsAllowedValAargtypeSeekModeTracknr:
                    return AargtypeSeekModeEnum.Tracknr;
                case CsAllowedValAargtypeSeekModeReltime:
                    return AargtypeSeekModeEnum.Reltime;
                case CsAllowedValAargtypeSeekModeSection:
                    return AargtypeSeekModeEnum.Section;
                default:
                    return AargtypeSeekModeEnum.Unknown;
            }
        }

        /// <summary>
        ///     Gets the string value for the AARGTYPESeekMode state var from its enumeration value.
        /// </summary>
        /// <param name="value">The enumeration value to get the string value for.</param>
        /// <returns>The string value for the enumeration, or string.empty if AARGTYPESeekModeEnum.Invalid or out of range.</returns>
        public static string ToStringAargtypeSeekMode(AargtypeSeekModeEnum value)
        {
            switch (value)
            {
                case AargtypeSeekModeEnum.Tracknr:
                    return CsAllowedValAargtypeSeekModeTracknr;
                case AargtypeSeekModeEnum.Reltime:
                    return CsAllowedValAargtypeSeekModeReltime;
                case AargtypeSeekModeEnum.Section:
                    return CsAllowedValAargtypeSeekModeSection;
                default:
                    return String.Empty;
            }
        }

        #endregion
    }
}