using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;        
using System.Text.Json.Serialization; 
using System.Linq;             
using System.Globalization;

namespace Currency_Converter
{
    public partial class Form1 : Form
    {

        private static readonly HttpClient client = new HttpClient();

        private List<CurrencyRate> apiRates;
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("USD");
            comboBox1.Items.Add("EUR");
            comboBox1.Items.Add("GBP");
            comboBox1.Items.Add("JPY");
            comboBox1.Items.Add("UAH");

            comboBox1.SelectedIndex = -1;

            comboBox2.Items.Add("USD");
            comboBox2.Items.Add("EUR");
            comboBox2.Items.Add("GBP");
            comboBox2.Items.Add("JPY");
            comboBox2.Items.Add("UAH");

            comboBox2.SelectedIndex = -1;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pbSwapCurrency.Width - 1, pbSwapCurrency.Height - 1);
            pbSwapCurrency.Region = new System.Drawing.Region(gp);
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCurrency = comboBox2.SelectedItem.ToString();

            if (selectedCurrency == "USD")
            {
                label5.Text = "$";
            }
            else if (selectedCurrency == "EUR")
            {
                label5.Text = "€";
            }
            else if (selectedCurrency == "GBP")
            {
                label5.Text = "£";
            }
            else if (selectedCurrency == "JPY")
            {
                label5.Text = "¥";
            }
            else if (selectedCurrency == "UAH")
            {
                label5.Text = "₴";
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            lblDate.Text = "Курс актуальний на " + DateTime.Now.ToString("dd.MM.yyyy");

            int radius = 50;
            Button buttonToRound = btnConvert;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddArc(0, 0, radius, radius, 180, 90);
            gp.AddLine(radius, 0, buttonToRound.Width - radius, 0);
            gp.AddArc(buttonToRound.Width - radius, 0, radius, radius, 270, 90);
            gp.AddLine(buttonToRound.Width, radius, buttonToRound.Width, buttonToRound.Height - radius);
            gp.AddArc(buttonToRound.Width - radius, buttonToRound.Height - radius, radius, radius, 0, 90);
            gp.AddLine(buttonToRound.Width - radius, buttonToRound.Height, radius, buttonToRound.Height);
            gp.AddArc(0, buttonToRound.Height - radius, radius, radius, 90, 90);
            gp.CloseFigure();

            buttonToRound.Region = new System.Drawing.Region(gp);

            string url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";

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
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void CurrentTime_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (apiRates == null)
            {
                MessageBox.Show("Курси ще завантажуються, зачекайте секунду.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Будь ласка, введіть коректну суму.");
                return;
            }

            string fromCurrency = comboBox1.Text.Split(' ')[0];
            string toCurrency = comboBox2.Text.Split(' ')[0];

            decimal result = 0;

            CurrencyRate fromRate = apiRates.FirstOrDefault(r => r.Ccy == fromCurrency);
            CurrencyRate toRate = apiRates.FirstOrDefault(r => r.Ccy == toCurrency);

            if (fromRate == null || toRate == null)
            {
                MessageBox.Show("Обрана валюта не знайдена в API.");
                return;
            }

            decimal fromBuy = decimal.Parse(fromRate.Buy, CultureInfo.InvariantCulture);
            decimal fromSale = decimal.Parse(fromRate.Sale, CultureInfo.InvariantCulture);
            decimal toBuy = decimal.Parse(toRate.Buy, CultureInfo.InvariantCulture);
            decimal toSale = decimal.Parse(toRate.Sale, CultureInfo.InvariantCulture);

            decimal uahAmount = amount * fromBuy;

            result = uahAmount / toSale;

            lblResult.Text = $"{result:N2}";
        }
    }
}
