using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Додано для зручної роботи з даними
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json.Linq;

namespace Currency_Converter
{
    public partial class Crypto_Converter : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private string currentExchange;
        private readonly List<string> ListForCryrto = new List<string>() { "BTC", "ETH", "BNB", "SOL", "XRP", "DOGE" };

        private Timer timerUpdate;
        private string selectedCoin = "";

        public Crypto_Converter(string exchangeName)
        {
            InitializeComponent();

            this.currentExchange = exchangeName;
            this.Text = $"Крипто Конвертер ({currentExchange})";

            System.Net.ServicePointManager.SecurityProtocol =
               System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

            if (client.DefaultRequestHeaders.UserAgent.Count == 0)
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            }

            InitializeChartConfig();
        }

        private void InitializeChartConfig()
        {
            if (chart1 == null) return;

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Дизайн
            chart1.BackColor = Color.FromArgb(30, 30, 40);

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.FromArgb(30, 30, 40);
            area.AxisX.LabelStyle.ForeColor = Color.White;
            area.AxisY.LabelStyle.ForeColor = Color.White;
            area.AxisX.LineColor = Color.Gray;
            area.AxisY.LineColor = Color.Gray;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(50, 50, 60);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(50, 50, 60);

            // ВАЖЛИВО: Налаштовуємо вісь X як ЧАС
            area.AxisX.LabelStyle.Format = "HH:mm:ss";
            area.AxisX.IntervalType = DateTimeIntervalType.Seconds;
            area.AxisY.IsStartedFromZero = false; // Автомасштаб ціни

            chart1.ChartAreas.Add(area);

            Series series = new Series("Price");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = Color.White;
            series.ChartArea = "MainArea";

            // Вказуємо, що по X у нас час (DateTime), а не просто текст
            series.XValueType = ChartValueType.Time;

            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;

            chart1.Series.Add(series);

            timerUpdate = new Timer();
            timerUpdate.Interval = 2000; // Оновлення кожні 2 сек
            timerUpdate.Tick += TimerUpdate_Tick;
        }

        private void Crypto_Converter_Load(object sender, EventArgs e)
        {
            if (Controls.ContainsKey("comboBoxCoins"))
            {
                ComboBox combo = (ComboBox)Controls["comboBoxCoins"];
                combo.DataSource = ListForCryrto;
                combo.SelectedIndexChanged -= ComboCoins_SelectedIndexChanged;
                combo.SelectedIndexChanged += ComboCoins_SelectedIndexChanged;
            }
        }

        private async void ComboCoins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            if (combo.SelectedItem == null) return;

            selectedCoin = combo.SelectedItem.ToString();

            timerUpdate.Stop(); // Зупиняємо таймер на час завантаження

            // 1. Очищаємо старий графік
            if (chart1.Series.IndexOf("Price") != -1)
                chart1.Series["Price"].Points.Clear();

            // 2. ЗАВАНТАЖУЄМО ІСТОРІЮ (щоб графік був відразу)
            await LoadHistoryAndDraw(selectedCoin);

            // 3. Запускаємо таймер для нових даних
            timerUpdate.Start();
        }

        // Новий метод для завантаження історії
        private async Task LoadHistoryAndDraw(string symbol)
        {
            try
            {
                // Беремо останні 30 свічок (хвилинний інтервал)
                string url = $"https://api.binance.com/api/v3/klines?symbol={symbol}USDT&interval=1m&limit=30";
                string response = await client.GetStringAsync(url);
                var jsonArray = JArray.Parse(response);

                var series = chart1.Series["Price"];

                foreach (var item in jsonArray)
                {
                    // Binance віддає час у Unix Timestamp (мілісекунди)
                    long unixTime = (long)item[0];
                    decimal closePrice = decimal.Parse((string)item[4], System.Globalization.CultureInfo.InvariantCulture);

                    DateTime time = DateTimeOffset.FromUnixTimeMilliseconds(unixTime).LocalDateTime;

                    AddPointToChart(time, closePrice);
                }
            }
            catch (Exception ex)
            {
                // Якщо помилка завантаження історії - нічого страшного, таймер почне малювати з нуля
                MessageBox.Show("Не вдалося завантажити історію: " + ex.Message);
            }
        }

        private async void TimerUpdate_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCoin)) return;

            decimal price = await GetCryptoPrice(selectedCoin);
            if (price == 0) return;

            // Додаємо ПОТОЧНИЙ час і ціну
            AddPointToChart(DateTime.Now, price);

            // Оновлюємо текст
            if (Controls.ContainsKey("lblResult"))
            {
                ((Label)Controls["lblResult"]).Text = $"1 {selectedCoin} = ${price:N2}";
                var points = chart1.Series["Price"].Points;
                if (points.Count > 0)
                    ((Label)Controls["lblResult"]).ForeColor = points[points.Count - 1].Color;
            }
        }

        // Універсальний метод додавання точки (з кольорами і прокруткою)
        private void AddPointToChart(DateTime time, decimal price)
        {
            if (chart1.Series.IndexOf("Price") == -1) return;

            var series = chart1.Series["Price"];
            Color pointColor = Color.White;

            // Визначаємо колір
            if (series.Points.Count > 0)
            {
                double lastPrice = series.Points[series.Points.Count - 1].YValues[0];
                if ((double)price > lastPrice) pointColor = Color.Lime;
                else if ((double)price < lastPrice) pointColor = Color.Red;
                else pointColor = series.Points[series.Points.Count - 1].Color;
            }

            // Додаємо точку (DateTime, Value)
            int index = series.Points.AddXY(time, price);

            series.Points[index].Color = pointColor;
            series.Points[index].MarkerColor = pointColor;

            // Обмежуємо кількість точок, щоб графік "йшов у бік"
            // Тримаємо, наприклад, 40 точок (30 історії + 10 нових)
            if (series.Points.Count > 40)
            {
                series.Points.RemoveAt(0);
            }

            // Примусово оновлюємо вигляд
            chart1.Invalidate();
        }

        private async Task<decimal> GetCryptoPrice(string symbol)
        {
            string url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}USDT";
            try
            {
                string response = await client.GetStringAsync(url);
                var json = JObject.Parse(response);
                return decimal.Parse((string)json["price"], System.Globalization.CultureInfo.InvariantCulture);
            }
            catch { return 0; }
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            // Ваш код конвертації залишається без змін
            ComboBox combo = Controls.ContainsKey("comboBoxCoins") ? (ComboBox)Controls["comboBoxCoins"] : null;
            TextBox txtAmount = Controls.ContainsKey("txtAmount") ? (TextBox)Controls["txtAmount"] : null;
            Label lblResult = Controls.ContainsKey("lblResult") ? (Label)Controls["lblResult"] : null;

            if (combo == null || txtAmount == null || lblResult == null || combo.SelectedItem == null) return;

            string amountString = txtAmount.Text.Replace(".", ",");
            if (!decimal.TryParse(amountString, out decimal amount)) { MessageBox.Show("Число!"); return; }

            string coin = combo.SelectedItem.ToString();
            decimal price = await GetCryptoPrice(coin);

            if (price > 0) lblResult.Text = $"{amount} {coin} = ${amount * price:N2}";
        }

        private void btnCheckPrice_Click(object sender, EventArgs e) { }
    }
}