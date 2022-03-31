using CargoEnvMon.Reader.ViewModels;

namespace CargoEnvMon.Reader.Infrastructure
{
    public static class CargoRequestViewModelMapper
    {
        public static CargoRequestViewModel Map(Result result, string cargoId)
        {
            return new CargoRequestViewModel(cargoId, result.IsSuccess, result.Message);
        }
    }
}