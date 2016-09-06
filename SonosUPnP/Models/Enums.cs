namespace Sonos.Models
{

    #region Public AlarmClock Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPERecurrence state variable.
    /// </summary>
    public enum AargtypeRecurrenceEnum
    {
        /// <summary>
        ///     Gets the AARGTYPERecurrence state var 'ONCE' value.
        /// </summary>
        Once,

        /// <summary>
        ///     Gets the AARGTYPERecurrence state var 'WEEKDAYS' value.
        /// </summary>
        Weekdays,

        /// <summary>
        ///     Gets the AARGTYPERecurrence state var 'WEEKENDS' value.
        /// </summary>
        Weekends,

        /// <summary>
        ///     Gets the AARGTYPERecurrence state var 'DAILY' value.
        /// </summary>
        Daily,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPERecurrence value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEAlarmPlayMode state variable.
    /// </summary>
    public enum AargtypeAlarmPlayModeEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEAlarmPlayMode state var 'NORMAL' value.
        /// </summary>
        Normal,

        /// <summary>
        ///     Gets the AARGTYPEAlarmPlayMode state var 'REPEATALL' value.
        /// </summary>
        Repeatall,

        /// <summary>
        ///     Gets the AARGTYPEAlarmPlayMode state var 'SHUFFLENOREPEAT' value.
        /// </summary>
        Shufflenorepeat,

        /// <summary>
        ///     Gets the AARGTYPEAlarmPlayMode state var 'SHUFFLE' value.
        /// </summary>
        Shuffle,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEAlarmPlayMode value.
        /// </summary>
        Unknown
    }

    #endregion

    #region Public AVTransport Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the TransportState state variable.
    /// </summary>
    public enum TransportState
    {
        /// <summary>
        ///     Value describing an invalid or unknown TransportState value.
        /// </summary>
        Unknown,
        /// <summary>
        ///     Gets the TransportState state var 'STOPPED' value.
        /// </summary>
        Stopped,

        /// <summary>
        ///     Gets the TransportState state var 'PLAYING' value.
        /// </summary>
        Playing,

        /// <summary>
        ///     Gets the TransportState state var 'PAUSEDPLAYBACK' value.
        /// </summary>
        Pausedplayback,

        /// <summary>
        ///     Gets the TransportState state var 'TRANSITIONING' value.
        /// </summary>
        Transitioning,

        
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the PlaybackStorageMedium state variable.
    /// </summary>
    public enum PlaybackStorageMediumEnum
    {
        /// <summary>
        ///     Gets the PlaybackStorageMedium state var 'NONE' value.
        /// </summary>
        None,

        /// <summary>
        ///     Gets the PlaybackStorageMedium state var 'NETWORK' value.
        /// </summary>
        Network,

        /// <summary>
        ///     Value describing an invalid or unknown PlaybackStorageMedium value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the RecordStorageMedium state variable.
    /// </summary>
    public enum RecordStorageMediumEnum
    {
        /// <summary>
        ///     Gets the RecordStorageMedium state var 'NONE' value.
        /// </summary>
        None,

        /// <summary>
        ///     Value describing an invalid or unknown RecordStorageMedium value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the CurrentPlayMode state variable.
    /// </summary>
    public enum CurrentPlayModeEnum
    {
        /// <summary>
        ///     Gets the CurrentPlayMode state var 'NORMAL' value.
        /// </summary>
        Normal,

        /// <summary>
        ///     Gets the CurrentPlayMode state var 'REPEATALL' value.
        /// </summary>
        Repeatall,

        /// <summary>
        ///     Gets the CurrentPlayMode state var 'SHUFFLENOREPEAT' value.
        /// </summary>
        Shufflenorepeat,

        /// <summary>
        ///     Gets the CurrentPlayMode state var 'SHUFFLE' value.
        /// </summary>
        Shuffle,

        /// <summary>
        ///     Value describing an invalid or unknown CurrentPlayMode value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the TransportPlaySpeed state variable.
    /// </summary>
    public enum PlaySpeed
    {
        /// <summary>
        ///     Gets the TransportPlaySpeed state var 'Normal' value.
        /// </summary>
        Normal,

        /// <summary>
        ///     Value describing an invalid or unknown TransportPlaySpeed value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPESeekMode state variable.
    /// </summary>
    public enum AargtypeSeekModeEnum
    {
        /// <summary>
        ///     Gets the AARGTYPESeekMode state var 'TRACKNR' value.
        /// </summary>
        Tracknr,

        /// <summary>
        ///     Gets the AARGTYPESeekMode state var 'RELTIME' value.
        /// </summary>
        Reltime,

        /// <summary>
        ///     Gets the AARGTYPESeekMode state var 'SECTION' value.
        /// </summary>
        Section,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPESeekMode value.
        /// </summary>
        Unknown
    }

    #endregion

    #region Public DeviceProperties Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the LEDState state variable.
    /// </summary>
    public enum LedStateEnum
    {
        /// <summary>
        ///     Gets the LEDState state var 'On' value.
        /// </summary>
        On,

        /// <summary>
        ///     Gets the LEDState state var 'Off' value.
        /// </summary>
        Off,

        /// <summary>
        ///     Value describing an invalid or unknown LEDState value.
        /// </summary>
        Unknown
    }

    #endregion

    #region Public ConnectionManager Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEConnectionStatus state variable.
    /// </summary>
    public enum AargtypeConnectionStatusEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEConnectionStatus state var 'OK' value.
        /// </summary>
        Ok,

        /// <summary>
        ///     Gets the AARGTYPEConnectionStatus state var 'ContentFormatMismatch' value.
        /// </summary>
        ContentFormatMismatch,

        /// <summary>
        ///     Gets the AARGTYPEConnectionStatus state var 'InsufficientBandwidth' value.
        /// </summary>
        InsufficientBandwidth,

        /// <summary>
        ///     Gets the AARGTYPEConnectionStatus state var 'UnreliableChannel' value.
        /// </summary>
        UnreliableChannel,

        /// <summary>
        ///     Gets the AARGTYPEConnectionStatus state var 'Unknown' value.
        /// </summary>
        Unknown,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEConnectionStatus value.
        /// </summary>
        Invalid
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEDirection state variable.
    /// </summary>
    public enum AargtypeDirectionEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEDirection state var 'Input' value.
        /// </summary>
        Input,

        /// <summary>
        ///     Gets the AARGTYPEDirection state var 'Output' value.
        /// </summary>
        Output,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEDirection value.
        /// </summary>
        Unknown
    }

    #endregion

    #region Public ZoneGroupTopology Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEUpdateType state variable.
    /// </summary>
    public enum AargtypeUpdateTypeEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEUpdateType state var 'All' value.
        /// </summary>
        All,

        /// <summary>
        ///     Gets the AARGTYPEUpdateType state var 'Software' value.
        /// </summary>
        Software,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEUpdateType value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEUnresponsiveDeviceActionType state variable.
    /// </summary>
    public enum AargtypeUnresponsiveDeviceActionTypeEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEUnresponsiveDeviceActionType state var 'Remove' value.
        /// </summary>
        Remove,

        /// <summary>
        ///     Gets the AARGTYPEUnresponsiveDeviceActionType state var 'VerifyThenRemoveSystemwide' value.
        /// </summary>
        VerifyThenRemoveSystemwide,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEUnresponsiveDeviceActionType value.
        /// </summary>
        Unknown
    }

    #endregion

    #region Public RenderingControl Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEChannel state variable.
    /// </summary>
    public enum AargtypeChannelEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEChannel state var 'Master' value.
        /// </summary>
        Master,

        /// <summary>
        ///     Gets the AARGTYPEChannel state var 'LF' value.
        /// </summary>
        Lf,

        /// <summary>
        ///     Gets the AARGTYPEChannel state var 'RF' value.
        /// </summary>
        Rf,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEChannel value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEMuteChannel state variable.
    /// </summary>
    public enum AargtypeMuteChannelEnum
    {
        /// <summary>
        ///     Gets the AARGTYPEMuteChannel state var 'Master' value.
        /// </summary>
        Master,

        /// <summary>
        ///     Gets the AARGTYPEMuteChannel state var 'LF' value.
        /// </summary>
        Lf,

        /// <summary>
        ///     Gets the AARGTYPEMuteChannel state var 'RF' value.
        /// </summary>
        Rf,

        /// <summary>
        ///     Gets the AARGTYPEMuteChannel state var 'SpeakerOnly' value.
        /// </summary>
        SpeakerOnly,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEMuteChannel value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPERampType state variable.
    /// </summary>
    public enum AargtypeRampTypeEnum
    {
        /// <summary>
        ///     Gets the AARGTYPERampType state var 'SLEEPTIMERRAMPTYPE' value.
        /// </summary>
        SleepTimerRampType,

        /// <summary>
        ///     Gets the AARGTYPERampType state var 'ALARMRAMPTYPE' value.
        /// </summary>
        AlarmRampType,

        /// <summary>
        ///     Gets the AARGTYPERampType state var 'AUTOPLAYRAMPTYPE' value.
        /// </summary>
        AutoplayRampType,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPERampType value.
        /// </summary>
        Unknown
    }

    #endregion

    #region Public ContentDirectory Enumerations

    /// <summary>
    ///     Gets the enumeration type to hold a value for the AARGTYPEBrowseFlag state variable.
    /// </summary>
    public enum BrowseFlag
    {
        /// <summary>
        ///     Gets the AARGTYPEBrowseFlag state var 'BrowseMetadata' value.
        /// </summary>
        BrowseMetadata,

        /// <summary>
        ///     Gets the AARGTYPEBrowseFlag state var 'BrowseDirectChildren' value.
        /// </summary>
        BrowseDirectChildren,

        /// <summary>
        ///     Value describing an invalid or unknown AARGTYPEBrowseFlag value.
        /// </summary>
        Unknown
    }

    /// <summary>
    ///     Gets the enumeration type to hold a value for the ShareListRefreshState state variable.
    /// </summary>
    public enum ShareListRefreshStateEnum
    {
        /// <summary>
        ///     Gets the ShareListRefreshState state var 'NOTRUN' value.
        /// </summary>
        Notrun,

        /// <summary>
        ///     Gets the ShareListRefreshState state var 'RUNNING' value.
        /// </summary>
        Running,

        /// <summary>
        ///     Gets the ShareListRefreshState state var 'DONE' value.
        /// </summary>
        Done,

        /// <summary>
        ///     Value describing an invalid or unknown ShareListRefreshState value.
        /// </summary>
        Unknown
    }

    public enum BaseBrowseIdEnum
    {
        Attributes,
        MusicShares,
        Queues,
        SavedQueues,
        InternetRadio,
        EntireNetwork,
        RecentlyPlayed,
        FavoritesAndPresets
    }

    #endregion

    #region Sonos Specific

    public enum ZoneType
    {
        Unknown,
        Bridge,
        Play3,
    }

    //public enum SonosServices
    //{
    //    AlarmClock,
    //    AVTransport,
    //    ConnectionManager,
    //    ContentDirectory,
    //    DeviceProperties,
    //    GroupManagement,
    //    MusicServices,
    //    RenderingControl,
    //    SystemProperties,
    //    ZoneGroupTopology
    //}

    #endregion
}