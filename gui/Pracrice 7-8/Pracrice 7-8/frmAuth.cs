using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pracrice_7_8
{
    public partial class frmAuth : DevExpress.XtraEditors.XtraForm
    {
        public frmAuth()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.appIcon;
        }

        private void frmAuth_Load(object sender, EventArgs e)
        {

        }

        private void authButton_Click(object sender, EventArgs e)
        {
            var a = DBServerUtils.GetDBConnection("postgres", "xecc99634e2080b087c8556d8e0251ed78d55819dc274628024e589b08b7e4bc1");
        }
    }
}
