using System;
using System.Threading.Tasks;
using WeatherApp.Commands;

namespace WeatherApp.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        #region Variables
        private string city;
        private string _rawText;
        
        private TemperatureModel currentTemp;
        


        public string City
        {
            get { return city; }
            set
            {
                city = value;

                if (TemperatureService != null)
                {
                    TemperatureService.SetLocation(City);
                }

                OnPropertyChanged();
            }
        }

        public TemperatureModel CurrentTemp
        {
            get => currentTemp;
            set
            {
                currentTemp = value;
                OnPropertyChanged();
                OnPropertyChanged("RawText");
            }
        }

        public string RawText
        {
            get
            {
                return _rawText;
            }
            set
            {
                _rawText = value;
                OnPropertyChanged();
            }
        }

        public ITemperatureService TemperatureService { get; private set; }
        #endregion



        #region Command
        public DelegateCommand<string> GetTempCommand { get; set; }


        #endregion



        #region Constructeur
        public TemperatureViewModel()
        {
            Name = GetType().Name;

            GetTempCommand = new DelegateCommand<string>(GetTemp, CanGetTemp);
        }

        #endregion



        #region Methodes Command
        /// <summary>
        /// TODO 12 (OK) Valider que la clé est là
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CanGetTemp(string obj)
        {
            return ( !(String.IsNullOrEmpty(Properties.Settings.Default.apiKey)) ); 
        }


        public void GetTemp(string obj)
        {
            if (TemperatureService == null) throw new NullReferenceException();

            _ = GetTempAsync();
        }

        private async Task GetTempAsync()
        {
            try
            {
                CurrentTemp = await TemperatureService.GetTempAsync();

                RawText = $"Time : {CurrentTemp.DateTime.ToLocalTime()} {Environment.NewLine}Temperature : {CurrentTemp.Temperature}";
            }
            catch (Exception ex)
            {
                RawText = ex.Message;
            }
            
        }

        #endregion



        #region Methode utilitaires
        public double CelsiusInFahrenheit(double c)
        {
            return c * 9.0 / 5.0 + 32;
        }

        public double FahrenheitInCelsius(double f)
        {
            return (f - 32) * 5.0 / 9.0;
        }

        public void SetTemperatureService(ITemperatureService srv)
        {
            TemperatureService = srv;
        }
        #endregion
    }
}
