using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Time
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string RequestUriString = "https://api.openweathermap.org/data/2.5/weather?q=Minsk&units=metric&appid=b1494cc125faf6478950ec35a91a4399";
        private readonly DispatcherTimer timer2 = new DispatcherTimer();
        private readonly DispatcherTimer timer3 = new DispatcherTimer();
        private readonly DispatcherTimer timer1 = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            timer1.Tick += new EventHandler(Timer1_Tick);
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer1.Start();
            GetWeather();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //if (CountdownTime.Text != "0")
            if (!string.Equals(CountdownTime.Text, "0"))
            {
                CountdownTime.Text = (int.Parse(CountdownTime.Text) - 1).ToString();
            }
            else
            {
                timer2.Stop();
                timer2.Tick -= Timer2_Tick;
                ResetButton.IsEnabled = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            CurrentTime.Text = dt.ToLongTimeString();
            CurrentDate.Text = dt.ToLongDateString();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CountdownTime.Text) || !int.TryParse(CountdownTime.Text, out int a) || a < 0)
            {
                return;
            }

            ResetButton.IsEnabled = false;
            timer2.Tick += new EventHandler(Timer2_Tick);
            timer2.Interval = new TimeSpan(0, 0, 1);
            timer2.Start();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            timer2.Stop();
            timer2.Tick -= Timer2_Tick;
            ResetButton.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CountdownTime.Text = string.Empty;
        }

        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            ResetStopWatch.IsEnabled = false;
            timer3.Tick += new EventHandler(Timer3_Tick);
            timer3.Interval = new TimeSpan(0, 0, 1);
            timer3.Start();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            Stopwatch.Content = int.Parse(Stopwatch.Content.ToString()) + 1;
        }

        private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            timer3.Stop();
            timer3.Tick -= Timer3_Tick;
            ResetStopWatch.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Stopwatch.Content = "0";
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
