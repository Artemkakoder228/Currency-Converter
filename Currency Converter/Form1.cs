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
            comboBox1.Items.Add("PLN");
            comboBox1.Items.Add("UAH");
            comboBox1.Items.Add("CZK");
            comboBox1.Items.Add("CHF");

            comboBox1.SelectedIndex = -1;

            comboBox2.Items.Add("USD");
            comboBox2.Items.Add("EUR");
            comboBox2.Items.Add("GBP");
            comboBox2.Items.Add("PLN");
            comboBox2.Items.Add("UAH");
            comboBox2.Items.Add("CZK");
            comboBox2.Items.Add("CHF");

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
            else if (selectedCurrency == "PLN")
            {
                label5.Text = "zł";
            }
            else if (selectedCurrency == "UAH")
            {
                label5.Text = "₴";
            }
            else if (selectedCurrency == "CHF")
            {
                label5.Text = "Fr";
            }
            else if (selectedCurrency == "CZK")
            {
                label5.Text = "Kč";
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

            int radius2 = 60;
            Button buttonToRound_2 = Rates_Button;

            System.Drawing.Drawing2D.GraphicsPath gg = new System.Drawing.Drawing2D.GraphicsPath();

            gg.AddArc(0, 0, radius2, radius2, 180, 90);
            gg.AddLine(radius2, 0, buttonToRound_2.Width - radius2, 0);
            gg.AddArc(buttonToRound_2.Width - radius2, 0, radius2, radius2, 270, 90);
            gg.AddLine(buttonToRound_2.Width, radius2, buttonToRound_2.Width, buttonToRound_2.Height - radius2);
            gg.AddArc(buttonToRound_2.Width - radius2, buttonToRound_2.Height - radius2, radius2, radius2, 0, 90);
            gg.AddLine(buttonToRound_2.Width - radius2, buttonToRound_2.Height, radius2, buttonToRound_2.Height);
            gg.AddArc(0, buttonToRound_2.Height - radius2, radius2, radius2, 90, 90);
            gg.CloseFigure();

            buttonToRound_2.Region = new System.Drawing.Region(gg);

            int radius3 = 60;
            Button buttonToRound_3 = Bank_btn;

            System.Drawing.Drawing2D.GraphicsPath gq = new System.Drawing.Drawing2D.GraphicsPath();

            gq.AddArc(0, 0, radius3, radius3, 180, 90);
            gq.AddLine(radius3, 0, buttonToRound_3.Width - radius3, 0);
            gq.AddArc(buttonToRound_3.Width - radius3, 0, radius3, radius3, 270, 90);
            gq.AddLine(buttonToRound_3.Width, radius3, buttonToRound_3.Width, buttonToRound_3.Height - radius3);
            gq.AddArc(buttonToRound_3.Width - radius3, buttonToRound_3.Height - radius3, radius3, radius3, 0, 90);
            gq.AddLine(buttonToRound_3.Width - radius3, buttonToRound_3.Height, radius3, buttonToRound_3.Height);
            gq.AddArc(0, buttonToRound_3.Height - radius3, radius3, radius3, 90, 90);
            gq.CloseFigure();

            buttonToRound_3.Region = new System.Drawing.Region(gq);
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

            CurrencyRate fromRate = apiRates.FirstOrDefault(r => r.Ccy == fromCurrency);
            CurrencyRate toRate = apiRates.FirstOrDefault(r => r.Ccy == toCurrency);

            if (fromRate == null || toRate == null)
            {
                MessageBox.Show("Обрана валюта не знайдена в API.");
                return;
            }

            if (!decimal.TryParse(fromRate.Buy, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal fromBuyRate) ||
                !decimal.TryParse(toRate.Sale, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal toSaleRate))
            {
                MessageBox.Show("Не вдалося розпізнати курс валют.");
                return;
            }
            if (toSaleRate == 0)
            {
                MessageBox.Show("Курс продажу цільової валюти не може бути нульовим.");
                return;
            }

            decimal uahAmount = amount * fromBuyRate;

            decimal result = uahAmount / toSaleRate;

            lblResult.Text = $"{result:N2}";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Exchange_rate settingsForm = new Exchange_rate();
            settingsForm.ShowDialog();
        }

        private void Bank_btn_Click(object sender, EventArgs e)
        {
            Bank_list settingsForm = new Bank_list ();
            settingsForm.ShowDialog();
        }
    }
}
