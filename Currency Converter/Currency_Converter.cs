using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Net.NetworkInformation;

namespace Currency_Converter
{
    public partial class Crypto_form : Form
    {
        private Timer blinkTimer;
        private bool isNetworkConnected = false;
        private bool isLightOn = false;

        private static readonly HttpClient client = new HttpClient();
        private List<StandardCurrencyRate> apiRates;

        private string selectedBank;

        private readonly List<string> listForNBU = new List<string> { "USD", "EUR", "PLN", "GBP", "CZK", "JPY" };
        private readonly List<string> listForOthers = new List<string> { "USD", "EUR" };

        private string dbPath = Path.Combine(Application.StartupPath, "history.db");
        private string dbPath2 = Path.Combine(Application.StartupPath, "AllRateHistory.db");
        private string dbPath3 = Path.Combine(Application.StartupPath, "history_rate.db");

        public Crypto_form(string bankName)
        {
            InitializeComponent();

            this.selectedBank = bankName;

            SetupUI();

            System.Net.ServicePointManager.SecurityProtocol =
                System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

            if (client.DefaultRequestHeaders.UserAgent.Count == 0)
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            }

            this.Opacity = 0;
            this.ShowInTaskbar = false;

            LogToConsole("SYSTEM", "UI components initialized successfully.", ConsoleColor.White);

            UpdateNetworkStatus(NetworkInterface.GetIsNetworkAvailable());
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(OnNetworkAvailabilityChanged);

            blinkTimer = new Timer();
            blinkTimer.Interval = 500;
            blinkTimer.Tick += BlinkTimer_Tick;
            blinkTimer.Start();

            timer1.Tick += timer1_Tick;
            timer1.Interval = 1000;
            timer1.Start();

            CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            LogToConsole("USER", $"User selected provider: {selectedBank}", ConsoleColor.Cyan);

            InitializeDatabase();

            this.Text = $"Конвертер валют ({selectedBank})";
            this.Opacity = 1;
            this.ShowInTaskbar = true;

            await LoadDataForBank();
        }

