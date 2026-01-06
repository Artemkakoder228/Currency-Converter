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
    public partial class AddCrypto : Form
    {
        public string CryptoName { get; set; }
        public string CryptoApiUrl { get; set; }
        public string SelectedImagePath { get; set; }
        public AddCrypto()
        {
            InitializeComponent();
            panel3.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Використовуємо вже існуючу властивість:
                    SelectedImagePath = openFileDialog.FileName;
                    pictureBox1.Image = new Bitmap(SelectedImagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    panel3.Visible = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Заповніть назву та API посилання!");
                return;
            }

            CryptoName = textBox1.Text;
            CryptoApiUrl = textBox2.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
