using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Time.UserControls;

/// <summary>
/// Логика взаимодействия для StopWatchControl.xaml
/// </summary>
public partial class StopWatchControl : UserControl
{
    private readonly DispatcherTimer _timerStopwatch = new();
    public StopWatchControl()
    {
        InitializeComponent();
    }
    private void ToggleButton_Checked_1(object sender, RoutedEventArgs e)
    {
        ResetStopWatch.IsEnabled = false;
        _timerStopwatch.Tick += Timer3_Tick;
        _timerStopwatch.Interval = new TimeSpan(0, 0, 1);
        _timerStopwatch.Start();
    }

    private void Timer3_Tick(object sender, EventArgs e)
    {
        Stopwatch.Content = int.Parse(Stopwatch.Content.ToString() ?? string.Empty) + 1;
    }

    private void ToggleButton_Unchecked_1(object sender, RoutedEventArgs e)
    {
        _timerStopwatch.Stop();
        _timerStopwatch.Tick -= Timer3_Tick;
        ResetStopWatch.IsEnabled = true;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Stopwatch.Content = "0";
    }
}