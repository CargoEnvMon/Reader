namespace CargoEnvMon.Reader.Infrastructure.Abstractions
{
    public interface IIpAddressProvider
    {
        byte[] GetIpAddressBytes();
    }
}