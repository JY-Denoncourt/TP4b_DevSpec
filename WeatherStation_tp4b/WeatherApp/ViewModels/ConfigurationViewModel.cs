using System;
using System.Diagnostics;
using WeatherApp.Commands;

namespace WeatherApp.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        #region Variables
        private string apiKey;        

        public string ApiKey { 
            get => apiKey;
            set
            {
                apiKey = value;
                OnPropertyChanged();
            }
        }
        #endregion

        
        #region Command
        public DelegateCommand<string> SaveConfigurationCommand { get; set; }

        #endregion


        #region Constructeur
        public ConfigurationViewModel()
        {
            Name = GetType().Name;

            ApiKey = GetApiKey();

            SaveConfigurationCommand = new DelegateCommand<string>(SaveConfiguration);
        }
        #endregion


        #region Methodes 
        private void SaveConfiguration(string obj)
        {
            /// TODO 02,03 (OK) 
            /// TODO 04 (OK) Sauvegarder la configuration
            Debug.Print("Key = " + ApiKey);
            Properties.Settings.Default.apiKey = ApiKey;
            Properties.Settings.Default.Save();
        }

        private string GetApiKey()
        {
            /// TODO 05 (OK) Retourner la configuration
            /// TODO 06 (OK)
            /// TODO 07 (OK)
            return Properties.Settings.Default.apiKey;
        }
        #endregion
    }
}
