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

            gridView1.AddNewRow();
            var a = gridView1.GetRow(0);
            gridView1.SetRowCellValue(0, "id", 1);

        }
    }
}