using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Currency_Converter
{
    public partial class Currency_rate_and_grafic : Form
    {
        private string dbPath;

        public Currency_rate_and_grafic(string rateHistoryDbPath)
        {
            InitializeComponent();
            this.dbPath = rateHistoryDbPath;
            this.Text = "Графіки курсів валют";

            this.Load += new System.EventHandler(this.Form4_Load);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SetupChart(chart1, "Динаміка курсу долара (USD)");

            SetupChart(chart2, "Динаміка курсу євро (EUR)");

            LoadChartData();
        }

        private void SetupChart(Chart chart, string title)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);

            chart.ChartAreas[0].AxisX.Title = "Дата";
            chart.ChartAreas[0].AxisY.Title = "Курс (UAH)";

            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;

            chart.ChartAreas[0].AxisY.LabelStyle.Format = "N2";

            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;

            chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            if (chart.Legends.Count == 0)
            {
                chart.Legends.Add(new Legend("DefaultLegend"));
            }
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
                        SELECT FetchDate, BankName, Currency, RateBuy 
                        FROM AllRateHistory 
                        WHERE Currency IN ('USD', 'EUR') 
                        ORDER BY FetchDate";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }

                if (dataGridBankRates != null)
                {
                    dataGridBankRates.DataSource = dt;
                    dataGridBankRates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                var uniqueBanks = dt.AsEnumerable()
                                    .Select(row => row["BankName"].ToString())
                                    .Distinct();

                foreach (string bank in uniqueBanks)
                {
                    var usdData = dt.AsEnumerable()
                        .Where(row => row["BankName"].ToString() == bank && row["Currency"].ToString() == "USD");

                    if (usdData.Any())
                    {
                        Series seriesUSD = new Series(bank);
                        seriesUSD.ChartType = SeriesChartType.Line;
                        seriesUSD.BorderWidth = 3;
                        seriesUSD.MarkerStyle = MarkerStyle.Circle;
                        seriesUSD.MarkerSize = 8;

                        foreach (var row in usdData)
                        {
                            DateTime date = Convert.ToDateTime(row["FetchDate"]);
                            double rate = Convert.ToDouble(row["RateBuy"]);
                            seriesUSD.Points.AddXY(date, rate);
                        }
                        chart1.Series.Add(seriesUSD);
                    }

                    var eurData = dt.AsEnumerable()
                        .Where(row => row["BankName"].ToString() == bank && row["Currency"].ToString() == "EUR");

                    if (eurData.Any())
                    {
                        Series seriesEUR = new Series(bank);
                        seriesEUR.ChartType = SeriesChartType.Line;
                        seriesEUR.BorderWidth = 3;
                        seriesEUR.MarkerStyle = MarkerStyle.Square;
                        seriesEUR.MarkerSize = 8;

                        foreach (var row in eurData)
                        {
                            DateTime date = Convert.ToDateTime(row["FetchDate"]);
                            double rate = Convert.ToDouble(row["RateBuy"]);
                            seriesEUR.Points.AddXY(date, rate);
                        }
                        chart2.Series.Add(seriesEUR);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка побудови графіків: {ex.Message}");
            }
        }

        private void chart1_Click(object sender, EventArgs e) { }
        private void dataGridBankRates_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Currency_rate_and_grafic_Load(object sender, EventArgs e) { }
    }
}