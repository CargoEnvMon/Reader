using System.Threading.Tasks;
using CargoEnvMon.Reader.Infrastructure;

namespace CargoEnvMon.Reader.Models
{
    public class ReaderServer : SslServer
    {
        private readonly MeasurementRequestProcessor requestProcessor;

        public ReaderServer(
            IIpAddressProvider ipAddressProvider,
            ISslCertificateProvider certificateProvider,
            MeasurementRequestProcessor requestProcessor
        ) : base(ipAddressProvider, certificateProvider)
        {
            this.requestProcessor = requestProcessor;
        }

        protected override string GetResponse(string request)
        {
            Task.Run(() => requestProcessor.Process(request));
            return "Ok";
        }
    }
}