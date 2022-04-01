using System;
using System.Collections.ObjectModel;
using CargoEnvMon.Reader.Infrastructure;
using CargoEnvMon.Reader.Models;
using CargoEnvMon.Reader.ViewModels;
using Xamarin.Forms;

namespace CargoEnvMon.Reader
{
    public partial class MainPage
    {
        private string buttonText;
        private bool isStarted;

        private bool IsStarted
        {
            get => isStarted;
            set
            {
                isStarted = value;
                OnPropertyChanged(nameof(EnableEntry));
            }
        }

        public bool EnableEntry => !IsStarted;

        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public string ShipmentId { get; set; }

        public string ExceptionText
        {
            get => exceptionText;
            set
            {
                exceptionText = value;
                OnPropertyChanged(nameof(ExceptionText));
            }
        }

        public ObservableCollection<CargoRequestViewModel> CargoRequestResults { get; } = new();

        private readonly ReaderServer readerServer;
        private string exceptionText;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            ButtonText = "Start";
            ExceptionsHandler.OnHandle(e => ExceptionText = e);
            try
            {
                readerServer = ReaderServerBuilder.Build(OnRequestCompleted);
            }
            catch (Exception e)
            {
                ExceptionsHandler.Handle(e);
            }
        }

        private void OnRequestCompleted(CargoRequestViewModel viewModel)
        {
            viewModel.Index = CargoRequestResults.Count + 1;
            CargoRequestResults.Add(viewModel);
        }

        private async void OnCellClicked(object sender, EventArgs args)
        {
            var viewModel = (sender as ViewCell)!.BindingContext as CargoRequestViewModel;
            if (!viewModel!.IsSuccess)
            {
                await DisplayAlert(
                    $"Cargo \"{viewModel.CargoId}\" error",
                    viewModel.Message,
                    "Ok"
                );
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            IsStarted = !IsStarted;
            if (IsStarted)
            {
                readerServer.SetShipmentId(ShipmentId);
                readerServer.Start();
                ButtonText = "Stop";
            }
            else
            {
                readerServer.Stop();
                ButtonText = "Start";
            }
        }
    }
}