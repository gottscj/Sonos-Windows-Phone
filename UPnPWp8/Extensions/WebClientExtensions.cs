using System;
using System.Net;
using System.Threading.Tasks;

namespace UPnP.Extensions
{
    public static class WebClientExtensions
    {
        public static Task<string> DownloadStringAsync(this WebClient webClient, string uri)
        {
            var tcs = new TaskCompletionSource<string>();
            DownloadStringCompletedEventHandler handler = null;
            handler = (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.SetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
                webClient.DownloadStringCompleted -= handler;
            };

            webClient.DownloadStringCompleted += handler;
            webClient.DownloadStringAsync(new Uri(uri), webClient);
            return tcs.Task;
        }
    }
}