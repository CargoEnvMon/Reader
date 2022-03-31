using Xamarin.Forms;

namespace CargoEnvMon.Reader.ViewModels
{
    public class CargoRequestViewModel
    {
        public CargoRequestViewModel(string cargoId, bool isSuccess, string message)
        {
            CargoId = cargoId;
            IsSuccess = isSuccess;
            Message = message;
        }

        public int Index { get; set; }
        public string CargoId { get; }
        public string Message { get; }
        public bool IsSuccess { get; }
        public string Text => IsSuccess ? "Success" : "Error"; 
    }
}