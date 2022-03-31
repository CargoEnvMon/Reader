using System;
using System.Net.Http;
using CargoEnvMon.Reader.Models;
using CargoEnvMon.Reader.Models.Client;
using CargoEnvMon.Reader.ViewModels;
using Xamarin.Forms;

namespace CargoEnvMon.Reader.Infrastructure
{
    public static class ReaderServerBuilder
    {
        public static ReaderServer Build(Action<CargoRequestViewModel> onCompleted)
        {
            var processor = new MeasurementRequestProcessor(
                GetClient(),
                (res, id) => onCompleted(CargoRequestViewModelMapper.Map(res, id))
            );
            return new ReaderServer(
                DependencyService.Get<IIpAddressProvider>(),
                DependencyService.Get<ISslCertificateProvider>(),
                processor
            );
        }

        private static StorageClient GetClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("")
            };
            return new StorageClient(client);
        }
    }
}