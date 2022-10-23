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
        
        public MainWindow()
        {
            InitializeComponent();
            InitTime();
        }

        
    }
}
