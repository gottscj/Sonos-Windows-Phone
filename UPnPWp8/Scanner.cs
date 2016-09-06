using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UPnP.Models;

namespace UPnP
{
    public class Scanner
    {
        public async Task<List<Device>> FindDevices(string whatToFind, int seconds)
        {
            var results = await Discover(whatToFind, seconds).ConfigureAwait(false);

            var devices = new List<Device>();

            foreach (var result in results)
            {
                devices.Add(await Device.CreateAsync(result).ConfigureAwait(false));
            }
            return devices;
        }

        private Task<List<string>> Discover(string whatToFind, int seconds)
        {
            return Task.Factory.StartNew(() =>
            {
                if (seconds < 1 || seconds > 4)
                    throw new ArgumentOutOfRangeException("seconds");

                // connection setup
                const string multicastIp = "239.255.255.250";
                const int multicastPort = 1900;
                const int unicastPort = 1901;
                const int maxResultSize = 8000;

                // search string
                var find = "M-SEARCH * HTTP/1.1\r\n" +
                              "HOST:239.255.255.250:1900\r\n" +
                              "MAN:\"ssdp:discover\"\r\n" +
                              "MX:" + seconds + "\r\n" +
                              "ST:" + whatToFind + "\r\n" +
                              "\r\n";
                var data = Encoding.UTF8.GetBytes(find);
                var args = new SocketAsyncEventArgs();
                var sendEp = new IPEndPoint(IPAddress.Parse(multicastIp), multicastPort);
                var recEp = new IPEndPoint(IPAddress.Any, unicastPort);
                args.RemoteEndPoint = sendEp;
                args.SetBuffer(data, 0, data.Length);

                // socket handling
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                {
                    SendBufferSize = data.Length,
                    ReceiveBufferSize = maxResultSize
                };
                var receiveBuffer = new byte[maxResultSize];

                var results = new List<string>();

                var socketOpen = true;
                var are = new AutoResetEvent(false);

                args.Completed += (sender, e) =>
                {
                    if (socket == null) return;


                    if (e.SocketError != SocketError.Success)
                    {
                        are.Set();
                        return;
                    }

                    switch (e.LastOperation)
                    {
                        case SocketAsyncOperation.SendTo:
                            e.RemoteEndPoint = recEp;
                            e.SetBuffer(receiveBuffer, 0, maxResultSize);
                            socket.ReceiveFromAsync(e);
                            break;
                        case SocketAsyncOperation.ReceiveFrom:

                            if (e.BytesTransferred > 0)
                            {
                                var result = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                                if (result.StartsWith("HTTP/1.1 200 OK"))
                                {
                                    var httpMessage = new HttpMessage(result);
                                    var location = httpMessage.Headers["LOCATION"];
                                    results.Add(location);
                                }
                            }
                            if (socketOpen)
                            {
                                // and kick off another 
                                socket.ReceiveFromAsync(e);
                            }
                            break;
                    }
                };
                socket.SendToAsync(args);
                are.WaitOne(seconds*1000);
                socket.Shutdown(SocketShutdown.Both);
                socketOpen = false;
                socket.Close();
                socket.Dispose();
                socket = null;
                return results;
            });
        }

        // SSDP response handler, returns location of xml file, or null
        public string GetSsdpLocation(string response)
        {
            var dict = ParseSsdpResponse(response);
            if (dict != null && dict.ContainsKey("location"))
            {
                return dict["location"];
            }

            return null;
        }

        // Probably not exactly compliant with RFC 2616 but good enough for now
        private static Dictionary<string, string> ParseSsdpResponse(string response)
        {
            var reader = new StringReader(response);

            var result = new Dictionary<string, string>();

            for (;;)
            {
                var line = reader.ReadLine();
                if (line == null)
                    break;
                if (string.IsNullOrEmpty(line)) continue;

                var colon = line.IndexOf(':');
                if (colon < 1)
                {
                    return null;
                }
                var name = line.Substring(0, colon).Trim();
                var value = line.Substring(colon + 1).Trim();
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                result[name.ToLowerInvariant()] = value;
            }
            return result;
        }
    }
}