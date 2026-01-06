using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Currency_Converter
{
    public partial class AddBank : Form
    {
        public string BankName { get; set; }
        public string SelectedImagePath { get; set; }
        public Image BankLogo { get; set; }
        private string selectedIconPath = "";
        public string BankApiUrl { get; set; }

        public AddBank()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedIconPath = openFileDialog.FileName;
                    pictureBox1.Image = new Bitmap(selectedIconPath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Заповніть назву та API посилання!");
                return;
            }

            BankName = textBox2.Text;
            BankApiUrl = textBox1.Text;
            SelectedImagePath = selectedIconPath;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddBank_Load(object sender, EventArgs e)
        {

        }
    }
}