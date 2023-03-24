using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pracrice_7_8.Forms
{
    public partial class frmSearchClient : DevExpress.XtraEditors.XtraForm
    {
        public frmSearchClient()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.appIcon;

            loadTable();
        }

        private void loadTable()
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT p.id as ID, ul.username as Логин,p.first_name as Имя, p.last_name as Фамилия, cft.contact as Телефон, cfe.contact as Почта, cfa.contact as Адрес, org.name as Организация " +
                "FROM person p " +
                "JOIN user_login ul ON p.user_id = ul.id " +
                "JOIN person_role pr ON p.id = pr.person_id AND pr.role_id = 4 " +
                "LEFT JOIN contact_info cft ON p.id = cft.person_id AND cft.contact_type = 'телефон' " +
                "LEFT JOIN contact_info cfe ON p.id = cfe.person_id AND cfe.contact_type = 'email' " +
                "LEFT JOIN contact_info cfa ON p.id = cfa.person_id AND cfa.contact_type = 'адрес' " +
                "JOIN organization org on p.id = org.person_id";

            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            connection.Dispose();
            connection.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            if (!String.IsNullOrEmpty(phoneText.Text))
            {
                cmd.Parameters.AddWithValue(phoneText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if(!String.IsNullOrEmpty(emailText.Text))
            {
                cmd.Parameters.AddWithValue(emailText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if (!String.IsNullOrEmpty(addressText.Text))
            {
                cmd.Parameters.AddWithValue(addressText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if (!String.IsNullOrEmpty(loginText.Text))
            {
                cmd.Parameters.AddWithValue(loginText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if(!String.IsNullOrEmpty(nameText.Text) && !String.IsNullOrEmpty(lastnameText.Text) && !String.IsNullOrEmpty(organizationText.Text))
            {
                cmd.Parameters.AddWithValue(nameText.Text);
                cmd.Parameters.AddWithValue(lastnameText.Text);
                cmd.Parameters.AddWithValue(organizationText.Text);
                cmd.CommandText = "SELECT p.id as ID, ul.username,p.first_name, p.last_name, cft.contact as Phone, cfe.contact as Email, cfa.contact as Address, org.name as Organization FROM person p " +
                    "JOIN user_login ul ON p.user_id = ul.id " +
                    "JOIN person_role pr ON p.id = pr.person_id AND pr.role_id = 4 " +
                    "LEFT JOIN contact_info cft ON p.id = cft.person_id AND cft.contact_type = 'телефон' " +
                    "LEFT JOIN contact_info cfe ON p.id = cfe.person_id AND cfe.contact_type = 'email' " +
                    "LEFT JOIN contact_info cfa ON p.id = cfa.person_id AND cfa.contact_type = 'адрес' " +
                    "JOIN organization org on p.id = org.person_id WHERE p.first_name = $1 AND p.last_name = $2 AND org.name = $3";

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if (!String.IsNullOrEmpty(nameText.Text) && !String.IsNullOrEmpty(lastnameText.Text))
            {
                cmd.Parameters.AddWithValue(nameText.Text);
                cmd.Parameters.AddWithValue(lastnameText.Text);
                cmd.CommandText = "SELECT p.id as ID, ul.username,p.first_name, p.last_name, cft.contact as Phone, cfe.contact as Email, cfa.contact as Address, org.name as Organization FROM person p " +
                    "JOIN user_login ul ON p.user_id = ul.id " +
                    "JOIN person_role pr ON p.id = pr.person_id AND pr.role_id = 4 " +
                    "LEFT JOIN contact_info cft ON p.id = cft.person_id AND cft.contact_type = 'телефон' " +
                    "LEFT JOIN contact_info cfe ON p.id = cfe.person_id AND cfe.contact_type = 'email' " +
                    "LEFT JOIN contact_info cfa ON p.id = cfa.person_id AND cfa.contact_type = 'адрес' " +
                    "JOIN organization org on p.id = org.person_id WHERE p.first_name = $1 AND p.last_name = $2";

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if(!String.IsNullOrEmpty(nameText.Text) && String.IsNullOrEmpty(lastnameText.Text) && String.IsNullOrEmpty(organizationText.Text))
            {
                cmd.Parameters.AddWithValue(nameText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if(!String.IsNullOrEmpty(lastnameText.Text) && String.IsNullOrEmpty(lastnameText.Text) && String.IsNullOrEmpty(organizationText.Text))
            {
                cmd.Parameters.AddWithValue(lastnameText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if(!String.IsNullOrEmpty(organizationText.Text) && String.IsNullOrEmpty(lastnameText.Text) && String.IsNullOrEmpty(organizationText.Text))
            {
                cmd.Parameters.AddWithValue(organizationText.Text);
                cmd.CommandText = "SELECT * FROM find_client($1); ";
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if (!String.IsNullOrEmpty(organizationText.Text) && String.IsNullOrEmpty(lastnameText.Text) && !String.IsNullOrEmpty(nameText.Text))
            {
                cmd.Parameters.AddWithValue(nameText.Text);
                cmd.Parameters.AddWithValue(organizationText.Text);
                cmd.CommandText = "SELECT p.id as ID, ul.username,p.first_name, p.last_name, cft.contact as Phone, cfe.contact as Email, cfa.contact as Address, org.name as Organization FROM person p " +
                    "JOIN user_login ul ON p.user_id = ul.id " +
                    "JOIN person_role pr ON p.id = pr.person_id AND pr.role_id = 4 " +
                    "LEFT JOIN contact_info cft ON p.id = cft.person_id AND cft.contact_type = 'телефон' " +
                    "LEFT JOIN contact_info cfe ON p.id = cfe.person_id AND cfe.contact_type = 'email' " +
                    "LEFT JOIN contact_info cfa ON p.id = cfa.person_id AND cfa.contact_type = 'адрес' " +
                    "JOIN organization org on p.id = org.person_id WHERE p.first_name = $1 AND org.name = $2";

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
            else if (!String.IsNullOrEmpty(organizationText.Text) && String.IsNullOrEmpty(nameText.Text) && !String.IsNullOrEmpty(lastnameText.Text))
            {
                cmd.Parameters.AddWithValue(lastnameText.Text);
                cmd.Parameters.AddWithValue(organizationText.Text);
                cmd.CommandText = "SELECT p.id as ID, ul.username,p.first_name, p.last_name, cft.contact as Phone, cfe.contact as Email, cfa.contact as Address, org.name as Organization FROM person p " +
                    "JOIN user_login ul ON p.user_id = ul.id " +
                    "JOIN person_role pr ON p.id = pr.person_id AND pr.role_id = 4 " +
                    "LEFT JOIN contact_info cft ON p.id = cft.person_id AND cft.contact_type = 'телефон' " +
                    "LEFT JOIN contact_info cfe ON p.id = cfe.person_id AND cfe.contact_type = 'email' " +
                    "LEFT JOIN contact_info cfa ON p.id = cfa.person_id AND cfa.contact_type = 'адрес' " +
                    "JOIN organization org on p.id = org.person_id WHERE p.last_name = $1 AND org.name = $2";

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                connection.Dispose();
                connection.Close();
            }
        }
    }
}