using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Crypto_currency_now : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private Timer updateTimer;
        private Dictionary<string, decimal> lastPrices = new Dictionary<string, decimal>();

        private Label[] nameLabels;
        private Label[] priceLabels;
        private Label[] percentLabels;
        private Panel[] borderPanels;

        private readonly string[] coins = { "BTC", "ETH", "BNB", "SOL", "XRP", "DOGE" };

        public Crypto_currency_now()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private async void Crypto_currency_now_Load(object sender, EventArgs e)
        {
            InitControlArrays();
            InitTimer();
            await UpdateAllCryptoData();
        }

        private void InitControlArrays()
        {
            nameLabels = new Label[] { label1, label4, label7, label10, label13, label16 };
            priceLabels = new Label[] { label2, label5, label8, label11, label14, label17 };
            percentLabels = new Label[] { label3, label6, label9, label12, label15, label18 };

            borderPanels = new Panel[] {
                BorderPanel, BorderPanel_1, BorderPanel_2, BorderPanel_3,
                BorderPanel_4, BorderPanel_5,
            };
        }

        private void InitTimer()
        {
            updateTimer = new Timer { Interval = 5000 };
            updateTimer.Tick += async (s, e) => await UpdateAllCryptoData();
            updateTimer.Start();
        }

        private async Task UpdateAllCryptoData()
        {
            try
            {
                string response = await client.GetStringAsync("https://api.binance.com/api/v3/ticker/price");
                JArray jArray = JArray.Parse(response);

                for (int i = 0; i < coins.Length; i++)
                {
                    string symbol = coins[i] + "USDT";
                    var token = jArray.FirstOrDefault(x => x["symbol"].ToString() == symbol);

                    if (token != null)
                    {
                        decimal currentPrice = (decimal)token["price"];
                        UpdateCoinUI(i, coins[i], currentPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Помилка API: " + ex.Message);
            }
        }

        private void UpdateCoinUI(int index, string coinName, decimal price)
        {
            if (index >= priceLabels.Length) return;

            Color themeColor = Color.White;
            string prefix = "";

            if (lastPrices.ContainsKey(coinName))
            {
                decimal oldPrice = lastPrices[coinName];
                decimal diff = price - oldPrice;
                decimal percent = (oldPrice != 0) ? (diff / oldPrice) * 100 : 0;

                if (price > oldPrice)
                {
                    themeColor = Color.LimeGreen;
                    prefix = "+";
                }
                else if (price < oldPrice)
                {
                    themeColor = Color.Crimson;
                }

                percentLabels[index].Text = $"{prefix}{percent:F2}%";
            }

            priceLabels[index].Text = price > 1 ? $"${price:N2}" : $"${price:F4}";

            nameLabels[index].ForeColor = themeColor;
            priceLabels[index].ForeColor = themeColor;
            percentLabels[index].ForeColor = themeColor;

            borderPanels[index].BackColor = themeColor;

            lastPrices[coinName] = price;
        }
    }
}