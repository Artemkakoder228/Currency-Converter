using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Currency_Converter
{
    public partial class Bank_list : Form
    {
        public string SelectedBank { get; private set; }
        private bool isDeleteMode = false;

        public Bank_list()
        {
            InitializeComponent();
            SelectedBank = null;
            this.Load += Bank_list_Load;
        }

        private void Bank_list_Load(object sender, EventArgs e)
        {
            // Ініціалізація візуального стану всіх кнопок при завантаженні форми
            UpdateButtonVisual(btnAddBank,
                Properties.Settings.Default.B1_Name,
                Properties.Settings.Default.B1_Path,
                pictureBox4);

            UpdateButtonVisual(btnAddBank1,
                Properties.Settings.Default.B2_Name,
                Properties.Settings.Default.B2_Path,
                pictureBox6);

            UpdateButtonVisual(btnAddBank2,
                Properties.Settings.Default.B3_Name,
                Properties.Settings.Default.B3_Path,
                pictureBox7);

            UpdateButtonVisual(btnAddBank3,
                Properties.Settings.Default.B4_Name,
                Properties.Settings.Default.B4_Path,
                pictureBox8);

            UpdateButtonVisual(btnAddBank4,
                Properties.Settings.Default.B5_Name,
                Properties.Settings.Default.B5_Path,
                pictureBox9);

            UpdateButtonVisual(btnAddBank5,
                Properties.Settings.Default.B6_Name,
                Properties.Settings.Default.B6_Path,
                pictureBox10);

            UpdateButtonVisual(btnAddBank6,
                Properties.Settings.Default.B7_Name,
                Properties.Settings.Default.B7_Path,
                pictureBox11);

            UpdateButtonVisual(btnAddBank7,
                Properties.Settings.Default.B8_Name,
                Properties.Settings.Default.B8_Path,
                pictureBox12);

            UpdateButtonVisual(btnAddBank8,
                Properties.Settings.Default.B9_Name,
                Properties.Settings.Default.B9_Path,
                pictureBox13);
        }

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
                SelectExistingBank(savedName);
            }
            // Якщо комірка порожня — відкриваємо форму додавання
            else
            {
                using (AddBank form = new AddBank())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SaveToSettings(id,
                            form.BankName,
                            form.SelectedImagePath,
                            form.BankApiUrl);

                        UpdateButtonVisual(btn,
                            form.BankName,
                            form.SelectedImagePath,
                            plusIcon);
                    }
                }
            }
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
                    Properties.Settings.Default.B1_Name = name;
                    Properties.Settings.Default.B1_Path = path;
                    Properties.Settings.Default.B1_Api = api;
                    break;
                case 2:
                    Properties.Settings.Default.B2_Name = name;
                    Properties.Settings.Default.B2_Path = path;
                    Properties.Settings.Default.B2_Api = api;
                    break;
                case 3:
                    Properties.Settings.Default.B3_Name = name;
                    Properties.Settings.Default.B3_Path = path;
                    Properties.Settings.Default.B3_Api = api;
                    break;
                case 4:
                    Properties.Settings.Default.B4_Name = name;
                    Properties.Settings.Default.B4_Path = path;
                    Properties.Settings.Default.B4_Api = api;
                    break;
                case 5:
                    Properties.Settings.Default.B5_Name = name;
                    Properties.Settings.Default.B5_Path = path;
                    Properties.Settings.Default.B5_Api = api;
                    break;
                case 6:
                    Properties.Settings.Default.B6_Name = name;
                    Properties.Settings.Default.B6_Path = path;
                    Properties.Settings.Default.B6_Api = api;
                    break;
                case 7:
                    Properties.Settings.Default.B7_Name = name;
                    Properties.Settings.Default.B7_Path = path;
                    Properties.Settings.Default.B7_Api = api;
                    break;
                case 8:
                    Properties.Settings.Default.B8_Name = name;
                    Properties.Settings.Default.B8_Path = path;
                    Properties.Settings.Default.B8_Api = api;
                    break;
                case 9:
                    Properties.Settings.Default.B9_Name = name;
                    Properties.Settings.Default.B9_Path = path;
                    Properties.Settings.Default.B9_Api = api;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private string GetBankNameFromSettings(int id)
        {
            switch (id)
            {
                case 1: return Properties.Settings.Default.B1_Name;
                case 2: return Properties.Settings.Default.B2_Name;
                case 3: return Properties.Settings.Default.B3_Name;
                case 4: return Properties.Settings.Default.B4_Name;
                case 5: return Properties.Settings.Default.B5_Name;
                case 6: return Properties.Settings.Default.B6_Name;
                case 7: return Properties.Settings.Default.B7_Name;
                case 8: return Properties.Settings.Default.B8_Name;
                case 9: return Properties.Settings.Default.B9_Name;
                default: return "";
            }
        }

        private void SelectExistingBank(string name)
        {
            SelectedBank = name;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Обробники кліків для кожної кнопки
        private void btnAddBank_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank, 1, "B1", pictureBox4);

        private void btnAddBank1_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank1, 2, "B2", pictureBox6);

        private void btnAddBank2_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank2, 3, "B3", pictureBox7);

        private void btnAddBank3_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank3, 4, "B4", pictureBox8);

        private void btnAddBank4_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank4, 5, "B5", pictureBox9);

        private void btnAddBank5_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank5, 6, "B6", pictureBox10);

        private void btnAddBank6_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank6, 7, "B7", pictureBox11);

        private void btnAddBank7_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank7, 8, "B8", pictureBox12);

        private void btnAddBank8_Click(object sender, EventArgs e) =>
            OnBankButtonClick(btnAddBank8, 9, "B9", pictureBox13);

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (isDeleteMode)
            {
                StopDeleteMode();
            }
            else
            {
                isDeleteMode = true;
                DeleteButton.BackColor = Color.Tomato;
                SetButtonsColor(Color.LightGray);
                SetButtomsColorPermament(Color.RosyBrown);
                btnGooglefinance.Enabled = false;
                btnNbu.Enabled = false;
                btnOschad.Enabled = false;
            }
        }

        private void StopDeleteMode()
        {
            isDeleteMode = false;
            DeleteButton.BackColor = Color.White;
            SetButtonsColor(Color.White);
            SetButtomsColorPermament(Color.White);
            btnGooglefinance.Enabled = true;
            btnNbu.Enabled = true;
            btnOschad.Enabled = true;
        }

        private void SetButtonsColor(Color color)
        {
            btnAddBank.BackColor = color;
            btnAddBank1.BackColor = color;
            btnAddBank2.BackColor = color;
            btnAddBank3.BackColor = color;
            btnAddBank4.BackColor = color;
            btnAddBank5.BackColor = color;
            btnAddBank6.BackColor = color;
            btnAddBank7.BackColor = color;
            btnAddBank8.BackColor = color;
        }

        private void SetButtomsColorPermament(Color color)
        {
            btnGooglefinance.BackColor = color;
            btnNbu.BackColor = color;
            btnOschad.BackColor = color;
        }

        // Методи для статичних кнопок вибору джерела
        private void btnPrivat_Click_1(object sender, EventArgs e) =>
            SelectExistingBank("GoogleFinance");

        private void btnNbu_Click_1(object sender, EventArgs e) =>
            SelectExistingBank("NBU");

        private void btnOschad_Click_1(object sender, EventArgs e) =>
            SelectExistingBank("Monobank");
    }
}