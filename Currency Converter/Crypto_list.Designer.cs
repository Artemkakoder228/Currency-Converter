namespace Currency_Converter
{
    partial class Crypto_list
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Crypto_list));
            label2 = new System.Windows.Forms.Label();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel2 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            btnBinance = new System.Windows.Forms.Button();
            btnBybit = new System.Windows.Forms.Button();
            btnOKX = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label2.Location = new System.Drawing.Point(16, 19);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(378, 70);
            label2.TabIndex = 81;
            label2.Text = "Оберіть біржу";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new System.Drawing.Point(277, 365);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(73, 70);
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 78;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = System.Drawing.Color.White;
            pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new System.Drawing.Point(277, 254);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(73, 65);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 77;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.Color.White;
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(277, 139);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(73, 67);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 76;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.SystemColors.ControlText;
            panel2.Location = new System.Drawing.Point(-28, 480);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(462, 3);
            panel2.TabIndex = 74;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(19, 32);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(0, 70);
            label1.TabIndex = 73;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ControlText;
            panel1.Location = new System.Drawing.Point(-28, 105);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(462, 3);
            panel1.TabIndex = 72;
            // 
            // btnBinance
            // 
            btnBinance.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            btnBinance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnBinance.Location = new System.Drawing.Point(45, 128);
            btnBinance.Name = "btnBinance";
            btnBinance.Size = new System.Drawing.Size(319, 90);
            btnBinance.TabIndex = 75;
            btnBinance.Text = "Binance";
            btnBinance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnBinance.UseVisualStyleBackColor = true;
            btnBinance.Click += btnBinance_Click;
            // 
            // btnBybit
            // 
            btnBybit.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            btnBybit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnBybit.Location = new System.Drawing.Point(45, 355);
            btnBybit.Name = "btnBybit";
            btnBybit.Size = new System.Drawing.Size(319, 90);
            btnBybit.TabIndex = 80;
            btnBybit.Text = "Bybit";
            btnBybit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnBybit.UseVisualStyleBackColor = true;
            btnBybit.Click += btnBybit_Click;
            // 
            // btnOKX
            // 
            btnOKX.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            btnOKX.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnOKX.Location = new System.Drawing.Point(45, 241);
            btnOKX.Name = "btnOKX";
            btnOKX.Size = new System.Drawing.Size(319, 90);
            btnOKX.TabIndex = 79;
            btnOKX.Text = "OKX";
            btnOKX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnOKX.UseVisualStyleBackColor = true;
            btnOKX.Click += btnOKX_Click;
            // 
            // Crypto_list
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(406, 514);
            Controls.Add(label2);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(btnBinance);
            Controls.Add(btnBybit);
            Controls.Add(btnOKX);
            Name = "Crypto_list";
            Text = "Form_crypto";
            Load += Crypto_list_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBinance;
        private System.Windows.Forms.Button btnBybit;
        private System.Windows.Forms.Button btnOKX;
    }
}