using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetMailSample.Forms
{
    public partial class frmMessageOptions : Form
    {
        public string enBody, enBodyTransfer, enSubject, enHeaders;

        public frmMessageOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// set the encoding variables to the user provided data
        /// this will get added when the message is actually sent from the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            NetMailSample.Properties.Settings.Default.BodyEncoding = cboBodyEncoding.Text;
            NetMailSample.Properties.Settings.Default.HeaderEncoding = cboHeaderEncoding.Text;
            NetMailSample.Properties.Settings.Default.SubjectEncoding = cboSubjectEncoding.Text;
            NetMailSample.Properties.Settings.Default.BodyHtml = chkBodyHtml.Checked;
            NetMailSample.Properties.Settings.Default.ReadRcpt = chkReadRcpt.Checked;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
