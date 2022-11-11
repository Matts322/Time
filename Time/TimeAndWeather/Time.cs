using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
//using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


namespace Time
{
    public partial class MainWindow
    {
        static readonly HttpClient client = new HttpClient();
        private string RequestUriString = "https://api.openweathermap.org/data/2.5/weather?q=Minsk&units=metric&appid=b1494cc125faf6478950ec35a91a4399";
        private readonly DispatcherTimer timerTime = new DispatcherTimer();
        private void InitTime()
        {
            timerTime.Tick += new EventHandler(Timer1_Tick);
            timerTime.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timerTime.Start();
#pragma warning disable CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до тех пор, пока вызов не будет завершен
            GetWeather();
#pragma warning restore CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до тех пор, пока вызов не будет завершен
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            CurrentTime.Text = dt.ToLongTimeString();
            CurrentDate.Text = dt.ToLongDateString();
        }

        private async Task GetWeather()
        {
            try
            {
                string responseBody = await client.GetStringAsync(RequestUriString);

                //use .Net json parser
                //JsonNode weatherResponse = JsonNode.Parse(responseBody);
                //Weather.Text = $"Temperature in {weatherResponse["name"]} is {weatherResponse["main"]["temp"]} ℃ ";

                //use Newtonsoft deserialiser
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);
                Weather.Text = $"Temperature in {weatherResponse.Name}: {Math.Round(weatherResponse.Main.Temp)} ℃ Сountry: {weatherResponse.Sys.Country} ";
            }
            catch (Exception)
            {
                Weather.Text = "Server is not available";
            }
        }

        private void Get_New_Weather(object sender, RoutedEventArgs e)
        {
            string City = setCity.Text.Trim();
            RequestUriString = string.Concat(@"https://api.openweathermap.org/data/2.5/weather?q=", City, @"&units=metric&appid=b1494cc125faf6478950ec35a91a4399");
#pragma warning disable CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до тех пор, пока вызов не будет завершен
            GetWeather();
#pragma warning restore CS4014 // Так как этот вызов не ожидается, выполнение существующего метода продолжается до тех пор, пока вызов не будет завершен
        }
    }
}
