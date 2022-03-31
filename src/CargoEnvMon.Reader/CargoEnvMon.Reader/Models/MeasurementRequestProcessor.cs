using System;
using System.Threading.Tasks;
using CargoEnvMon.Reader.Infrastructure;
using CargoEnvMon.Reader.Models.Client;

namespace CargoEnvMon.Reader.Models
{
    public class MeasurementRequestProcessor
    {
        private readonly StorageClient client;
        private readonly Action<Result, string> onCompleted;

        public MeasurementRequestProcessor(
            StorageClient client,
            Action<Result, string> onCompleted
        )
        {
            this.client = client;
            this.onCompleted = onCompleted;
        }

        public async Task Process(string request)
        {
            try
            {
                var saveRequest = MeasurementRequestParser.Parse(request);
                if (saveRequest == null)
                {
                    return;
                }

                var result = await client.SaveCargo(saveRequest);
                onCompleted(result, saveRequest.CargoId);
            }
            catch
            {
            }
        }
    }
}