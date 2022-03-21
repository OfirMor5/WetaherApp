using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WetaherApp.model;

namespace WetaherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENDPOINT = "http://dataservice.accuweather.com/currentconditions/v1/{0}?apikey=";
        public const string API_KEY = "cKWWNR0UmxI3BiVfjl6S4xsN1cF576Ck";


        public static async Task<List<City>> GetCitiesAsync(string query)
        {
            List<City> cities = new List<City>();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }
            return cities;
        }

        public static async Task<CurrentConditiion> GetCurrentConditiions(string cityKey)
        {
            CurrentConditiion currentConditiion = new CurrentConditiion();

            string url = BASE_URL + string.Format(CURRENT_CONDITION_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditiion = (JsonConvert.DeserializeObject<List<CurrentConditiion>>(json)).FirstOrDefault();
            }

            return currentConditiion;
        }
    }
}
