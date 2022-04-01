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

        private readonly string shipmentId;
        private readonly string baseUrl;

        public MeasurementRequestProcessor(
            StorageClient client,
            Action<Result, string> onCompleted, 
            string shipmentId,
            string baseUrl)
        {
            this.client = client;
            this.onCompleted = onCompleted;
            this.shipmentId = shipmentId;
            this.baseUrl = baseUrl;
        }

        public async Task Process(string request)
        {
            try
            {
                var saveRequest = MeasurementRequestParser.Parse(request);
                if (saveRequest == null)
                {
                    var emptyReqRes = new Result(false, "Invalid request received");
                    onCompleted(emptyReqRes, "");
                    return;
                }

                saveRequest.ShipmentId = shipmentId;
                var result = await client.SaveCargo(baseUrl, saveRequest);
                onCompleted(result, saveRequest.CargoId);
            }
            catch (Exception e)
            {
                ExceptionsHandler.Handle(e);
            }
        }
    }
}