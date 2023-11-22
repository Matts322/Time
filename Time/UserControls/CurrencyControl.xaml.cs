using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Time.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CurrencyControl.xaml
    /// </summary>
    public partial class CurrencyControl : UserControl
    {
        private const string CurrencyUri = @"https://myfin.by/currency/minsk";
        public CurrencyControl()
        {
            InitializeComponent();
            _ = GetCurrency();
        }

        private async void UpdCurrency_Click(object sender, RoutedEventArgs e)
        {
            Usd.Text = string.Empty;
            Sell.Text = string.Empty;
            Buy.Text = string.Empty;
            Nb.Text = string.Empty;
            await GetCurrency();
        }

        private async Task GetCurrency()
        {
            HtmlWeb web = new();
            HtmlDocument htmlDoc = await web.LoadFromWebAsync(CurrencyUri);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'brief-info__r')]");
            string usdNodeText = nodes[5].InnerText;
            int separatorIndex = usdNodeText.LastIndexOf('.') - 1;
            string sell = usdNodeText[..separatorIndex];
            string buy = usdNodeText[separatorIndex..];

            Usd.Text = "USD";
            Sell.Text = sell;
            Buy.Text = buy;
        }
    }
}
