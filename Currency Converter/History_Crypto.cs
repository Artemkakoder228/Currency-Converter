using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Currency_Converter
{
    public partial class History_Crypto : Form
    {
        private string dbPath = Path.Combine(Application.StartupPath, "crypto_history.db");

        public History_Crypto()
        {
            InitializeComponent();
            LoadHistory();
        }

        private void LoadHistory()
        {
            if (!File.Exists(dbPath)) return;

            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT Exchange, Coin, Price, Amount, Result, Date, Time FROM History ORDER BY Id DESC";

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (Controls.Find("dataGridView1", true).Length > 0)
                        {
                            var dgv = (DataGridView)Controls.Find("dataGridView1", true)[0];
                            dgv.DataSource = dt;
                            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження історії: " + ex.Message);
                }
            }
        }
    }
}