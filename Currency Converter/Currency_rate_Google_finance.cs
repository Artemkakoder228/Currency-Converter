using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Currency_rate_Google_finance : Form
    {
        public Currency_rate_Google_finance(List<StandardCurrencyRate> rates)
        {
            InitializeComponent();

            var usd = rates.FirstOrDefault(r => r.Ccy == "USD");
            if (usd != null)
            {
                label2.Text = usd.Buy.ToString("N2");
                label3.Text = usd.Sale.ToString("N2");
            }

            var eur = rates.FirstOrDefault(r => r.Ccy == "EUR");
            if (eur != null)
            {
                label17.Text = eur.Buy.ToString("N2");
                label18.Text = eur.Sale.ToString("N2");
            }
        }

        private void Currency_rate_Google_finance_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}