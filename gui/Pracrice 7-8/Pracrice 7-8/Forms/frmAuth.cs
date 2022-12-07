using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pracrice_7_8.Forms;

namespace Pracrice_7_8
{
    public partial class frmAuth : DevExpress.XtraEditors.XtraForm
    {
        public frmAuth()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.appIcon;
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection connection = DBUtils.GetDBConnection();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            string login = this.loginText.Text.Trim();
            string password = Hash.ComputeSHA256(this.passwordText.Text.Trim()).ToLower();

            cmd.Parameters.AddWithValue(login);
            cmd.Parameters.AddWithValue(password);

            cmd.CommandText = "SELECT r.name FROM user_login ul JOIN person_role pr ON pr.id = ul.id JOIN roles r ON pr.id = r.id WHERE ul.username = $1 AND ul.password = $2";

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string role = reader.GetString(reader.GetOrdinal("name"));

                        frmMain main = new frmMain(role);
                        main.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
