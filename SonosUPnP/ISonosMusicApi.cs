using System.Collections.Generic;
using System.Threading.Tasks;
using Sonos.Devices;
using Sonos.Models;

namespace Sonos
{
    public interface ISonosMusicApi
    {
        SonosMusicApiDevice GetMusicApiDevice(MusicServiceContainer serviceContainer);
        Task<List<MusicServiceContainer>> GetAvailableMusicServices();
        Task<List<MusicServiceAccount>> GetActiveServiceAccounts();
        DeviceAuthToken GetDeviceAuthToken(string serviceName);
        void SetDeviceAuthToken(string serviceName, DeviceAuthToken deviceAuthToken);
    }
}