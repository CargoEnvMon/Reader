using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CargoEnvMon.Reader.Infrastructure;
using Newtonsoft.Json;

namespace CargoEnvMon.Reader.Models.Client
{
    public class StorageClient
    {
        private readonly HttpClient httpClient;

        public StorageClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Result> SaveCargo(SaveCargoRequest model)
        {
            var str = JsonConvert.SerializeObject(model);
            var request = new StringContent(str, Encoding.Default, "application/json");

            try
            {
                var response = await httpClient.PostAsync(new Uri("/api/cargo"), request);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success;
                }

                var errorResponse = await ParseResponse(response);
                return Result.Error(errorResponse.Message);
            }
            catch (Exception e)
            {
                return new Result(false, $"Exception occured: {e}");
            }
        }

        private static async Task<ErrorResponse> ParseResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ErrorResponse>(content);
        }
    }
}