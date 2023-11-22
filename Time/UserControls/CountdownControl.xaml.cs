using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Time.UserControls;

/// <summary>
/// Логика взаимодействия для CountdownControl.xaml
/// </summary>
public partial class CountdownControl : UserControl
{
    private readonly DispatcherTimer _timerCountDown = new();
    private void Timer2_Tick(object sender, EventArgs e)
    {
        if (!string.Equals(CountdownTime.Text, "0"))
        {
            CountdownTime.Text = (int.Parse(CountdownTime.Text) - 1).ToString();
        }
        else
        {
            _timerCountDown.Stop();
            _timerCountDown.Tick -= Timer2_Tick;
            ResetButton.IsEnabled = true;
        }

    }
    private void ToggleButton_Checked(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CountdownTime.Text) || !int.TryParse(CountdownTime.Text, out var a) || a < 0)
        {
            return;
        }

        ResetButton.IsEnabled = false;
        _timerCountDown.Tick += Timer2_Tick;
        _timerCountDown.Interval = new TimeSpan(0, 0, 1);
        _timerCountDown.Start();
    }

    private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
    {
        _timerCountDown.Stop();
        _timerCountDown.Tick -= Timer2_Tick;
        ResetButton.IsEnabled = true;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        CountdownTime.Text = string.Empty;
    }
    public CountdownControl()
    {
        InitializeComponent();
    }
}
