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
    public partial class frmAddTask : DevExpress.XtraEditors.XtraForm
    {
        private string login;

        private Dictionary<string, int> executors = new Dictionary<string, int>();
        private Dictionary<string, int> clients = new Dictionary<string, int>();
        private Dictionary<string, int> types = new Dictionary<string, int>();

        public frmAddTask(string login)
        {
            InitializeComponent();

            this.login = login;
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
            // ----------- Исполнитель -----------
            Label executorLabel = new Label();
            executorLabel.Text = "Исполнитель";
            executorLabel.Dock = DockStyle.Fill;
            executorLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox executorCombobox = new ComboBox();
            loadExecutor(executorCombobox);
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
            typeCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            typeCombobox.Dock = DockStyle.Fill;
            typeCombobox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // ----------- Инфо -----------
            Label infoLabel = new Label();
            infoLabel.Text = "Инфо";
            infoLabel.Dock = DockStyle.Fill;
            infoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RichTextBox infoTextbox = new RichTextBox();
            infoTextbox.WordWrap = true;
            infoTextbox.Dock = DockStyle.Fill;
            infoTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // ----------- Проблема -----------
            Label problemLabel = new Label();
            problemLabel.Text = "Проблема";
            problemLabel.Dock = DockStyle.Fill;
            problemLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBox problemTextbox = new TextBox();
            problemTextbox.Dock = DockStyle.Fill;
            problemTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // ----------- Дедлайн -----------
            Label deadlineLabel = new Label();
            deadlineLabel.Text = "Дедлайн";
            deadlineLabel.Dock = DockStyle.Fill;
            deadlineLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DateTimePicker deadlineDateTimePicker = new DateTimePicker();
            deadlineDateTimePicker.Dock = DockStyle.Fill;
            deadlineDateTimePicker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.table.Controls.Add(executorLabel, 0, 0);
            this.table.Controls.Add(executorCombobox, 1, 0);
            this.table.Controls.Add(clientLabel, 0, 1);
            this.table.Controls.Add(clientCombobox, 1, 1);
            this.table.Controls.Add(priorityLabel, 0, 2);
            this.table.Controls.Add(priorityCombobox, 1, 2);
            this.table.Controls.Add(typeLabel, 0, 3);
            this.table.Controls.Add(typeCombobox, 1, 3);
            this.table.Controls.Add(infoLabel, 0, 4);
            this.table.Controls.Add(infoTextbox, 1, 4);
            this.table.Controls.Add(problemLabel, 0, 5);
            this.table.Controls.Add(problemTextbox, 1, 5);
            this.table.Controls.Add(deadlineLabel, 0, 6);
            this.table.Controls.Add(deadlineDateTimePicker, 1, 6);
        }

        private void frmEditTask_Load(object sender, EventArgs e)
        {
            loadTable();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();
            NpgsqlTransaction transaction = connection.BeginTransaction();

            NpgsqlCommand cmd0 = new NpgsqlCommand(
                "SELECT id FROM user_login WHERE username = $1",
                connection,
                transaction);

            cmd0.Parameters.AddWithValue(this.login);

            int author_id = (int) cmd0.ExecuteScalar();

            string data = ((RichTextBox) this.Controls.Find("infoTextbox", false).FirstOrDefault()).Text;
            object contract_id = null;
            if (data.Length > 0)
            {
                NpgsqlCommand cmd1 = new NpgsqlCommand(
                "INSERT INTO contracts (extra_data) VALUES ($1) RETURNING id",
                connection,
                transaction);

                cmd1.Parameters.AddWithValue(data);

                contract_id = (int) cmd1.ExecuteScalar();
            }

            NpgsqlCommand cmd2 = new NpgsqlCommand(
                "INSERT INTO tasks (" +
                    "contact_id, " +
                    "author_id, " +
                    "executor_id, " +
                    "contract_id, " +
                    "task_type_id, " +
                    "priority, " +
                    "data, " +
                    "dt_created," +
                    "dt_deadline) " +
                "VALUES (" +
                    "$1, " +
                    "$2, " +
                    "$3, " +
                    "$4, " +
                    "$5, " +
                    "$6, " +
                    "$7, " +
                    "now()::timestamp, " +
                    "$8)",
                connection,
                transaction);

            cmd2.Parameters.AddWithValue(this.clients[((ComboBox) this.Controls.Find("clientCombobox", false).FirstOrDefault()).SelectedText]); // contact_id
            cmd2.Parameters.AddWithValue(author_id); // author_id
            cmd2.Parameters.AddWithValue(this.executors[((ComboBox) this.Controls.Find("executorCombobox", false).FirstOrDefault()).SelectedText]); // executor_id
            cmd2.Parameters.AddWithValue(contract_id); // contact_id
            cmd2.Parameters.AddWithValue(this.types[((ComboBox) this.Controls.Find("typeCombobox", false).FirstOrDefault()).SelectedText]); // task_type_id
            cmd2.Parameters.AddWithValue(((ComboBox) this.Controls.Find("priorityCombobox", false).FirstOrDefault()).SelectedText); // priority
            cmd2.Parameters.AddWithValue(((TextBox) this.Controls.Find("problemTextbox", false).FirstOrDefault()).Text); // data
            cmd2.Parameters.AddWithValue(((DateTimePicker) this.Controls.Find("deadlineDateTimePicker", false).FirstOrDefault()).Value); // dt_deadline

            cmd2.ExecuteNonQuery();

            transaction.Commit();
        }
    }
}