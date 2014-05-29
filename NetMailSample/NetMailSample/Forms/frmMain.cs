using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using NetMailSample.Common;

namespace NetMailSample
{
    public partial class frmMain : Form
    {
        public string hdrName, hdrValue;
        DataTable inlineAttachmentsTable = new DataTable();
        bool ContinueTimerRun = false;
        ClassLogger _logger = null;

        public frmMain()
        {
            InitializeComponent();
            checkDotNetVersion();

            // create the logger
            _logger = new ClassLogger("NetMailErrors.log");
            _logger.LogAdded += new ClassLogger.LoggerEventHandler(_logger_LogAdded);
        }

        void _logger_LogAdded(object sender, LoggerEventArgs a)
        {
            try
            {
                if (txtBoxErrorLog.InvokeRequired)
                {
                    // Need to invoke
                    txtBoxErrorLog.Invoke(new MethodInvoker(delegate()
                    {
                        txtBoxErrorLog.Text = a.LogDetails;
                    }));
                }
                else
                {
                    txtBoxErrorLog.Text = a.LogDetails;
                }
            }
            catch (Exception ex)
            {
                txtBoxErrorLog.Text = ex.Message;
            }
        }

        /// <summary>
        /// check/display the version of .NET installed on the machine
        /// </summary>
        void checkDotNetVersion()
        {
            try
            {
                // check for the installed .NET versions
                toolStripRuntime.Text = DotNetVersion.GetRuntimeVersionFromEnvironment();
                toolStripNETVer.Text = DotNetVersion.GetDotNetVerFromRegistry();
            }
            catch (Exception ex)
            {
                _logger.Log("Error:" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// This function takes the input from the form, creates a mail message and sends it to the smtp server
        /// </summary>
        private void SendEmail()
        {
            // make sure we have values in user, password and To
            if (ValidateForm() == false)
            {
                return;
            }

            // create mail and smtp object
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            try
            {
                // set the sender email address
                mail.From = new MailAddress(MessageUtilities.parseEmail(txtBoxEmailAddress.Text));

                // check for To, Cc and Bcc
                // Cc and Bcc can be empty so we only need to validate when the textbox contains a value
                MessageUtilities.parseEmails(txtBoxTo.Text, mail, MessageUtilities.addressType.To);

                if (txtBoxCC.Text.Trim() != "")
                {
                    MessageUtilities.parseEmails(txtBoxCC.Text, mail, MessageUtilities.addressType.Cc);
                }

                if (txtBoxBCC.Text.Trim() != "")
                {
                    MessageUtilities.parseEmails(txtBoxBCC.Text, mail, MessageUtilities.addressType.Bcc);
                }

                // set encoding for message
                if (NetMailSample.Properties.Settings.Default.BodyEncoding != "")
                {
                    mail.BodyEncoding = MessageUtilities.GetEncodingValue(NetMailSample.Properties.Settings.Default.BodyEncoding);
                }
                if (NetMailSample.Properties.Settings.Default.SubjectEncoding != "")
                {
                    mail.SubjectEncoding = MessageUtilities.GetEncodingValue(NetMailSample.Properties.Settings.Default.SubjectEncoding);
                }
                if (NetMailSample.Properties.Settings.Default.HeaderEncoding != "")
                {
                    mail.HeadersEncoding = MessageUtilities.GetEncodingValue(NetMailSample.Properties.Settings.Default.HeaderEncoding);
                }

                // set priority for the message
                switch (NetMailSample.Properties.Settings.Default.MsgPriority)
                {
                    case "High":
                        mail.Priority = MailPriority.High;
                        break;
                    case "Low":
                        mail.Priority = MailPriority.Low;
                        break;
                    default:
                        mail.Priority = MailPriority.Normal;
                        break;
                }

                // add HTML AltView
                if (NetMailSample.Properties.Settings.Default.AltViewHtml != "")
                {
                    ContentType sHtmlContentType = new ContentType("text/html");
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(NetMailSample.Properties.Settings.Default.AltViewHtml, sHtmlContentType);

                    // add inline attachments / linked resource
                    if (inlineAttachmentsTable.Rows.Count > 0)
                    {
                        foreach (DataRow rowInl in inlineAttachmentsTable.Rows)
                        {
                            LinkedResource lr = new LinkedResource(rowInl.ItemArray[0].ToString());
                            lr.ContentId = rowInl.ItemArray[1].ToString();
                            lr.ContentType.MediaType = rowInl.ItemArray[2].ToString();
                            htmlView.LinkedResources.Add(lr);
                        }
                    }

                    // set transfer encoding
                    htmlView.TransferEncoding = MessageUtilities.GetTransferEncoding(NetMailSample.Properties.Settings.Default.htmlBodyTransferEncoding);
                    mail.AlternateViews.Add(htmlView);
                }
                
                // add Plain Text AltView
                if (NetMailSample.Properties.Settings.Default.AltViewPlain != "")
                {
                    ContentType sPlainContentType = new ContentType("text/plain");
                    AlternateView plainView = AlternateView.CreateAlternateViewFromString(NetMailSample.Properties.Settings.Default.AltViewPlain, sPlainContentType);
                    plainView.TransferEncoding = MessageUtilities.GetTransferEncoding(NetMailSample.Properties.Settings.Default.plainBodyTransferEncoding);
                    mail.AlternateViews.Add(plainView);
                }

                // add vCal AltView
                if (NetMailSample.Properties.Settings.Default.AltViewCal != "")
                {
                    ContentType ct = new ContentType("text/calendar");
                    ct.Parameters.Add("method", "REQUEST");
                    ct.Parameters.Add("name", "meeting.ics");
                    AlternateView calView = AlternateView.CreateAlternateViewFromString(NetMailSample.Properties.Settings.Default.AltViewCal, ct);
                    calView.TransferEncoding = MessageUtilities.GetTransferEncoding(NetMailSample.Properties.Settings.Default.vCalBodyTransferEncoding);
                    mail.AlternateViews.Add(calView);
                }

                // add custom headers
                foreach (DataGridViewRow rowHdr in dGridHeaders.Rows)
                {
                    if (rowHdr.Cells[0].Value != null)
                    {
                        mail.Headers.Add(rowHdr.Cells[0].Value.ToString(), rowHdr.Cells[1].Value.ToString());
                    }
                }
                
                // add attachements
                foreach (DataGridViewRow rowAtt in dGridAttachments.Rows)
                {
                    if (rowAtt.Cells[0].Value != null)
                    {
                        Attachment data = new Attachment(rowAtt.Cells[0].Value.ToString(), rowAtt.Cells[1].Value.ToString());
                        if (rowAtt.Cells[4].Value.ToString() == "True")
                        {
                            data.ContentDisposition.Inline = true;
                            data.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                            data.ContentId = rowAtt.Cells[3].Value.ToString();
                            NetMailSample.Properties.Settings.Default.BodyHtml = true;
                        }
                        else
                        {
                            data.ContentDisposition.Inline = false;
                            data.ContentDisposition.DispositionType = DispositionTypeNames.Attachment;
                        }
                        mail.Attachments.Add(data);
                    }
                }

                // add read receipt
                if (NetMailSample.Properties.Settings.Default.ReadRcpt == true)
                {
                    mail.Headers.Add("Disposition-Notification-To", MessageUtilities.parseEmail(txtBoxEmailAddress.Text));
                }

                // set the content
                mail.Subject = txtBoxSubject.Text;
                mail.Body = richTxtBody.Text;
                mail.IsBodyHtml = NetMailSample.Properties.Settings.Default.BodyHtml;

                // smtp client setup
                string sUser = txtBoxEmailAddress.Text.Trim();
                string sPassword = mskPassword.Text.Trim();
                string sDomain = txtBoxDomain.Text.Trim();

                smtp.EnableSsl = chkEnableSSL.Checked;
                smtp.Port = Int32.Parse(cboPort.Text.Trim());
                smtp.Host = cboServer.Text;

                // check for credentials
                if (sUser.Length != 0)
                {
                    if (sDomain.Length != 0)
                    {
                        smtp.Credentials = new NetworkCredential(sUser, sPassword);
                    }                    
                    else
                    {
                        smtp.Credentials = new NetworkCredential(sUser, sPassword, sDomain);
                    }
                }

                // send by pickup folder?
                if (rdoSendByPickupFolder.Checked)
                {
                    if (this.chkBoxSpecificPickupFolder.Checked)
                    {
                        if (Directory.Exists(txtPickupFolder.Text))
                        {
                            smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                            smtp.PickupDirectoryLocation = txtPickupFolder.Text;
                        }
                        else
                        {
                            throw new DirectoryNotFoundException("The specified directory does not exist.");
                        }
                    }
                    else
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    }
                }

                // send email
                smtp.Send(mail);
                
                // output successful send notification
                _logger.Log("Message sent successfully." + "\r\n");
            }
            catch (SmtpException se)
            {
                _logger.Log("Error: " + se.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" 
                    + se.StackTrace + "\r\n" + "\r\n" + "Status Code: " + se.StatusCode + "\r\n" 
                    + "Description:" + MessageUtilities.GetSmtpStatusCodeDescription(se.StatusCode.ToString()));
            }
            catch (Exception ex)
            {
                _logger.Log("Error:" + ex.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" + ex.StackTrace);
            }
            finally
            {
                // cleanup resources
                if (mail != null)
                {
                    mail.Dispose();
                    mail = null;
                }
                if (smtp != null)
                {
                    smtp.Dispose();
                    smtp = null;
                }
                
            }
        }

        /// <summary>
        /// This button calls the sendemail function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            txtBoxErrorLog.Clear();
            SendEmail();
        }

        /// <summary>
        /// This button will allow the user to add an attachment to the mail message list
        /// but the send function handles actually adding the file to the mail message
        /// this just creates the file paths
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertAttachment_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBoxErrorLog.Clear();
                string file = openFileDialog1.FileName;
                string size;
                FileInfo f = new FileInfo(file);
                
                try
                {
                    string text = File.ReadAllText(file);
                    int n = dGridAttachments.Rows.Add();
                    size = FileUtilities.ConvertFileSize(f);
                    dGridAttachments.Rows[n].Cells[0].Value = file;
                    dGridAttachments.Rows[n].Cells[1].Value = MediaTypeNames.Application.Octet;
                    dGridAttachments.Rows[n].Cells[2].Value = size;
                    dGridAttachments.Rows[n].Cells[3].Value = "";
                    dGridAttachments.Rows[n].Cells[4].Value = "False";
                }
                catch (IOException ioe)
                {
                    _logger.Log("Error: " + ioe.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" + ioe.StackTrace);
                }
                catch (Exception ex)
                {
                    _logger.Log("Error: " + ex.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// display the form to add custom headers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddHeaders_Click(object sender, EventArgs e)
        {
            try
            {
                NetMailSample.Forms.frmAddHeaders aHdrForm = new Forms.frmAddHeaders();
                aHdrForm.Owner = this;
                aHdrForm.ShowDialog(this);
                if (hdrName != null && hdrValue != null)
                {
                    int n = dGridHeaders.Rows.Add();
                    dGridHeaders.Rows[n].Cells[0].Value = hdrName;
                    dGridHeaders.Rows[n].Cells[1].Value = hdrValue;
                }
            }
            catch (Exception ex)
            {
                _logger.Log("Error: " + ex.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// delete the currently selected attachment in the datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                int cellRow = dGridAttachments.CurrentCellAddress.Y;
                if (dGridAttachments.CurrentCell.ColumnIndex >= 0) 
                { 
                    dGridAttachments.Rows.RemoveAt(dGridAttachments.Rows[cellRow].Index); 
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
            catch (Exception ex)
            {
                _logger.Log("Error: " + ex.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// delete the currently selected row for the header datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteHeader_Click(object sender, EventArgs e)
        {
            try
            {
                int cellRow = dGridHeaders.CurrentCellAddress.Y;
                if (dGridHeaders.CurrentCell.ColumnIndex >= 0) 
                { 
                    dGridHeaders.Rows.RemoveAt(dGridHeaders.Rows[cellRow].Index); 
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
            catch (Exception ex)
            {
                _logger.Log("Error: " + ex.Message + "\r\n" + "\r\n" + "StackTrace: " + "\r\n" + ex.StackTrace);
            }
            
        }

        /// <summary>
        /// send an email every x seconds, based on the value of the numUpDn control
        /// ContinueTimerRun is initially set to true and you need to press the stop button to set it to false
        /// which should stop the sending loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartSendLoop_Click(object sender, EventArgs e)
        {
            this.txtBoxErrorLog.Text = string.Empty;
            Decimal msgCount = 0;
            ContinueTimerRun = true;

            _logger.Log("Started time based send.\r\n");
            btnStopSendLoop.Focus();

            if (ValidateForm() == false)
            {
                return;
            }

            while (ContinueTimerRun == true)
            {
                msgCount++;
                if (msgCount == 300)
                {
                    ContinueTimerRun = false;
                }
                _logger.Log(string.Format("Sending Message {0}...\r\n", msgCount));
                SendEmail();
                WaitLoop((int)numUpDnSeconds.Value);
            }

            _logger.Log("Finished timer based email send.\r\n");
        }

        /// <summary>
        /// this button sets the ContinueTimerRun to false, which ends the sending loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopSendLoop_Click(object sender, EventArgs e)
        {
            txtBoxErrorLog.Clear();
            ContinueTimerRun = false;
            _logger.Log("User chose to stop email loop.\r\n");
        }

        /// <summary>
        /// sleep function to wait "numSeconds" before returning control back to the caller
        /// </summary>
        /// <param name="numSeconds">amount of time to wait</param>
        private void WaitLoop(int numSeconds)
        {
            DateTime oStart = DateTime.Now;

            for (int secondsLoop = 0; secondsLoop < numSeconds; secondsLoop++)
            {
                for (int Loop1 = 0; Loop1 < 10; Loop1++)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();

                    if (ContinueTimerRun == false)
                        break;
                }

                if (ContinueTimerRun == false)
                    break;
            }
            DateTime oEnd = DateTime.Now;
        }

        /// <summary>
        /// used to validate the User, Password and To fields before sending
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            bool bRet = true;

            StringBuilder oSB = new System.Text.StringBuilder();

            if (txtBoxEmailAddress.Text.Trim() == "")
            {
                oSB.AppendFormat("User is required.\r\n");
                bRet = false;
            }

            if (chkPasswordRequired.Checked && mskPassword.Text.Trim() == "")
            {
                oSB.AppendFormat("Password is required.\r\n");
                bRet = false;
            }

            if (txtBoxTo.Text.Trim() == "")
            {
                oSB.AppendFormat("To address is not valid. \r\n");
                bRet = false;
            }

            _logger.Log(oSB.ToString());
            return bRet;
        }

        /// <summary>
        /// display the add alt view form
        /// this sets altViewHtml and altViewPlain when it returns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAltView_Click(object sender, EventArgs e)
        {
            NetMailSample.Forms.frmAlternateView aAltViewForm = new Forms.frmAlternateView(txtBoxSubject.Text);
            aAltViewForm.Owner = this;
            aAltViewForm.ShowDialog(this);
            if (NetMailSample.Properties.Settings.Default.AltViewCal != "")
            {
                richTxtBody.Text = "";
            }
            else
            {
                if (NetMailSample.Properties.Settings.Default.AltViewHtml != "")
                {
                    richTxtBody.Text = NetMailSample.Properties.Settings.Default.AltViewHtml;
                    inlineAttachmentsTable = aAltViewForm.inlineTable;
                }
                else
                {
                    richTxtBody.Text = NetMailSample.Properties.Settings.Default.AltViewPlain;
                }
            }
            
        }

        /// <summary>
        /// display the message options form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessageOptions_Click(object sender, EventArgs e)
        {
            NetMailSample.Forms.frmMessageOptions mEncoding = new Forms.frmMessageOptions();
            mEncoding.Owner = this;
            mEncoding.ShowDialog(this);
        }

        /// <summary>
        /// display the edit content type form
        /// the constructor for this form takes a string value which allows the previous value
        /// to be returned if the user cancels the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditContentType_Click(object sender, EventArgs e)
        {
            try
            {
                if (dGridAttachments.CurrentCell.ColumnIndex >= 0)
                {
                    int cellRow = dGridAttachments.CurrentCellAddress.Y;

                    NetMailSample.Forms.frmEditContentType mEditContentType = new Forms.frmEditContentType(dGridAttachments.Rows[cellRow].Cells[1].Value.ToString(), dGridAttachments.Rows[cellRow].Cells[3].Value.ToString());
                    mEditContentType.Owner = this;
                    mEditContentType.ShowDialog(this);
                    dGridAttachments.Rows[cellRow].Cells[1].Value = mEditContentType.newContentType;
                    if (mEditContentType.isInline == true)
                    {
                        dGridAttachments.Rows[cellRow].Cells[3].Value = mEditContentType.newCid;
                        dGridAttachments.Rows[cellRow].Cells[4].Value = "True";
                    }
                    else
                    {
                        dGridAttachments.Rows[cellRow].Cells[3].Value = mEditContentType.newCid;
                        dGridAttachments.Rows[cellRow].Cells[4].Value = "False";
                    }
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        // toggle the time based send feature
        private void chkTimeBasedSend_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkTimeBasedSend.Checked)
            {
                btnSendEmail.Enabled = false;
                btnStartSendLoop.Enabled = true;
                btnStopSendLoop.Enabled = true;
                lblNumSeconds.Enabled = true;
                numUpDnSeconds.Enabled = true;
            }
            else
            {
                btnSendEmail.Enabled = true;
                btnStartSendLoop.Enabled = false;
                btnStopSendLoop.Enabled = false;
                lblNumSeconds.Enabled = false;
                numUpDnSeconds.Enabled = false;
            }
        }

        // toggle the pickup folder textbox
        private void chkBoxSpecificPickupFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxSpecificPickupFolder.Checked)
            {
                txtPickupFolder.Enabled = true;
            }
            else
            {
                txtPickupFolder.Enabled = false;
            }
        }

        // toggle the send by pickup checkbox
        private void rdoSendByPickupFolder_CheckedChanged(object sender, EventArgs e)
        {
            chkEnableSSL.Checked = false;
            if (rdoSendByPickupFolder.Checked)
            {
                chkBoxSpecificPickupFolder.Enabled = true;
            }
            else
            {
                chkBoxSpecificPickupFolder.Checked = false;
                chkBoxSpecificPickupFolder.Enabled = false;
                txtPickupFolder.Enabled = false;
            }
        }       
    }
}