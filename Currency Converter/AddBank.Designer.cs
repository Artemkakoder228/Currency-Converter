namespace Currency_Converter
{
    partial class AddBank
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
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label2.Location = new System.Drawing.Point(47, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(525, 70);
            label2.TabIndex = 74;
            label2.Text = "Введіть інформацію";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Semibold", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(128, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(0, 70);
            label1.TabIndex = 73;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ControlText;
            panel1.Location = new System.Drawing.Point(0, 82);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(618, 4);
            panel1.TabIndex = 72;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.SystemColors.ControlText;
            panel2.Location = new System.Drawing.Point(0, 502);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(618, 4);
            panel2.TabIndex = 75;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            label3.Location = new System.Drawing.Point(33, 239);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(174, 45);
            label3.TabIndex = 76;
            label3.Text = "API банку:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            label4.Location = new System.Drawing.Point(33, 131);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(216, 45);
            label4.TabIndex = 77;
            label4.Text = "Назва банку:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            label5.Location = new System.Drawing.Point(33, 343);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(222, 45);
            label5.TabIndex = 78;
            label5.Text = "Іконка банку:";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(272, 317);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(118, 93);
            pictureBox1.TabIndex = 79;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox1.Location = new System.Drawing.Point(213, 246);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(387, 39);
            textBox1.TabIndex = 80;
            // 
            // textBox2
            // 
            textBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            textBox2.Location = new System.Drawing.Point(251, 137);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(349, 39);
            textBox2.TabIndex = 81;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(423, 343);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(149, 45);
            button1.TabIndex = 82;
            button1.Text = "Завантажити";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(213, 427);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(187, 58);
            button2.TabIndex = 83;
            button2.Text = "Додати";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // AddBank
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(612, 531);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panel2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddBank";
            Text = "AddBank";
            Load += AddBank_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button button1;
    }
}