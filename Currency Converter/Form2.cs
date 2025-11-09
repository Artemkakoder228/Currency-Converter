using System;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Bank_list : Form
    {
        public string SelectedBank { get; private set; }

        public Bank_list()
        {
            InitializeComponent();
            SelectedBank = null;
        }

        private void btnPrivat_Click_1(object sender, EventArgs e)
        {
            SelectedBank = "Privat_bank";
            this.Close();
        }

        private void btnNbu_Click_1(object sender, EventArgs e)
        {
            SelectedBank = "NBU";
            this.Close();
        }

        private void btnOschad_Click_1(object sender, EventArgs e)
        {
            SelectedBank = "Monobank";
            this.Close();
        }
    }
}