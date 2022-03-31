using System.Net;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using CargoEnvMon.Reader.Droid.PlatformSpecific;
using CargoEnvMon.Reader.Infrastructure.Abstractions;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidIpAddressProvider))]
namespace CargoEnvMon.Reader.Droid.PlatformSpecific
{
    public class AndroidIpAddressProvider : IIpAddressProvider
    {
        public byte[] GetIpAddressBytes()
        {
            var wifiManager = Application.Context.GetSystemService(Context.WifiService) as WifiManager;
            var ip = wifiManager!.ConnectionInfo!.IpAddress;
            return new IPAddress(ip).GetAddressBytes();
        }
    }
}