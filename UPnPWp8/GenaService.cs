using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Microsoft.Phone.Net.NetworkInformation;
using UPnP.Models;

namespace UPnP
{
    public sealed class GenaService
    {
        private static GenaService _instance;
        private static readonly object SharedLock = new object();

        private const string Ns = "urn:schemas-upnp-org:event-1-0";

        private readonly Dictionary<string, SubscribtionHandler> _services;

        private string _ip = string.Empty;
        private StreamSocketListener _listener;

        public GenaService()
        {
            Port = new Random(GetHashCode()).Next(10000, 20000);
            _services = new Dictionary<string, SubscribtionHandler>();
        }

        public static GenaService Default
        {
            get { return _instance ?? (_instance = new GenaService()); }
        }

        public bool IsRunning
        {
            get { return (_listener != null); }
        }

        public string Ip
        {
            get { return _ip; }
        }

        public int Port { get; private set; }

        public string Information
        {
            get
            {
                return _listener != null ? _listener.Information.ToString() : "";
            }
        }

        public async Task Start()
        {
            if (_listener == null)
            {
                _ip = FindIpAddress();
                _listener = new StreamSocketListener();
                _listener.ConnectionReceived += _listener_ConnectionReceived;

                await _listener.BindServiceNameAsync(Port.ToString(CultureInfo.InvariantCulture));
            }
        }


        public void Unsubscribe(Service service)
        {
            if (!IsRunning) return;

            lock (SharedLock)
            {
                if (_services.ContainsKey(service.Name))
                    _services.Remove(service.Name);
            }
            var target = new Uri(service.EventSubUrl);

            var request = (HttpWebRequest) WebRequest.Create(target);
            request.SetNetworkPreference(NetworkSelectionCharacteristics.NonCellular);

            request.Method = "UNSUBSCRIBE";
            request.Headers["SID"] = service.EventUuid;
            request.Headers["HOST"] = Ip + ":" + Port;
            InvokeRequestAsync(request, service)
                .Wait();

            if (_services.Count == 0)
            {
                Close();
            }
        }

        public async Task Subscribe(Service service,
            Action<StateVariableChangedEventArgs> stateVariableChangedCallback)
        {
            if (!IsRunning)
            {
                await Start();
            }

            bool containsKey;
            lock (SharedLock)
            {
                containsKey = _services.ContainsKey(service.Name);
                _services[service.Name] = new SubscribtionHandler(service, stateVariableChangedCallback);
            }

            if (containsKey) return;

            var target = new Uri(service.EventSubUrl);

            var request = (HttpWebRequest) WebRequest.Create(target);
            request.SetNetworkPreference(NetworkSelectionCharacteristics.NonCellular);

            request.Method = "SUBSCRIBE";
            request.Headers["CALLBACK"] = "<http://" + Default.Ip + ":" + Port + "/" + service.Name + ">";
            request.Headers["NT"] = "upnp:event";
            request.Headers["TIMEOUT"] = "Second-3600";

            await InvokeRequestAsync(request, service);
        }

        public void Close()
        {
            lock (SharedLock)
            {
                _listener.Dispose();
                _listener = null;
            }
        }
        private static string FindIpAddress()
        {
            var ipAddresses = new List<string>();
            var hostnames = NetworkInformation.GetHostNames();
            foreach (var hn in hostnames)
            {
                //IanaInterfaceType == 71 => Wifi
                //IanaInterfaceType == 6 => Ethernet (Emulator)
                if (hn.IPInformation != null &&
                    (hn.IPInformation.NetworkAdapter.IanaInterfaceType == 71
                    || hn.IPInformation.NetworkAdapter.IanaInterfaceType == 6))
                {
                    string ipAddress = hn.DisplayName;
                    ipAddresses.Add(ipAddress);
                }
            }

            if (ipAddresses.Count < 1)
            {
                return null;
            }
            if (ipAddresses.Count == 1)
            {
                return ipAddresses[0];
            }
            //if multiple suitable address were found use the last one
            //(regularly the external interface of an emulated device)
            return ipAddresses[ipAddresses.Count - 1];
        }
        private static async Task InvokeRequestAsync(HttpWebRequest request, Service service)
        {
            WebResponse response = null;
            try
            {
                response = await request.GetResponseAsync().ConfigureAwait(false);
                if (((HttpWebResponse) response).StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("GENA RESPONSE NOT OK: " + ((HttpWebResponse) response).StatusCode);
                }

                var uuid = response.Headers["SID"];
                if (uuid != null)
                {
                    service.SetEventUuid(uuid);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR " + service.Name + "\r\n" + ex.Message);
            }
            finally
            {
                if (response != null)
                    response.Dispose();
            }
        }

        private void _listener_ConnectionReceived(StreamSocketListener sender,
            StreamSocketListenerConnectionReceivedEventArgs args)
        {
            var httpMessage = new HttpMessage(args.Socket.InputStream.AsStreamForRead());
            Debug.WriteLine("GENA MESSAGE: " + DateTime.Now);
            Debug.WriteLine(httpMessage.ToString());
            args.Socket.Dispose();

            SubscribtionHandler handler;
            lock (_services)
            {
                if (!_services.TryGetValue(httpMessage.ServiceName, out handler)) return;
            }

            if (handler.Callback == null) return;

            var xml = XElement.Parse(httpMessage.Body);
            var query = from properties in xml.Descendants(XName.Get("property", Ns))
                let p = properties.Descendants().FirstOrDefault()
                where p != null
                select p;

            foreach (var property in query)
            {
                handler.Callback
                    .Invoke(new StateVariableChangedEventArgs(property.Name.LocalName, property));
            }
        }

        internal class SubscribtionHandler
        {
            public SubscribtionHandler(Service service, Action<StateVariableChangedEventArgs> callback)
            {
                Service = service;
                Callback = callback;
            }

            public Service Service { get; set; }
            public Action<StateVariableChangedEventArgs> Callback { get; set; }
        }
    }
}