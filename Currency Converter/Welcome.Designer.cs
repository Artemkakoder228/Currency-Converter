namespace Currency_Converter
{
    partial class Welcome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            button1 = new System.Windows.Forms.Button();
            panel2 = new System.Windows.Forms.Panel();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            button2 = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            button1.FlatAppearance.BorderSize = 3;
            button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(0, 50, 50);
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(40, 40, 60);
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            button1.ForeColor = System.Drawing.Color.Aqua;
            button1.Location = new System.Drawing.Point(0, 0);
            button1.Margin = new System.Windows.Forms.Padding(5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(344, 179);
            button1.TabIndex = 0;
            button1.Text = "Банк";
            button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(50, 60, 110);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(button1);
            panel2.Location = new System.Drawing.Point(88, 266);
            panel2.Margin = new System.Windows.Forms.Padding(5);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(344, 179);
            panel2.TabIndex = 3;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new System.Drawing.Point(113, 22);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(112, 109);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.FromArgb(75, 55, 100);
            button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 0, 255);
            button2.FlatAppearance.BorderSize = 3;
            button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(0, 50, 50);
            button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(40, 40, 60);
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            button2.ForeColor = System.Drawing.Color.FromArgb(200, 0, 255);
            button2.Location = new System.Drawing.Point(0, 0);
            button2.Margin = new System.Windows.Forms.Padding(5);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(344, 179);
            button2.TabIndex = 4;
            button2.Text = "Криптобіржа";
            button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(50, 60, 110);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button2);
            panel1.Location = new System.Drawing.Point(602, 266);
            panel1.Margin = new System.Windows.Forms.Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(344, 179);
            panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(114, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(110, 109);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(54, 105);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(980, 65);
            label1.TabIndex = 6;
            label1.Text = "Вітаємо в застосунку \"Corrency Converter\"";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label2.ForeColor = System.Drawing.Color.Silver;
            label2.Location = new System.Drawing.Point(283, 486);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(478, 32);
            label2.TabIndex = 7;
            label2.Text = "Оберіть середовище для початку обміну";
            // 
            // Welcome
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(16F, 38F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(20, 20, 30);
            ClientSize = new System.Drawing.Size(1062, 582);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ForeColor = System.Drawing.Color.FromArgb(200, 0, 255);
            Margin = new System.Windows.Forms.Padding(5);
            Name = "Welcome";
            Text = "Welcome Page";
            Load += Welcome_Load;
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}