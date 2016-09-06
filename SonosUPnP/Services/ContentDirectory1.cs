using System;
using System.Threading.Tasks;
using Sonos.Extensions;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the ContentDirectory1 (urn:schemas-upnp-org:service:ContentDirectory:1)
    ///     service.
    /// </summary>
    public class ContentDirectory1Service : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the SystemUpdateID state variable.
        /// </summary>
        protected const string CsStateVarSystemUpdateId = "SystemUpdateID";

        /// <summary>
        ///     The name for the ContainerUpdateIDs state variable.
        /// </summary>
        protected const string CsStateVarContainerUpdateIDs = "ContainerUpdateIDs";

        /// <summary>
        ///     The name for the ShareListRefreshState state variable.
        /// </summary>
        protected const string CsStateVarShareListRefreshState = "ShareListRefreshState";

        /// <summary>
        ///     The name for the ShareIndexInProgress state variable.
        /// </summary>
        protected const string CsStateVarShareIndexInProgress = "ShareIndexInProgress";

        /// <summary>
        ///     The name for the ShareIndexLastError state variable.
        /// </summary>
        protected const string CsStateVarShareIndexLastError = "ShareIndexLastError";

        /// <summary>
        ///     The name for the UserRadioUpdateID state variable.
        /// </summary>
        protected const string CsStateVarUserRadioUpdateId = "UserRadioUpdateID";

        /// <summary>
        ///     The name for the SavedQueuesUpdateID state variable.
        /// </summary>
        protected const string CsStateVarSavedQueuesUpdateId = "SavedQueuesUpdateID";

        /// <summary>
        ///     The name for the ShareListUpdateID state variable.
        /// </summary>
        protected const string CsStateVarShareListUpdateId = "ShareListUpdateID";

        /// <summary>
        ///     The name for the RecentlyPlayedUpdateID state variable.
        /// </summary>
        protected const string CsStateVarRecentlyPlayedUpdateId = "RecentlyPlayedUpdateID";

        /// <summary>
        ///     The name for the Browseable state variable.
        /// </summary>
        protected const string CsStateVarBrowseable = "Browseable";

        /// <summary>
        ///     The string value for the allowed value NOTRUN of the ShareListRefreshState state variable.
        /// </summary>
        protected const string CsAllowedValShareListRefreshStateNotrun = "NOTRUN";

        /// <summary>
        ///     The string value for the allowed value RUNNING of the ShareListRefreshState state variable.
        /// </summary>
        protected const string CsAllowedValShareListRefreshStateRunning = "RUNNING";

        /// <summary>
        ///     The string value for the allowed value DONE of the ShareListRefreshState state variable.
        /// </summary>
        protected const string CsAllowedValShareListRefreshStateDone = "DONE";

        /// <summary>
        ///     The name for the GetSearchCapabilities action.
        /// </summary>
        protected const string CsActionGetSearchCapabilities = "GetSearchCapabilities";

        /// <summary>
        ///     The name for the GetSortCapabilities action.
        /// </summary>
        protected const string CsActionGetSortCapabilities = "GetSortCapabilities";

        /// <summary>
        ///     The name for the GetSystemUpdateID action.
        /// </summary>
        protected const string CsActionGetSystemUpdateId = "GetSystemUpdateID";

        /// <summary>
        ///     The name for the GetAlbumArtistDisplayOption action.
        /// </summary>
        protected const string CsActionGetAlbumArtistDisplayOption = "GetAlbumArtistDisplayOption";

        /// <summary>
        ///     The name for the GetLastIndexChange action.
        /// </summary>
        protected const string CsActionGetLastIndexChange = "GetLastIndexChange";

        /// <summary>
        ///     The name for the Browse action.
        /// </summary>
        protected const string CsActionBrowse = "Browse";

        /// <summary>
        ///     The name for the FindPrefix action.
        /// </summary>
        protected const string CsActionFindPrefix = "FindPrefix";

        /// <summary>
        ///     The name for the GetAllPrefixLocations action.
        /// </summary>
        protected const string CsActionGetAllPrefixLocations = "GetAllPrefixLocations";

        /// <summary>
        ///     The name for the CreateObject action.
        /// </summary>
        protected const string CsActionCreateObject = "CreateObject";

        /// <summary>
        ///     The name for the UpdateObject action.
        /// </summary>
        protected const string CsActionUpdateObject = "UpdateObject";

        /// <summary>
        ///     The name for the DestroyObject action.
        /// </summary>
        protected const string CsActionDestroyObject = "DestroyObject";

        /// <summary>
        ///     The name for the RefreshShareList action.
        /// </summary>
        protected const string CsActionRefreshShareList = "RefreshShareList";

        /// <summary>
        ///     The name for the RefreshShareIndex action.
        /// </summary>
        protected const string CsActionRefreshShareIndex = "RefreshShareIndex";

        /// <summary>
        ///     The name for the RequestResort action.
        /// </summary>
        protected const string CsActionRequestResort = "RequestResort";

        /// <summary>
        ///     The name for the GetShareIndexInProgress action.
        /// </summary>
        protected const string CsActionGetShareIndexInProgress = "GetShareIndexInProgress";

        /// <summary>
        ///     The name for the GetBrowseable action.
        /// </summary>
        protected const string CsActionGetBrowseable = "GetBrowseable";

        /// <summary>
        ///     The name for the SetBrowseable action.
        /// </summary>
        protected const string CsActionSetBrowseable = "SetBrowseable";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the ContentDirectory1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the ContentDirectory1 service from.</param>
        public ContentDirectory1Service(Service service)
            : base(service)
        {
            //this.SubcribeToEvents();
        }

        #endregion

        #region Event Handlers

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

        /// <summary>
        ///     Occurs when the service notifies that the SystemUpdateID state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<UInt32> SystemUpdateIdChanged;


        /// <summary>
        ///     Occurs when the service notifies that the ContainerUpdateIDs state variable has changed its value.
        /// </summary>
        private event StateVariableChangedEventHandler<String> ContainerUpdateIDsChangedInternal;

        public event StateVariableChangedEventHandler<String> ContainerUpdateIDsChanged
        {
            add
            {
                EventSemaphore++;
                ContainerUpdateIDsChangedInternal += value;
            }
            remove
            {
                EventSemaphore--;
                ContainerUpdateIDsChangedInternal -= value;
            }
        }

        /// <summary>
        ///     Occurs when the service notifies that the ShareListRefreshState state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<ShareListRefreshStateEnum> ShareListRefreshStateChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ShareIndexInProgress state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<Boolean> ShareIndexInProgressChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ShareIndexLastError state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> ShareIndexLastErrorChanged;

        /// <summary>
        ///     Occurs when the service notifies that the UserRadioUpdateID state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> UserRadioUpdateIdChanged;

        /// <summary>
        ///     Occurs when the service notifies that the SavedQueuesUpdateID state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> SavedQueuesUpdateIdChanged;

        /// <summary>
        ///     Occurs when the service notifies that the ShareListUpdateID state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> ShareListUpdateIdChanged;

        /// <summary>
        ///     Occurs when the service notifies that the RecentlyPlayedUpdateID state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<String> RecentlyPlayedUpdateIdChanged;

        /// <summary>
        ///     Occurs when the service notifies that the Browseable state variable has changed its value.
        /// </summary>
        public event StateVariableChangedEventHandler<Boolean> BrowseableChanged;

        #endregion

        #region Event Callers

        /// <summary>
        ///     Raises the SystemUpdateIDChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnSystemUpdateIdChanged(StateVariableChangedEventArgs e)
        {
            if (SystemUpdateIdChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ContainerUpdateIDsChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnContainerUpdateIDsChanged(StateVariableChangedEventArgs e)
        {
            if (ContainerUpdateIDsChangedInternal != null)
            {
                string id = e.StateVarValue.Value;
                //var length = id.IndexOf(",");
                //id = id.Substring(0, length);
                ContainerUpdateIDsChangedInternal(this, id);
            }
        }

        /// <summary>
        ///     Raises the ShareListRefreshStateChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnShareListRefreshStateChanged(StateVariableChangedEventArgs e)
        {
            if (ShareListRefreshStateChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ShareIndexInProgressChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnShareIndexInProgressChanged(StateVariableChangedEventArgs e)
        {
            if (ShareIndexInProgressChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ShareIndexLastErrorChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnShareIndexLastErrorChanged(StateVariableChangedEventArgs e)
        {
            if (ShareIndexLastErrorChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the UserRadioUpdateIDChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnUserRadioUpdateIdChanged(StateVariableChangedEventArgs e)
        {
            if (UserRadioUpdateIdChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the SavedQueuesUpdateIDChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnSavedQueuesUpdateIdChanged(StateVariableChangedEventArgs e)
        {
            if (SavedQueuesUpdateIdChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the ShareListUpdateIDChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnShareListUpdateIdChanged(StateVariableChangedEventArgs e)
        {
            if (ShareListUpdateIdChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the RecentlyPlayedUpdateIDChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnRecentlyPlayedUpdateIdChanged(StateVariableChangedEventArgs e)
        {
            if (RecentlyPlayedUpdateIdChanged != null)
                throw new NotImplementedException();
        }

        /// <summary>
        ///     Raises the BrowseableChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnBrowseableChanged(StateVariableChangedEventArgs e)
        {
            if (BrowseableChanged != null)
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
                case CsStateVarSystemUpdateId:
                    // Raise the event for the SystemUpdateID state variable
                    OnSystemUpdateIdChanged(e);
                    break;

                case CsStateVarContainerUpdateIDs:
                    // Raise the event for the ContainerUpdateIDs state variable
                    OnContainerUpdateIDsChanged(e);
                    break;

                case CsStateVarShareListRefreshState:
                    // Raise the event for the ShareListRefreshState state variable
                    OnShareListRefreshStateChanged(e);
                    break;

                case CsStateVarShareIndexInProgress:
                    // Raise the event for the ShareIndexInProgress state variable
                    OnShareIndexInProgressChanged(e);
                    break;

                case CsStateVarShareIndexLastError:
                    // Raise the event for the ShareIndexLastError state variable
                    OnShareIndexLastErrorChanged(e);
                    break;

                case CsStateVarUserRadioUpdateId:
                    // Raise the event for the UserRadioUpdateID state variable
                    OnUserRadioUpdateIdChanged(e);
                    break;

                case CsStateVarSavedQueuesUpdateId:
                    // Raise the event for the SavedQueuesUpdateID state variable
                    OnSavedQueuesUpdateIdChanged(e);
                    break;

                case CsStateVarShareListUpdateId:
                    // Raise the event for the ShareListUpdateID state variable
                    OnShareListUpdateIdChanged(e);
                    break;

                case CsStateVarRecentlyPlayedUpdateId:
                    // Raise the event for the RecentlyPlayedUpdateID state variable
                    OnRecentlyPlayedUpdateIdChanged(e);
                    break;

                case CsStateVarBrowseable:
                    // Raise the event for the Browseable state variable
                    OnBrowseableChanged(e);
                    break;
            }


            base.OnStateVariableChanged(e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the GetSearchCapabilities action.
        /// </summary>
        /// <returns>Out value for the SearchCaps action parameter.</returns>
        public async Task<ActionResult> GetSearchCapabilities()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetSearchCapabilities,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetSortCapabilities action.
        /// </summary>
        /// <returns>Out value for the SortCaps action parameter.</returns>
        public async Task<ActionResult> GetSortCapabilities()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetSortCapabilities,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetSystemUpdateID action.
        /// </summary>
        /// <returns>Out value for the Id action parameter.</returns>
        public async Task<ActionResult> GetSystemUpdateId()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetSystemUpdateId,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetAlbumArtistDisplayOption action.
        /// </summary>
        /// <returns>Out value for the AlbumArtistDisplayOption action parameter.</returns>
        public async Task<ActionResult> GetAlbumArtistDisplayOption()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetAlbumArtistDisplayOption,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetLastIndexChange action.
        /// </summary>
        /// <returns>Out value for the LastIndexChange action parameter.</returns>
        public async Task<ActionResult> GetLastIndexChange()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetLastIndexChange,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the Browse action.
        /// </summary>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        /// <param name="browseFlag">In value for the BrowseFlag action parameter.</param>
        /// <param name="filter">In value for the Filter action parameter.</param>
        /// <param name="startingIndex">In value for the StartingIndex action parameter.</param>
        /// <param name="requestedCount">In value for the RequestedCount action parameter.</param>
        /// <param name="sortCriteria">In value for the SortCriteria action parameter.</param>
        public async Task<ActionResult> Browse(String objectId, BrowseFlag browseFlag, String filter,
            UInt32 startingIndex, UInt32 requestedCount, String sortCriteria)
        {
            var loIn = new object[6];

            loIn[0] = objectId;
            loIn[1] = ContendDirectoryExtensions.ToStringAargtypeBrowseFlag(browseFlag);
            loIn[2] = filter;
            loIn[3] = startingIndex;
            loIn[4] = requestedCount;
            loIn[5] = sortCriteria;
            var action = new SoapAction
            {
                ArgNames = new[] {"ObjectID", "BrowseFlag", "Filter", "StartingIndex", "RequestedCount", "SortCriteria"},
                Name = CsActionBrowse,
                ExpectedReplyCount = 4
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

#if false
    /// <summary>
    /// Executes the Browse action.
    /// </summary>
    /// <param name="objectID">In value for the ObjectID action parameter.</param>
    /// <param name="browseFlag">In value for the BrowseFlag action parameter.</param>
    /// <param name="filter">In value for the Filter action parameter.</param>
    /// <param name="startingIndex">In value for the StartingIndex action parameter.</param>
    /// <param name="requestedCount">In value for the RequestedCount action parameter.</param>
    /// <param name="sortCriteria">In value for the SortCriteria action parameter.</param>
    /// <param name="result">Out value for the Result action parameter.</param>
    /// <param name="numberReturned">Out value for the NumberReturned action parameter.</param>
    /// <param name="totalMatches">Out value for the TotalMatches action parameter.</param>
    /// <param name="updateID">Out value for the UpdateID action parameter.</param>
        public async Task<BrowseResult<T>> Browse<T>(String objectID, BrowseFlag browseFlag, String filter, UInt32 startingIndex, UInt32 requestedCount, String sortCriteria)
        {
            object[] loIn = new object[6];

            loIn[0] = objectID;
            loIn[1] = Extensions.ContendDirectoryExtensions.ToStringAARGTYPEBrowseFlag(browseFlag);
            loIn[2] = filter;
            loIn[3] = startingIndex;
            loIn[4] = requestedCount;
            loIn[5] = sortCriteria;
            var action = new SOAPAction()
            {
                ArgNames = new string[] { "ObjectID", "BrowseFlag", "Filter", "StartingIndex", "RequestedCount", "SortCriteria" },
                Name = csAction_Browse,
                ExpectedReplyCount = 4
            };
            var result = await InvokeActionAsync(action, loIn);

            if (result.Error == null)
            {
                var bResult = new BrowseResult<T>(result.Data);

                // TEMP
                if (typeof(T).Equals(typeof(QueueItem)))
                {
                    foreach (var item in bResult.Result)
                    {
                        QueueItem q = (QueueItem)(object)item;
                        q.AlbumArtUri = base.Description.BaseAddress.Scheme + "://" +
                                        base.Description.BaseAddress.Host + ":" +
                                        base.Description.BaseAddress.Port + q.AlbumArtUri;
                    }
                }

                return bResult;
            }

            return new BrowseResult<T>();
        } 
#endif

        /// <summary>
        ///     Executes the FindPrefix action.
        /// </summary>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        /// <param name="prefix">In value for the Prefix action parameter.</param>
        public async Task<ActionResult> FindPrefix(String objectId, String prefix)
        {
            var loIn = new object[2];

            loIn[0] = objectId;
            loIn[1] = prefix;

            var action = new SoapAction
            {
                ArgNames = new[] {"ObjectID", "Prefix"},
                Name = CsActionFindPrefix,
                ExpectedReplyCount = 2
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetAllPrefixLocations action.
        /// </summary>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        public async Task<ActionResult> GetAllPrefixLocations(String objectId)
        {
            var loIn = new object[1];

            loIn[0] = objectId;

            var action = new SoapAction
            {
                ArgNames = new[] {"ObjectID"},
                Name = CsActionGetAllPrefixLocations,
                ExpectedReplyCount = 3
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the CreateObject action.
        /// </summary>
        /// <param name="containerId">In value for the ContainerID action parameter.</param>
        /// <param name="elements">In value for the Elements action parameter.</param>
        public async Task<ActionResult> CreateObject(String containerId, String elements)
        {
            var loIn = new object[2];

            loIn[0] = containerId;
            loIn[1] = elements;

            var action = new SoapAction
            {
                ArgNames = new[] {"ObjectID", "Elements"},
                Name = CsActionCreateObject,
                ExpectedReplyCount = 2
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the UpdateObject action.
        /// </summary>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        /// <param name="currentTagValue">In value for the CurrentTagValue action parameter.</param>
        /// <param name="newTagValue">In value for the NewTagValue action parameter.</param>
        public async Task UpdateObject(String objectId, String currentTagValue, String newTagValue)
        {
            var loIn = new object[3];

            loIn[0] = objectId;
            loIn[1] = currentTagValue;
            loIn[2] = newTagValue;
            var action = new SoapAction
            {
                ArgNames = new[] {"ObjectID", "CurrentTagValue", "NewTagValue"},
                Name = CsActionUpdateObject,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the DestroyObject action.
        /// </summary>
        /// <param name="objectId">In value for the ObjectID action parameter.</param>
        public async Task DestroyObject(String objectId)
        {
            var loIn = new object[1];

            loIn[0] = objectId;

            var action = new SoapAction
            {
                ArgNames = new[] {"ObjectID"},
                Name = CsActionDestroyObject,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RefreshShareList action.
        /// </summary>
        public async Task RefreshShareList()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionRefreshShareList,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RefreshShareIndex action.
        /// </summary>
        /// <param name="albumArtistDisplayOption">In value for the AlbumArtistDisplayOption action parameter.</param>
        public async Task RefreshShareIndex(String albumArtistDisplayOption)
        {
            var loIn = new object[1];

            loIn[0] = albumArtistDisplayOption;
            var action = new SoapAction
            {
                ArgNames = new[] {"AlbumArtistDisplayOption"},
                Name = CsActionRefreshShareIndex,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RequestResort action.
        /// </summary>
        /// <param name="sortOrder">In value for the SortOrder action parameter.</param>
        public async Task RequestResort(String sortOrder)
        {
            var loIn = new object[1];

            loIn[0] = sortOrder;

            var action = new SoapAction
            {
                ArgNames = new[] {"SortOrder"},
                Name = CsActionRequestResort,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetShareIndexInProgress action.
        /// </summary>
        /// <returns>Out value for the IsIndexing action parameter.</returns>
        public async Task<ActionResult> GetShareIndexInProgress()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new[] {"IsIndexing"},
                Name = CsActionGetShareIndexInProgress,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetBrowseable action.
        /// </summary>
        /// <returns>Out value for the IsBrowseable action parameter.</returns>
        public async Task<ActionResult> GetBrowseable()
        {
            var loIn = new object[0];

            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionGetBrowseable,
                ExpectedReplyCount = 1
            };
            var result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the SetBrowseable action.
        /// </summary>
        /// <param name="browseable">In value for the Browseable action parameter.</param>
        public async Task SetBrowseable(Boolean browseable)
        {
            var loIn = new object[1];

            loIn[0] = browseable;

            var action = new SoapAction
            {
                ArgNames = new[] {"Browseable"},
                Name = CsActionSetBrowseable,
                ExpectedReplyCount = 1
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}