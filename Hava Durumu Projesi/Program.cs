using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			string city;
			Console.WriteLine("Lütfen Hava Durumunu Öğrenmek İstediğiniz Şehiri Yazınız:");
			city=Console.ReadLine();
			string apiKey = "3dbf3733bf11e32e401637cf3f0161d7";
			
			string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";

			using var httpClient = new HttpClient();

			HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				dynamic weatherData = JObject.Parse(jsonResponse);

				string weatherDescription = weatherData.weather[0].description;
				double temperature = weatherData.main.temp - 273.15; // Convert to Celsius
				double hissedilensicaklik = weatherData.main.feels_like - 273.15;
				double minimumsicaklik = weatherData.main.temp_min-273.15;
				double maksimumsicaklik = weatherData.main.temp_max - 273.15;
				string country = weatherData.sys.country;
				string sehir = weatherData.name;

				Console.WriteLine($"Hava Durumu: {weatherDescription}");
				Console.WriteLine($"Sıcaklık: {temperature:F2}°C");
				Console.WriteLine($"Hissedilen Sıcaklık: {hissedilensicaklik:F2}°C");
				Console.WriteLine("Minimum Sıcaklık:"+minimumsicaklik+ "°C");
				Console.WriteLine("Maksimum Sıcaklık:"+maksimumsicaklik+ "°C");
				Console.WriteLine("ÜLKE:"+country);
				Console.WriteLine("ŞEHİR:"+sehir);

			}
			else
			{
				Console.WriteLine($"Hata Kodu: {response.StatusCode}");
			}
			Console.ReadLine();
		}
	}
}