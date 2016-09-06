using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sonos.Models;
using UPnP;
using UPnP.Models;

namespace Sonos.Services
{
    public class MusicApiService : Service
    {
        private readonly string _deviceId;
        private readonly ZoneService _zoneService;
        private readonly ISonosMusicApi _sonosMusicApi;
        private bool? _credentialsOk;

        public MusicApiService(ISonosMusicApi sonosMusicApi, 
            string deviceId, 
            Service service, 
            MusicServiceContainer myServiceContainer, ZoneService zoneService)
            : base(service)
        {
            _sonosMusicApi = sonosMusicApi;
            _deviceId = deviceId;
            _zoneService = zoneService;
            MusicServiceContainer = myServiceContainer;
        }

        private MusicServiceContainer MusicServiceContainer { get; set; }

        public async Task<ActionResult> GetMetadata(string id, int index, int count)
        {
            var loIn = new object[3];
            loIn[0] = id;
            loIn[1] = index;
            loIn[2] = count;

            var action = new SoapAction
            {
                ArgNames = new[] {"id", "index", "count"},
                Name = "getMetadata",
                ExpectedReplyCount = count
            };
            string header = await GetMessageHeader();

            SoapActionResult soapActionResult = await InvokeActionAsync(action, loIn, header, false);


            // TODO: check for execption
            return new ActionResult(soapActionResult.XElement);
        }

        public async Task<DeviceLink> GetDeviceLinkCode(string householdId)
        {
            var loIn = new object[1];
            loIn[0] = householdId;

            var action = new SoapAction
            {
                ArgNames = new[] {"householdId"},
                Name = "getDeviceLinkCode",
                ExpectedReplyCount = 3
            };
            string header = "<s:Header>" +
                            "<credentials xmlns=\"{0}\">" +
                            "<deviceId>{2}</deviceId>" +
                            "<deviceProvider>Sonos</deviceProvider>" +
                            "</credentials>" +
                            "</s:Header>";
            header = string.Format(header, ServiceType, "", _deviceId);

            SoapActionResult result = await InvokeActionAsync(action, loIn, header, false);
            var actionResult = new ActionResult(result.XElement);
            if (actionResult.Exception != null)
            {
                Debug.WriteLine("GetDeviceLinkCode Exception: " + actionResult.Exception.Message);
                return null;
            }
            var deviceLink = new DeviceLink(actionResult.Result);
            return deviceLink;
        }

        public async Task<DeviceAuthToken> GetDeviceAuthToken(string householdId, string linkCode)
        {
            var loIn = new object[2];
            loIn[0] = householdId;
            loIn[1] = linkCode;

            var action = new SoapAction
            {
                ArgNames = new[] {"householdId", "linkCode"},
                Name = "getDeviceAuthToken",
                ExpectedReplyCount = 2
            };
            string header = "<s:Header>" +
                            "<credentials xmlns=\"{0}\">" +
                            "<sessionId>{1}</sessionId>" +
                            "<deviceId>{2}</deviceId>" +
                            "<deviceProvider>Sonos</deviceProvider>" +
                            "</credentials>" +
                            "</s:Header>";
            header = string.Format(header, ServiceType, "", _deviceId);
            //var te = await GetMessageHeader();
            SoapActionResult result = await InvokeActionAsync(action, loIn, header, false);

            Exception ex = null;
            if (result.Error != null) ex = result.Error.Exception;

            var actionResult = new ActionResult(result.XElement, ex);
            if (actionResult.Exception != null)
            {
                return null;
            }
            var deviceAuthToken = new DeviceAuthToken(actionResult.Result);
            _credentialsOk = null;
            _sonosMusicApi.SetDeviceAuthToken(MusicServiceContainer.Title, deviceAuthToken);
            return deviceAuthToken;
        }

        public async Task<bool> CheckCredentials()
        {
            if (_credentialsOk != null) return _credentialsOk.Value;

            if (MusicServiceContainer.Auth != Auth.DeviceLink &&
                MusicServiceContainer.Auth != Auth.UserId)
            {
                _credentialsOk = true;
                return _credentialsOk.Value;
            }

            string header = await GetMessageHeader();
            _credentialsOk = header != "";

            return _credentialsOk.Value;
        }

        private async Task<string> GetMessageHeader()
        {
            string header;

            switch (MusicServiceContainer.Auth)
            {
                case Auth.DeviceLink:
                    header = await GetAccessTokenHeader();
                    break;
                case Auth.UserId:
                    header = await GetSessionIdHeader();
                    break;
                    //case Auth.Anonymous:
                    //    break;
                    //case Auth.Unknown:
                    //    break;
                default:
                    // try empty header
                    header = "";
                    break;
            }

            return header;
        }

        private async Task<string> GetAccessTokenHeader()
        {
            var header = "<s:Header>" +
                            "<credentials xmlns=\"{0}\">" +
                            "<deviceId>{1}</deviceId>" +
                            "<loginToken>" +
                            "<token>{2}</token>" +
                            "<key>{3}</key>" +
                            "<householdId>{4}</householdId>" +
                            "</loginToken>" +
                            "</credentials>" +
                            "</s:Header>";

            var householdId = await _zoneService.GetHouseholdId();
            var authToken = _sonosMusicApi.GetDeviceAuthToken(MusicServiceContainer.Title);
            if (authToken == null) return "";
            header = string.Format(header, ServiceType, _deviceId, authToken.Token, authToken.Key, householdId);
            return header;
        }

        private async Task<string> GetSessionIdHeader()
        {
            var header = "<s:Header>" +
                            "<credentials xmlns=\"{0}\">" +
                            "<sessionId>{1}</sessionId>" +
                            "<deviceId>{2}</deviceId>" +
                            "<deviceProvider>Sonos</deviceProvider>" +
                            "</credentials>" +
                            "</s:Header>";
            var accounts = await _sonosMusicApi.GetActiveServiceAccounts();
            var account = accounts.FirstOrDefault(x => x.ServiceId == MusicServiceContainer.ServiceId);
            if (account == null) return "";

            var username = account.UserName;
            short id;
            if (!short.TryParse(MusicServiceContainer.Id, out id))
            {
                Debug.WriteLine("Unable to parse music service id");
                return "";
            }

            var sessionId = await _zoneService.GetSessionId(id, username);
            if (sessionId == "") return "";

            header = string.Format(header, ServiceType, sessionId, _deviceId);

            return header;
        }
    }
}