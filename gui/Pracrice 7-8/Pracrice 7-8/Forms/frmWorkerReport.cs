using DevExpress.XtraEditors;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pracrice_7_8.Forms
{
    public partial class frmWorkerReport : DevExpress.XtraEditors.XtraForm
    {
        private string _worker;

        public frmWorkerReport()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.appIcon;
            loadExecutor(comboBox);
        }

        private void loadExecutor(ComboBoxEdit comboBox)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT " +
                "ul.username as username " +
                "FROM user_login ul " +
                "JOIN person_role pr ON pr.id = ul.id " +
                "JOIN roles r ON r.id = pr.role_id " +
                "WHERE r.title IN ('Менеджер', 'Сотрудник')";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("username"));
                        comboBox.Properties.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            if(comboBox.SelectedItem != null && startDate.DateTime != null && endDate.DateTime != null)
            {
                _worker = comboBox.SelectedItem.ToString();
                DateTime startDateValue = startDate.DateTime.Date;
                DateTime endDateValue = endDate.DateTime.Date;

                NpgsqlConnection connection = DBUtils.GetDBConnection();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue(_worker);
                cmd.Parameters.AddWithValue(startDateValue);
                cmd.Parameters.AddWithValue(endDateValue);

                cmd.CommandText = "SELECT * FROM create_report(get_person_id_by_login($1), $2::DATE, $3::DATE)";

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    treeList1.DataSource = dt;
                }
                connection.Dispose();
                connection.Close();

            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pdfButton_Click(object sender, EventArgs e)
        {
            if(treeList1.Nodes.Count > 0)
            {
                treeList1.ExportToPdf($"Отчет по {_worker}.pdf");
            }
        }
    }
}