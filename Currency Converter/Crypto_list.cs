using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Currency_Converter
{
    public partial class Crypto_list : Form
    {
        public string SelectedCrypto { get; private set; }
        private bool isDeleteMode = false;

        public Crypto_list()
        {
            InitializeComponent();
            SelectedCrypto = null;
        }

        private void Crypto_list_Load(object sender, EventArgs e)
        {
            UpdateButtonVisual(AddCrypto1,
                Properties.Settings.Default.C1_Name,
                Properties.Settings.Default.C1_Path,
                pictureBox4);

            UpdateButtonVisual(AddCrypto2,
                Properties.Settings.Default.C2_Name,
                Properties.Settings.Default.C2_Path,
                pictureBox5);

            UpdateButtonVisual(AddCrypto3,
                Properties.Settings.Default.C3_Name,
                Properties.Settings.Default.C3_Path,
                pictureBox6);

            UpdateButtonVisual(AddCrypto4,
                Properties.Settings.Default.C4_Name,
                Properties.Settings.Default.C4_Path,
                pictureBox7);

            UpdateButtonVisual(AddCrypto5,
                Properties.Settings.Default.C5_Name,
                Properties.Settings.Default.C5_Path,
                pictureBox8);

            UpdateButtonVisual(AddCrypto6,
                Properties.Settings.Default.C6_Name,
                Properties.Settings.Default.C6_Path,
                pictureBox9);

            UpdateButtonVisual(AddCrypto7,
                Properties.Settings.Default.C7_Name,
                Properties.Settings.Default.C7_Path,
                pictureBox10);

            UpdateButtonVisual(AddCrypto8,
                Properties.Settings.Default.C8_Name,
                Properties.Settings.Default.C8_Path,
                pictureBox11);

            UpdateButtonVisual(AddCrypto9,
                Properties.Settings.Default.C9_Name,
                Properties.Settings.Default.C9_Path,
                pictureBox12);
        }

        private void OpenConverter(string provider)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [USER] Selected provider: {provider}");

            if (this.Owner is Crypto_Converter existingConverter)
            {
                existingConverter.ChangeExchange(provider);

                this.Close();
            }
            else
            {
                this.Hide();
                using (Crypto_Converter converter = new Crypto_Converter(provider))
                {
                    converter.ShowDialog();
                }
                this.Close();
            }
        }

        private void btnBinance_Click(object sender, EventArgs e) => OpenConverter("Binance");
        private void btnOKX_Click(object sender, EventArgs e) => OpenConverter("OKX");
        private void btnBybit_Click(object sender, EventArgs e) => OpenConverter("Bybit");

        private void Log(string tag, string message, ConsoleColor color)
        {
            Console.Write($"[{DateTime.Now:HH:mm:ss}] ");
            Console.ForegroundColor = color;
            Console.Write($"[{tag}] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        private void UpdateButtonVisual(Button btn, string name, string path, PictureBox plusIcon)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            btn.Text = "  " + name;

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                btn.Image = new Bitmap(Image.FromFile(path),
                    new Size(72, 62));
            }

            btn.Padding = new Padding(0, 0, 10, 0);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.ImageAlign = ContentAlignment.MiddleRight;
            btn.TextImageRelation = TextImageRelation.TextBeforeImage;
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;

            if (plusIcon != null)
            {
                plusIcon.Visible = false;
            }
        }

        private void ResetButton(Button btn, int id, PictureBox plusIcon)
        {
            SaveToSettings(id, "", "", "");

            btn.Text = "";
            btn.Image = null;
            btn.BackColor = Color.White;

            if (plusIcon != null)
            {
                plusIcon.Visible = true;
            }

            MessageBox.Show("Банк успішно видалено.");
        }

        private void SaveToSettings(int id, string name, string path, string api)
        {
            switch (id)
            {
                case 1:
                    Properties.Settings.Default.C1_Name = name;
                    Properties.Settings.Default.C1_Path = path;
                    Properties.Settings.Default.C1_Api = api;
                    break;
                case 2:
                    Properties.Settings.Default.C2_Name = name;
                    Properties.Settings.Default.C2_Path = path;
                    Properties.Settings.Default.C2_Api = api;
                    break;
                case 3:
                    Properties.Settings.Default.C3_Name = name;
                    Properties.Settings.Default.C3_Path = path;
                    Properties.Settings.Default.C3_Api = api;
                    break;
                case 4:
                    Properties.Settings.Default.C4_Name = name;
                    Properties.Settings.Default.C4_Path = path;
                    Properties.Settings.Default.C4_Api = api;
                    break;
                case 5:
                    Properties.Settings.Default.C5_Name = name;
                    Properties.Settings.Default.C5_Path = path;
                    Properties.Settings.Default.C5_Api = api;
                    break;
                case 6:
                    Properties.Settings.Default.C6_Name = name;
                    Properties.Settings.Default.C6_Path = path;
                    Properties.Settings.Default.C6_Api = api;
                    break;
                case 7:
                    Properties.Settings.Default.C7_Name = name;
                    Properties.Settings.Default.C7_Path = path;
                    Properties.Settings.Default.C7_Api = api;
                    break;
                case 8:
                    Properties.Settings.Default.C8_Name = name;
                    Properties.Settings.Default.C8_Path = path;
                    Properties.Settings.Default.C8_Api = api;
                    break;
                case 9:
                    Properties.Settings.Default.C9_Name = name;
                    Properties.Settings.Default.C9_Path = path;
                    Properties.Settings.Default.C9_Api = api;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private string GetBankNameFromSettings(int id)
        {
            switch (id)
            {
                case 1: return Properties.Settings.Default.C1_Name;
                case 2: return Properties.Settings.Default.C2_Name;
                case 3: return Properties.Settings.Default.C3_Name;
                case 4: return Properties.Settings.Default.C4_Name;
                case 5: return Properties.Settings.Default.C5_Name;
                case 6: return Properties.Settings.Default.C6_Name;
                case 7: return Properties.Settings.Default.C7_Name;
                case 8: return Properties.Settings.Default.C8_Name;
                case 9: return Properties.Settings.Default.C9_Name;
                default: return "";
            }
        }
        private void SelectExistingCrypto(string name)
        {
            SelectedCrypto = name;
            OpenConverter(name);
        }
        private void AddCrypto1_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto1, 1, "C1", pictureBox4);

        private void AddCrypto2_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto2, 2, "C2", pictureBox5);

        private void AddCrypto3_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto3, 3, "C3", pictureBox6);

        private void AddCrypto4_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto4, 4, "C4", pictureBox7);

        private void AddCrypto5_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto5, 5, "C5", pictureBox8);

        private void AddCrypto6_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto6, 6, "C6", pictureBox9);

        private void AddCrypto7_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto7, 7, "C7", pictureBox10);

        private void AddCrypto8_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto8, 8, "C8", pictureBox11);

        private void AddCrypto9_Click(object sender, EventArgs e) =>
            OnBankButtonClick(AddCrypto9, 9, "C9", pictureBox12);
        private void OnBankButtonClick(Button btn, int id, string pref, PictureBox plusIcon)
        {
            // Перевірка режиму видалення
            if (isDeleteMode)
            {
                ResetButton(btn, id, plusIcon);
                StopDeleteMode();
                return;
            }

            string savedName = GetBankNameFromSettings(id);

            // Якщо банк вже налаштований — обираємо його
            if (!string.IsNullOrEmpty(savedName))
            {
                SelectExistingCrypto(savedName);
            }
            // Якщо комірка порожня — відкриваємо форму додавання
            else
            {
                using (AddCrypto form = new AddCrypto())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SaveToSettings(id,
                            form.CryptoName,
                            form.SelectedImagePath,
                            form.CryptoApiUrl);

                        UpdateButtonVisual(btn,
                            form.CryptoName,
                            form.SelectedImagePath,
                            plusIcon);
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (isDeleteMode)
            {
                StopDeleteMode();
            }
            else
            {
                isDeleteMode = true;
                DeleteButton.BackColor = Color.Lime;
                SetButtonsColor(Color.RosyBrown);
                SetStatikButton(Color.Teal);
                btnBinance.Enabled = false;
                btnOKX.Enabled = false;
                btnBybit.Enabled = false;
            }
        }

        private void StopDeleteMode()
        {
            isDeleteMode = false;
            DeleteButton.BackColor = Color.White;
            SetButtonsColor(Color.White);
        }

        private void SetButtonsColor(Color color)
        {
            AddCrypto1.BackColor = color;
            AddCrypto2.BackColor = color;
            AddCrypto3.BackColor = color;
            AddCrypto4.BackColor = color;
            AddCrypto5.BackColor = color;
            AddCrypto6.BackColor = color;
            AddCrypto7.BackColor = color;
            AddCrypto8.BackColor = color;
            AddCrypto9.BackColor = color;
        }

        private void SetStatikButton(Color color)
        {
            btnBinance.BackColor = color;
            btnOKX.BackColor = color;
            btnBybit.BackColor = color;

            btnBinance.Enabled = true;
            btnOKX.Enabled = true;
            btnBybit.Enabled = true;
        } 
    }
}