using System;
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
            try
            {
                // set the individual options for the form based on the app settings
                if (NetMailSample.Properties.Settings.Default.BodyHtml == true) { chkBodyHtml.Checked = true; }
                if (NetMailSample.Properties.Settings.Default.ReadRcpt == true) { chkReadRcpt.Checked = true; }

                switch (NetMailSample.Properties.Settings.Default.MsgPriority)
                {
                    case "High":
                        cboMsgPriority.Text = "High";
                        break;
                    case "Low":
                        cboMsgPriority.Text = "Low";
                        break;
                    default:
                        cboMsgPriority.Text = "Normal";
                        break;
                }

                cboBodyEncoding.Text = NetMailSample.Properties.Settings.Default.BodyEncoding;
                cboHeaderEncoding.Text = NetMailSample.Properties.Settings.Default.HeaderEncoding;
                cboSubjectEncoding.Text = NetMailSample.Properties.Settings.Default.SubjectEncoding;
            }
            catch (Exception)
            {
                return;
            }
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
            NetMailSample.Properties.Settings.Default.MsgPriority = cboMsgPriority.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
