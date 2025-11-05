
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
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            lblResult = new System.Windows.Forms.Label();
            lblDate = new System.Windows.Forms.Label();
            timer1 = new System.Windows.Forms.Timer(components);
            CurrentTime = new System.Windows.Forms.Label();
            pbSwapCurrency = new System.Windows.Forms.PictureBox();
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
            btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnConvert.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            btnConvert.Location = new System.Drawing.Point(364, 442);
            btnConvert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new System.Drawing.Size(438, 105);
            btnConvert.TabIndex = 6;
            btnConvert.Text = "Конвертувати";
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += btnConvert_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(57, 592);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(241, 65);
            label4.TabIndex = 7;
            label4.Text = "Результат";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label5.ForeColor = System.Drawing.Color.Turquoise;
            label5.Location = new System.Drawing.Point(680, 592);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(41, 65);
            label5.TabIndex = 8;
            label5.Text = ".";
            label5.Click += label5_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblResult.ForeColor = System.Drawing.Color.Turquoise;
            lblResult.Location = new System.Drawing.Point(761, 592);
            lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new System.Drawing.Size(41, 65);
            lblResult.TabIndex = 9;
            lblResult.Text = ".";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            lblDate.Location = new System.Drawing.Point(35, 693);
            lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(0, 48);
            lblDate.TabIndex = 10;
            lblDate.Click += lblDate_Click;
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
            CurrentTime.Location = new System.Drawing.Point(968, 693);
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
            pbSwapCurrency.Location = new System.Drawing.Point(500, 213);
            pbSwapCurrency.Name = "pbSwapCurrency";
            pbSwapCurrency.Size = new System.Drawing.Size(182, 189);
            pbSwapCurrency.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pbSwapCurrency.TabIndex = 12;
            pbSwapCurrency.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(1143, 750);
            Controls.Add(pbSwapCurrency);
            Controls.Add(CurrentTime);
            Controls.Add(lblDate);
            Controls.Add(lblResult);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(btnConvert);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtAmount);
            Controls.Add(label1);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label CurrentTime;
        private System.Windows.Forms.PictureBox pbSwapCurrency;
    }
}

