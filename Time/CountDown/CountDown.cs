using System;
using System.Windows;
using System.Windows.Threading;

namespace Time
{
    public partial class MainWindow
    {
        private readonly DispatcherTimer timerCountDown = new DispatcherTimer();
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (!string.Equals(CountdownTime.Text, "0"))
            {
                CountdownTime.Text = (int.Parse(CountdownTime.Text) - 1).ToString();
            }
            else
            {
                timerCountDown.Stop();
                timerCountDown.Tick -= Timer2_Tick;
                ResetButton.IsEnabled = true;
            }

        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CountdownTime.Text) || !int.TryParse(CountdownTime.Text, out int a) || a < 0)
            {
                return;
            }

            ResetButton.IsEnabled = false;
            timerCountDown.Tick += new EventHandler(Timer2_Tick);
            timerCountDown.Interval = new TimeSpan(0, 0, 1);
            timerCountDown.Start();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            timerCountDown.Stop();
            timerCountDown.Tick -= Timer2_Tick;
            ResetButton.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CountdownTime.Text = string.Empty;
        }
    }
}
