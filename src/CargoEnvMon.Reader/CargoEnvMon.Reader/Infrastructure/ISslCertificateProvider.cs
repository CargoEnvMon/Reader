using System.Security.Cryptography.X509Certificates;

namespace CargoEnvMon.Reader.Infrastructure
{
    public interface ISslCertificateProvider
    {
        X509Certificate GetCertificate();
    }
}