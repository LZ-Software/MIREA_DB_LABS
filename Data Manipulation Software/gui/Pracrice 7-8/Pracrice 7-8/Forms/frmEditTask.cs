using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pracrice_7_8.Forms
{
    public partial class frmEditTask : DevExpress.XtraEditors.XtraForm
    {
        private int task_id;
        private int contract_id;

        private Dictionary<string, int> executors = new Dictionary<string, int>();
        private Dictionary<string, int> clients = new Dictionary<string, int>();
        private Dictionary<string, int> types = new Dictionary<string, int>();

        public frmEditTask(int task_id, int contract_id)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.appIcon;

            this.task_id = task_id;
            this.contract_id = contract_id;
        }

        private void loadExecutor(ComboBox comboBox)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT " +
                "ul.id as id, " +
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
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("username"));
                        comboBox.Items.Add(name);
                        this.executors.Add(name, id);
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
                "ul.id as id, " +
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
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("username"));
                        comboBox.Items.Add(name);
                        this.clients.Add(name, id);
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

            cmd.CommandText = "SELECT id, title FROM task_type";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("title"));
                        comboBox.Items.Add(name);
                        this.types.Add(name, id);
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
            cmd.Parameters.AddWithValue(this.task_id);

            cmd.CommandText = "SELECT " +
                "contact.username as Клиент," + // +
                "author.username as Автор," + // +
                "executor.username as Исполнитель," + // +
                "cont.id as Конт_ИД," + // +
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
                "WHERE tsk.id = $1";

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
                        if (reader.IsDBNull(reader.GetOrdinal("Конт_ИД")))
                        {
                            this.contract_id = -1;
                        }
                        else
                        {
                            this.contract_id = reader.GetInt32(reader.GetOrdinal("Конт_ИД"));
                        }
                        string info;
                        if(reader.IsDBNull(reader.GetOrdinal("Инфо")))
                        {
                            info = "";
                        }
                        else
                        {
                            info = reader.GetString(reader.GetOrdinal("Инфо"));
                        }
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
                        authorTextbox.Name = "authorTextbox";
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
                        executorCombobox.Name = "executorCombobox";
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
                        clientCombobox.Name = "clientCombobox";
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
                        priorityCombobox.Name = "priorityCombobox";
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
                        typeCombobox.Name = "typeCombobox";
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
                        infoTextbox.Name = "infoTextbox";
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
                        problemTextbox.Name = "problemTextbox";
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
                        createdDateTimePicker.Name = "createdDateTimePicker";
                        createdDateTimePicker.Enabled = false;
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
                            finishedDateTimePicker.Name = "finishedDateTimePicker";
                            finishedDateTimePicker.Value = finishedDateTime;
                            finishedDateTimePicker.Dock = DockStyle.Fill;
                            finishedDateTimePicker.Enabled = false;
                            finishedDateTimePicker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        }
                        else
                        {
                            finishedTextbox.Name = "finishedTextbox";
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
                        deadlineDateTimePicker.Name = "deadlineDateTimePicker";
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

        private void finishButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "UPDATE tasks SET dt_finished = now()::timestamp WHERE id = $1";

            cmd.Parameters.AddWithValue(this.task_id);

            if (cmd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Задача закрыта.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Что-то отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.Dispose();
            connection.Close();

            this.Close();
        }

        private void changeTaskButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();
            NpgsqlTransaction transaction = connection.BeginTransaction();

            TableLayoutPanel table = (TableLayoutPanel) this.Controls.Find("table", false).FirstOrDefault();

            string data = ((RichTextBox) table.Controls.Find("infoTextbox", false).FirstOrDefault()).Text; // contract text

            NpgsqlCommand cmd1 = new NpgsqlCommand();
            cmd1.Connection = connection;
            cmd1.Transaction = transaction;

            if (this.contract_id == -1 && data.Length > 0) // Контракта не было, а теперь есть
            {
                cmd1.CommandText = "INSERT INTO contracts (extra_data) VALUES ($1::jsonb) RETURNING id";
                cmd1.Parameters.AddWithValue(data);

                try
                {
                    this.contract_id = (int) cmd1.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось добавить контракт.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    transaction.Rollback();
                    transaction.Dispose();
                    return;
                }
            }
            else if (this.contract_id == -1 && data.Length == 0) // Контракта не было и нет
            {
                this.contract_id = -1;
            }
            else if (this.contract_id != 1 && data.Length == 0) // Контракт был, а теперь нет
            {
                cmd1.CommandText = "DELETE FROM contracts WHERE id = $1";
                cmd1.Parameters.AddWithValue(this.contract_id);

                try
                {
                    if (cmd1.ExecuteNonQuery() == -1)
                    {
                        return;
                    }
                    else
                    {
                        this.contract_id = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить контракт.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    transaction.Rollback();
                    transaction.Dispose();
                    return;
                }
            }
            else if (this.contract_id != 1 && data.Length > 0) // Контракт был и есть
            {
                cmd1.CommandText = "UPDATE contracts SET extra_data = (($1)::jsonb) WHERE id = $2";
                cmd1.Parameters.AddWithValue(data);
                cmd1.Parameters.AddWithValue(this.contract_id);

                if (cmd1.ExecuteNonQuery() == -1)
                {
                    MessageBox.Show("Не удалось обновить контракт.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    transaction.Rollback();
                    transaction.Dispose();
                    return;
                }
            }

            NpgsqlCommand cmd2 = new NpgsqlCommand(
                "UPDATE tasks SET " +
                    "contact_id = $1, " +
                    "executor_id = $2, " +
                    "contract_id = $3, " +
                    "task_type_id = $4, " +
                    "priority = $5::priority_enum, " +
                    "data = $6, " +
                    "dt_deadline = $7 " +
                "WHERE id = $8",
                connection,
                transaction);

            object cont_id;
            if (this.contract_id == -1)
            {
                cont_id = DBNull.Value;
            }
            else
            {
                cont_id = this.contract_id;
            }

            string problem_text = ((TextBox)table.Controls.Find("problemTextbox", false).FirstOrDefault()).Text;
            object prblm;
            if (problem_text.Length == 0)
            {
                prblm = DBNull.Value;
            }
            else
            {
                prblm = problem_text;
            }

            cmd2.Parameters.AddWithValue(this.clients[((ComboBox) table.Controls.Find("clientCombobox", false).FirstOrDefault()).SelectedItem.ToString()]); // contact_id
            cmd2.Parameters.AddWithValue(this.executors[((ComboBox) table.Controls.Find("executorCombobox", false).FirstOrDefault()).SelectedItem.ToString()]); // executor_id
            cmd2.Parameters.AddWithValue(cont_id); // contract_id
            cmd2.Parameters.AddWithValue(this.types[((ComboBox) table.Controls.Find("typeCombobox", false).FirstOrDefault()).SelectedItem.ToString()]); // task_type_id
            cmd2.Parameters.AddWithValue(((ComboBox) table.Controls.Find("priorityCombobox", false).FirstOrDefault()).SelectedItem.ToString()); // priority
            cmd2.Parameters.AddWithValue(prblm); // data
            cmd2.Parameters.AddWithValue(((DateTimePicker) table.Controls.Find("deadlineDateTimePicker", false).FirstOrDefault()).Value); // dt_deadline
            cmd2.Parameters.AddWithValue(this.task_id);

            try
            {
                if (cmd2.ExecuteNonQuery() != -1)
                {
                    transaction.Commit();
                    MessageBox.Show("Задача обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Задача не обновлена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    transaction.Rollback();
                    transaction.Dispose();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Задача не обновлена.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transaction.Rollback();
                transaction.Dispose();
                return;
            }

            this.Close();
        }
    }
}