using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WeatherApp.Commands;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Membres
        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private OpenWeatherService ows;
        private ConfigurationViewModel cvm;

        #endregion

        #region Propriétés
        /// <summary>
        /// Model actuellement affiché
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { 
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Commande pour changer la page à afficher
        /// </summary>
        public DelegateCommand<string> ChangePageCommand { get; set; }

        public List<BaseViewModel> ViewModels
        {
            get {
                if (viewModels == null)
                    viewModels = new List<BaseViewModel>();
                return viewModels; 
            }
        }
        #endregion

        #region Constructeur
        public ApplicationViewModel()
        {
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
           
            /// TODO 11 (OK) Commenter cette ligne lorsque la configuration utilisateur fonctionne
            //var apiKey = AppConfiguration.GetValue("OWApiKey");
            //ows = new OpenWeatherService(apiKey);

            initViewModels();
        }
        #endregion


        #region Méthodes
        void initViewModels()
        {
            /// TemperatureViewModel setup
            var tvm = new TemperatureViewModel();
            cvm = new ConfigurationViewModel();
           

            /// TODO 09 (OK) Indiquer qu'il n'y a aucune clé si le Settings apiKey est vide.
            /// S'il y a une valeur, instancié OpenWeatherService avec la clé
            if ( String.IsNullOrEmpty(Properties.Settings.Default.apiKey))   //cvm.ApiKey, Properties.Settings.Default.apiKey
            {
                tvm.RawText = "Mettre une cle API dans la fenetre de configuration";
                ows = new OpenWeatherService(null);
            }
            else
            {
                ows = new OpenWeatherService(Properties.Settings.Default.apiKey);
                //ows.SetApiKey(Properties.Settings.Default.apiKey);
            }


            tvm.SetTemperatureService(ows);
            ViewModels.Add(tvm);

            /// TODO 01 (OK) ConfigurationViewModel Add Configuration ViewModel
            ViewModels.Add(cvm);


            CurrentViewModel = ViewModels[0];
        }

        private void ChangePage(string pageName)
        {

            /// TODO 10 (OK) Si on a changé la clé, il faudra la mettre dans le service.
            /// Algo
            /// Si la vue actuelle est ConfigurationViewModel
            ///   Mettre la nouvelle clé dans le OpenWeatherService
            ///   Rechercher le TemperatureViewModel dans la liste des ViewModels
            ///   Si le service de temperature est null
            ///     Assigner le service de température
            /// 
            if (pageName == "ConfigurationViewModel")
            {
                ows.SetApiKey(Properties.Settings.Default.apiKey);

                TemperatureViewModel tvm = (TemperatureViewModel)ViewModels.FirstOrDefault(x => x.Name == "TemperatureViewModel");
                
                if (tvm.TemperatureService == null)
                {
                    tvm.SetTemperatureService(ows);
                }
            }
            else
            {
                ows.SetApiKey(Properties.Settings.Default.apiKey);
            }
            

          
            /// Permet de retrouver le ViewModel avec le nom indiqé
            CurrentViewModel = ViewModels.FirstOrDefault(x => x.Name == pageName);  
        }

        #endregion
    }
}
