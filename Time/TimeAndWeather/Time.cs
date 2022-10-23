using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Time
{
    public partial class MainWindow
    {
        private string RequestUriString = "https://api.openweathermap.org/data/2.5/weather?q=Minsk&units=metric&appid=b1494cc125faf6478950ec35a91a4399";
        private readonly DispatcherTimer timerTime = new DispatcherTimer();
        private void InitTime()
        {
            timerTime.Tick += new EventHandler(Timer1_Tick);
            timerTime.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timerTime.Start();
            GetWeather();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            CurrentTime.Text = dt.ToLongTimeString();
            CurrentDate.Text = dt.ToLongDateString();
        }

        private async void GetWeather()
        {
            await Task.Run(() =>
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestUriString);

                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true);
                    string resp = sr.ReadToEnd();
                    sr.Close();
                    WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(resp);
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        Weather.Text = $"Temperature in {weatherResponse.Name}: {Math.Round(weatherResponse.Main.Temp)} ℃";
                    });
                }
                catch (Exception)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        Weather.Text = "Server is not available";
                    });

                }
            });
        }

        private void Get_New_Weather(object sender, RoutedEventArgs e)
        {
            string City = setCity.Text.Trim();
            RequestUriString = string.Concat(@"https://api.openweathermap.org/data/2.5/weather?q=", City, @"&units=metric&appid=b1494cc125faf6478950ec35a91a4399");
            GetWeather();
        }
    }
}
