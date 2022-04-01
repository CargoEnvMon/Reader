using System;
using CargoEnvMon.Reader.Infrastructure.Abstractions;
using CargoEnvMon.Reader.Models;
using CargoEnvMon.Reader.Models.Client;
using CargoEnvMon.Reader.ViewModels;
using Xamarin.Forms;

namespace CargoEnvMon.Reader.Infrastructure
{
    public static class ReaderServerBuilder
    {
        public static ReaderServer Build(
            Action<CargoRequestViewModel> onCompleted,
            string shipmentId,
            string baseUrl
        )
        {
            var processor = new MeasurementRequestProcessor(
                new StorageClient(),
                (res, id) => onCompleted(CargoRequestViewModelMapper.Map(res, id)),
                shipmentId,
                baseUrl
            );

            return new ReaderServer(DependencyService.Get<IIpAddressProvider>(), processor);
        }
    }
}