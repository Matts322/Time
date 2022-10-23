using System;
using System.Windows;
using System.Windows.Threading;


namespace Time
{
    public partial class MainWindow
    {
        private readonly DispatcherTimer timerStopwatch = new DispatcherTimer();
        private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
        {
            ResetStopWatch.IsEnabled = false;
            timerStopwatch.Tick += new EventHandler(Timer3_Tick);
            timerStopwatch.Interval = new TimeSpan(0, 0, 1);
            timerStopwatch.Start();
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            Stopwatch.Content = int.Parse(Stopwatch.Content.ToString()) + 1;
        }

        private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
        {
            timerStopwatch.Stop();
            timerStopwatch.Tick -= Timer3_Tick;
            ResetStopWatch.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Stopwatch.Content = "0";
        }
    }
}
