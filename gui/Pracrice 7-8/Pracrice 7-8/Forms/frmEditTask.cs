/*using DevExpress.XtraEditors;*/
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

        private void loadExecutor(ComboBox comboBox)
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
                        comboBox.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadClient(ComboBox comboBox)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT " +
                "ul.username as username " +
                "FROM user_login ul " +
                "JOIN person_role pr ON pr.id = ul.id " +
                "JOIN roles r ON r.id = pr.role_id " +
                "WHERE r.title = 'Клиент'";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("username"));
                        comboBox.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadType(ComboBox comboBox)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT title FROM task_type";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("title"));
                        comboBox.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadPriority(ComboBox comboBox)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT unnest(enum_range(NULL::priority_enum));";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("unnest"));
                        comboBox.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void loadTable()
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT " +
                "contact.username as Клиент," + // +
                "author.username as Автор," + // +
                "executor.username as Исполнитель," + // +
                "cont.extra_data as Инфо," + // +
                "t.title as Тип," + // +
                "tsk.priority as Приоритет," + // +
                "tsk.data as Проблема," + // +
                "tsk.dt_created as Создано," +
                "tsk.dt_finished as Завершено," +
                "tsk.dt_deadline as Дедлайн " +
                "FROM tasks tsk " +
                "JOIN user_login contact ON tsk.contact_id = contact.id " +
                "JOIN user_login author ON tsk.author_id = author.id " +
                "JOIN user_login executor ON tsk.executor_id = executor.id " +
                "LEFT JOIN contracts cont ON tsk.contract_id = cont.id " +
                "JOIN task_type t ON tsk.task_type_id = t.id " +
                "WHERE tsk.id = 1";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string author = reader.GetString(reader.GetOrdinal("Автор"));
                        string executor = reader.GetString(reader.GetOrdinal("Исполнитель"));
                        string client = reader.GetString(reader.GetOrdinal("Клиент"));
                        string priority = reader.GetString(reader.GetOrdinal("Приоритет"));
                        string type = reader.GetString(reader.GetOrdinal("Тип"));
                        string info = reader.GetString(reader.GetOrdinal("Инфо"));
                        string problem;
                        if (reader.IsDBNull(reader.GetOrdinal("Проблема")))
                        {
                           problem = "";
                        }
                        else
                        {
                            problem = reader.GetString(reader.GetOrdinal("Проблема"));
                        }
                        DateTime created = reader.GetDateTime(reader.GetOrdinal("Создано"));
                        bool finished = !reader.IsDBNull(reader.GetOrdinal("Завершено"));
                        DateTime finishedDateTime = DateTime.Now;
                        if (finished)
                        {
                            finishedDateTime = reader.GetDateTime(reader.GetOrdinal("Завершено"));
                        }
                        DateTime deadline = reader.GetDateTime(reader.GetOrdinal("Дедлайн"));


                        // ----------- Автор -----------
                        Label authorLabel = new Label();
                        authorLabel.Text = "Автор";
                        authorLabel.Dock = DockStyle.Fill;
                        authorLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        TextBox authorTextbox = new TextBox();
                        authorTextbox.Text = author;
                        authorTextbox.ReadOnly = true;
                        authorTextbox.Dock = DockStyle.Fill;
                        authorTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Исполнитель -----------
                        Label executorLabel = new Label();
                        executorLabel.Text = "Исполнитель";
                        executorLabel.Dock = DockStyle.Fill;
                        executorLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        ComboBox executorCombobox = new ComboBox();
                        loadExecutor(executorCombobox);
                        executorCombobox.SelectedItem = executor;
                        executorCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                        executorCombobox.Dock = DockStyle.Fill;
                        executorCombobox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Клиент -----------
                        Label clientLabel = new Label();
                        clientLabel.Text = "Клиент";
                        clientLabel.Dock = DockStyle.Fill;
                        clientLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        ComboBox clientCombobox = new ComboBox();
                        loadClient(clientCombobox);
                        clientCombobox.SelectedItem = client;
                        clientCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                        clientCombobox.Dock = DockStyle.Fill;
                        clientCombobox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Приориетет -----------
                        Label priorityLabel = new Label();
                        priorityLabel.Text = "Приориетет";
                        priorityLabel.Dock = DockStyle.Fill;
                        priorityLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        ComboBox priorityCombobox = new ComboBox();
                        loadPriority(priorityCombobox);
                        priorityCombobox.SelectedItem = priority;
                        priorityCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                        priorityCombobox.Dock = DockStyle.Fill;
                        priorityCombobox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Тип -----------
                        Label typeLabel = new Label();
                        typeLabel.Text = "Тип";
                        typeLabel.Dock = DockStyle.Fill;
                        typeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        ComboBox typeCombobox = new ComboBox();
                        loadType(typeCombobox);
                        typeCombobox.Text = type;
                        typeCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                        typeCombobox.Dock = DockStyle.Fill;
                        typeCombobox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Инфо -----------
                        Label infoLabel = new Label();
                        infoLabel.Text = "Инфо";
                        infoLabel.Dock = DockStyle.Fill;
                        infoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        RichTextBox infoTextbox = new RichTextBox();
                        infoTextbox.Text = info;
                        infoTextbox.WordWrap = true;
                        infoTextbox.Dock = DockStyle.Fill;
                        infoTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Проблема -----------
                        Label problemLabel = new Label();
                        problemLabel.Text = "Проблема";
                        problemLabel.Dock = DockStyle.Fill;
                        problemLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        TextBox problemTextbox = new TextBox();
                        problemTextbox.Text = problem;
                        problemTextbox.Dock = DockStyle.Fill;
                        problemTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Создано -----------
                        Label createdLabel = new Label();
                        createdLabel.Text = "Создано";
                        createdLabel.Dock = DockStyle.Fill;
                        createdLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        DateTimePicker createdDateTimePicker = new DateTimePicker();
                        createdDateTimePicker.Value = created;
                        createdDateTimePicker.Dock = DockStyle.Fill;
                        createdDateTimePicker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        // ----------- Завершено -----------
                        Label finishedLabel = new Label();
                        finishedLabel.Text = "Завершено";
                        finishedLabel.Dock = DockStyle.Fill;
                        finishedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        DateTimePicker finishedDateTimePicker = new DateTimePicker();
                        TextBox finishedTextbox = new TextBox();
                        if (finished)
                        {
                            
                            finishedDateTimePicker.Value = finishedDateTime;
                            finishedDateTimePicker.Dock = DockStyle.Fill;
                            finishedDateTimePicker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        }
                        else
                        {
                            finishedTextbox.Text = "";
                            finishedTextbox.ReadOnly = true;
                            finishedTextbox.Dock = DockStyle.Fill;
                            finishedTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        }

                        // ----------- Дедлайн -----------
                        Label deadlineLabel = new Label();
                        deadlineLabel.Text = "Дедлайн";
                        deadlineLabel.Dock = DockStyle.Fill;
                        deadlineLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        DateTimePicker deadlineDateTimePicker = new DateTimePicker();
                        deadlineDateTimePicker.Value = deadline;
                        deadlineDateTimePicker.Dock = DockStyle.Fill;
                        deadlineDateTimePicker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                        this.table.Controls.Add(authorLabel, 0, 0);
                        this.table.Controls.Add(authorTextbox, 1, 0);
                        this.table.Controls.Add(executorLabel, 0, 1);
                        this.table.Controls.Add(executorCombobox, 1, 1);
                        this.table.Controls.Add(clientLabel, 0, 2);
                        this.table.Controls.Add(clientCombobox, 1, 2);
                        this.table.Controls.Add(priorityLabel, 0, 3);
                        this.table.Controls.Add(priorityCombobox, 1, 3);
                        this.table.Controls.Add(typeLabel, 0, 4);
                        this.table.Controls.Add(typeCombobox, 1, 4);
                        this.table.Controls.Add(infoLabel, 0, 5);
                        this.table.Controls.Add(infoTextbox, 1, 5);
                        this.table.Controls.Add(problemLabel, 0, 6);
                        this.table.Controls.Add(problemTextbox, 1, 6);
                        this.table.Controls.Add(createdLabel, 0, 7);
                        this.table.Controls.Add(createdDateTimePicker, 1, 7);
                        this.table.Controls.Add(finishedLabel, 0, 8);
                        if (finished)
                        {
                            this.table.Controls.Add(finishedDateTimePicker, 1, 8);
                        }
                        else
                        {
                            this.table.Controls.Add(finishedTextbox, 1, 8);
                        }
                        this.table.Controls.Add(deadlineLabel, 0, 9);
                        this.table.Controls.Add(deadlineDateTimePicker, 1, 9);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}