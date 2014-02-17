using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMailSample.Forms
{
    public partial class frmAddHeaders : Form
    {
        public frmAddHeaders()
        {
            InitializeComponent();
        }

        // if the header and value both have data, then we can add that to the 
        // data grid on the main form, otherwise just close out of the form
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtValue.Text != "")
            {
                NetMailSample.frmMain f = this.Owner as NetMailSample.frmMain;
                f.hdrName = txtName.Text;
                f.hdrValue = txtValue.Text;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
