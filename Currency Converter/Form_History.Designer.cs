namespace Currency_Converter
{
    partial class Form_History
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
            dataGridConversionHistory = new System.Windows.Forms.DataGridView();
            btnShowRateHistory = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dataGridConversionHistory).BeginInit();
            SuspendLayout();
            // 
            // dataGridConversionHistory
            // 
            dataGridConversionHistory.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridConversionHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridConversionHistory.Location = new System.Drawing.Point(-3, -1);
            dataGridConversionHistory.Name = "dataGridConversionHistory";
            dataGridConversionHistory.RowHeadersWidth = 62;
            dataGridConversionHistory.Size = new System.Drawing.Size(801, 545);
            dataGridConversionHistory.TabIndex = 0;
            dataGridConversionHistory.CellContentClick += dataGridConversionHistory_CellContentClick;
            // 
            // btnShowRateHistory
            // 
            btnShowRateHistory.BackColor = System.Drawing.Color.Turquoise;
            btnShowRateHistory.FlatAppearance.BorderSize = 0;
            btnShowRateHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnShowRateHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            btnShowRateHistory.Location = new System.Drawing.Point(162, 570);
            btnShowRateHistory.Name = "btnShowRateHistory";
            btnShowRateHistory.Size = new System.Drawing.Size(476, 69);
            btnShowRateHistory.TabIndex = 14;
            btnShowRateHistory.Text = "Історія курсів валют ";
            btnShowRateHistory.UseVisualStyleBackColor = false;
            btnShowRateHistory.Click += btnShowRateHistory_Click;
            // 
            // label1
            // 
            label1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            label1.Location = new System.Drawing.Point(146, 557);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(508, 94);
            label1.TabIndex = 17;
            // 
            // Form_History
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 660);
            Controls.Add(btnShowRateHistory);
            Controls.Add(dataGridConversionHistory);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form_History";
            Text = "Form_Histori";
            Load += Form_History_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridConversionHistory).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridConversionHistory;
        private System.Windows.Forms.Button btnShowRateHistory;
        private System.Windows.Forms.Label label1;
    }
}