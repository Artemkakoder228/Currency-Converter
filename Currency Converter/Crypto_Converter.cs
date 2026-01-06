using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Currency_Converter
{
    public partial class Crypto_Converter : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private string currentExchange;
        private Timer timerUpdate;

        private Timer blinkTimer;
        private bool isLightOn = false;

        private string selectedCoin = "";
        private readonly List<string> ListForCryrto = new List<string>() { "BTC", "ETH", "BNB", "SOL", "XRP", "DOGE" };
        private const int MaxPoints = 100;
        private string dbPath = Path.Combine(Application.StartupPath, "crypto_history.db");

        private decimal lastPrice = 0;
        private int pointIndex = 0;
        private bool isOffline = false;

        private readonly Dictionary<int, int> coinImageMap = new Dictionary<int, int>
        {
            { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 6 }
        };

        public Crypto_Converter(string exchangeName)
        {
            InitializeComponent();
            this.currentExchange = exchangeName;
            InitDatabase();

            timerGraph.Tick += timerGraph_Tick;
            timerGraph.Interval = 1000;
            timerGraph.Start();
            this.Load += (s, e) => RoundControl(Controls.Find("pnlStatusLamp", true).FirstOrDefault());

            CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
            CurrentDate.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void RoundControl(Control ctrl)
        {
            if (ctrl == null) return;
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, ctrl.Width, ctrl.Height);
            ctrl.Region = new Region(gp);
        }

        private bool HasInternalConnection()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch { return false; }
        }
        private void InitBlinkTimer()
        {
            blinkTimer = new Timer();
            blinkTimer.Interval = 500;
            blinkTimer.Tick += (s, e) =>
            {
                var lamp = Controls.Find("pnlStatusLamp", true).FirstOrDefault();
                if (lamp == null) return;

                isLightOn = !isLightOn;
                bool hasNet = HasInternalConnection();

                if (hasNet)
                    lamp.BackColor = isLightOn ? Color.Lime : Color.Green;
                else
                    lamp.BackColor = isLightOn ? Color.Red : Color.Maroon;
            };
            blinkTimer.Start();
        }

        public void ChangeExchange(string newExchange)
        {
            this.currentExchange = newExchange;
            Log("SYSTEM", $"Exchange changed to: {currentExchange}", ConsoleColor.Cyan);

            if (chart1 != null)
                chart1.Series.Clear();

            lastPrice = 0;
            pointIndex = 0;
            _ = UpdateData(false);

            if (timerUpdate != null)
                timerUpdate.Start();
        }

        private void Log(string tag, string message, ConsoleColor color)
        {
            Console.Write($"[{DateTime.Now:HH:mm:ss}] ");
            Console.ForegroundColor = color;
            Console.Write($"[{tag}] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        private void InitDatabase()
        {
            if (!File.Exists(dbPath)) SQLiteConnection.CreateFile(dbPath);
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS History (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Exchange TEXT, Coin TEXT, Price REAL, 
                    Amount REAL, Result REAL, Date TEXT, Time TEXT)";

                string sqlCache = @"CREATE TABLE IF NOT EXISTS Cache (
                    Coin TEXT PRIMARY KEY, 
                    Price REAL, 
                    Exchange TEXT,
                    UpdateTime TEXT)";

                using (var command = new SQLiteCommand(sql, connection)) command.ExecuteNonQuery();
                using (var command = new SQLiteCommand(sqlCache, connection)) command.ExecuteNonQuery();
            }
        }

        private void SaveToCache(string coin, decimal price)
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    string sql = "INSERT OR REPLACE INTO Cache (Coin, Price, Exchange, UpdateTime) VALUES (@Coin, @Price, @Ex, @Time)";
                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Coin", coin);
                        cmd.Parameters.AddWithValue("@Price", (double)price);
                        cmd.Parameters.AddWithValue("@Ex", currentExchange);
                        cmd.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private decimal GetCachedPrice(string coin)
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    string sql = "SELECT Price FROM Cache WHERE Coin = @Coin";
                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Coin", coin);
                        var res = cmd.ExecuteScalar();
                        return res != null ? Convert.ToDecimal(res) : 0;
                    }
                }
            }
            catch { return 0; }
        }

        private void InitializeChartConfig()
        {
            if (chart1 == null) return;

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();
            chart1.BackColor = Color.FromArgb(25, 25, 35);

            ChartArea area = new ChartArea("MainArea") { BackColor = Color.FromArgb(25, 25, 35) };
            Font tinyFont = new Font("Segoe UI", 7f);

            area.AxisX.LabelStyle.Font = tinyFont;
            area.AxisY.LabelStyle.Font = tinyFont;
            area.AxisX.LabelStyle.ForeColor = Color.Gray;
            area.AxisY.LabelStyle.ForeColor = Color.Gray;
            area.AxisY.LabelStyle.Format = "F2";

            area.AxisX.MajorGrid.LineColor = Color.FromArgb(40, 40, 50);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(40, 40, 50);
            area.AxisY.IsStartedFromZero = false;

            chart1.ChartAreas.Add(area);

            if (timerUpdate == null)
            {
                timerUpdate = new Timer { Interval = 10000 }; // 10 секунд
                timerUpdate.Tick += async (s, e) => await UpdateData(isSilent: true);
                timerUpdate.Start(); // <--- ДОДАЙТЕ ЦЕЙ РЯДОК
            }
        }

        private void AddPointToChart(DateTime time, decimal price)
        {
            if (chart1.InvokeRequired)
            {
                chart1.Invoke(new MethodInvoker(() => AddPointToChart(time, price)));
                return;
            }

            if (chart1 == null || chart1.ChartAreas.Count == 0 || chart1.ChartAreas[0].AxisY == null) return;

            var area = chart1.ChartAreas[0];
            string timeStr = time.ToString("HH:mm:ss");

            if (lastPrice != 0)
            {
                string seriesName = "Segment" + pointIndex;
                Series segment = new Series(seriesName)
                {
                    ChartArea = "MainArea",
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                    Color = (price >= lastPrice) ? Color.LimeGreen : Color.Crimson,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6
                };
                chart1.Series.Add(segment);
                segment.Points.Add(new DataPoint(pointIndex - 1, (double)lastPrice));
                segment.Points.Add(new DataPoint(pointIndex, (double)price) { AxisLabel = timeStr });

                while (chart1.Series.Count > MaxPoints) chart1.Series.RemoveAt(0);
            }
            else
            {
                Series firstPoint = new Series("Start")
                {
                    ChartArea = "MainArea",
                    ChartType = SeriesChartType.Point,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6,
                    Color = Color.Gray
                };
                chart1.Series.Add(firstPoint);
                firstPoint.Points.AddXY(pointIndex, (double)price);
            }

            lastPrice = price;
            pointIndex++;

            var allPoints = chart1.Series.SelectMany(s => s.Points).ToList();
            if (allPoints.Count > 0)
            {
                double min = allPoints.Min(p => p.YValues[0]);
                double max = allPoints.Max(p => p.YValues[0]);
                double margin = (max - min) * 0.2;

                if (max - min <= 0) margin = (double)price * 0.001;

                area.AxisY.Minimum = Math.Floor(min - margin);
                area.AxisY.Maximum = Math.Ceiling(max + margin);
            }
            chart1.Invalidate();
        }

        private async Task UpdateData(bool isSilent)
        {
            if (string.IsNullOrEmpty(selectedCoin)) return;

            if (HasInternalConnection())
            {
                if (isOffline)
                {
                    Log("NETWORK", "Connection restored!", ConsoleColor.Green);
                    isOffline = false;
                }
                decimal price = await GetPriceFromAPI(selectedCoin, isSilent);
                if (price > 0)
                {
                    SaveToCache(selectedCoin, price);
                    AddPointToChart(DateTime.Now, price);
                }
                else
                {
                    if (!isOffline)
                    {
                        Log("NETWORK", "Connection lost! Using cached data.", ConsoleColor.Red);
                        MessageBox.Show("Відсутнє підключення. Використовуються збережені дані.");
                        isOffline = true;
                    }
                    decimal cachedPrice = GetCachedPrice(selectedCoin);
                    if (cachedPrice > 0) AddPointToChart(DateTime.Now, cachedPrice);
                }
            }
        }

        private async Task<decimal> GetPriceFromAPI(string coin, bool isSilent)
        {
            string url = currentExchange switch
            {
                "Binance" => $"https://api.binance.com/api/v3/ticker/price?symbol={coin}USDT",
                "Bybit" => $"https://api.bybit.com/v5/market/tickers?category=spot&symbol={coin}USDT",
                "OKX" => $"https://www.okx.com/api/v5/market/ticker?instId={coin}-USDT",
                _ => ""
            };

            if (string.IsNullOrEmpty(url))
            {
                for (int i = 1; i <= 9; i++)
                {
                    string nameInSettings = Properties.Settings.Default[$"C{i}_Name"]?.ToString();
                    if (nameInSettings == currentExchange)
                    {
                        url = Properties.Settings.Default[$"C{i}_Api"]?.ToString();
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(url)) return 0;

            url = url.Replace("{coin}", coin).Replace("{COIN}", coin);

            // 1. Отримуємо ціну
            decimal price = await GetPriceFromCustomAPI(url);

            // 2. ВСТАВЛЯЄМО ТУТ: Логіка виведення в консоль
            if (price > 0)
            {
                if (!isSilent)
                {
                    Log(currentExchange.ToUpper(), $"Курс завантажено: {price}", ConsoleColor.Green);
                }
                else
                {
                    // Визначаємо колір: зелений, якщо ціна виросла або така ж, червоний - якщо впала
                    ConsoleColor color = (price >= lastPrice) ? ConsoleColor.Green : ConsoleColor.Red;
                    Log("TICK", $"{coin}: {price} USD", color);
                }
            }

            return price;
        }

        private async Task<decimal> GetPriceFromCustomAPI(string url)
        {
            try
            {
                string response = await client.GetStringAsync(url);
                JToken token = JToken.Parse(response);
                decimal price = FindPriceInJson(token);

                if (price == 0) Log("API", "Ціну не знайдено в JSON", ConsoleColor.Yellow);
                return price;
            }
            catch (Exception ex)
            {
                Log("ERROR", $"Помилка API: {ex.Message}", ConsoleColor.Red);
                return 0;
            }
        }

        private decimal FindPriceInJson(JToken token)
        {
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
                return (decimal)token;

            if (token.Type == JTokenType.String && decimal.TryParse(token.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal val))
                return val;

            foreach (var child in token.Children())
            {
                if (child is JProperty prop)
                {
                    string name = prop.Name.ToLower();
                    if (name == "price" || name == "last" || name == "usd" || name == "rate")
                    {
                        var result = FindPriceInJson(prop.Value);
                        if (result != 0) return result;
                    }
                }
                var subResult = FindPriceInJson(child);
                if (subResult != 0) return subResult;
            }
            return 0;
        }
        private void Crypto_Converter_Load(object sender, EventArgs e)
        {
            InitializeChartConfig();
            InitBlinkTimer();
            Log("SYSTEM", $"Session started with: {currentExchange}", ConsoleColor.Cyan);
            UpdateCoinUI(-1);

            if (Controls.Find("comboBoxCoins", true).FirstOrDefault() is ComboBox combo)
            {
                combo.DataSource = ListForCryrto;
                combo.SelectedIndex = -1;
                combo.SelectedIndexChanged -= ComboCoins_SelectedIndexChanged;
                combo.SelectedIndexChanged += ComboCoins_SelectedIndexChanged;
            }

            if (timerUpdate != null) timerUpdate.Start();
        }

        private void ComboCoins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedIndex == -1) return;

            selectedCoin = combo.SelectedItem.ToString();
            UpdateCoinUI(combo.SelectedIndex);

            if (chart1 != null) chart1.Series.Clear();

            lastPrice = 0;
            pointIndex = 0;
            _ = UpdateData(false);
        }

        private void UpdateCoinUI(int selectedIndex)
        {
            if (Controls.Find("pictureBox7", true).FirstOrDefault() is PictureBox pb7) pb7.Visible = true;
            if (Controls.Find("pictureBox8", true).FirstOrDefault() is PictureBox pb8) pb8.Visible = true;

            for (int i = 1; i <= 6; i++)
            {
                if (Controls.Find($"pictureBox{i}", true).FirstOrDefault() is PictureBox pb) pb.Visible = false;
            }

            if (coinImageMap.TryGetValue(selectedIndex, out int targetPB))
            {
                if (Controls.Find($"pictureBox{targetPB}", true).FirstOrDefault() is PictureBox pb) pb.Visible = true;
            }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCoin)) { MessageBox.Show("Оберіть монету!"); return; }

            if (Controls.Find("txtAmount", true).FirstOrDefault() is TextBox txt &&
                decimal.TryParse(txt.Text.Replace(".", ","), out decimal amount))
            {
                decimal price = 0;
                if (HasInternalConnection()) price = await GetPriceFromAPI(selectedCoin, isSilent: true);
                else price = GetCachedPrice(selectedCoin);

                if (price > 0)
                {
                    decimal result = amount * price;
                    if (Controls.Find("lblResult", true).FirstOrDefault() is Label lbl) lbl.Text = $"{result:N2}";
                    SaveToDatabase(price, amount, result);
                    Log("USER", $"Converted {amount} {selectedCoin}", ConsoleColor.Yellow);
                }
            }
        }

        private void SaveToDatabase(decimal price, decimal amount, decimal result)
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    string sql = "INSERT INTO History (Exchange, Coin, Price, Amount, Result, Date, Time) VALUES (@Ex, @Coin, @Price, @Amt, @Res, @Date, @Time)";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Ex", currentExchange);
                        command.Parameters.AddWithValue("@Coin", selectedCoin);
                        command.Parameters.AddWithValue("@Price", (double)price);
                        command.Parameters.AddWithValue("@Amt", (double)amount);
                        command.Parameters.AddWithValue("@Res", (double)result);
                        command.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Time", DateTime.Now.ToString("HH:mm:ss"));
                        command.ExecuteNonQuery();
                    }
                }
                Log("DB", "Saved to SQLite", ConsoleColor.Blue);
            }
            catch (Exception ex) { Log("DB_ERROR", ex.Message, ConsoleColor.Red); }
        }

        private void button1_Click(object sender, EventArgs e) { new History_Crypto().Show(); }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timerUpdate != null) timerUpdate.Stop();
            Crypto_list list = new Crypto_list { Owner = this };
            list.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Crypto_currency_now secondForm = new Crypto_currency_now();
            secondForm.ShowDialog();
        }

        private void timerGraph_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}