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
        
        public string ShipmentId { get; set; }

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
                    var emptyReqRes = new Result(false, "Invalid request received");
                    onCompleted(emptyReqRes, "");
                    return;
                }

                saveRequest.ShipmentId = ShipmentId;
                var result = await client.SaveCargo(saveRequest);
                onCompleted(result, saveRequest.CargoId);
            }
            catch (Exception e)
            {
                ExceptionsHandler.Handle(e);
            }
        }
    }
}