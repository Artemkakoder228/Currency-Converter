using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Currency_Converter
{
    public partial class Form_History : Form
    {
        private string _rateHistoryDbPath;
        private string _conversionDbPath;

        public Form_History(string conversionDbPath, string rateDbPath)
        {
            InitializeComponent();

            this._conversionDbPath = conversionDbPath;
            this._rateHistoryDbPath = rateDbPath;

            SetRoundedShape(btnShowRateHistory, 20);

            LoadConversionHistory();
        }

        private void SetRoundedShape(Control control, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, radius, radius, 180, 90);
            gp.AddLine(radius, 0, control.Width - radius, 0);
            gp.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            gp.AddLine(control.Width, radius, control.Width, control.Height - radius);
            gp.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            gp.AddLine(control.Width - radius, control.Height, radius, control.Height);
            gp.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            gp.CloseFigure();
            control.Region = new Region(gp);
        }

        private void LoadConversionHistory()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={_conversionDbPath}"))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    OperationDate AS 'Дата', 
                    BankName AS 'Банк', 
                    CurrencyFrom AS 'З валюти', 
                    ROUND(AmountFrom, 2) AS 'Сума', 
                    CurrencyTo AS 'В валюту', 
                    ROUND(AmountTo, 2) AS 'Результат', 
                    ROUND(RateUsed, 2) AS 'Курс' 
                FROM ConversionHistory 
                ORDER BY OperationDate DESC";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        if (dataGridConversionHistory != null)
                        {
                            dataGridConversionHistory.DataSource = dt;
                            dataGridConversionHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження історії: {ex.Message}");
            }
        }

        private void btnShowRateHistory_Click(object sender, EventArgs e)
        {
            try
            {
                Currency_rate_and_grafic chartForm = new Currency_rate_and_grafic(this._rateHistoryDbPath);
                chartForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не вдалося відкрити вікно графіка.\nПереконайтеся, що у форми Currency_rate_and_grafic є правильний конструктор.\nПомилка: {ex.Message}");
            }
        }

        private void dataGridConversionHistory_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Form_History_Load(object sender, EventArgs e)
        {

        }
    }
}