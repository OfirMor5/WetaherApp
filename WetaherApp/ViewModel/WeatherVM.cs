using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Helpers;
using WetaherApp.ViewModel.Commands;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public SearchCommand SearchCommand { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private CurrentConditions currentConditions;
        private string query;
        private City selectedCity;


        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }


        public CurrentConditions CurrentConditions
        {
            get => currentConditions;
            set
            {
                currentConditions = value;
                OnPropertyChanged("currentConditions");
            }
        }

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }



        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new City
                {
                    LocalizedName = "jerusalem"
                };
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "sunny",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "7"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();

        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(selectedCity.Key);
        }

        public async Task MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach(var city in cities)
            {
                Cities.Add(city);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
