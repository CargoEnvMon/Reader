using Android.App;
using Android.Content;
using Android.Net.Wifi;

namespace CargoEnvMon.Reader.Droid.PlatformSpecific
{
    internal static class WiFiManagerProvider
    {
        public static WifiManager GetWifiManager()
        {
            return Application.Context.GetSystemService(Context.WifiService) as WifiManager;
        }
    }
}