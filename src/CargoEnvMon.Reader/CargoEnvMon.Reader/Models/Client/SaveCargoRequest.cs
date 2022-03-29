using System.Collections.Generic;
using Newtonsoft.Json;

namespace CargoEnvMon.Reader.Models.Client
{
    public class SaveCargoRequest
    {
        [JsonProperty("shipmentId")]
        public string ShipmentId { get; }
        
        [JsonProperty("cargoId")]
        public string CargoId { get; }
        
        [JsonProperty("items")]
        public IReadOnlyList<MeasurementModel> Items { get; set; }
    }
}