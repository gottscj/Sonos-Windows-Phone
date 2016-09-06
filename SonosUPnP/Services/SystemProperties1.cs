using System;
using System.Threading.Tasks;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    /// <summary>
    ///     Encapsulates a specific Service class for the SystemProperties1 (urn:schemas-upnp-org:service:SystemProperties:1)
    ///     service.
    /// </summary>
    public class SystemProperties1 : Service
    {
        #region Protected Constants

        /// <summary>
        ///     The name for the SetString action.
        /// </summary>
        protected const string CsActionSetString = "SetString";

        /// <summary>
        ///     The name for the SetStringX action.
        /// </summary>
        protected const string CsActionSetStringX = "SetStringX";

        /// <summary>
        ///     The name for the GetString action.
        /// </summary>
        protected const string CsActionGetString = "GetString";

        /// <summary>
        ///     The name for the GetStringX action.
        /// </summary>
        protected const string CsActionGetStringX = "GetStringX";

        /// <summary>
        ///     The name for the Remove action.
        /// </summary>
        protected const string CsActionRemove = "Remove";

        /// <summary>
        ///     The name for the GetWebCode action.
        /// </summary>
        protected const string CsActionGetWebCode = "GetWebCode";

        /// <summary>
        ///     The name for the ProvisionTrialAccount action.
        /// </summary>
        protected const string CsActionProvisionTrialAccount = "ProvisionTrialAccount";

        /// <summary>
        ///     The name for the ProvisionCredentialedTrialAccountX action.
        /// </summary>
        protected const string CsActionProvisionCredentialedTrialAccountX = "ProvisionCredentialedTrialAccountX";

        /// <summary>
        ///     The name for the MigrateTrialAccountX action.
        /// </summary>
        protected const string CsActionMigrateTrialAccountX = "MigrateTrialAccountX";

        /// <summary>
        ///     The name for the AddAccountX action.
        /// </summary>
        protected const string CsActionAddAccountX = "AddAccountX";

        /// <summary>
        ///     The name for the AddAccountWithCredentialsX action.
        /// </summary>
        protected const string CsActionAddAccountWithCredentialsX = "AddAccountWithCredentialsX";

        /// <summary>
        ///     The name for the RemoveAccount action.
        /// </summary>
        protected const string CsActionRemoveAccount = "RemoveAccount";

        /// <summary>
        ///     The name for the EditAccountPasswordX action.
        /// </summary>
        protected const string CsActionEditAccountPasswordX = "EditAccountPasswordX";

        /// <summary>
        ///     The name for the EditAccountMd action.
        /// </summary>
        protected const string CsActionEditAccountMd = "EditAccountMd";

        /// <summary>
        ///     The name for the DoPostUpdateTasks action.
        /// </summary>
        protected const string CsActionDoPostUpdateTasks = "DoPostUpdateTasks";

        /// <summary>
        ///     The name for the ResetThirdPartyCredentials action.
        /// </summary>
        protected const string CsActionResetThirdPartyCredentials = "ResetThirdPartyCredentials";

        #endregion

        #region Initialisation

        /// <summary>
        ///     Creates a new instance of the SystemProperties1 service from a base service.
        /// </summary>
        /// <param name="service">The base service to create the SystemProperties1 service from.</param>
        public SystemProperties1(Service service)
            : base(service)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Executes the SetString action.
        /// </summary>
        /// <param name="variableName">In value for the VariableName action parameter.</param>
        /// <param name="stringValue">In value for the StringValue action parameter.</param>
        public async Task SetString(String variableName, String stringValue)
        {
            var loIn = new object[2];

            loIn[0] = variableName;
            loIn[1] = stringValue;
            var action = new SoapAction
            {
                ArgNames = new[] {"VariableName", "StringValue"},
                Name = CsActionSetString,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the SetStringX action.
        /// </summary>
        /// <param name="variableName">In value for the VariableName action parameter.</param>
        /// <param name="stringValue">In value for the StringValue action parameter.</param>
        public async Task SetStringX(String variableName, String stringValue)
        {
            var loIn = new object[2];

            loIn[0] = variableName;
            loIn[1] = stringValue;
            var action = new SoapAction
            {
                ArgNames = new[] {"VariableName", "StringValue"},
                Name = CsActionSetStringX,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetString action.
        /// </summary>
        /// <param name="variableName">In value for the VariableName action parameter.</param>
        /// <returns>Out value for the StringValue action parameter.</returns>
        public async Task<ActionResult> GetString(String variableName)
        {
            var loIn = new object[1];

            loIn[0] = variableName;

            var action = new SoapAction
            {
                ArgNames = new[] {"VariableName"},
                Name = CsActionGetString,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the GetStringX action.
        /// </summary>
        /// <param name="variableName">In value for the VariableName action parameter.</param>
        /// <returns>Out value for the StringValue action parameter.</returns>
        public async Task<ActionResult> GetStringX(String variableName)
        {
            var loIn = new object[1];

            loIn[0] = variableName;
            var action = new SoapAction
            {
                ArgNames = new[] {"VariableName"},
                Name = CsActionGetStringX,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);
            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the Remove action.
        /// </summary>
        /// <param name="variableName">In value for the VariableName action parameter.</param>
        public async Task Remove(String variableName)
        {
            var loIn = new object[1];

            loIn[0] = variableName;

            var action = new SoapAction
            {
                ArgNames = new[] {"VariableName"},
                Name = CsActionRemove,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the GetWebCode action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <returns>Out value for the WebCode action parameter.</returns>
        public async Task<ActionResult> GetWebCode(UInt32 accountType)
        {
            var loIn = new object[1];

            loIn[0] = accountType;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType"},
                Name = CsActionGetWebCode,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);
            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the ProvisionTrialAccount action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        public async Task ProvisionTrialAccount(UInt32 accountType)
        {
            var loIn = new object[1];

            loIn[0] = accountType;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType"},
                Name = CsActionProvisionTrialAccount,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ProvisionCredentialedTrialAccountX action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <param name="accountId">In value for the AccountID action parameter.</param>
        /// <param name="accountPassword">In value for the AccountPassword action parameter.</param>
        /// <returns>Out value for the IsExpired action parameter.</returns>
        public async Task<ActionResult> ProvisionCredentialedTrialAccountX(UInt32 accountType, String accountId,
            String accountPassword)
        {
            var loIn = new object[3];

            loIn[0] = accountType;
            loIn[1] = accountId;
            loIn[2] = accountPassword;
            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType", "AccountID", "AccountPassword"},
                Name = CsActionProvisionCredentialedTrialAccountX,
                ExpectedReplyCount = 1
            };
            SoapActionResult result = await InvokeActionAsync(action, loIn);

            // TODO: check for execption
            return new ActionResult(result.XElement);
        }

        /// <summary>
        ///     Executes the MigrateTrialAccountX action.
        /// </summary>
        /// <param name="targetAccountType">In value for the TargetAccountType action parameter.</param>
        /// <param name="targetAccountId">In value for the TargetAccountID action parameter.</param>
        /// <param name="targetAccountPassword">In value for the TargetAccountPassword action parameter.</param>
        public async Task MigrateTrialAccountX(UInt32 targetAccountType, String targetAccountId,
            String targetAccountPassword)
        {
            var loIn = new object[3];

            loIn[0] = targetAccountType;
            loIn[1] = targetAccountId;
            loIn[2] = targetAccountPassword;

            var action = new SoapAction
            {
                ArgNames = new[] {"TargetAccountType", "TargetAccountID", "TargetAccountPassword"},
                Name = CsActionMigrateTrialAccountX,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the AddAccountX action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <param name="accountId">In value for the AccountID action parameter.</param>
        /// <param name="accountPassword">In value for the AccountPassword action parameter.</param>
        public async Task AddAccountX(UInt32 accountType, String accountId, String accountPassword)
        {
            var loIn = new object[3];

            loIn[0] = accountType;
            loIn[1] = accountId;
            loIn[2] = accountPassword;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType", "AccountID", "AccountPassword"},
                Name = CsActionAddAccountX,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the AddAccountWithCredentialsX action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <param name="accountToken">In value for the AccountToken action parameter.</param>
        /// <param name="accountKey">In value for the AccountKey action parameter.</param>
        public async Task AddAccountWithCredentialsX(UInt32 accountType, String accountToken, String accountKey)
        {
            var loIn = new object[3];

            loIn[0] = accountType;
            loIn[1] = accountToken;
            loIn[2] = accountKey;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType", "AccountToken", "AccountKey"},
                Name = CsActionAddAccountWithCredentialsX,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the RemoveAccount action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <param name="accountId">In value for the AccountID action parameter.</param>
        public async Task RemoveAccount(UInt32 accountType, String accountId)
        {
            var loIn = new object[2];

            loIn[0] = accountType;
            loIn[1] = accountId;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType", "AccountID"},
                Name = CsActionRemoveAccount,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the EditAccountPasswordX action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <param name="accountId">In value for the AccountID action parameter.</param>
        /// <param name="newAccountPassword">In value for the NewAccountPassword action parameter.</param>
        public async Task EditAccountPasswordX(UInt32 accountType, String accountId, String newAccountPassword)
        {
            var loIn = new object[3];

            loIn[0] = accountType;
            loIn[1] = accountId;
            loIn[2] = newAccountPassword;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType", "AccountID", "NewAccountPassword"},
                Name = CsActionEditAccountPasswordX,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the EditAccountMd action.
        /// </summary>
        /// <param name="accountType">In value for the AccountType action parameter.</param>
        /// <param name="accountId">In value for the AccountID action parameter.</param>
        /// <param name="newAccountMd">In value for the NewAccountMd action parameter.</param>
        public async Task EditAccountMd(UInt32 accountType, String accountId, String newAccountMd)
        {
            var loIn = new object[3];

            loIn[0] = accountType;
            loIn[1] = accountId;
            loIn[2] = newAccountMd;

            var action = new SoapAction
            {
                ArgNames = new[] {"AccountType", "AccountID", "NewAccountMd"},
                Name = CsActionEditAccountMd,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the DoPostUpdateTasks action.
        /// </summary>
        public async Task DoPostUpdateTasks()
        {
            var loIn = new object[0];


            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionDoPostUpdateTasks,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        /// <summary>
        ///     Executes the ResetThirdPartyCredentials action.
        /// </summary>
        public async Task ResetThirdPartyCredentials()
        {
            var loIn = new object[0];


            var action = new SoapAction
            {
                ArgNames = new string[0],
                Name = CsActionResetThirdPartyCredentials,
                ExpectedReplyCount = 0
            };
            await InvokeActionAsync(action, loIn);
        }

        #endregion
    }
}