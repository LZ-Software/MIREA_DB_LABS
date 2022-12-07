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
    public partial class frmEditTask : DevExpress.XtraEditors.XtraForm
    {
        private string role;

        public frmEditTask(string role)
        {
            InitializeComponent();

            this.role = role;
        }

        private void loadTable()
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT contact.username as Клиент,author.username as Автор,executor.username as Исполнитель,cont.extra_data as Инфо,t.title as Тип,tsk.priority as Приоритет,tsk.data as Проблема,tsk.dt_created as Создано,tsk.dt_finished as Завершено,tsk.dt_deadline as Дедлайн FROM tasks tsk JOIN user_login contact ON tsk.contact_id = contact.id JOIN user_login author ON tsk.author_id = author.id JOIN user_login executor ON tsk.executor_id = executor.id LEFT JOIN contracts cont ON tsk.contract_id = cont.id JOIN task_type t ON tsk.task_type_id = t.id WHERE tsk.id = 1";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string author = reader.GetString(reader.GetOrdinal("Автор"));
                        string executor = reader.GetString(reader.GetOrdinal("Исполнитель"));
                        string priority = reader.GetString(reader.GetOrdinal("Приоритет"));
                        string type = reader.GetString(reader.GetOrdinal("Тип"));

                        Label authorLabel = new Label();
                        authorLabel.Text = "Автор";

                        Label executorLabel = new Label();
                        executorLabel.Text = "Исполнитель";

                        Label priorityLabel = new Label();
                        priorityLabel.Text = "Приориетет";

                        Label typeLabel = new Label();
                        typeLabel.Text = "Тип";

                        TextBox authorTextbox = new TextBox();
                        authorTextbox.Text = author;

                        TextBox executorTextbox = new TextBox();
                        executorTextbox.Text = executor;

                        TextBox priorityTextbox = new TextBox();
                        priorityTextbox.Text = priority;

                        TextBox typeTextbox = new TextBox();
                        typeTextbox.Text = type;


                        this.table.Controls.Add(authorLabel, 0, 0);
                        this.table.Controls.Add(authorTextbox, 1, 0);
                        this.table.Controls.Add(executorLabel, 0, 1);
                        this.table.Controls.Add(executorTextbox, 1, 1);
                        this.table.Controls.Add(priorityLabel, 0, 2);
                        this.table.Controls.Add(priorityTextbox, 1, 2);
                        this.table.Controls.Add(typeLabel, 0, 3);
                        this.table.Controls.Add(typeTextbox, 1, 3);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmEditTask_Load(object sender, EventArgs e)
        {
            loadTable();
        }
    }
}