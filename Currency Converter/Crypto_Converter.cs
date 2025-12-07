using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            // Дизайн - Темна тема
            chart1.BackColor = Color.FromArgb(30, 30, 40);

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.FromArgb(30, 30, 40);
            area.AxisX.LabelStyle.ForeColor = Color.White;
            area.AxisY.LabelStyle.ForeColor = Color.White;
            area.AxisX.LineColor = Color.Gray;
            area.AxisY.LineColor = Color.Gray;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(50, 50, 60);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(50, 50, 60);

            // Налаштування часу на осі X (тільки години та хвилини)
            area.AxisX.LabelStyle.Format = "HH:mm:ss";
            area.AxisX.IntervalType = DateTimeIntervalType.Seconds;

            // Автомасштаб ціни (щоб графік не притискався до верху чи низу)
            area.AxisY.IsStartedFromZero = false;

            chart1.ChartAreas.Add(area);

            Series series = new Series("Price");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            series.Color = Color.White;
            series.ChartArea = "MainArea";
            series.XValueType = ChartValueType.Time; // Важливо: вісь X це Час

            // Маркери (кружечки)
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;

            chart1.Series.Add(series);

            timerUpdate = new Timer();
            // ЗМІНА: Інтервал 5 секунд (5000 мс), щоб точки не наліплювались
            timerUpdate.Interval = 5000;
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

            timerUpdate.Stop();

            // Очищаємо графік перед завантаженням нової монети
            if (chart1.Series.IndexOf("Price") != -1)
                chart1.Series["Price"].Points.Clear();

            await LoadHistoryAndDraw(selectedCoin);

            timerUpdate.Start();
        }

        private async Task LoadHistoryAndDraw(string symbol)
        {
            try
            {
                // ЗМІНА: limit=15 (було 30). Завантажуємо менше історії -> більше простору.
                string url = $"https://api.binance.com/api/v3/klines?symbol={symbol}USDT&interval=1m&limit=15";
                string response = await client.GetStringAsync(url);
                var jsonArray = JArray.Parse(response);

                foreach (var item in jsonArray)
                {
                    long unixTime = (long)item[0];
                    decimal closePrice = decimal.Parse((string)item[4], System.Globalization.CultureInfo.InvariantCulture);
                    DateTime time = DateTimeOffset.FromUnixTimeMilliseconds(unixTime).LocalDateTime;

                    AddPointToChart(time, closePrice);
                }
            }
            catch (Exception ex)
            {
                // Ігноруємо помилки історії, просто почнемо малювати з поточного моменту
            }
        }

        private async void TimerUpdate_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCoin)) return;

            decimal price = await GetCryptoPrice(selectedCoin);
            if (price == 0) return;

            // Додаємо нову точку
            AddPointToChart(DateTime.Now, price);

            // ТУТ БУВ КОД ОНОВЛЕННЯ ЛЕЙБЛУ - ВИДАЛЕНО
        }

        private void AddPointToChart(DateTime time, decimal price)
        {
            if (chart1.Series.IndexOf("Price") == -1) return;

            var series = chart1.Series["Price"];
            Color pointColor = Color.White;

            // Визначаємо колір (Зелений/Червоний)
            if (series.Points.Count > 0)
            {
                double lastPrice = series.Points[series.Points.Count - 1].YValues[0];
                if ((double)price > lastPrice) pointColor = Color.Lime;
                else if ((double)price < lastPrice) pointColor = Color.Red;
                else pointColor = series.Points[series.Points.Count - 1].Color;
            }

            int index = series.Points.AddXY(time, price);
            series.Points[index].Color = pointColor;
            series.Points[index].MarkerColor = pointColor;

            // ЗМІНА: Тримаємо максимум 20 точок.
            // Старі точки видаляються, щоб графік "рухався" вліво.
            if (series.Points.Count > 20)
            {
                series.Points.RemoveAt(0);
            }

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
            ComboBox combo = Controls.ContainsKey("comboBoxCoins") ? (ComboBox)Controls["comboBoxCoins"] : null;
            TextBox txtAmount = Controls.ContainsKey("txtAmount") ? (TextBox)Controls["txtAmount"] : null;
            Label lblResult = Controls.ContainsKey("lblResult") ? (Label)Controls["lblResult"] : null;

            if (combo == null || txtAmount == null || lblResult == null || combo.SelectedItem == null) return;

            string amountString = txtAmount.Text.Replace(".", ",");
            if (!decimal.TryParse(amountString, out decimal amount)) { MessageBox.Show("Введіть число!"); return; }

            string coin = combo.SelectedItem.ToString();
            decimal price = await GetCryptoPrice(coin);

            if (price > 0) lblResult.Text = $"{amount} {coin} = ${amount * price:N2}";
        }

        private void btnCheckPrice_Click(object sender, EventArgs e) { }
    }
}