using Newtonsoft.Json;

namespace CargoEnvMon.Reader.Models.Client
{
    public class MeasurementModel
    {
        [JsonProperty("temperature")]
        public float Temperature { get; set; }
        
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}