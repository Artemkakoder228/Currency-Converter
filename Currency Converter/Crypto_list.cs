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
    public partial class Crypto_list : Form
    {
        public string SelectedCrypto { get; private set; }
        public Crypto_list()
        {
            InitializeComponent();
            SelectedCrypto = null;
        }

        private void Crypto_list_Load(object sender, EventArgs e)
        {
        }

        private void btnBinance_Click(object sender, EventArgs e)
        {
            SelectedCrypto = "Binance";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOKX_Click(object sender, EventArgs e)
        {
            SelectedCrypto = "OKX";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBybit_Click(object sender, EventArgs e)
        {
            SelectedCrypto = "Bybit";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
