using System;
using System.Collections.ObjectModel;
using CargoEnvMon.Reader.ViewModels;

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

        public ObservableCollection<CargoRequestViewModel> CargoRequestResults { get; } = new();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            ButtonText = "Start";
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            IsStarted = !IsStarted;
            if (IsStarted)
            {
                ButtonText = "Stop";
            }
            else
            {
                ButtonText  = "Start";
            }
            
            CargoRequestResults.Add(new CargoRequestViewModel(CargoRequestResults.Count, new Random().Next().ToString(), isStarted));
        }
    }
}