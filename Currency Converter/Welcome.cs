using System;
using System.Windows.Forms;

namespace Currency_Converter
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        // Кнопка БАНКИ
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Bank_list listForm = new Bank_list())
            {
                if (listForm.ShowDialog() == DialogResult.OK)
                {
                    string bank = listForm.SelectedBank;
                    // Тепер це працює, бо ми виправили Crypto_form
                    using (Crypto_form mainForm = new Crypto_form(bank))
                    {
                        mainForm.ShowDialog();
                    }
                }
            }
            this.Show();
        }

        // Кнопка КРИПТА
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Crypto_list listForm = new Crypto_list())
            {
                if (listForm.ShowDialog() == DialogResult.OK)
                {
                    string exchange = listForm.SelectedCrypto;
                    // Тепер це працює, бо ми виправили Crypto_Converter
                    using (Crypto_Converter cryptoForm = new Crypto_Converter(exchange))
                    {
                        cryptoForm.ShowDialog();
                    }
                }
            }
            this.Show();
        }

        private void Welcome_Load(object sender, EventArgs e) { }
    }
}