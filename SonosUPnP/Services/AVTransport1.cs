using System;
using System.Threading.Tasks;
using Sonos.Extensions;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the AVTransport1 (urn:schemas-upnp-org:service:AVTransport:1) service.
    /// </summary>
    public class AvTransport1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the LastChange state variable.
        /// </summary>
        protected const string CsStateVarLastChange = "LastChange";


        /// <summary>
        ///     The name for the SetAVTransportURI action.
        /// </summary>
        protected const string CsActionSetAvTransportUri = "SetAVTransportURI";

        /// <summary>
        ///     The name for the AddURIToQueue action.
        /// </summary>
        protected const string CsActionAddUriToQueue = "AddURIToQueue";

        /// <summary>
        ///     The name for the AddMultipleURIsToQueue action.
        /// </summary>
        protected const string CsActionAddMultipleUrIsToQueue = "AddMultipleURIsToQueue";

        /// <summary>
        ///     The name for the ReorderTracksInQueue action.
        /// </summary>
        protected const string CsActionReorderTracksInQueue = "ReorderTracksInQueue";

        /// <summary>
        ///     The name for the RemoveTrackFromQueue action.
        /// </summary>
        protected const string CsActionRemoveTrackFromQueue = "RemoveTrackFromQueue";

        /// <summary>
        ///     The name for the RemoveTrackRangeFromQueue action.
        /// </summary>
        protected const string CsActionRemoveTrackRangeFromQueue = "RemoveTrackRangeFromQueue";

        /// <summary>
        ///     The name for the RemoveAllTracksFromQueue action.
        /// </summary>
        protected const string CsActionRemoveAllTracksFromQueue = "RemoveAllTracksFromQueue";

        /// <summary>
        ///     The name for the SaveQueue action.
        /// </summary>
        protected const string CsActionSaveQueue = "SaveQueue";

        /// <summary>
        ///     The name for the BackupQueue action.
        /// </summary>
        protected const string CsActionBackupQueue = "BackupQueue";

        /// <summary>
        ///     The name for the GetMediaInfo action.
        /// </summary>
        protected const string CsActionGetMediaInfo = "GetMediaInfo";

        /// <summary>
        ///     The name for the GetTransportInfo action.
        /// </summary>
        protected const string CsActionGetTransportInfo = "GetTransportInfo";

        /// <summary>
        ///     The name for the GetPositionInfo action.
        /// </summary>
        protected const string CsActionGetPositionInfo = "GetPositionInfo";

        /// <summary>
        ///     The name for the GetDeviceCapabilities action.
        /// </summary>
        protected const string CsActionGetDeviceCapabilities = "GetDeviceCapabilities";

        /// <summary>
        ///     The name for the GetTransportSettings action.
        /// </summary>
        protected const string CsActionGetTransportSettings = "GetTransportSettings";

        /// <summary>
        ///     The name for the GetCrossfadeMode action.
        /// </summary>
        protected const string CsActionGetCrossfadeMode = "GetCrossfadeMode";

        /// <summary>
        ///     The name for the Stop action.
        /// </summary>
        protected const string CsActionStop = "Stop";

        /// <summary>
        ///     The name for the Play action.
        /// </summary>
        protected const string CsActionPlay = "Play";

        /// <summary>
        ///     The name for the Pause action.
        /// </summary>
        protected const string CsActionPause = "Pause";

        /// <summary>
        ///     The name for the Seek action.
        /// </summary>
        protected const string CsActionSeek = "Seek";

        /// <summary>
        ///     The name for the Next action.
        /// </summary>
        protected const string CsActionNext = "Next";

        /// <summary>
        ///     The name for the NextProgrammedRadioTracks action.
        /// </summary>
        protected const string CsActionNextProgrammedRadioTracks = "NextProgrammedRadioTracks";

        /// <summary>
        ///     The name for the Previous action.
        /// </summary>
        protected const string CsActionPrevious = "Previous";

        /// <summary>
        ///     The name for the NextSection action.
        /// </summary>
        protected const string CsActionNextSection = "NextSection";

        /// <summary>
        ///     The name for the PreviousSection action.
        /// </summary>
        protected const string CsActionPreviousSection = "PreviousSection";

        /// <summary>
        ///     The name for the SetPlayMode action.
        /// </summary>
        protected const string CsActionSetPlayMode = "SetPlayMode";

        /// <summary>
        ///     The name for the SetCrossfadeMode action.
        /// </summary>
        protected const string CsActionSetCrossfadeMode = "SetCrossfadeMode";

        /// <summary>
        ///     The name for the NotifyDeletedURI action.
        /// </summary>
        protected const string CsActionNotifyDeletedUri = "NotifyDeletedURI";

        /// <summary>
        ///     The name for the GetCurrentTransportActions action.
        /// </summary>
        protected const string CsActionGetCurrentTransportActions = "GetCurrentTransportActions";

        /// <summary>
        ///     The name for the BecomeCoordinatorOfStandaloneGroup action.
        /// </summary>
        protected const string CsActionBecomeCoordinatorOfStandaloneGroup = "BecomeCoordinatorOfStandaloneGroup";

        /// <summary>
        ///     The name for the BecomeGroupCoordinator action.
        /// </summary>
        protected const string CsActionBecomeGroupCoordinator = "BecomeGroupCoordinator";

        /// <summary>
        ///     The name for the BecomeGroupCoordinatorAndSource action.
        /// </summary>
        protected const string CsActionBecomeGroupCoordinatorAndSource = "BecomeGroupCoordinatorAndSource";

        /// <summary>
        ///     The name for the ChangeCoordinator action.
        /// </summary>
        protected const string CsActionChangeCoordinator = "ChangeCoordinator";

        /// <summary>
        ///     The name for the ChangeTransportSettings action.
        /// </summary>
        protected const string CsActionChangeTransportSettings = "ChangeTransportSettings";

        /// <summary>
        ///     The name for the ConfigureSleepTimer action.
        /// </summary>
        protected const string CsActionConfigureSleepTimer = "ConfigureSleepTimer";

        /// <summary>
        ///     The name for the GetRemainingSleepTimerDuration action.
        /// </summary>
        protected const string CsActionGetRemainingSleepTimerDuration = "GetRemainingSleepTimerDuration";

        /// <summary>
        ///     The name for the RunAlarm action.
        /// </summary>
        protected const string CsActionRunAlarm = "RunAlarm";

        /// <summary>
        ///     The name for the StartAutoplay action.
        /// </summary>
        protected const string CsActionStartAutoplay = "StartAutoplay";

        /// <summary>
        ///     The name for the GetRunningAlarmProperties action.
        /// </summary>
        protected const string CsActionGetRunningAlarmProperties = "GetRunningAlarmProperties";

        /// <summary>
        ///     The name for the SnoozeAlarm action.
        /// </summary>
        protected const string CsActionSnoozeAlarm = "SnoozeAlarm";

        private int _eventSemaphore;

        private int EventSemaphore
        {
            get { return _eventSemaphore; }
            set
            {
                if (_eventSemaphore == 0 && value == 1)
                {
                    SubcribeToEvents();
                }
                else if (_eventSemaphore > 0 && value == 0)
                {
                    UnsubscribeToEvents();
                }
                _eventSemaphore = value;
            }
        }

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the AVTransport1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the AVTransport1 service from.</param>
        public AvTransport1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Occurs when the service notifies that the LastChange state variable has changed its value.
        /// </summary>
        private event StateVariableChangedEventHandler<AvTransportStatus> LastChangeChangedInternal;

        public event StateVariableChangedEventHandler<AvTransportStatus> LastChangeChanged
        {
            add
            {
                EventSemaphore++;
                LastChangeChangedInternal += value;
            }
            remove
            {
                EventSemaphore--;
                LastChangeChangedInternal -= value;
            }
        }

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the LastChangeChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnLastChangeChanged(StateVariableChangedEventArgs e)
        {
            if (LastChangeChangedInternal != null)
            {
                LastChangeChangedInternal(this, new AvTransportStatus(e.StateVarValue));
            }
        }

        /// <summary>
        ///     Raises the StateVariableChanged event.
        /// </summary>
        protected override void OnStateVariableChanged(StateVariableChangedEventArgs e)
        {
            // Determine state variable that is changing
            switch (e.StateVarName)
            {
                case CsStateVarLastChange:
                    // Raise the event for the LastChange state variable
                    OnLastChangeChanged(e);
                    break;
            }

            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the SetAVTransportURI action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="currentUri">In value for the CurrentURI action parameter.</param>
        /// <param name="currentUriMetaData">In value for the CurrentURIMetaData action parameter.</param>
        public async Task SetAvTransportUri(UInt32 instanceId, String currentUri, String currentUriMetaData)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = currentUri;
            loIn[2] = currentUriMetaData;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "CurrentURI", "CurrentURIMetaData"},
                Name = CsActionSetAvTransportUri,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the AddURIToQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="enqueuedUri">In value for the EnqueuedURI action parameter.</param>
        /// <param name="enqueuedUriMetaData">In value for the EnqueuedURIMetaData action parameter.</param>
        /// <param name="desiredFirstTrackNumberEnqueued">In value for the DesiredFirstTrackNumberEnqueued action parameter.</param>
        /// <param name="enqueueAsNext">In value for the EnqueueAsNext action parameter.</param>
        public async Task<ActionResult> AddUriToQueue(UInt32 instanceId, String enqueuedUri, String enqueuedUriMetaData,
            UInt32 desiredFirstTrackNumberEnqueued, Boolean enqueueAsNext)
        {
            var loIn = new object[5];

            loIn[0] = instanceId;
            loIn[1] = enqueuedUri;
            loIn[2] = enqueuedUriMetaData;
            loIn[3] = desiredFirstTrackNumberEnqueued;
            loIn[4] = enqueueAsNext;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "InstanceID", "EnqueuedURI", "EnqueuedURIMetaData", "DesiredFirstTrackNumberEnqueued",
                        "EnqueueAsNext"
                    },
                Name = CsActionAddUriToQueue,
                ExpectedReplyCount = 3
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the AddMultipleURIsToQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="updateId">In value for the UpdateID action parameter.</param>
        /// <param name="numberOfUrIs">In value for the NumberOfURIs action parameter.</param>
        /// <param name="enqueuedUrIs">In value for the EnqueuedURIs action parameter.</param>
        /// <param name="enqueuedUrIsMetaData">In value for the EnqueuedURIsMetaData action parameter.</param>
        /// <param name="containerUri">In value for the ContainerURI action parameter.</param>
        /// <param name="containerMetaData">In value for the ContainerMetaData action parameter.</param>
        /// <param name="desiredFirstTrackNumberEnqueued">In value for the DesiredFirstTrackNumberEnqueued action parameter.</param>
        /// <param name="enqueueAsNext">In value for the EnqueueAsNext action parameter.</param>
        public async Task<ActionResult> AddMultipleUrIsToQueue(UInt32 instanceId, UInt32 updateId, UInt32 numberOfUrIs,
            String enqueuedUrIs, String enqueuedUrIsMetaData, String containerUri, String containerMetaData,
            UInt32 desiredFirstTrackNumberEnqueued, Boolean enqueueAsNext)
        {
            var loIn = new object[9];

            loIn[0] = instanceId;
            loIn[1] = updateId;
            loIn[2] = numberOfUrIs;
            loIn[3] = enqueuedUrIs;
            loIn[4] = enqueuedUrIsMetaData;
            loIn[5] = containerUri;
            loIn[6] = containerMetaData;
            loIn[7] = desiredFirstTrackNumberEnqueued;
            loIn[8] = enqueueAsNext;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "InstanceID", "UpdateID", "NumberOfURIs", "EnqueuedURIs", "EnqueuedURIsMetaData", "ContainerURI",
                        "ContainerMetaData", "DesiredFirstTrackNumberEnqueued", "EnqueueAsNext"
                    },
                Name = CsActionAddMultipleUrIsToQueue,
                ExpectedReplyCount = 4
            };

            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the ReorderTracksInQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="startingIndex">In value for the StartingIndex action parameter.</param>
        /// <param name="numberOfTracks">In value for the NumberOfTracks action parameter.</param>
        /// <param name="insertBefore">In value for the InsertBefore action parameter.</param>
        /// <param name="updateId">In value for the UpdateID action parameter.</param>
        public async Task ReorderTracksInQueue(UInt32 instanceId, UInt32 startingIndex, UInt32 numberOfTracks,
            UInt32 insertBefore, UInt32 updateId)
        {
            var loIn = new object[5];

            loIn[0] = instanceId;
            loIn[1] = startingIndex;
            loIn[2] = numberOfTracks;
            loIn[3] = insertBefore;
            loIn[4] = updateId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "StartingIndex", "NumberOfTracks", "InsertBefore", "UpdateID"},
                Name = CsActionReorderTracksInQueue,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RemoveTrackFromQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        /// <param name="updateId">In value for the UpdateID action parameter.</param>
        public async Task RemoveTrackFromQueue(UInt32 instanceId, String objectId, UInt32 updateId)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = objectId;
            loIn[2] = updateId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "ObjectID", "UpdateID"},
                Name = CsActionRemoveTrackFromQueue,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RemoveTrackRangeFromQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="updateId">In value for the UpdateID action parameter.</param>
        /// <param name="startingIndex">In value for the StartingIndex action parameter.</param>
        /// <param name="numberOfTracks">In value for the NumberOfTracks action parameter.</param>
        /// <returns>Out value for the NewUpdateID action parameter.</returns>
        public async Task<ActionResult> RemoveTrackRangeFromQueue(UInt32 instanceId, UInt32 updateId,
            UInt32 startingIndex, UInt32 numberOfTracks)
        {
            var loIn = new object[4];

            loIn[0] = instanceId;
            loIn[1] = updateId;
            loIn[2] = startingIndex;
            loIn[3] = numberOfTracks;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "UpdateID", "StartingIndex", "NumberOfTracks"},
                Name = CsActionRemoveTrackRangeFromQueue,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the RemoveAllTracksFromQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task RemoveAllTracksFromQueue(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionRemoveAllTracksFromQueue,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SaveQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="title">In value for the Title action parameter.</param>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        /// <returns>Out value for the AssignedObjectID action parameter.</returns>
        public async Task<ActionResult> SaveQueue(UInt32 instanceId, String title, String objectId)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = title;
            loIn[2] = objectId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Title", "ObjectID"},
                Name = CsActionSaveQueue,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the BackupQueue action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task BackupQueue(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionBackupQueue,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetMediaInfo action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetMediaInfo(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetMediaInfo,
                ExpectedReplyCount = 9
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetTransportInfo action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetTransportInfo(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetTransportInfo,
                ExpectedReplyCount = 3
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetPositionInfo action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetPositionInfo(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetPositionInfo,
                ExpectedReplyCount = 8
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetDeviceCapabilities action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetDeviceCapabilities(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetDeviceCapabilities,
                ExpectedReplyCount = 3
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetTransportSettings action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetTransportSettings(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetTransportSettings,
                ExpectedReplyCount = 2
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetCrossfadeMode action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the CrossfadeMode action parameter.</returns>
        public async Task<ActionResult> GetCrossfadeMode(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetCrossfadeMode,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the Stop action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task Stop(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionStop,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the Play action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="speed">In value for the Speed action parameter.</param>
        public async Task Play(UInt32 instanceId, PlaySpeed speed)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = AvTransport.ToStringTransportPlaySpeed(speed);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Speed"},
                Name = CsActionPlay,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the Pause action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task Pause(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionPause,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the Seek action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="unit">In value for the Unit action parameter.</param>
        /// <param name="target">In value for the Target action parameter.</param>
        public async Task Seek(UInt32 instanceId, AargtypeSeekModeEnum unit, String target)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = AvTransport.ToStringAargtypeSeekMode(unit);
            loIn[2] = target;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Unit", "Target"},
                Name = CsActionSeek,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the Next action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task Next(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionNext,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the NextProgrammedRadioTracks action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task NextProgrammedRadioTracks(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionNextProgrammedRadioTracks,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the Previous action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task Previous(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionPrevious,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the NextSection action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task NextSection(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionNextSection,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the PreviousSection action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task PreviousSection(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionPreviousSection,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetPlayMode action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="newPlayMode">In value for the NewPlayMode action parameter. Default value of NORMAL.</param>
        public async Task SetPlayMode(UInt32 instanceId, CurrentPlayModeEnum newPlayMode)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = AvTransport.ToStringCurrentPlayMode(newPlayMode);
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "NewPlayMode"},
                Name = CsActionSetPlayMode,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetCrossfadeMode action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="crossfadeMode">In value for the CrossfadeMode action parameter.</param>
        public async Task SetCrossfadeMode(UInt32 instanceId, Boolean crossfadeMode)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = crossfadeMode;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "CrossfadeMode"},
                Name = CsActionSetCrossfadeMode,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the NotifyDeletedURI action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="deletedUri">In value for the DeletedURI action parameter.</param>
        public async Task NotifyDeletedUri(UInt32 instanceId, String deletedUri)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = deletedUri;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "DeletedURI"},
                Name = CsActionNotifyDeletedUri,
                ExpectedReplyCount = 0
            };

            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetCurrentTransportActions action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <returns>Out value for the Actions action parameter.</returns>
        public async Task<ActionResult> GetCurrentTransportActions(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetCurrentTransportActions,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the BecomeCoordinatorOfStandaloneGroup action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task BecomeCoordinatorOfStandaloneGroup(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionBecomeCoordinatorOfStandaloneGroup,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the BecomeGroupCoordinator action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="currentCoordinator">In value for the CurrentCoordinator action parameter.</param>
        /// <param name="currentGroupId">In value for the CurrentGroupID action parameter.</param>
        /// <param name="otherMembers">In value for the OtherMembers action parameter.</param>
        /// <param name="transportSettings">In value for the TransportSettings action parameter.</param>
        /// <param name="currentUri">In value for the CurrentURI action parameter.</param>
        /// <param name="currentUriMetaData">In value for the CurrentURIMetaData action parameter.</param>
        /// <param name="sleepTimerState">In value for the SleepTimerState action parameter.</param>
        /// <param name="alarmState">In value for the AlarmState action parameter.</param>
        /// <param name="streamRestartState">In value for the StreamRestartState action parameter.</param>
        /// <param name="currentQueueTrackList">In value for the CurrentQueueTrackList action parameter.</param>
        public async Task BecomeGroupCoordinator(UInt32 instanceId, String currentCoordinator, String currentGroupId,
            String otherMembers, String transportSettings, String currentUri, String currentUriMetaData,
            String sleepTimerState, String alarmState, String streamRestartState, String currentQueueTrackList)
        {
            var loIn = new object[11];

            loIn[0] = instanceId;
            loIn[1] = currentCoordinator;
            loIn[2] = currentGroupId;
            loIn[3] = otherMembers;
            loIn[4] = transportSettings;
            loIn[5] = currentUri;
            loIn[6] = currentUriMetaData;
            loIn[7] = sleepTimerState;
            loIn[8] = alarmState;
            loIn[9] = streamRestartState;
            loIn[10] = currentQueueTrackList;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "InstanceID", "CurrentCoordinator", "CurrentGroupID", "OtherMembers", "TransportSettings",
                        "CurrentURI", "CurrentURIMetaData", "SleepTimerState", "AlarmState", "StreamRestartState",
                        "CurrentQueueTrackList"
                    },
                Name = CsActionBecomeGroupCoordinator,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the BecomeGroupCoordinatorAndSource action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="currentCoordinator">In value for the CurrentCoordinator action parameter.</param>
        /// <param name="currentGroupId">In value for the CurrentGroupID action parameter.</param>
        /// <param name="otherMembers">In value for the OtherMembers action parameter.</param>
        /// <param name="currentUri">In value for the CurrentURI action parameter.</param>
        /// <param name="currentUriMetaData">In value for the CurrentURIMetaData action parameter.</param>
        /// <param name="sleepTimerState">In value for the SleepTimerState action parameter.</param>
        /// <param name="alarmState">In value for the AlarmState action parameter.</param>
        /// <param name="streamRestartState">In value for the StreamRestartState action parameter.</param>
        /// <param name="currentAvtTrackList">In value for the CurrentAVTTrackList action parameter.</param>
        /// <param name="currentQueueTrackList">In value for the CurrentQueueTrackList action parameter.</param>
        /// <param name="currentSourceState">In value for the CurrentSourceState action parameter.</param>
        /// <param name="resumePlayback">In value for the ResumePlayback action parameter.</param>
        public async Task BecomeGroupCoordinatorAndSource(UInt32 instanceId, String currentCoordinator,
            String currentGroupId, String otherMembers, String currentUri, String currentUriMetaData,
            String sleepTimerState, String alarmState, String streamRestartState, String currentAvtTrackList,
            String currentQueueTrackList, String currentSourceState, Boolean resumePlayback)
        {
            var loIn = new object[13];

            loIn[0] = instanceId;
            loIn[1] = currentCoordinator;
            loIn[2] = currentGroupId;
            loIn[3] = otherMembers;
            loIn[4] = currentUri;
            loIn[5] = currentUriMetaData;
            loIn[6] = sleepTimerState;
            loIn[7] = alarmState;
            loIn[8] = streamRestartState;
            loIn[9] = currentAvtTrackList;
            loIn[10] = currentQueueTrackList;
            loIn[11] = currentSourceState;
            loIn[12] = resumePlayback;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "InstanceID", "CurrentCoordinator", "CurrentGroupID", "OtherMembers", "CurrentURI",
                        "CurrentURIMetaData", "SleepTimerState", "AlarmState", "StreamRestartState",
                        "CurrentAVTTrackList", "CurrentQueueTrackList", "CurrentSourceState", "ResumePlayback"
                    },
                Name = CsActionBecomeGroupCoordinatorAndSource,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ChangeCoordinator action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="currentCoordinator">In value for the CurrentCoordinator action parameter.</param>
        /// <param name="newCoordinator">In value for the NewCoordinator action parameter.</param>
        /// <param name="newTransportSettings">In value for the NewTransportSettings action parameter.</param>
        public async Task ChangeCoordinator(UInt32 instanceId, String currentCoordinator, String newCoordinator,
            String newTransportSettings)
        {
            var loIn = new object[4];

            loIn[0] = instanceId;
            loIn[1] = currentCoordinator;
            loIn[2] = newCoordinator;
            loIn[3] = newTransportSettings;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "CurrentCoordinator", "NewCoordinator", "NewTransportSettings"},
                Name = CsActionChangeCoordinator,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ChangeTransportSettings action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="newTransportSettings">In value for the NewTransportSettings action parameter.</param>
        /// <param name="currentAvTransportUri">In value for the CurrentAVTransportURI action parameter.</param>
        public async Task ChangeTransportSettings(UInt32 instanceId, String newTransportSettings,
            String currentAvTransportUri)
        {
            var loIn = new object[3];

            loIn[0] = instanceId;
            loIn[1] = newTransportSettings;
            loIn[2] = currentAvTransportUri;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "NewTransportSettings", "CurrentAVTransportURI"},
                Name = CsActionChangeTransportSettings,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ConfigureSleepTimer action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="newSleepTimerDuration">In value for the NewSleepTimerDuration action parameter.</param>
        public async Task ConfigureSleepTimer(UInt32 instanceId, String newSleepTimerDuration)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = newSleepTimerDuration;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "NewTransportSettings"},
                Name = CsActionConfigureSleepTimer,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetRemainingSleepTimerDuration action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetRemainingSleepTimerDuration(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetRemainingSleepTimerDuration,
                ExpectedReplyCount = 0
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the RunAlarm action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="alarmId">In value for the AlarmID action parameter.</param>
        /// <param name="loggedStartTime">In value for the LoggedStartTime action parameter.</param>
        /// <param name="duration">In value for the Duration action parameter.</param>
        /// <param name="programUri">In value for the ProgramURI action parameter.</param>
        /// <param name="programMetaData">In value for the ProgramMetaData action parameter.</param>
        /// <param name="playMode">In value for the PlayMode action parameter. Default value of NORMAL.</param>
        /// <param name="volume">In value for the Volume action parameter.</param>
        /// <param name="includeLinkedZones">In value for the IncludeLinkedZones action parameter.</param>
        public async Task RunAlarm(UInt32 instanceId, UInt32 alarmId, String loggedStartTime, String duration,
            String programUri, String programMetaData, CurrentPlayModeEnum playMode, UInt16 volume,
            Boolean includeLinkedZones)
        {
            var loIn = new object[9];

            loIn[0] = instanceId;
            loIn[1] = alarmId;
            loIn[2] = loggedStartTime;
            loIn[3] = duration;
            loIn[4] = programUri;
            loIn[5] = programMetaData;
            loIn[6] = AvTransport.ToStringCurrentPlayMode(playMode);
            loIn[7] = volume;
            loIn[8] = includeLinkedZones;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {
                        "InstanceID", "AlarmID", "LoggedStartTime", "Duration", "ProgramURI", "ProgramMetaData",
                        "PlayMode",
                        "Volume", "IncludeLinkedZones"
                    },
                Name = CsActionRunAlarm,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the StartAutoplay action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="programUri">In value for the ProgramURI action parameter.</param>
        /// <param name="programMetaData">In value for the ProgramMetaData action parameter.</param>
        /// <param name="volume">In value for the Volume action parameter.</param>
        /// <param name="includeLinkedZones">In value for the IncludeLinkedZones action parameter.</param>
        /// <param name="resetVolumeAfter">In value for the ResetVolumeAfter action parameter.</param>
        public async Task StartAutoplay(UInt32 instanceId, String programUri, String programMetaData, UInt16 volume,
            Boolean includeLinkedZones, Boolean resetVolumeAfter)
        {
            var loIn = new object[6];

            loIn[0] = instanceId;
            loIn[1] = programUri;
            loIn[2] = programMetaData;
            loIn[3] = volume;
            loIn[4] = includeLinkedZones;
            loIn[5] = resetVolumeAfter;
            var action = new SoapAction
            {
                ArgNames =
                    new[]
                    {"InstanceID", "ProgramURI", "ProgramMetaData", "Volume", "IncludeLinkedZones", "ResetVolumeAfter"},
                Name = CsActionStartAutoplay,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetRunningAlarmProperties action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        public async Task<ActionResult> GetRunningAlarmProperties(UInt32 instanceId)
        {
            var loIn = new object[1];

            loIn[0] = instanceId;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID"},
                Name = CsActionGetRunningAlarmProperties,
                ExpectedReplyCount = 3
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SnoozeAlarm action.
        /// </summary>
        /// <param name="instanceId">In value for the InstanceID action parameter.</param>
        /// <param name="duration">In value for the Duration action parameter.</param>
        public async Task SnoozeAlarm(UInt32 instanceId, String duration)
        {
            var loIn = new object[2];

            loIn[0] = instanceId;
            loIn[1] = duration;
            var action = new SoapAction
            {
                ArgNames = new[] {"InstanceID", "Duration"},
                Name = CsActionSnoozeAlarm,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}