        private async Task LoadDataForBank()
        {
            lblDate.ForeColor = Color.Black;
            lblDate.Text = "Курс актуальний на " + DateTime.Now.ToString("dd.MM.yyyy");
            apiRates = new List<StandardCurrencyRate>();
            lblResult.Text = "Завантаження...";

            bool hasInternet = NetworkInterface.GetIsNetworkAvailable();

            if (hasInternet)
            {
                try
                {
                    LogToConsole("API", $"Starting data fetch for provider: {selectedBank}...", ConsoleColor.Blue);

                    switch (selectedBank)
                    {
                        case "GoogleFinance":
                            MessageBox.Show(this, "Завантаження ринкових курсів (Google Finance)...", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadGoogleFinanceData();
                            break;
                        case "NBU":
                            MessageBox.Show(this, "Ви обрали НБУ. Завантажуються всі офіційні курси.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadNbuData();
                            break;
                        case "Monobank":
                            await LoadMonoData();
                            break;
                        default:
                            await LoadGoogleFinanceData();
                            break;
                    }

                    if (apiRates.Count > 0)
                    {
                        LogToConsole("API", $"Received {apiRates.Count} currency pairs.", ConsoleColor.Green);
                        AddRatesToHistory(apiRates, selectedBank);
                        SaveRatesToCache(selectedBank, apiRates);
                    }
                }
                catch (Exception ex)
                {
                    LogToConsole("API_ERROR", $"Fetch error: {ex.Message}", ConsoleColor.Red);
                    hasInternet = false;
                }
            }
            else
            {
                LogToConsole("NETWORK", "No internet connection detected.", ConsoleColor.Red);
            }

            if (!hasInternet || apiRates.Count == 0)
            {
                MessageBox.Show(this,
                    "Відсутнє з'єднання з інтернетом або помилка банку.\nБуде завантажено останній збережений курс.",
                    "Увага: Офлайн режим",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                apiRates = LoadRatesFromCache(selectedBank);

                if (apiRates.Count == 0)
                {
                    lblResult.Text = "Немає даних";
                    LogToConsole("CRITICAL", "No data available (Network down + Cache empty).", ConsoleColor.DarkRed);
                    MessageBox.Show("Немає збережених даних для цього банку в історії.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!apiRates.Any(r => r.Ccy == "UAH"))
            {
                apiRates.Add(new StandardCurrencyRate { Ccy = "UAH", Buy = 1, Sale = 1, Name = "Українська гривня" });
            }

            lblResult.Text = "0.00";
            PopulateCurrencyComboBoxes();
            LogToConsole("UI", "UI updated with new rates.", ConsoleColor.White);
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

        private void LogToConsole(string tag, string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now:HH:mm:ss}] [{tag}] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        private void SetupUI()
        {
            pictureBox15.Visible = false; pictureBox12.Visible = false; pictureBox1.Visible = false;
            pictureBox14.Visible = false; pictureBox13.Visible = false; pictureBox11.Visible = false;
            pictureBox10.Visible = false; pictureBox16.Visible = false; pictureBox21.Visible = false;
            pictureBox18.Visible = false; pictureBox19.Visible = false; pictureBox9.Visible = false;
            pictureBox20.Visible = false; pictureBox17.Visible = false;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            try
            {
                if (Controls.ContainsKey("Rates_Button")) RoundButton(Rates_Button, 60);
                if (Controls.ContainsKey("Bank_btn")) RoundButton(Bank_btn, 60);
                if (Controls.ContainsKey("btnConvert")) RoundButton(btnConvert, 60);
                if (Controls.ContainsKey("Histori_btn")) RoundButton(Histori_btn, 60);
                if (Controls.ContainsKey("pnlStatusLamp")) RoundButton(pnlStatusLamp, 60);
            }
            catch { }
        }

        private void RoundButton(Control btn, int radius)
        {
            if (btn == null) return;
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
        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action<bool>(UpdateNetworkStatus), e.IsAvailable);
            else
                UpdateNetworkStatus(e.IsAvailable);
        }

        private void UpdateNetworkStatus(bool isAvailable)
        {
            isNetworkConnected = isAvailable;
            lblNetworkStatus.Text = "Статус мережі";
            if (isAvailable)
                LogToConsole("NETWORK", "Connection established. Status: ONLINE", ConsoleColor.Green);
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                LogToConsole("NETWORK", "CONNECTION LOST! Status: OFFLINE", ConsoleColor.White);
                Console.ResetColor();
            }
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            isLightOn = !isLightOn;
            if (isNetworkConnected)
                pnlStatusLamp.BackColor = isLightOn ? Color.Lime : Color.Green;
            else
                pnlStatusLamp.BackColor = isLightOn ? Color.Red : Color.Maroon;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        #region Data Load Methods
        private async Task LoadGoogleFinanceData()
        {
            string url = "https://api.exchangerate-api.com/v4/latest/UAH";
            LogToConsole("HTTP", $"Sending GET Request: {url}", ConsoleColor.Cyan);
            string jsonResponse = await client.GetStringAsync(url);
            var googleData = JsonSerializer.Deserialize<GoogleApiResponse>(jsonResponse);

            if (googleData != null && googleData.Rates != null)
            {
                foreach (string currencyCode in listForOthers)
                {
                    if (googleData.Rates.ContainsKey(currencyCode))
                    {
                        decimal rateFromUah = googleData.Rates[currencyCode];
                        decimal normalRate = (rateFromUah != 0) ? 1.0m / rateFromUah : 0;
                        apiRates.Add(new StandardCurrencyRate
                        {
                            Ccy = currencyCode,
                            Buy = normalRate,
                            Sale = normalRate,
                            Name = GetCurrencyName(currencyCode)
                        });
                    }
                }
            }
        }

        private async Task LoadNbuData()
        {
            string url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
            string jsonResponse = await client.GetStringAsync(url);
            var nbuRates = JsonSerializer.Deserialize<List<NbuCurrencyRate>>(jsonResponse);
            foreach (var rate in nbuRates.Where(r => listForNBU.Contains(r.Ccy)))
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
            var isoCodes = new Dictionary<int, string> { { 840, "USD" }, { 978, "EUR" }, { 985, "PLN" }, { 826, "GBP" }, { 203, "CZK" }, { 392, "JPY" } };

            foreach (var rate in monoRates)
            {
                if (rate.CurrencyCodeB == 980 && isoCodes.ContainsKey(rate.CurrencyCodeA))
                {
                    string ccyStr = isoCodes[rate.CurrencyCodeA];
                    if (listForOthers.Contains(ccyStr))
                    {
                        apiRates.Add(new StandardCurrencyRate
                        {
                            Ccy = ccyStr,
                            Buy = rate.RateBuy,
                            Sale = rate.RateSell,
                            Name = GetCurrencyName(ccyStr)
                        });
                    }
                }
            }
        }

        private string GetCurrencyName(string code)
        {
            switch (code)
            {
                case "USD": return "Долар США";
                case "EUR": return "Євро";
                case "PLN": return "Польський злотий";
                case "GBP": return "Фунт стерлінгів";
                case "CZK": return "Чеська крона";
                case "JPY": return "Японська єна";
                default: return code;
            }
        }
        #endregion

        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string q = @"CREATE TABLE IF NOT EXISTS ConversionHistory (OperationID INTEGER PRIMARY KEY AUTOINCREMENT, OperationDate DATETIME NOT NULL, BankName TEXT, CurrencyFrom TEXT, AmountFrom REAL, CurrencyTo TEXT, AmountTo REAL, RateUsed REAL);";
                    using (var command = connection.CreateCommand()) { command.CommandText = q; command.ExecuteNonQuery(); }
                }
                using (var connection = new SqliteConnection($"Data Source={dbPath2}"))
                {
                    connection.Open();
                    string q = @"CREATE TABLE IF NOT EXISTS AllRateHistory (HistoryID INTEGER PRIMARY KEY AUTOINCREMENT, FetchDate DATETIME NOT NULL, BankName TEXT, Currency TEXT, RateBuy REAL, RateSale REAL);";
                    using (var command = connection.CreateCommand()) { command.CommandText = q; command.ExecuteNonQuery(); }
                }
                using (var connection = new SqliteConnection($"Data Source={dbPath3}"))
                {
                    connection.Open();
                    string q = @"CREATE TABLE IF NOT EXISTS RateCache (BankName TEXT, Currency TEXT, RateBuy REAL, RateSale REAL, FetchDate DATETIME);";
                    using (var command = connection.CreateCommand()) { command.CommandText = q; command.ExecuteNonQuery(); }
                }
            }
            catch (Exception ex) { LogToConsole("DB_ERROR", ex.Message, ConsoleColor.Red); }
        }

        private void SaveRatesToCache(string bankName, List<StandardCurrencyRate> rates)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath3}"))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM RateCache WHERE BankName = $bank";
                        command.Parameters.AddWithValue("$bank", bankName);
                        command.ExecuteNonQuery();

                        command.CommandText = @"INSERT INTO RateCache (BankName, Currency, RateBuy, RateSale, FetchDate) VALUES ($bank, $ccy, $buy, $sale, $date)";
                        command.Parameters.Add("$ccy", SqliteType.Text);
                        command.Parameters.Add("$buy", SqliteType.Real);
                        command.Parameters.Add("$sale", SqliteType.Real);
                        command.Parameters.AddWithValue("$date", DateTime.Now);

                        foreach (var rate in rates)
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
            catch (Exception ex) { LogToConsole("ERROR", "Cache write error: " + ex.Message, ConsoleColor.Red); }
        }

