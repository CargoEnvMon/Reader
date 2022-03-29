using Newtonsoft.Json;

namespace CargoEnvMon.Reader.Models.Client
{
    public class ErrorResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}