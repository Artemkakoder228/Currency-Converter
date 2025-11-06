
namespace Currency_Converter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new System.Windows.Forms.Label();
            txtAmount = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            comboBox2 = new System.Windows.Forms.ComboBox();
            btnConvert = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            lblResult = new System.Windows.Forms.Label();
            timer1 = new System.Windows.Forms.Timer(components);
            CurrentTime = new System.Windows.Forms.Label();
            pbSwapCurrency = new System.Windows.Forms.PictureBox();
            Rates_Button = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            label6 = new System.Windows.Forms.Label();
            lblDate = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            Bank_btn = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pbSwapCurrency).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(57, 24);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(142, 65);
            label1.TabIndex = 0;
            label1.Text = "Сума";
            // 
            // txtAmount
            // 
            txtAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            txtAmount.Location = new System.Drawing.Point(57, 105);
            txtAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new System.Drawing.Size(1045, 61);
            txtAmount.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(57, 196);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(142, 65);
            label2.TabIndex = 2;
            label2.Text = "From";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            label3.Location = new System.Drawing.Point(724, 196);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(78, 65);
            label3.TabIndex = 3;
            label3.Text = "To";
            // 
            // comboBox1
            // 
            comboBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(57, 290);
            comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(413, 56);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(724, 290);
            comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(378, 56);
            comboBox2.TabIndex = 5;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // btnConvert
            // 
            btnConvert.BackColor = System.Drawing.Color.Turquoise;
            btnConvert.FlatAppearance.BorderSize = 0;
            btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnConvert.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            btnConvert.Location = new System.Drawing.Point(375, 426);
            btnConvert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new System.Drawing.Size(438, 105);
            btnConvert.TabIndex = 6;
            btnConvert.Text = "Конвертувати";
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += btnConvert_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.Gainsboro;
            label5.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.Turquoise;
            label5.Location = new System.Drawing.Point(467, 576);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(0, 65);
            label5.TabIndex = 8;
            label5.Click += label5_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.BackColor = System.Drawing.Color.Gainsboro;
            lblResult.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblResult.ForeColor = System.Drawing.SystemColors.ControlText;
            lblResult.Location = new System.Drawing.Point(545, 576);
            lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new System.Drawing.Size(0, 65);
            lblResult.TabIndex = 9;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // CurrentTime
            // 
            CurrentTime.AutoSize = true;
            CurrentTime.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            CurrentTime.Location = new System.Drawing.Point(964, 693);
            CurrentTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            CurrentTime.Name = "CurrentTime";
            CurrentTime.Size = new System.Drawing.Size(0, 48);
            CurrentTime.TabIndex = 11;
            CurrentTime.Click += CurrentTime_Click;
            // 
            // pbSwapCurrency
            // 
            pbSwapCurrency.BackColor = System.Drawing.Color.Transparent;
            pbSwapCurrency.ErrorImage = (System.Drawing.Image)resources.GetObject("pbSwapCurrency.ErrorImage");
            pbSwapCurrency.Image = (System.Drawing.Image)resources.GetObject("pbSwapCurrency.Image");
            pbSwapCurrency.Location = new System.Drawing.Point(533, 260);
            pbSwapCurrency.Name = "pbSwapCurrency";
            pbSwapCurrency.Size = new System.Drawing.Size(125, 119);
            pbSwapCurrency.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pbSwapCurrency.TabIndex = 12;
            pbSwapCurrency.TabStop = false;
            // 
            // Rates_Button
            // 
            Rates_Button.BackColor = System.Drawing.Color.Turquoise;
            Rates_Button.FlatAppearance.BorderSize = 0;
            Rates_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Rates_Button.ForeColor = System.Drawing.SystemColors.ControlText;
            Rates_Button.Location = new System.Drawing.Point(786, 20);
            Rates_Button.Name = "Rates_Button";
            Rates_Button.Size = new System.Drawing.Size(95, 69);
            Rates_Button.TabIndex = 13;
            Rates_Button.Text = "Курс";
            Rates_Button.UseVisualStyleBackColor = false;
            Rates_Button.Click += button1_Click;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label4.BackColor = System.Drawing.Color.Gainsboro;
            label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
            label4.Location = new System.Drawing.Point(12, 550);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(1119, 100);
            label4.TabIndex = 15;
            label4.Text = "Ви отримаєте";
            label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            label4.Click += label4_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ControlText;
            panel1.Location = new System.Drawing.Point(0, 687);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1147, 3);
            panel1.TabIndex = 16;
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(12, 697);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(50, 44);
            label6.TabIndex = 17;
            label6.Text = "📅";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            lblDate.Location = new System.Drawing.Point(62, 693);
            lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(0, 48);
            lblDate.TabIndex = 10;
            lblDate.Click += lblDate_Click;
            // 
            // label7
            // 
            label7.Location = new System.Drawing.Point(920, 697);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(44, 44);
            label7.TabIndex = 18;
            label7.Text = "🕑";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label7.Click += label7_Click_1;
            // 
            // Bank_btn
            // 
            Bank_btn.BackColor = System.Drawing.Color.Turquoise;
            Bank_btn.FlatAppearance.BorderSize = 0;
            Bank_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Bank_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            Bank_btn.Location = new System.Drawing.Point(898, 20);
            Bank_btn.Name = "Bank_btn";
            Bank_btn.Size = new System.Drawing.Size(95, 69);
            Bank_btn.TabIndex = 19;
            Bank_btn.Text = "Банк";
            Bank_btn.UseVisualStyleBackColor = false;
            Bank_btn.Click += Bank_btn_Click;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.Turquoise;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.ForeColor = System.Drawing.SystemColors.ControlText;
            button1.Location = new System.Drawing.Point(1007, 20);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(95, 69);
            button1.TabIndex = 20;
            button1.Text = "Історія";
            button1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1143, 750);
            Controls.Add(button1);
            Controls.Add(Bank_btn);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(panel1);
            Controls.Add(Rates_Button);
            Controls.Add(pbSwapCurrency);
            Controls.Add(CurrentTime);
            Controls.Add(lblDate);
            Controls.Add(lblResult);
            Controls.Add(label5);
            Controls.Add(btnConvert);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtAmount);
            Controls.Add(label1);
            Controls.Add(label4);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Currency Converter";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbSwapCurrency).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label CurrentTime;
        private System.Windows.Forms.PictureBox pbSwapCurrency;
        private System.Windows.Forms.Button Rates_Button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Bank_btn;
        private System.Windows.Forms.Button button1;
    }
}

