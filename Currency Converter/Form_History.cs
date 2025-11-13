using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Currency_Converter
{
    public partial class Form_History : Form
    {
        private string rateHistoryDbPath;

        public Form_History(string conversionDbPath, string rateDbPath)
        {
            InitializeComponent();
            this.rateHistoryDbPath = rateDbPath;
            LoadConversionHistory(conversionDbPath);
        }

        private void LoadConversionHistory(string dbPath)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "SELECT OperationDate, BankName, CurrencyFrom, AmountFrom, CurrencyTo, AmountTo, RateUsed FROM ConversionHistory ORDER BY OperationDate DESC";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            dataGridConversionHistory.DataSource = dt;
                            dataGridConversionHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження історії конвертацій: {ex.Message}");
            }
        }

        private void btnShowRateHistory_Click(object sender, EventArgs e)
        {
            Form4 rateHistoryForm = new Form4(this.rateHistoryDbPath);
            rateHistoryForm.Show();
        }

        private void dataGridConversionHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowRateHistory_Click_1(object sender, EventArgs e)
        {
            Form4 chartForm = new Form4(this.rateHistoryDbPath);
            chartForm.Show();
        }
    }
}