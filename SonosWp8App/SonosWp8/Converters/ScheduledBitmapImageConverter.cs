using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SonosWp8.Converters
{
    public class ScheduledBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (String.IsNullOrEmpty(path)) return null;

            var bm = new BitmapImage();
            bm.DecodePixelWidth = 72;
            if (!path.StartsWith("http"))
            {
                bm.UriSource = new Uri(path, UriKind.Relative);
                return bm;
            }

            //TaskScheduler.Enqueue(CreateDownloadTask(path, bm), path);
            EventHandler<RoutedEventArgs> onSuccess = null;
            EventHandler<ExceptionRoutedEventArgs> onFail = null;
            onSuccess = (s, e) =>
            {
                bm.ImageOpened -= onSuccess;
                bm.ImageFailed -= onFail;
            };
            onFail = (s, e) =>
            {
                onSuccess(null, null);
                bm.UriSource = new Uri("/Assets/defaultImageArt.jpg", UriKind.Relative);
            };
            bm.ImageOpened += onSuccess;
            bm.ImageFailed += onFail;
            bm.UriSource = new Uri(path, UriKind.Absolute);

            return bm;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

/*
        private Task CreateDownloadTask(string path, BitmapImage bm)
        {
            return new Task(() =>
            {
                var waitHandle = new AutoResetEvent(false);
                EventHandler<RoutedEventArgs> onSuccess = (s, e) => waitHandle.Set();

                EventHandler<ExceptionRoutedEventArgs> onFail = (s, e) =>
                {
                    bm.Dispatcher.BeginInvoke(
                        () => bm.UriSource = new Uri("/Assets/defaultImageArt.jpg", UriKind.Relative));
                    waitHandle.Set();
                };

                bm.Dispatcher.BeginInvoke(() =>
                {
                    bm.ImageOpened += onSuccess;
                    bm.ImageFailed += onFail;
                    bm.UriSource = new Uri(path, UriKind.Absolute);
                });
                // wait for image to download
                bool signalReceived = waitHandle.WaitOne(TimeSpan.FromSeconds(20));

                // clean up
                bm.Dispatcher.BeginInvoke(() =>
                {
                    if (!signalReceived) bm.UriSource = null;

                    bm.ImageOpened -= onSuccess;
                    bm.ImageFailed -= onFail;
                    waitHandle.Set();
                });

                // wait for clean up
                waitHandle.WaitOne();
            });
        }
*/
    }
}