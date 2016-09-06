using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace UPnP.Extensions
{
    public static class HttpWebRequestExtensions
    {
        public static Task<Stream> GetRequestStreamAsync(this HttpWebRequest r)
        {
            var tcs = new TaskCompletionSource<Stream>();
            r.BeginGetRequestStream(e =>
            {
                var wr = e.AsyncState as HttpWebRequest;
                if (e.IsCompleted && wr != null)
                {
                    Stream stream = wr.EndGetRequestStream(e);
                    tcs.SetResult(stream);
                }
                else
                {
                    tcs.SetException(new Exception("Error getting response stream"));
                }
            }, r);
            return tcs.Task;
        }

        public static Task<WebResponse> GetResponseAsync(this HttpWebRequest r)
        {
            var tcs = new TaskCompletionSource<WebResponse>();
            
            r.BeginGetResponse(e =>
            {
                if (e.IsCompleted)
                {
                    var wr = e.AsyncState as HttpWebRequest;
                    try
                    {
                        if (wr == null) return;
                        var resp = wr.EndGetResponse(e);
                        tcs.SetResult(resp);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }
                else
                {
                    tcs.SetException(new Exception("Error getting response"));
                }
            }, r);

            return tcs.Task;
        }
    }
}