using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    internal class Program {
        static async Task Main(string[] args) {
            WeatherService weatherService = new WeatherService();

            while (true) {
                Console.Write("Enter city name (exit - for leaving): ");
                string city = Console.ReadLine();

                if (city.Trim().ToLower() == "exit") {
                    break;
                }

                if (string.IsNullOrWhiteSpace(city)) {
                    Console.WriteLine("City name has to contain data...");
                    continue;
                }

                try {
                    var weather = await weatherService.GetWeather(city);

                    if (weather == null) {
                        Console.WriteLine("Information hasn't been found");
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n--- WEATHER INFORMATION ---");
                    Console.WriteLine($"City: {weather.location?.name}");
                    Console.WriteLine($"Temprature: {weather.current.temp_c} C");
                    Console.WriteLine($"Felt Temprature: {weather.current.feelslike_c} C");
                    Console.WriteLine($"Weather Condition: {weather.current.condition?.text}");
                    Console.WriteLine($"Humidity: {weather.current.humidity}%");
                    Console.WriteLine($"Wind speed: {weather.current.wind_kph} km/h");
                    Console.WriteLine("------------------------\n");
                    Console.ResetColor();
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