        private List<StandardCurrencyRate> LoadRatesFromCache(string bankName)
        {
            var cachedRates = new List<StandardCurrencyRate>();
            DateTime? cacheDate = null;
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath3}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT Currency, RateBuy, RateSale, FetchDate FROM RateCache WHERE BankName = $bank";
                    command.Parameters.AddWithValue("$bank", bankName);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cachedRates.Add(new StandardCurrencyRate
                            {
                                Ccy = reader.GetString(0),
                                Buy = reader.GetDecimal(1),
                                Sale = reader.GetDecimal(2),
                                Name = GetCurrencyName(reader.GetString(0))
                            });
                            if (cacheDate == null) cacheDate = reader.GetDateTime(3);
                        }
                    }
                }
            }
            catch { }

            if (cacheDate != null)
            {
                lblDate.Text = "ОФЛАЙН: " + cacheDate.Value.ToString("dd.MM.yyyy HH:mm");
                lblDate.ForeColor = Color.Red;
            }
            return cachedRates;
        }

        private void AddOperationToHistory(string bank, string fromCcy, decimal fromAmount, string toCcy, decimal toAmount, decimal rate)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO ConversionHistory (OperationDate, BankName, CurrencyFrom, AmountFrom, CurrencyTo, AmountTo, RateUsed) VALUES ($now, $bank, $fromCcy, $fromAmount, $toCcy, $toAmount, $rate)";
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
            catch (Exception ex) { LogToConsole("ERROR", "History save failed: " + ex.Message, ConsoleColor.Red); }
        }

        private void AddRatesToHistory(List<StandardCurrencyRate> rates, string bankName)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath2}"))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = @"INSERT INTO AllRateHistory (FetchDate, BankName, Currency, RateBuy, RateSale) VALUES ($now, $bank, $ccy, $buy, $sale)";
                        command.Parameters.AddWithValue("$now", DateTime.Now);
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
            catch { }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (apiRates == null || !apiRates.Any()) { MessageBox.Show(this, "Курси ще завантажуються."); return; }
            if (!decimal.TryParse(txtAmount.Text, out decimal amount)) { MessageBox.Show(this, "Введіть коректну суму."); return; }
            string fromCurrency = comboBox1.Text;
            string toCurrency = comboBox2.Text;

            StandardCurrencyRate fromRate = apiRates.FirstOrDefault(r => r.Ccy == fromCurrency);
            StandardCurrencyRate toRate = apiRates.FirstOrDefault(r => r.Ccy == toCurrency);

            if (fromRate == null || toRate == null || toRate.Sale == 0) return;

            decimal uahAmount = amount * fromRate.Buy;
            decimal result = uahAmount / toRate.Sale;

            lblResult.Text = $"{result:N2}";

            AddOperationToHistory(selectedBank, fromCurrency, amount, toCurrency, result, fromRate.Buy / toRate.Sale);
        }

        private void Bank_btn_Click(object sender, EventArgs e)
        {
            using (Bank_list bankForm = new Bank_list())
            {
                if (bankForm.ShowDialog() == DialogResult.OK)
                {
                    string oldBank = selectedBank;
                    selectedBank = bankForm.SelectedBank;
                    if (selectedBank != oldBank)
                    {
                        txtAmount.Text = "";
                        lblResult.Text = "0.00";
                        LoadDataForBank();
                        this.Text = $"Конвертер валют ({selectedBank})";
                    }
                }
            }
        }

        private void Histori_btn_Click(object sender, EventArgs e)
        {
            using (Form_History historyForm = new Form_History(dbPath, dbPath2)) { historyForm.ShowDialog(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (apiRates == null || !apiRates.Any()) return;
            switch (selectedBank)
            {
                case "NBU": using (Currency_rate_NBU f = new Currency_rate_NBU(apiRates)) { f.ShowDialog(); } break;
                case "GoogleFinance": using (Currency_rate_Google_finance f = new Currency_rate_Google_finance(apiRates)) { f.ShowDialog(); } break;
                case "Monobank": using (Currency_rate_Monobank f = new Currency_rate_Monobank(apiRates)) { f.ShowDialog(); } break;
                default: using (Currency_rate_Google_finance f = new Currency_rate_Google_finance(apiRates)) { f.ShowDialog(); } break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => UpdateCurrencyImage(comboBox1.Text, true);
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) => UpdateCurrencyImage(comboBox2.Text, false);

        private void UpdateCurrencyImage(string currency, bool isLeft)
        {
            pictureBox15.Visible = false; pictureBox12.Visible = false; pictureBox1.Visible = false;
            pictureBox14.Visible = false; pictureBox13.Visible = false; pictureBox11.Visible = false;
            pictureBox10.Visible = false; pictureBox16.Visible = false; pictureBox21.Visible = false;
            pictureBox18.Visible = false; pictureBox19.Visible = false; pictureBox9.Visible = false;
            pictureBox20.Visible = false; pictureBox17.Visible = false;

            if (isLeft)
            {
                if (currency == "UAH") { pictureBox15.Visible = true; label13.Text = "UAH"; }
                else if (currency == "USD") { pictureBox12.Visible = true; label13.Text = "USD"; }
                else if (currency == "EUR") { pictureBox1.Visible = true; label13.Text = "EUR"; }
                else if (currency == "JPY") { pictureBox14.Visible = true; label13.Text = "JPY"; }
                else if (currency == "CZK") { pictureBox13.Visible = true; label13.Text = "CZK"; }
                else if (currency == "PLN") { pictureBox11.Visible = true; label13.Text = "PLN"; }
                else if (currency == "GBP") { pictureBox10.Visible = true; label13.Text = "GBP"; }
            }
            else
            {
                if (currency == "UAH") { pictureBox16.Visible = true; label14.Text = "UAH"; }
                else if (currency == "USD") { pictureBox21.Visible = true; label14.Text = "USD"; }
                else if (currency == "EUR") { pictureBox18.Visible = true; label14.Text = "EUR"; }
                else if (currency == "JPY") { pictureBox9.Visible = true; label14.Text = "JPY"; }
                else if (currency == "CZK") { pictureBox19.Visible = true; label14.Text = "CZK"; }
                else if (currency == "PLN") { pictureBox20.Visible = true; label14.Text = "PLN"; }
                else if (currency == "GBP") { pictureBox17.Visible = true; label14.Text = "GBP"; }
            }
        }

        private void label7_Click(object sender, EventArgs e) { }
        private void lblDate_Click(object sender, EventArgs e) { }
        private void CurrentTime_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label7_Click_1(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label5_Click_1(object sender, EventArgs e) { }
    }

    public class StandardCurrencyRate { public string Ccy { get; set; } public decimal Buy { get; set; } public decimal Sale { get; set; } public string Name { get; set; } }
    public class GoogleApiResponse { [JsonPropertyName("base")] public string Base { get; set; } [JsonPropertyName("date")] public string Date { get; set; } [JsonPropertyName("rates")] public Dictionary<string, decimal> Rates { get; set; } }
    public class NbuCurrencyRate { [JsonPropertyName("cc")] public string Ccy { get; set; } [JsonPropertyName("rate")] public decimal Rate { get; set; } [JsonPropertyName("txt")] public string Txt { get; set; } }
    public class MonoCurrencyRate { [JsonPropertyName("currencyCodeA")] public int CurrencyCodeA { get; set; } [JsonPropertyName("currencyCodeB")] public int CurrencyCodeB { get; set; } [JsonPropertyName("rateBuy")] public decimal RateBuy { get; set; } [JsonPropertyName("rateSell")] public decimal RateSell { get; set; } }
}