using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Exchange_rate : Form
    {
        private List<StandardCurrencyRate> apiRates;
        private string bankName;
        public Exchange_rate(List<StandardCurrencyRate> rates, string bank)
        {
            InitializeComponent();
            this.apiRates = rates;
            this.bankName = bank;
            this.Text = $"Курси валют ({bankName})";
        }
        private void Exchange_rate_Load(object sender, EventArgs e)
        {
            if (apiRates == null || !apiRates.Any())
            {
                MessageBox.Show("Помилка: не вдалося отримати дані про курси.");
                this.Close();
                return;
            }

            StandardCurrencyRate usdRate = apiRates.FirstOrDefault(rate => rate.Ccy == "USD");
            StandardCurrencyRate eurRate = apiRates.FirstOrDefault(rate => rate.Ccy == "EUR");

            // --- Обробка USD ---
            if (usdRate != null)
            {
                if (bankName == "NBU")
                {
                    label2.Text = $"{usdRate.Buy:N2}";
                    label3.Text = "N/A";
                }
                else
                {
                    label2.Text = $"{usdRate.Buy:N2}";
                    label3.Text = $"{usdRate.Sale:N2}";
                }
            }
            else
            {
                label2.Text = "N/A";
                label3.Text = "N/A";
            }
            if (eurRate != null)
            {
                if (bankName == "NBU")
                {
                    label18.Text = $"{eurRate.Buy:N2}";
                    label17.Text = "N/A";
                }
                else
                {
                    label18.Text = $"{eurRate.Buy:N2}";
                    label17.Text = $"{eurRate.Sale:N2}";
                }
            }
            else
            {
                label18.Text = "N/A";
                label17.Text = "N/A";
            }
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