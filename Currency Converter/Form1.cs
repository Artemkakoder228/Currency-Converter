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
using Microsoft.Data.Sqlite;
using System.IO;

namespace Currency_Converter
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private List<StandardCurrencyRate> apiRates;
        private string selectedBank;
        private readonly List<string> allowedCurrencies = new List<string> { "USD", "EUR", "PLN", "GBP", "CZK" };

        private string dbPath = Path.Combine(Application.StartupPath, "history.db");
        private string dbPath2 = Path.Combine(Application.StartupPath, "AllRateHistory.db");

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS ConversionHistory (
                    OperationID     INTEGER PRIMARY KEY AUTOINCREMENT,
                    OperationDate   DATETIME NOT_NULL,
                    BankName        TEXT NOT_NULL,
                    CurrencyFrom    TEXT NOT_NULL,
                    AmountFrom      REAL NOT_NULL,
                    CurrencyTo      TEXT NOT_NULL,
                    AmountTo        REAL NOT_NULL,
                    RateUsed        REAL NOT_NULL
                );";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = createTableQuery;
                    command.ExecuteNonQuery();
                }
            }

            using (var connection = new SqliteConnection($"Data Source={dbPath2}"))
            {
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS AllRateHistory (
                    HistoryID       INTEGER PRIMARY KEY AUTOINCREMENT,
                    FetchDate       DATETIME NOT NULL,
                    BankName        TEXT NOT NULL,
                    Currency        TEXT NOT NULL,
                    RateBuy         REAL NOT NULL,
                    RateSale        REAL NOT NULL
                );";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = createTableQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            this.Opacity = 0;
            this.ShowInTaskbar = false;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pbSwapCurrency.Width - 1, pbSwapCurrency.Height - 1);
            pbSwapCurrency.Region = new System.Drawing.Region(gp);

            RoundButton(Rates_Button, 60);
            RoundButton(Bank_btn, 60);
            RoundButton(btnConvert, 60);
            RoundButton(Histori_btn, 60);
        }

        private void AddOperationToHistory(string bank, string fromCcy, decimal fromAmount, string toCcy, decimal toAmount, decimal rate)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO ConversionHistory 
                (OperationDate, BankName, CurrencyFrom, AmountFrom, CurrencyTo, AmountTo, RateUsed) 
                VALUES 
                ($now, $bank, $fromCcy, $fromAmount, $toCcy, $toAmount, $rate)";
                command.Parameters.AddWithValue("$now", DateTime.Now);
                command.Parameters.AddWithValue("$bank", bank);
                command.Parameters.AddWithValue("$fromCcy", fromCcy);
                command.Parameters.AddWithValue("$fromAmount", fromAmount);
                command.Parameters.AddWithValue("$toCcy", toCcy);
                command.Parameters.AddWithValue("$toAmount", toAmount);
                command.Parameters.AddWithValue("$rate", rate);
                command.ExecuteNonQuery();
            }
        }

        private void AddRatesToHistory(List<StandardCurrencyRate> rates, string bankName)
        {
            if (rates == null || !rates.Any()) return;

            DateTime fetchTime = DateTime.Now;

            using (var connection = new SqliteConnection($"Data Source={dbPath2}"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                        @"INSERT INTO AllRateHistory 
                            (FetchDate, BankName, Currency, RateBuy, RateSale) 
                            VALUES 
                            ($now, $bank, $ccy, $buy, $sale)";
                    command.Parameters.AddWithValue("$now", fetchTime);
                    command.Parameters.AddWithValue("$bank", bankName);
                    command.Parameters.Add("$ccy", SqliteType.Text);
                    command.Parameters.Add("$buy", SqliteType.Real);
                    command.Parameters.Add("$sale", SqliteType.Real);

                    foreach (var rate in rates.Where(r => r.Ccy != "UAH"))
                    {
                        command.Parameters["$ccy"].Value = rate.Ccy;
                        command.Parameters["$buy"].Value = rate.Buy;
                        command.Parameters["$sale"].Value = rate.Sale;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
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

            InitializeDatabase();
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
                        MessageBox.Show(this, "Ви обрали НБУ. Зверніть увагу: НБУ надає лише один офіційний курс, тому 'купівля' та 'продаж' будуть однаковими.",
                                        "Інформація про курс",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        await LoadNbuData();
                        break;
                    case "Monobank":
                        await LoadMonoData();
                        break;
                }
                try
                {
                    AddRatesToHistory(apiRates, selectedBank);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Не вдалося зберегти історію курсів: {ex.Message}");
                }

                apiRates.Add(new StandardCurrencyRate { Ccy = "UAH", Buy = 1, Sale = 1, Name = "Українська гривня" });
                lblResult.Text = "0.00";
                PopulateCurrencyComboBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не вдалося завантажити курси: {ex.Message}");
                lblResult.Text = "Помилка";
            }
        }

        private void PopulateCurrencyComboBoxes()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            if (apiRates != null)
            {
                foreach (var rate in apiRates.OrderBy(r => r.Ccy))
                {
                    comboBox1.Items.Add(rate.Ccy);
                    comboBox2.Items.Add(rate.Ccy);
                }
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        #region Методи завантаження даних

        private async Task LoadPrivatData()
        {
            string url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=4";
            string jsonResponse = await client.GetStringAsync(url);
            var privatRates = JsonSerializer.Deserialize<List<PrivatCurrencyRate>>(jsonResponse);

            foreach (var rate in privatRates)
            {
                if (allowedCurrencies.Contains(rate.Ccy))
                {
                    apiRates.Add(new StandardCurrencyRate
                    {
                        Ccy = rate.Ccy,
                        Buy = decimal.Parse(rate.Buy, CultureInfo.InvariantCulture),
                        Sale = decimal.Parse(rate.Sale, CultureInfo.InvariantCulture),
                        Name = (rate.Ccy == "USD") ? "Долар США" : "Євро"
                    });
                }
            }
        }

        private async Task LoadNbuData()
        {
            string url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
            string jsonResponse = await client.GetStringAsync(url);
            var nbuRates = JsonSerializer.Deserialize<List<NbuCurrencyRate>>(jsonResponse);

            foreach (var rate in nbuRates.Where(r => allowedCurrencies.Contains(r.Ccy)))
            {
                apiRates.Add(new StandardCurrencyRate
                {
                    Ccy = rate.Ccy,
                    Buy = rate.Rate,
                    Sale = rate.Rate,
                    Name = rate.Txt
                });
            }
        }

        private async Task LoadMonoData()
        {
            string url = "https://api.monobank.ua/bank/currency";
            string jsonResponse = await client.GetStringAsync(url);
            var monoRates = JsonSerializer.Deserialize<List<MonoCurrencyRate>>(jsonResponse);

            if (allowedCurrencies.Contains("USD"))
            {
                var usdRate = monoRates.FirstOrDefault(r => r.CurrencyCodeA == 840 && r.CurrencyCodeB == 980);
                if (usdRate != null)
                {
                    apiRates.Add(new StandardCurrencyRate
                    {
                        Ccy = "USD",
                        Buy = usdRate.RateBuy,
                        Sale = usdRate.RateSell,
                        Name = "Долар США"
                    });
                }
            }

            if (allowedCurrencies.Contains("EUR"))
            {
                var eurRate = monoRates.FirstOrDefault(r => r.CurrencyCodeA == 978 && r.CurrencyCodeB == 980);
                if (eurRate != null)
                {
                    apiRates.Add(new StandardCurrencyRate
                    {
                        Ccy = "EUR",
                        Buy = eurRate.RateBuy,
                        Sale = eurRate.RateSell,
                        Name = "Євро"
                    });
                }
            }
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
                MessageBox.Show(this, "Курси ще завантажуються, зачекайте секунду.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show(this, "Будь ласка, введіть коректну суму.");
                return;
            }

            string fromCurrency = comboBox1.Text;
            string toCurrency = comboBox2.Text;

            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
            {
                MessageBox.Show(this, "Будь ласка, оберіть обидві валюти.");
                return;
            }

            StandardCurrencyRate fromRate = apiRates.FirstOrDefault(r => r.Ccy == fromCurrency);
            StandardCurrencyRate toRate = apiRates.FirstOrDefault(r => r.Ccy == toCurrency);

            if (fromRate == null || toRate == null)
            {
                MessageBox.Show(this, "Обрана валюта не знайдена. Можливо, цей банк її не підтримує.");
                return;
            }

            decimal fromBuyRate = fromRate.Buy;
            decimal toSaleRate = toRate.Sale;

            if (toSaleRate == 0)
            {
                MessageBox.Show(this, "Курс продажу цільової валюти не може бути нульовим.");
                return;
            }

            decimal uahAmount = amount * fromBuyRate;
            decimal result = uahAmount / toSaleRate;
            decimal actualRate = fromBuyRate / toSaleRate;
            lblResult.Text = $"{result:N2}";

            try
            {
                AddOperationToHistory(
                    selectedBank,
                    fromCurrency,
                    amount,
                    toCurrency,
                    result,
                    actualRate
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Не вдалося зберегти операцію в історію: {ex.Message}");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCurrency = comboBox2.SelectedItem?.ToString();

            switch (selectedCurrency)
            {
                case "USD":
                    label5.Text = "$";
                    break;
                case "EUR":
                    label5.Text = "€";
                    break;
                case "UAH":
                    label5.Text = "₴";
                    break;
                case "GBP":
                    label5.Text = "£";
                    break;
                case "JPY":
                    label5.Text = "¥";
                    break;
                case "PLN":
                    label5.Text = "zł";
                    break;
                case "CZK":
                    label5.Text = "Kč";
                    break;
                default:
                    label5.Text = "";
                    break;
            }
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
                MessageBox.Show(this, "Дані про курси ще не завантажені.");
                return;
            }

            if (selectedBank == "NBU")
            {
                try
                {
                    Form3 nbuRatesForm = new Form3(apiRates);
                    nbuRatesForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Не вдалося відкрити Form3. Переконайтеся, що вона існує. \n{ex.Message}");
                }
            }
            else
            {
                List<StandardCurrencyRate> ratesForForm = apiRates
                    .Select(r => new StandardCurrencyRate
                    {
                        Ccy = r.Ccy,
                        Buy = r.Buy,
                        Sale = r.Sale,
                        Name = r.Name
                    }).ToList();

                Exchange_rate ratesForm = new Exchange_rate(ratesForForm, selectedBank);
                ratesForm.ShowDialog();
            }
        }


        private void Histori_btn_Click(object sender, EventArgs e)
        {
            Form_History historyForm = new Form_History(dbPath, dbPath2);
            historyForm.ShowDialog();
        }

    }

    public class StandardCurrencyRate
    {
        public string Ccy { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
        public string Name { get; set; }
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
        [JsonPropertyName("txt")]
        public string Txt { get; set; }
    }

    public class MonoCurrencyRate
    {
        [JsonPropertyName("currencyCodeA")]
        public int CurrencyCodeA { get; set; }

        [JsonPropertyName("currencyCodeB")]
        public int CurrencyCodeB { get; set; }

        [JsonPropertyName("rateBuy")]
        public decimal RateBuy { get; set; }

        [JsonPropertyName("rateSell")]
        public decimal RateSell { get; set; }
    }
}