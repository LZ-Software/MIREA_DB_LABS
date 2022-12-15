using DevExpress.XtraEditors;
using Npgsql;
using System;
using System.Windows.Forms;

namespace Pracrice_7_8.Forms
{
    public partial class frmRegister : DevExpress.XtraEditors.XtraForm
    {
        public frmRegister()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.appIcon;

            loadRoles(comboBox);
        }

        private void loadRoles(ComboBoxEdit comboBox)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "SELECT * FROM roles WHERE title != 'Клиент'";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("title"));
                        comboBox.Properties.Items.Add(name);
                    }
                }
                else
                {
                    MessageBox.Show("Отрыгнуло.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(loginText.Text) && !String.IsNullOrEmpty(passwordText.Text) && !String.IsNullOrEmpty(nameText.Text) && !String.IsNullOrEmpty(lastnameText.Text) && !String.IsNullOrEmpty(emailText.Text) && !String.IsNullOrEmpty(phoneText.Text) && !String.IsNullOrEmpty(addressText.Text) && comboBox.SelectedItem != null)
            {
                NpgsqlConnection connection = DBUtils.GetDBConnection();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue(loginText.Text);
                cmd.Parameters.AddWithValue(passwordText.Text);
                cmd.Parameters.AddWithValue(nameText.Text);
                cmd.Parameters.AddWithValue(lastnameText.Text);
                cmd.Parameters.AddWithValue(comboBox.SelectedItem.ToString());
                cmd.Parameters.AddWithValue(emailText.Text);
                cmd.Parameters.AddWithValue(phoneText.Text);
                cmd.Parameters.AddWithValue(addressText.Text);

                cmd.CommandText = "CALL create_worker($1, $2, $3, $4, $5, $6, $7, $8)";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Пользователь не добавлен.\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Заполните пустые поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}