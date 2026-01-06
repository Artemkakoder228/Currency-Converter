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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (Bank_list listForm = new Bank_list())
            {
                // Чекаємо, поки користувач обере банк
                if (listForm.ShowDialog() == DialogResult.OK)
                {
                    string bank = listForm.SelectedBank;
                    using (Crypto_form mainForm = new Crypto_form(bank))
                    {
                        mainForm.ShowDialog();
                    }
                }
            }

            // Повертаємо Welcome на екран, коли всі інші вікна закриті
            // Додаємо перевірку, чи програма не перебуває в стані перезапуску
            if (Application.OpenForms.Count > 0)
            {
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            bool keepShowingCryptoList = true;

            while (keepShowingCryptoList)
            {
                using (Crypto_list listForm = new Crypto_list())
                {
                    DialogResult result_1 = listForm.ShowDialog();

                    if (result_1 == DialogResult.OK)
                    {
                        string bank = listForm.SelectedCrypto;
                        using (Crypto_form startForm = new Crypto_form(bank))
                        {
                            startForm.ShowDialog();
                        }
                        keepShowingCryptoList = false;
                    }
                    else if (result_1 == DialogResult.No)
                    {
                        using (AddCrypto addCryptoForm = new AddCrypto())
                        {
                            addCryptoForm.ShowDialog();
                        }
                    }
                    else
                    {
                        keepShowingCryptoList = false;
                    }
                }
            }
            this.Show();
        }
    }
}