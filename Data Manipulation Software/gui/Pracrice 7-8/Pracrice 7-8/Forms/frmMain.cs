using DevExpress.XtraEditors;
using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Pracrice_7_8.Forms
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private string role;
        private string login;

        public frmMain(string role, string login)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.appIcon;
            this.role = role;
            this.login = login;
            if (role != "Администратор") loadButtons();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd_name = new NpgsqlCommand();
            cmd_name.Connection = connection;
            cmd_name.Parameters.AddWithValue(login);

            cmd_name.CommandText = "SELECT first_name, last_name FROM person WHERE id = get_person_id_by_login($1)";

            using (NpgsqlDataReader reader_name = cmd_name.ExecuteReader())
            {
                if (reader_name.HasRows)
                {
                    while (reader_name.Read())
                    {
                        nameLabel.Text = $"{reader_name.GetString(reader_name.GetOrdinal("first_name"))} {reader_name.GetString(reader_name.GetOrdinal("last_name"))}";
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            roleLabel.Text = role;
            loadTable();
        }

        private void loadButtons()
        {
            if (role == "Менеджер")
            {
                registerButton.Enabled = false;
                registerButton.Visible = false;
            }
            else if (role == "Сотрудник")
            {
                createTaskButton.Enabled = false;
                createTaskButton.Visible = false;
                registerButton.Enabled = false;
                registerButton.Visible = false;
                employeeReportButton.Enabled = false;
                employeeReportButton.Visible = false;
            }
        }

        private void loadTable()
        {
            this.Refresh();

            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd_name = new NpgsqlCommand();
            cmd_name.Connection = connection;

            NpgsqlCommand cmd_tasks = new NpgsqlCommand();
            cmd_tasks.Connection = connection;

            cmd_tasks.CommandText = "SELECT tsk.id as ID, contact.username as Клиент,author.username as Автор,executor.username as Исполнитель,cont.id as Контракт," +
                "t.title as Тип,tsk.priority as Приоритет,tsk.data as Проблема,tsk.dt_created as Создано,tsk.dt_deadline as Дедлайн, tsk.dt_finished as Завершено " +
                "FROM tasks tsk " +
                "JOIN user_login contact ON tsk.contact_id = contact.id " +
                "JOIN user_login author ON tsk.author_id = author.id " +
                "JOIN user_login executor ON tsk.executor_id = executor.id " +
                "LEFT JOIN contracts cont ON tsk.contract_id = cont.id " +
                "JOIN task_type t ON tsk.task_type_id = t.id";

            NpgsqlDataReader reader = cmd_tasks.ExecuteReader();

            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }

            connection.Dispose();
            connection.Close();

            dataGridView1.Columns["ID"].Width = 20;
            dataGridView1.Columns["Контракт"].Width = 65;

            DataGridViewButtonColumn buttonEdit = new DataGridViewButtonColumn();
            buttonEdit.Name = "buttonEdit";
            buttonEdit.HeaderText = "Изменить";
            buttonEdit.FlatStyle = FlatStyle.Flat;
            buttonEdit.DefaultCellStyle.BackColor = Color.FromArgb(68, 68, 68);
            dataGridView1.Columns.Add(buttonEdit);

            dataGridView1.Sort(dataGridView1.Columns["ID"], ListSortDirection.Descending);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Контракт" && dataGridView1.CurrentCell.Value.ToString() != "")
            {
                NpgsqlConnection connection = DBUtils.GetDBConnection();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue(dataGridView1.CurrentCell.Value);

                cmd.CommandText = "SELECT extra_data FROM contracts WHERE id = $1";

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            XtraMessageBox.Show(reader.GetString(reader.GetOrdinal("extra_data")), "Контракт", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "buttonEdit")
            {
                if(dataGridView1.CurrentRow.Cells[4].Value.ToString() != "")
                {
                    frmEditTask editTask = new frmEditTask((int) dataGridView1.Rows[e.RowIndex].Cells["ID"].Value, (int) dataGridView1.CurrentRow.Cells[4].Value);
                    editTask.ShowDialog();
                    loadTable();
                }
                else
                {
                    frmEditTask editTask = new frmEditTask((int) dataGridView1.Rows[e.RowIndex].Cells["ID"].Value, - 1);
                    editTask.ShowDialog();
                    loadTable();
                }
            }
        }

        private void createTaskButton_Click(object sender, EventArgs e)
        {
            frmAddTask frmAddTask = new frmAddTask(this.login);
            frmAddTask.ShowDialog();
            loadTable();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            frmRegister register = new frmRegister();
            register.ShowDialog();
        }

        private void searchClientButton_Click(object sender, EventArgs e)
        {
            frmSearchClient searchClient = new frmSearchClient();
            searchClient.ShowDialog();
            loadTable();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void employeeReportButton_Click(object sender, EventArgs e)
        {
            frmWorkerReport workerReport = new frmWorkerReport();
            workerReport.ShowDialog();
        }

        private void createReportButton_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.ShowDialog();
        }
    }
}