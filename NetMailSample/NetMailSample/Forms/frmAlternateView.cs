﻿using System;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace NetMailSample.Forms
{
    public partial class frmAlternateView : Form
    {
        public string cid, cidPath, tempSubject;
        public DataTable inlineTable = new DataTable();
        public AlternateView avCal;

        public frmAlternateView(string subject)
        {
            InitializeComponent();
            cboTransferEncoding.Text = NetMailSample.Properties.Settings.Default.htmlBodyTransferEncoding;
            tempSubject = subject;
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
                    NetMailSample.Properties.Settings.Default.vCalBodyTransferEncoding = cboTransferEncoding.Text;
                    break;
                case "PlainText":
                    NetMailSample.Properties.Settings.Default.AltViewPlain = txtAltViewBody.Text;
                    NetMailSample.Properties.Settings.Default.plainBodyTransferEncoding = cboTransferEncoding.Text;
                    break;
                default:
                    NetMailSample.Properties.Settings.Default.AltViewHtml = txtAltViewBody.Text;
                    NetMailSample.Properties.Settings.Default.htmlBodyTransferEncoding = cboTransferEncoding.Text;
                    AddInlineTableForAttachments();
                    break;
            }

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
                NetMailSample.Properties.Settings.Default.BodyHtml = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Path and Content Id are required to add a linked resource.",
                "Invalid Linked Resource", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteAttachment_Click(object sender, EventArgs e)
        {
            try
            { 
                int cellRow = dGridInlineAttachments.CurrentCellAddress.Y;
                if (dGridInlineAttachments.CurrentCell.ColumnIndex >= 0) 
                { 
                    dGridInlineAttachments.Rows.RemoveAt(dGridInlineAttachments.Rows[cellRow].Index); 
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        private void btnModifyContentType_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGridInlineAttachments.CurrentCell.ColumnIndex >= 0)
                {
                    int cellRow = dGridInlineAttachments.CurrentCellAddress.Y;
                    string ctype, cid;

                    // null column check
                    if (dGridInlineAttachments.Rows[cellRow].Cells[1].Value != null)
                    {
                        ctype = dGridInlineAttachments.Rows[cellRow].Cells[1].Value.ToString();
                    }
                    else
                    {
                        ctype = "";
                    }

                    if (dGridInlineAttachments.Rows[cellRow].Cells[2].Value != null)
                    {
                        cid = dGridInlineAttachments.Rows[cellRow].Cells[2].Value.ToString();
                    }
                    else
                    {
                        cid = "";
                    }

                    NetMailSample.Forms.frmEditContentType mEditContentType = new Forms.frmEditContentType(cid, ctype);
                    mEditContentType.Owner = this;
                    mEditContentType.ShowDialog(this);
                    dGridInlineAttachments.Rows[cellRow].Cells[2].Value = mEditContentType.newContentType;
                    dGridInlineAttachments.Rows[cellRow].Cells[1].Value = mEditContentType.newCid;
                    if (mEditContentType.isInline == true)
                    {
                        NetMailSample.Properties.Settings.Default.BodyHtml = true;
                    }
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
            cboTransferEncoding.Text = "Base64";

            DateTime dtStart = DateTime.Now.AddHours(1);
            DateTime dtEnd = DateTime.Now.AddHours(2);
            string msgBody = "Test Message Body";
            string msgSubject = tempSubject;
            string msgTo = "ToEmail@email.com";
            string msgFrom = "FromEmail@email.com";
            string msgDispName = "FNLN";

            StringBuilder sbCal = new StringBuilder();
            sbCal.Append("BEGIN:VCALENDAR\r\n");
            sbCal.Append("METHOD:REQUEST\r\n");
            sbCal.Append("STATUS:CONFIRMED\r\n");
            sbCal.Append("BEGIN:VEVENT\r\n");

            sbCal.Append(string.Format("TZID:{0}", "US-Eastern\r\n"));
            sbCal.Append(string.Format("DTSTART:{0:yyyyMMddTHHmmss\r\n}", dtStart));
            sbCal.Append(string.Format("DTEND:{0:yyyyMMddTHHmmss\r\n}", dtEnd));
            sbCal.Append(string.Format("DTSTAMP:{0:yyyyMMddTHHmmss\r\n}", DateTime.Now));

            sbCal.Append(string.Format("UID:{0}\r\n", Guid.NewGuid()));
            sbCal.Append(string.Format("DESCRIPTION:{0}\r\n", msgBody));
            sbCal.Append(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}\r\n", msgBody));
            sbCal.Append(string.Format("SUMMARY:{0}\r\n", msgSubject));
            sbCal.Append(string.Format("ORGANIZER:MAILTO:{0}\r\n", msgFrom));

            sbCal.Append(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}\r\n", msgDispName, msgTo));
            sbCal.Append("END:VEVENT\r\n");
            sbCal.Append("END:VCALENDAR\r\n");
            
            txtAltViewBody.Text = sbCal.ToString();
        }

        /// <summary>
        /// This button click will decode a base64 string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertEncoding_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] encodedBytes = Convert.FromBase64String(txtAltViewBody.Text);
                string result = Encoding.UTF8.GetString(encodedBytes);
                txtAltViewBody.Text = result;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
