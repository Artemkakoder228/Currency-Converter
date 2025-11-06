using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private List<StandardCurrencyRate> apiRates;
        private string selectedBank;

        public Form1()
        {
            InitializeComponent();

            this.Opacity = 0;
            this.ShowInTaskbar = false;

            // Ініціалізація ComboBox (лише USD, EUR, UAH)
            comboBox1.Items.Add("USD");
            comboBox1.Items.Add("EUR");
            comboBox1.Items.Add("UAH");
            comboBox1.SelectedIndex = -1;

            comboBox2.Items.Add("USD");
            comboBox2.Items.Add("EUR");
            comboBox2.Items.Add("UAH");
            comboBox2.SelectedIndex = -1;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pbSwapCurrency.Width - 1, pbSwapCurrency.Height - 1);
            pbSwapCurrency.Region = new System.Drawing.Region(gp);

            RoundButton(Rates_Button, 60);
            RoundButton(Bank_btn, 60);
            RoundButton(btnConvert, 50);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            using (Bank_list bankForm = new Bank_list())
            {
                bankForm.ShowDialog();
                selectedBank = bankForm.SelectedBank;
            }

            if (string.IsNullOrEmpty(selectedBank))
            {
                Application.Exit();
                return;
            }

            this.Text = $"Конвертер валют ({selectedBank})";
            this.Opacity = 1;
            this.ShowInTaskbar = true;

            await LoadDataForBank();
        }

        private async Task LoadDataForBank()
        {
            lblDate.Text = "Курс актуальний на " + DateTime.Now.ToString("dd.MM.yyyy");
            apiRates = new List<StandardCurrencyRate>();
            lblResult.Text = "Завантаження...";

            try
            {
                switch (selectedBank)
                {
                    case "Privat":
                        await LoadPrivatData();
                        break;
                    case "NBU":
                        await LoadNbuData();
                        break;
                    case "Oschad":
                        LoadOschadData();
                        break;
                }
                apiRates.Add(new StandardCurrencyRate { Ccy = "UAH", Buy = 1, Sale = 1 });
                lblResult.Text = "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося завантажити курси: {ex.Message}");
                lblResult.Text = "Помилка";
            }
        }

        #region Методи завантаження даних

        private async Task LoadPrivatData()
        {
            string url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=11";
            string jsonResponse = await client.GetStringAsync(url);
            var privatRates = JsonSerializer.Deserialize<List<PrivatCurrencyRate>>(jsonResponse);

            foreach (var rate in privatRates.Where(r => r.Ccy == "USD" || r.Ccy == "EUR"))
            {
                apiRates.Add(new StandardCurrencyRate
                {
                    Ccy = rate.Ccy,
                    Buy = decimal.Parse(rate.Buy, CultureInfo.InvariantCulture),
                    Sale = decimal.Parse(rate.Sale, CultureInfo.InvariantCulture)
                });
            }
        }

        private async Task LoadNbuData()
        {
            string url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
            string jsonResponse = await client.GetStringAsync(url);
            var nbuRates = JsonSerializer.Deserialize<List<NbuCurrencyRate>>(jsonResponse);

            foreach (var rate in nbuRates.Where(r => r.Ccy == "USD" || r.Ccy == "EUR"))
            {
                apiRates.Add(new StandardCurrencyRate
                {
                    Ccy = rate.Ccy,
                    Buy = rate.Rate,
                    Sale = rate.Rate
                });
            }
        }

        private void LoadOschadData()
        {
            MessageBox.Show("Публічне API Ощадбанку недоступне. Завантажено умовні (mock) дані.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            apiRates.Add(new StandardCurrencyRate { Ccy = "USD", Buy = 40.00m, Sale = 40.50m });
            apiRates.Add(new StandardCurrencyRate { Ccy = "EUR", Buy = 41.00m, Sale = 41.60m });
        }

        #endregion

        private async void Bank_btn_Click(object sender, EventArgs e)
        {
            string oldBank = selectedBank;
            using (Bank_list bankForm = new Bank_list())
            {
                bankForm.ShowDialog();

                if (!string.IsNullOrEmpty(bankForm.SelectedBank) && bankForm.SelectedBank != oldBank)
                {
                    selectedBank = bankForm.SelectedBank;
                    this.Text = $"Конвертер валют ({selectedBank})";

                    lblResult.Text = "0.00";
                    txtAmount.Text = "";
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;

                    await LoadDataForBank();
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (apiRates == null || !apiRates.Any())
            {
                MessageBox.Show("Курси ще завантажуються, зачекайте секунду.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Будь ласка, введіть коректну суму.");
                return;
            }

            string fromCurrency = comboBox1.Text;
            string toCurrency = comboBox2.Text;

            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
            {
                MessageBox.Show("Будь ласка, оберіть обидві валюти.");
                return;
            }

            StandardCurrencyRate fromRate = apiRates.FirstOrDefault(r => r.Ccy == fromCurrency);
            StandardCurrencyRate toRate = apiRates.FirstOrDefault(r => r.Ccy == toCurrency);

            if (fromRate == null || toRate == null)
            {
                MessageBox.Show("Обрана валюта не знайдена. Можливо, цей банк її не підтримує.");
                return;
            }

            decimal fromBuyRate = fromRate.Buy;
            decimal toSaleRate = toRate.Sale;

            if (toSaleRate == 0)
            {
                MessageBox.Show("Курс продажу цільової валюти не може бути нульовим.");
                return;
            }

            decimal uahAmount = amount * fromBuyRate;
            decimal result = uahAmount / toSaleRate;

            lblResult.Text = $"{result:N2}";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCurrency = comboBox2.SelectedItem?.ToString();

            if (selectedCurrency == "USD")
                label5.Text = "$";
            else if (selectedCurrency == "EUR")
                label5.Text = "€";
            else if (selectedCurrency == "UAH")
                label5.Text = "₴";
            else
                label5.Text = "";
        }

        private void RoundButton(Button btn, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddArc(0, 0, radius, radius, 180, 90);
            gp.AddLine(radius, 0, btn.Width - radius, 0);
            gp.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            gp.AddLine(btn.Width, radius, btn.Width, btn.Height - radius);
            gp.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            gp.AddLine(btn.Width - radius, btn.Height, radius, btn.Height);
            gp.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            gp.CloseFigure();
            btn.Region = new System.Drawing.Region(gp);
        }

        // --- Ваші інші обробники подій ---
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void lblDate_Click(object sender, EventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e) { CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss"); }
        private void CurrentTime_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label7_Click_1(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e)
        {
            if (apiRates == null || !apiRates.Any())
            {
                MessageBox.Show("Дані про курси ще не завантажені.");
                return;
            }

            // БУЛО: Form2 ratesForm = new Form2(apiRates, selectedBank);
            // СТАЛО:
            Exchange_rate ratesForm = new Exchange_rate(apiRates, selectedBank);

            ratesForm.ShowDialog(); 
        }
    }

    //
    // --- КЛАСИ ДАНИХ (саме через їх відсутність були помилки) ---
    //

    public class StandardCurrencyRate
    {
        public string Ccy { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
    }

    public class PrivatCurrencyRate
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

    public class NbuCurrencyRate
    {
        [JsonPropertyName("cc")]
        public string Ccy { get; set; }
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
    }
}