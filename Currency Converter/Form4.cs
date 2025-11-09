using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Currency_Converter
{
    public partial class Form4 : Form
    {
        private string dbPath;

        public Form4(string rateHistoryDbPath)
        {
            InitializeComponent();
            this.dbPath = rateHistoryDbPath;
            this.Text = "Графіки курсів валют";

            this.Load += new System.EventHandler(this.Form4_Load);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Дата";
            chart1.ChartAreas[0].AxisY.Title = "Курс купівлі"; 
            chart1.Titles.Add("Порівняння курсів USD (купівля)");

            chart1.Legends.Add(new Legend("DefaultLegend"));


            LoadChartData();
        }

        private void LoadChartData()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"
                        SELECT FetchDate, BankName, RateBuy 
                        FROM AllRateHistory 
                        WHERE Currency = 'USD' 
                        ORDER BY FetchDate";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних для графіка: {ex.Message}");
                return;
            }
            var bankNames = dt.AsEnumerable()
                              .Select(row => row.Field<string>("BankName"))
                              .Distinct();

            foreach (string bank in bankNames)
            {
                Series bankSeries = new Series(bank);
                bankSeries.ChartType = SeriesChartType.Line;
                bankSeries.BorderWidth = 3;
                bankSeries.MarkerStyle = MarkerStyle.Circle;
                bankSeries.MarkerSize = 8;

                var bankData = dt.AsEnumerable()
                                 .Where(row => row.Field<string>("BankName") == bank);

                foreach (var row in bankData)
                {
                    DateTime date = DateTime.Parse(row.Field<string>("FetchDate"));
                    double rate = row.Field<double>("RateBuy");


                    bankSeries.Points.AddXY(date, rate);
                }


                chart1.Series.Add(bankSeries);
            }
        }
    }
}