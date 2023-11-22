using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Time.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TimeControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl
    {
        private readonly DispatcherTimer _timerTime = new();
        private static readonly HttpClient Client = new();

        private string _requestUriString =
            "https://api.openweathermap.org/data/2.5/weather?q=Minsk&units=metric&appid=b1494cc125faf6478950ec35a91a4399";

        private async void InitTime()
        {
            _timerTime.Tick += Timer1_Tick;
            _timerTime.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _timerTime.Start();
            await GetWeather();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            CurrentTime.Text = dt.ToLongTimeString();
            CurrentDate.Text = dt.ToLongDateString();
        }

        private async Task GetWeather()
        {
            var response = await Client.GetAsync(_requestUriString);
            if (response.StatusCode is HttpStatusCode.OK)
            {
                var responseBody = await Client.GetStringAsync(_requestUriString);
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);
                Weather.Text = weatherResponse != null
                    ? $"Temperature in {weatherResponse.Name}: {Math.Round(weatherResponse.Main.Temp)} ℃ Country: {weatherResponse.Sys.Country} "
                    : "Server is not available";
            }
            else
            {
                Weather.Text = "Server is not available";
            }

        }

        private async void Get_New_Weather(object sender, RoutedEventArgs e)
        {
            var city = SetCity.Text.Trim();
            _requestUriString = string.Concat(@"https://api.openweathermap.org/data/2.5/weather?q=", city,
                @"&units=metric&appid=b1494cc125faf6478950ec35a91a4399");
            await GetWeather();
        }
        public TimeControl()
        {
            InitializeComponent();
            InitTime();
        }
    }
}
