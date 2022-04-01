using System.Threading.Tasks;
using CargoEnvMon.Reader.Infrastructure;
using CargoEnvMon.Reader.Infrastructure.Abstractions;

namespace CargoEnvMon.Reader.Models
{
    public class ReaderServer : SslServer
    {
        private readonly MeasurementRequestProcessor requestProcessor;

        public ReaderServer(IIpAddressProvider ipAddressProvider, MeasurementRequestProcessor requestProcessor)
            : base(ipAddressProvider)
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