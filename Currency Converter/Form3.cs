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
    public partial class Form3 : Form
    {
 
        private List<StandardCurrencyRate> _nbuRates;


        public Form3(List<StandardCurrencyRate> rates)
        {
            InitializeComponent();
            _nbuRates = rates;
        }

 
        private void Form3_Load(object sender, EventArgs e)
        {
            PopulateRates();
        }
        private void PopulateRates()
        {
            if (_nbuRates == null) return;
            var usd = _nbuRates.FirstOrDefault(r => r.Ccy == "USD");
            if (usd != null)
            {
                label3.Text = usd.Ccy;
                label4.Text = usd.Sale.ToString("N2");
            }
            var eur = _nbuRates.FirstOrDefault(r => r.Ccy == "EUR");
            if (eur != null)
            {
                label6.Text = eur.Ccy;
                label5.Text = eur.Sale.ToString("N2");
            }

            var pln = _nbuRates.FirstOrDefault(r => r.Ccy == "PLN");
            if (pln != null)
            {
                label9.Text = pln.Ccy;
                label8.Text = pln.Sale.ToString("N2");
            }

            var gbp = _nbuRates.FirstOrDefault(r => r.Ccy == "GBP");
            if (gbp != null)
            {
                label12.Text = gbp.Ccy;
                label11.Text = gbp.Sale.ToString("N2");
            }

            var czk = _nbuRates.FirstOrDefault(r => r.Ccy == "CZK");
            if (czk != null)
            {
                label15.Text = czk.Ccy;
                label14.Text = czk.Sale.ToString("N2");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}