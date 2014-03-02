using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMailSample.Forms
{
    public partial class frmAlternateView : Form
    {
        public string cid, cidPath;
        public DataTable inlineTable = new DataTable();
        public AlternateView avCal;

        public frmAlternateView()
        {
            InitializeComponent();
            cboTransferEncoding.Text = NetMailSample.Properties.Settings.Default.BodyTransferEncoding;
        }

        /// <summary>
        /// This button adds the html and plain text Alt Views to 
        /// the main forms string values, which will get added when the message is sent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAlternateViews_Click(object sender, EventArgs e)
        {
            switch (cboAltViewContentType.Text)
            {
                case "vCalendar":
                    StringBuilder sb = new StringBuilder();
                    string[] lines = txtAltViewBody.Lines;

                    for(int i = 0; i < lines.GetUpperBound(0); i++)
                    {
                        sb.AppendLine((lines[i]));
                    }
                    NetMailSample.Properties.Settings.Default.AltViewCal = sb.ToString();
                    break;
                case "PlainText":
                    NetMailSample.Properties.Settings.Default.AltViewPlain = txtAltViewBody.Text;
                    break;
                default:
                    NetMailSample.Properties.Settings.Default.AltViewHtml = txtAltViewBody.Text;
                    AddInlineTableForAttachments();
                    break;
            }

            NetMailSample.Properties.Settings.Default.BodyTransferEncoding = cboTransferEncoding.Text;
            this.Close();
        }

        private void AddInlineTableForAttachments()
        {
            inlineTable.Columns.Add("Path", typeof(string));
            inlineTable.Columns.Add("Cid", typeof(string));
            inlineTable.Columns.Add("ContentType", typeof(string));

            foreach (DataGridViewRow rowAtt in dGridInlineAttachments.Rows)
            {
                if (rowAtt.Cells[0].Value != null)
                {
                    inlineTable.Rows.Add(rowAtt.Cells[0].Value, rowAtt.Cells[1].Value, rowAtt.Cells[2].Value);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLinkedResBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtLinkedResPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnAddLR_Click(object sender, EventArgs e)
        {
            if (txtLinkedResPath.Text != "" && txtCid.Text != "")
            {
                int n = dGridInlineAttachments.Rows.Add();
                dGridInlineAttachments.Rows[n].Cells[0].Value = txtLinkedResPath.Text;
                dGridInlineAttachments.Rows[n].Cells[1].Value = txtCid.Text;
                dGridInlineAttachments.Rows[n].Cells[2].Value = MediaTypeNames.Application.Octet;
                txtLinkedResPath.Text = "";
                txtCid.Text = "";
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Both ContentId and File Path are required to add a linked resource.",
                "Invalid Linked Resource", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteAttachment_Click(object sender, EventArgs e)
        {
            if (dGridInlineAttachments.SelectedRows.Count > 0) { dGridInlineAttachments.Rows.RemoveAt(dGridInlineAttachments.SelectedRows[0].Index); }
        }

        private void btnModifyContentType_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGridInlineAttachments.SelectedRows.Count > 0)
                {
                    NetMailSample.Forms.frmEditContentType mEditContentType = new Forms.frmEditContentType(dGridInlineAttachments.SelectedRows[0].Cells[2].Value.ToString());
                    mEditContentType.Owner = this;
                    mEditContentType.ShowDialog(this);
                    dGridInlineAttachments.SelectedRows[0].Cells[2].Value = mEditContentType.attContentType;
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        // this button will display a vCalendar sample in the textbox
        private void btnCalSample_Click(object sender, EventArgs e)
        {
            cboAltViewContentType.Text = "vCalendar";

            DateTime dtStart = DateTime.Now.AddHours(1);
            DateTime dtEnd = DateTime.Now.AddHours(2);
            string msgBody = "Test Message Body";
            string msgSubject = "Test Subject";
            string msgTo = "brandon.desjarlais@hotmail.com";
            string msgFrom = "brandondesjarlais@outlook.com";
            string msgDispName = "BD";

            StringBuilder sbCal = new StringBuilder();
            sbCal.AppendLine("BEGIN:VCALENDAR");
            sbCal.AppendLine("METHOD:REQUEST");
            sbCal.AppendLine("STATUS:CONFIRMED");
            sbCal.AppendLine("BEGIN:VEVENT");

            sbCal.AppendLine(string.Format("TZID:{0}", "US-Eastern"));
            sbCal.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmss}", dtStart));
            sbCal.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmss}", dtEnd));
            sbCal.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmss}", DateTime.Now));

            sbCal.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            sbCal.AppendLine(string.Format("DESCRIPTION:{0}", msgBody));
            sbCal.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msgBody));
            sbCal.AppendLine(string.Format("SUMMARY:{0}", msgSubject));
            sbCal.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msgFrom));

            sbCal.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msgDispName, msgTo));
            sbCal.AppendLine("END:VEVENT");
            sbCal.AppendLine("END:VCALENDAR");
            
            txtAltViewBody.Text = sbCal.ToString();
        }
    }
}
