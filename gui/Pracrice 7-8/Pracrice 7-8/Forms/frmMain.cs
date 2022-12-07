using DevExpress.XtraEditors;
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
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private string role;

        public frmMain(string role)
        {
            InitializeComponent();

            this.role = role;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show(this.role, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}