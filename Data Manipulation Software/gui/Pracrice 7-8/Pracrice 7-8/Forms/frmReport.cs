using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pracrice_7_8.Forms
{
    public partial class frmReport : DevExpress.XtraEditors.XtraForm
    {
        DateTime _startDateValue;
        DateTime _endDateValue;

        public frmReport()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.appIcon;
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            if(startDate.DateTime != null && endDate.DateTime != null)
            {
                _startDateValue = startDate.DateTime.Date;
                _endDateValue = endDate.DateTime.Date;

                NpgsqlConnection connection = DBUtils.GetDBConnection();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue(_startDateValue);
                cmd.Parameters.AddWithValue(_endDateValue);

                cmd.CommandText = "SELECT * FROM create_tasks_report($1::DATE, $2::DATE)";

                try
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        treeList1.ClearNodes();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        treeList1.DataSource = dt;
                    }

                    connection.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось получить отчет.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pdfButton_Click(object sender, EventArgs e)
        {
            if (treeList1.Nodes.Count > 0)
            {
                treeList1.ExportToPdf($"Отчет {_startDateValue.ToString("dd/MM/yyyy")} - {_endDateValue.ToString("dd/MM/yyyy")}.pdf");
            }
        }
    }
}