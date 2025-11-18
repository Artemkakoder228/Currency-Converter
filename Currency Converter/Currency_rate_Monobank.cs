using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Currency_rate_Monobank : Form
    {
        // ПРИБЕРИ ", string bank" з дужок, залиш тільки rates
        public Currency_rate_Monobank(List<StandardCurrencyRate> rates)
        {
            InitializeComponent();

            // Перевір назви лейблів (label_USD_Buy тощо) у своєму дизайнері!
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
    }
}