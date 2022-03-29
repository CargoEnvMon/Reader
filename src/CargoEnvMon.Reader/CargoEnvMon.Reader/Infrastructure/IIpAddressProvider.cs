namespace CargoEnvMon.Reader.Infrastructure
{
    public interface IIpAddressProvider
    {
        byte[] GetIpAddressBytes();
    }
}