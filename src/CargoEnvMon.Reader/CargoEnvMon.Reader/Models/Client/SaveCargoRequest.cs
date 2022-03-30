using System.Collections.Generic;
using Newtonsoft.Json;

namespace CargoEnvMon.Reader.Models.Client
{
    public class SaveCargoRequest
    {
        [JsonProperty("shipmentId")]
        public string ShipmentId { get; set; }
        
        [JsonProperty("cargoId")]
        public string CargoId { get; set; }
        
        [JsonProperty("startTimestamp")]
        public string StartTimestamp { get; set; }
        
        [JsonProperty("timeShiftUnits")]
        public int? TimeShiftUnits { get; set; }
        
        [JsonProperty("items")]
        public IReadOnlyList<MeasurementModel> Items { get; set; }
    }
}