using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Currency_Converter.Form1;

namespace Currency_Converter
{
    public partial class Exchange_rate : Form
    {

        private static readonly HttpClient client = new HttpClient();

        private List<CurrencyRate> apiRates;

        public Exchange_rate()
        {
            InitializeComponent();
        }

        public class CurrencyRate
        {
            [JsonPropertyName("ccy")]
            public string Ccy { get; set; }

            [JsonPropertyName("base_ccy")]
            public string BaseCcy { get; set; }

            [JsonPropertyName("buy")]
            public string Buy { get; set; }

            [JsonPropertyName("sale")]
            public string Sale { get; set; }
        }

        private async void Exchange_rate_Load(object sender, EventArgs e)
        {
            string url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=11";

            try
            {
                string jsonResponse = await client.GetStringAsync(url);

                apiRates = JsonSerializer.Deserialize<List<CurrencyRate>>(jsonResponse);

                apiRates.Add(new CurrencyRate { Ccy = "UAH", BaseCcy = "UAH", Buy = "1", Sale = "1" });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося завантажити курси: {ex.Message}");
            }

            CurrencyRate usdRate = apiRates.FirstOrDefault(rate => rate.Ccy == "USD");
            CurrencyRate eurRate = apiRates.FirstOrDefault(rate => rate.Ccy == "EUR");

            decimal buyRate = decimal.Parse(usdRate.Buy, CultureInfo.InvariantCulture);
            decimal saleRate = decimal.Parse(usdRate.Sale, CultureInfo.InvariantCulture);
            label2.Text = $"{buyRate:N2}";
            label3.Text = $"{saleRate:N2}";

            decimal buyRateEur = decimal.Parse(eurRate.Buy, CultureInfo.InvariantCulture);
            decimal saleRateEur = decimal.Parse(eurRate.Sale, CultureInfo.InvariantCulture);
            label18.Text = $"{buyRateEur:N2}";
            label17.Text = $"{saleRateEur:N2}";


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
