using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public class WeatherService {
        public class WeatherResponse {
            public WeatherLocation? location { get; set; }
            public CurrentWeather? current { get; set; }
        }

        public class WeatherLocation {
            public string? name { get; set; }
        }

        public class CurrentWeather {
            public decimal temp_c { get; set; }
            public decimal feelslike_c { get; set; }
            public Condition? condition { get; set; }
            public int humidity { get; set; }
            public decimal wind_kph { get; set; }
        }

        public class Condition {
            public string? text { get; set; }
        }

        private readonly string MyApiKey = "b7d461c98373444da57191626262610";

        private readonly string url = "https://api.weatherapi.com/v1/current.json";

        private readonly HttpClient httpClient = new HttpClient();

        public async Task<WeatherResponse?> GetWeather(string city) {
            string requestUrl =
                $"{url}?key={MyApiKey}&q={Uri.EscapeDataString(city)}";

            var response = await httpClient.GetAsync(requestUrl);

            Console.WriteLine($"Status code: {(int)response.StatusCode}");

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<WeatherResponse>(jsonString);

            return data;
        }
    }
}