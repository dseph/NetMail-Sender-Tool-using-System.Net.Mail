using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;
using NetMailSample.Common;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Remoting.Messaging;

using System.Text;
using System.Runtime.InteropServices;

namespace NetMailSample
{
    public partial class frmMain : Form
    {
        public string hdrName, hdrValue, msgSubject, cellEdit;
        DataTable inlineAttachmentsTable = new DataTable();
        AlternateView plainView, htmlView, calView;
        bool continueTimerRun = false;
        bool formValidated = false;
        bool noErrFound = true;
        ClassLogger _logger = null;
        string _logFilePath = string.Empty;
        public StringBuilder _sbLogLine = new StringBuilder();


        public frmMain()
        {
            InitializeComponent();

            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _logFilePath = Path.Combine(userFolder, "NetMailErrors.log");

            // create the logger
            _logger = new ClassLogger(_logFilePath);
            
            _logger.LogAdded += new ClassLogger.LoggerEventHandler(_logger_LogAdded);
 
            txtBoxErrorLog.Clear();

            string sVerWarning  = string.Empty;
            sVerWarning += "Note - Running under installed .Net version:  " + RuntimeInformation.FrameworkDescription + "\r\n";
            sVerWarning += "Some SMTP serves require TLS 1.1 or 1.2 to be used.\r\n";
            sVerWarning += ".Net 4.7.1+ should be used for servers needing TLS 1.1 support and .Net 4.8+ for TLS 1.3 support.\r\n";
            sVerWarning += "Exchange Online requires TLS 1.1.\r\n\r\n";

            txtBoxErrorLog.Text = sVerWarning;
 
    
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
        //void CheckDotNetVersion()
        //{
        //    StringBuilder oSB = new StringBuilder();


        //    string sVer = "Note - Running under installed .Net version:  " + RuntimeInformation.FrameworkDescription + "\r\n";
        //    //sVer += "The current runtime version is " + DotNetVersion.GetRuntimeVersionFromEnvironment();
           
        
        //    try
        //    {
        //        // check for the installed .NET versions
        //        oSB.AppendLine("The .NET Runtime = " + DotNetVersion.GetRuntimeVersionFromEnvironment());
        //        oSB.AppendLine(DotNetVersion.GetDotNetVerFromRegistry());
        //        string sDotNetVersionCheck = DotNetVersion.GetDotNetVerFromRegistry(); 
        //        if (sDotNetVersionCheck == "The .NET Framework version 4.5 or higher is NOT installed.")
        //        {
        //            oSB.AppendLine("Installed versions of the .NET Framework that are:\n");
        //            oSB.AppendLine(DotNetVersion.GetPreV45FromRegistry());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        oSB.AppendLine("Error:" + ex.Message);
        //        oSB.AppendLine("Stack Trace: " + ex.StackTrace);
        //    }

        //    _logger.Log(oSB.ToString());
        //}

        /// <summary>
        /// This method takes the input from the form, creates a mail message and sends it to the smtp server
        /// </summary>
        private void SendEmail()
        {
            _sbLogLine = new StringBuilder();
            _sbLogLine.AppendLine("Preparing to send: " + DateTime.Now.ToString());

            // make sure we have values in user, password and To
            if (ValidateForm() == false)
            {
                return;
            }

            // create mail, smtp and mailaddress objects
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            MailAddressCollection mailAddrCol = new MailAddressCollection();

            try
            {
                // set the From email address information
                mail.From = new MailAddress(txtBoxFrom.Text.Trim());

                // set the To email address information
                mailAddrCol.Clear();
                _sbLogLine.AppendLine("Adding From Address addresses: " + txtBoxFrom.Text.Trim());
                mail.From = new MailAddress(txtBoxFrom.Text.Trim());
                //mailAddrCol.Add(txtBoxTo.Text);

                _sbLogLine.AppendLine("Adding To addresses: " + txtBoxTo.Text);
                MessageUtilities.SetRecipientsFromString(ref mail, "To", txtBoxTo.Text.Trim());
                _sbLogLine.AppendLine("Adding CC addresses: " + txtBoxCC.Text);
                MessageUtilities.SetRecipientsFromString(ref mail, "CC", txtBoxCC.Text.Trim());
                _sbLogLine.AppendLine("Adding BCC addresses: " + txtBoxBCC.Text);
                MessageUtilities.SetRecipientsFromString(ref mail, "BCC", txtBoxBCC.Text.Trim());


 

                // set encoding for message
                if (Properties.Settings.Default.BodyEncoding != "")
                {
                    _sbLogLine.AppendLine("Setting Body encoding: " + Properties.Settings.Default.BodyEncoding.ToString());
                    mail.BodyEncoding = MessageUtilities.GetEncodingValue(Properties.Settings.Default.BodyEncoding);
                     
                }
                if (Properties.Settings.Default.SubjectEncoding != "")
                {
                    _sbLogLine.AppendLine("Setting Subject encoding: " + Properties.Settings.Default.SubjectEncoding.ToString());
                    mail.SubjectEncoding = MessageUtilities.GetEncodingValue(Properties.Settings.Default.SubjectEncoding);
                }
                if (Properties.Settings.Default.HeaderEncoding != "")
                {
                    _sbLogLine.AppendLine("Setting Header encoding: " + Properties.Settings.Default.HeaderEncoding.ToString());
                    mail.HeadersEncoding = MessageUtilities.GetEncodingValue(Properties.Settings.Default.HeaderEncoding);
                }

                // set priority for the message
                switch (Properties.Settings.Default.MsgPriority)
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
                _sbLogLine.AppendLine("Setting Priority: " + mail.Priority.ToString());

                // add HTML AltView
                if (Properties.Settings.Default.AltViewHtml != "")
                {
                    _sbLogLine.AppendLine("Setting HTML Alternative view. ");

                    ContentType ctHtml = new ContentType("text/html");
                    htmlView = AlternateView.CreateAlternateViewFromString(Properties.Settings.Default.AltViewHtml, ctHtml);
                    
                    // add inline attachments / linked resource
                    if (inlineAttachmentsTable.Rows.Count > 0)
                    {
                        foreach (DataRow rowInl in inlineAttachmentsTable.Rows)
                        {
                            LinkedResource lr = new LinkedResource(rowInl.ItemArray[0].ToString());
                            lr.ContentId = rowInl.ItemArray[1].ToString();
                            lr.ContentType.MediaType = rowInl.ItemArray[2].ToString();
                            htmlView.LinkedResources.Add(lr);
                            lr.Dispose();
                        }
                    }

                    // set transfer encoding
                    htmlView.TransferEncoding = MessageUtilities.GetTransferEncoding(Properties.Settings.Default.htmlBodyTransferEncoding);
                    mail.AlternateViews.Add(htmlView);                   
                }

                // add Plain Text AltView
                if (Properties.Settings.Default.AltViewPlain != "")
                {
                    _sbLogLine.AppendLine("Setting Plain Alternative view. ");
                    

                    ContentType ctPlain = new ContentType("text/plain");
                    plainView = AlternateView.CreateAlternateViewFromString(Properties.Settings.Default.AltViewPlain, ctPlain);
                    plainView.TransferEncoding = MessageUtilities.GetTransferEncoding(Properties.Settings.Default.plainBodyTransferEncoding);
                    mail.AlternateViews.Add(plainView);
                }

                // add vCal AltView
                if (Properties.Settings.Default.AltViewCal != "")
                {
                    _sbLogLine.AppendLine("Setting VCAL Alternative view. ");
                    _sbLogLine.AppendLine("Warning: Creating meeting requests, meeting updats and meeting cancelations is not supported with System.Web.Mail.  Consider using Graph or EWS.");

                    ContentType ctCal = new ContentType("text/calendar");
                    ctCal.Parameters.Add("method", "REQUEST");
                    ctCal.Parameters.Add("name", "meeting.ics");
                    calView = AlternateView.CreateAlternateViewFromString(Properties.Settings.Default.AltViewCal, ctCal);
                    calView.TransferEncoding = MessageUtilities.GetTransferEncoding(Properties.Settings.Default.vCalBodyTransferEncoding);
                    mail.AlternateViews.Add(calView);
                }

                // add custom headers
                foreach (DataGridViewRow rowHdr in dGridHeaders.Rows)
                {
                    _sbLogLine.AppendLine("Adding specified headers");

                    if (rowHdr.Cells[0].Value != null)
                    {
                        _sbLogLine.AppendLine("  " + rowHdr.Cells[0].Value.ToString() + " - " + rowHdr.Cells[1].Value.ToString());
                        mail.Headers.Add(rowHdr.Cells[0].Value.ToString(), rowHdr.Cells[1].Value.ToString());
                    }
                }

                _sbLogLine.AppendLine("Adding Attatchments - starting");
                // add attachements
                foreach (DataGridViewRow rowAtt in dGridAttachments.Rows)
                {
                    if (rowAtt.Cells[0].Value != null)
                    {
                        Attachment data = new Attachment(rowAtt.Cells[0].Value.ToString(), FileUtilities.GetContentType(rowAtt.Cells[1].Value.ToString()));
                        if (rowAtt.Cells[4].Value.ToString() == "True")
                        {
                            data.ContentDisposition.Inline = true;
                            data.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                            data.ContentId = rowAtt.Cells[3].Value.ToString();
                            Properties.Settings.Default.BodyHtml = true;
                        }
                        else
                        {
                            data.ContentDisposition.Inline = false;
                            data.ContentDisposition.DispositionType = DispositionTypeNames.Attachment;
                        }
                        _sbLogLine.AppendLine("Adding...");

                        mail.Attachments.Add(data);
                        //data.Dispose();
                        _sbLogLine.AppendLine("Adding Attatchments - finished");
                    }
                }

                // add read receipt
                if (Properties.Settings.Default.ReadRcpt == true)
                {
                    _sbLogLine.AppendLine("Set Read Receipt.");
                    mail.Headers.Add("Disposition-Notification-To", txtBoxUserUpn.Text);
                }

                 

                // set the content
                mail.Subject = txtBoxSubject.Text;
                _sbLogLine.AppendLine("Setting Subject:  " + txtBoxSubject.Text);
                msgSubject = txtBoxSubject.Text;

                _sbLogLine.AppendLine("Setting Body text. ");
                mail.Body = richTxtBody.Text;

                _sbLogLine.AppendLine("Setting IsBodyHtml to:  " + mail.IsBodyHtml.ToString());
                mail.IsBodyHtml = Properties.Settings.Default.BodyHtml;
                
                // add delivery notifications
                if (Properties.Settings.Default.DelNotifOnFailure == true)
                {
                    _sbLogLine.AppendLine("Setting DeliveryNotificationOptions to:  DeliveryNotificationOptions.OnFailure"  );
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                }

                if (Properties.Settings.Default.DelNotifOnSuccess == true)
                {
                    _sbLogLine.AppendLine("Setting DeliveryNotificationOptions to:  DeliveryNotificationOptions.OnSuccess");
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }
                
                // check for credentials
                string sUser = txtBoxUserUpn.Text.Trim();
                string sPassword = mskPassword.Text.Trim();
                string sDomain = txtBoxDomain.Text.Trim();

                if (sUser.Length != 0)
                {
                    if (sDomain.Length != 0)
                    {
                        _sbLogLine.AppendLine("User, Password and Domain is set. Likely NTLM is used.");
                        smtp.Credentials = new NetworkCredential(sUser, sPassword, sDomain);
                    }
                    else
                    {
                        _sbLogLine.AppendLine("User and Password are set. Likely Basic/STMP auth is used.");
                        smtp.Credentials = new NetworkCredential(sUser, sPassword);
                    }
                }

                // send by pickup folder?
                if (rdoSendByPickupFolder.Checked)
                {
                    _sbLogLine.AppendLine("Send by pickup folder is chosen.");
                    if (chkBoxSpecificPickupFolder.Checked)
                    {
                        if (Directory.Exists(txtPickupFolder.Text.Trim()))
                        {
                            smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                            smtp.PickupDirectoryLocation = txtPickupFolder.Text.Trim();
                            _sbLogLine.AppendLine("Pickup folder:  " + txtPickupFolder.Text.Trim());
                        }
                        else
                        {
                            _sbLogLine.AppendLine("Error: The specified pickup folder does not exist.");
                            throw new DirectoryNotFoundException(@"The specified directory """ + txtPickupFolder.Text.Trim() + @""" does not exist.");
                        }
                    }
                    else
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    }
                }

                // smtp client setup 
                smtp.UseDefaultCredentials = false;
                _sbLogLine.AppendLine("UseDefaultCredentials:  " + smtp.UseDefaultCredentials.ToString());

                smtp.EnableSsl = chkEnableSSL.Checked;
                _sbLogLine.AppendLine("SSL Enabled:  " + chkEnableSSL.Checked.ToString());

                smtp.Port = Int32.Parse(cboPort.Text.Trim());
                _sbLogLine.AppendLine("Port:  " + smtp.Port.ToString());

                smtp.Host = cboServer.Text;
                _sbLogLine.AppendLine("Host:  " + smtp.Host.ToString());

                //smtp.Timeout = Properties.Settings.Default.SendSyncTimeout;
                //_sbLogLine.AppendLine("Timeout:  " + smtp.Timeout);
                _sbLogLine.AppendLine("Note: Timeout is not overridden.");

                // send email
                _sbLogLine.AppendLine("Sending: " + DateTime.Now.ToString());

                smtp.Send(mail);
            }
            catch (SmtpException se)
            {
                txtBoxErrorLog.Clear();
                noErrFound = false;

                _sbLogLine.AppendLine("Error occured at:  " + DateTime.Now.ToString());

                _logger.Log("StatusCode:  " + se.StatusCode.ToString());

                if (se.StatusCode == SmtpStatusCode.MailboxBusy || se.StatusCode == SmtpStatusCode.MailboxUnavailable)
                {
                    _sbLogLine.AppendLine("Error: " + se.Message);

                    _sbLogLine.AppendLine("Error: " + se.Source);
                    _sbLogLine.AppendLine("StackTrace: " + se.StackTrace);

                    _sbLogLine.AppendLine("Description:" + MessageUtilities.GetSmtpStatusCodeDescription(se.StatusCode.ToString()));
                    _sbLogLine.AppendLine("Inner Exception: " + se.InnerException);

                    _sbLogLine.AppendLine("Suggestion: Retry");
 
                     
                }
                else
                {
                    _sbLogLine.AppendLine("Error: " + se.Message);

                    _sbLogLine.AppendLine("Error: " + se.Source);
                    _sbLogLine.AppendLine("StackTrace: " + se.StackTrace);

                    _sbLogLine.AppendLine("Description:" + MessageUtilities.GetSmtpStatusCodeDescription(se.StatusCode.ToString()));
                    _sbLogLine.AppendLine("Inner Exception: " + se.InnerException);

                }
                 

               
            }
            catch (InvalidOperationException ioe)
            {
                // invalid smtp address used
                //txtBoxErrorLog.Clear();
                noErrFound = false;

                _sbLogLine.AppendLine("Error: " + ioe.Message);
                _sbLogLine.AppendLine("Source: " + ioe.Source);
                _sbLogLine.AppendLine("StackTrace: " + ioe.StackTrace);
                _sbLogLine.AppendLine("Inner Exception: " + ioe.InnerException);
                 
            }
            catch (FormatException fe)
            {
                // invalid smtp address used
                //txtBoxErrorLog.Clear();
                noErrFound = false;

                _sbLogLine.AppendLine("Source: " + fe.Source);
                _sbLogLine.AppendLine("Error: " + fe.Message);
                _sbLogLine.AppendLine("StackTrace: " + fe.StackTrace);
                _sbLogLine.AppendLine("Inner Exception: " + fe.InnerException);
            }
            catch (Exception ex)
            {
                //txtBoxErrorLog.Clear();
                noErrFound = false;

                _sbLogLine.AppendLine("Error: " + ex.Message);
                _sbLogLine.AppendLine("Source: " + ex.Source);
                _sbLogLine.AppendLine("StackTrace: " + ex.StackTrace);
                _sbLogLine.AppendLine("Inner Exception: " + ex.InnerException);
            }
            finally
            {
                // log success
                if (formValidated == true && noErrFound == true)
                {
                    _sbLogLine.AppendLine("Message subject = " + msgSubject);
                    _sbLogLine.AppendLine("Message send = SUCCESS");
                }

                _logger.Log(_sbLogLine.ToString());
                txtBoxErrorLog.Text = _sbLogLine.ToString();

                // cleanup resources
                mail.Dispose();
                mail = null;
                smtp.Dispose();
                smtp = null;
                
                // reset variables
                formValidated = false;
                noErrFound = true;
                inlineAttachmentsTable.Clear();
                hdrName = null;
                hdrValue = null;
                msgSubject = null;

                //txtBoxErrorLog.Text = File.ReadAllText(_logFilePath);
            }
        }

        /// <summary>
        /// This button calls the SendEmail method
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
        /// but the send method handles actually adding the file to the mail message
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
                FileInfo f = new FileInfo(file);

                try
                {
                    int n = dGridAttachments.Rows.Add();
                    string size = FileUtilities.SizeSuffix(f.Length);
                    dGridAttachments.Rows[n].Cells[0].Value = file;
                    dGridAttachments.Rows[n].Cells[1].Value = "Octet";
                    dGridAttachments.Rows[n].Cells[2].Value = size;
                    dGridAttachments.Rows[n].Cells[3].Value = "";
                    dGridAttachments.Rows[n].Cells[4].Value = "False";
                }
                catch (IOException ioe)
                {
                    _logger.Log("Error: " + ioe.Message);
                    _logger.Log("StackTrace: " + ioe.StackTrace);
                }
                catch (Exception ex)
                {
                    _logger.Log("Error: " + ex.Message);
                    _logger.Log("StackTrace: " + ex.StackTrace);
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
                Forms.frmAddHeaders aHdrForm = new Forms.frmAddHeaders();
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
                _logger.Log("Error: " + ex.Message);
                _logger.Log("StackTrace: " + ex.StackTrace);
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
            catch (NullReferenceException nre)
            {
                // if the attachment grid is empty, just return
                if (dGridAttachments.CurrentCellAddress.Y < 1)
                {
                    return;
                }
                else
                {
                    // log any other null ref errors
                    _logger.Log("Error: " + nre.Message);
                    _logger.Log("StackTrace: " + nre.StackTrace);
                    _logger.Log("Inner Exception: " + nre.InnerException);
                }
            }
            catch (Exception ex)
            {
                _logger.Log("Error: " + ex.Message);
                _logger.Log("StackTrace: " + ex.StackTrace);
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
            catch (NullReferenceException nre)
            {
                // if the header grid is empty, just return
                if (dGridHeaders.CurrentCellAddress.Y < 1)
                {
                    return;
                }
                else
                {
                    _logger.Log("Error: " + nre.Message);
                    _logger.Log("StackTrace: " + nre.StackTrace);
                    _logger.Log("Inner Exception: " + nre.InnerException);
                }
                
            }
            catch (Exception ex)
            {
                _logger.Log("Error: " + ex.Message);
                _logger.Log("StackTrace: " + ex.StackTrace);
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
            decimal msgCount = 0;
            continueTimerRun = true;

            btnStopSendLoop.Focus();

            if (ValidateForm() == false)
            {
                return;
            }

            while (continueTimerRun == true)
            {
                msgCount++;
                if (msgCount == 300)
                {
                    continueTimerRun = false;
                }
                _logger.Log(string.Format("Sending Message {0}...\r\n", msgCount));
                SendEmail();
                txtBoxErrorLog.Text = "Sending message " + msgCount;
                WaitLoop((int)numUpDnSeconds.Value);
            }

            _logger.Log("Finished timer based email send.\r\n");
            txtBoxErrorLog.Text = "Finished timer based email send.";
        }

        /// <summary>
        /// this button sets the ContinueTimerRun to false, which ends the sending loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopSendLoop_Click(object sender, EventArgs e)
        {
            continueTimerRun = false;
            _logger.Log("User chose to stop email loop.\r\n");
        }

        /// <summary>
        /// sleep function to wait "numSeconds" before returning control back to the caller
        /// </summary>
        /// <param name="numSeconds">amount of time to wait</param>
        private void WaitLoop(int numSeconds)
        {
            for (int secondsLoop = 0; secondsLoop < numSeconds; secondsLoop++)
            {
                for (int Loop1 = 0; Loop1 < 10; Loop1++)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();

                    if (continueTimerRun == false)
                        break;
                }

                if (continueTimerRun == false)
                    break;
            }
        }

        /// <summary>
        /// function used to validate the User, Password and To fields before sending
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            bool bRet = true;

            if (txtBoxUserUpn.Text.Trim() == "")
            {
                _logger.Log("User is required.");
                bRet = false;
            }

            if (chkPasswordRequired.Checked && mskPassword.Text.Trim() == "")
            {
                _logger.Log("Password is required.");
                bRet = false;
            }

            if (txtBoxTo.Text.Trim() == "")
            {
                _logger.Log("To address is required.");
                bRet = false;
            }

            formValidated = bRet;
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
            Forms.frmAlternateView aAltViewForm = new Forms.frmAlternateView();
            aAltViewForm.Owner = this;
            aAltViewForm.ShowDialog(this);
            richTxtBody.Text = Properties.Settings.Default.AltViewPlain;
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

        // form close event
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // clean up resources on exit
            _logger.Dispose();
        }

        // enable ssl checkbox when port radio is clicked
        private void rdoSendByPort_CheckedChanged(object sender, EventArgs e)
        {
            chkEnableSSL.Checked = true;
        }

        // when the user changes the dropdown for server, automatically change the port number to a recommended value
        private void cboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboServer.Text == "smtp.gmail.com" || cboServer.Text == "smtp.mail.yahoo.com" || cboServer.Text == "plus.smtp.mail.yahoo.com")
            {
                cboPort.Text = "465";
            }
            else if (cboServer.Text == "smtp.live.com" || cboServer.Text == "smtp.office365.com")
            {
                cboPort.Text = "587";
            }
            else
            {
                cboPort.Text = "25";
            }
        }

        /// <summary>
        /// open the log file in Notepad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("Notepad.exe", AppDomain.CurrentDomain.BaseDirectory + "NetMailErrors.log");
            }
            catch (Exception ex)
            {
                txtBoxErrorLog.Text = "Unable to open log file: " + ex.Message;
            }
        }

        /// <summary>
        /// this button handles displaying the File Open dialog and loading the settings from an existing xml file
        /// useriohelper is for displaying the dialog and serialhelper is for parsing out the xml for settings
        /// once the settings have been parsed from the xml file, setformfromconnectionsettings is called to populate the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileLoadSettings_Click(object sender, EventArgs e)
        {
            string sFile = string.Empty;
            string sFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            string sConnectionSettings = string.Empty;

            ConnectionSettings oConnectionSetting = null;
            string sFileContents = string.Empty;

            if (UserIoHelper.PickLoadFromFile(AppDomain.CurrentDomain.BaseDirectory, "*.xml", ref sFile, sFilter))
            {
                try
                {
                    sFileContents = File.ReadAllText(sFile);
                    oConnectionSetting = SerialHelper.DeserializeObjectFromString<ConnectionSettings>(sFileContents);
                    if (oConnectionSetting == null)
                        throw new Exception("Settings file cannot be deserialized.");
                    SetFormFromConnectionSettings(oConnectionSetting);
                }
                catch (Exception ex)
                {
                    txtBoxErrorLog.Clear();
                    txtBoxErrorLog.Text = ex.ToString() + "Error Loading File";
                }

            }
            oConnectionSetting = null;
        }

        /// <summary>
        /// save the current form settings to an xml file
        /// useriohelper is for displaying the save dialog
        /// setconnectionsettingsfromform handles which settings need to be saved
        /// then serialhelper streams that information to the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSaveSettings_Click(object sender, EventArgs e)
        {
            string sFile = string.Empty;
            string sFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            string sConnectionSettings = string.Empty;
            ConnectionSettings oConnectionSetting = new ConnectionSettings();

            SetConnectionSettingsFromForm(ref oConnectionSetting);

            if (UserIoHelper.PickSaveFileToFolder(AppDomain.CurrentDomain.BaseDirectory, "Email Settings.xml", ref sFile, sFilter))
            {
                sConnectionSettings = SerialHelper.SerializeObjectToString<ConnectionSettings>(oConnectionSetting);
                if (sConnectionSettings != string.Empty)
                {
                    try
                    {
                        System.IO.File.WriteAllText(sFile, sConnectionSettings);
                    }
                    catch (Exception ex)
                    {
                        txtBoxErrorLog.Clear();
                        txtBoxErrorLog.Text = ex.Message + "Error Saving File";
                    }
                }
            }
        }

        // display File Options dialog
        private void mnuFileOptions_Click(object sender, EventArgs e)
        {
            Forms.frmMessageOptions mEncoding = new Forms.frmMessageOptions();
            mEncoding.Owner = this;
            mEncoding.ShowDialog(this);
        }

        // display File About dialog
        private void mnuFileAbout_Click(object sender, EventArgs e)
        {
            Forms.frmAbout frm = new Forms.frmAbout();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        // apply settings for the form
        private void SetFormFromConnectionSettings(ConnectionSettings oConnectionSetting)
        {
            try
            {
                txtBoxUserUpn.Text = FixSetting(oConnectionSetting.User);
                txtBoxDomain.Text = FixSetting(oConnectionSetting.Domain);
                txtBoxSubject.Text = FixSetting(oConnectionSetting.MessageSubject);
                txtBoxTo.Text = FixSetting(oConnectionSetting.MessageTo);
                txtBoxCC.Text = FixSetting(oConnectionSetting.MessageCC);
                txtBoxBCC.Text = FixSetting(oConnectionSetting.MessageBcc);
                richTxtBody.Text = FixSetting(oConnectionSetting.MessageBody);

                if (oConnectionSetting.SendByPort == true)
                {
                    rdoSendByPort.Checked = true;
                    cboPort.Text = oConnectionSetting.Port;
                    cboServer.Text = oConnectionSetting.Server;
                }
                else
                {
                    rdoSendByPickupFolder.Checked = true;
                    chkBoxSpecificPickupFolder.Checked = oConnectionSetting.CustomPickupLocation;
                    txtPickupFolder.Text = FixSetting(oConnectionSetting.PickupLocation);
                }
            }
            catch (Exception ex)
            {
                txtBoxErrorLog.Clear();
                txtBoxErrorLog.Text = ex.Message + "Error loading settings into form";
            }
        }

        // string function to make sure we aren't using null values, empty values are fine
        private string FixSetting(string sSetting)
        {
            if (sSetting == null)
                return "";
            else
                return sSetting;
        }

        // specifiy the connection settings
        private void SetConnectionSettingsFromForm(ref ConnectionSettings oConnectionSetting)
        {
            oConnectionSetting.User = this.txtBoxUserUpn.Text;
            oConnectionSetting.Domain = this.txtBoxDomain.Text;
            oConnectionSetting.UseSSL = this.chkEnableSSL.Checked;
            oConnectionSetting.PasswordRequired = this.chkPasswordRequired.Checked;
            oConnectionSetting.MessageTo = this.txtBoxTo.Text;
            oConnectionSetting.MessageCC = this.txtBoxCC.Text;
            oConnectionSetting.MessageBcc = this.txtBoxBCC.Text;
            oConnectionSetting.MessageSubject = this.txtBoxSubject.Text;
            oConnectionSetting.MessageBody = this.richTxtBody.Text;
            
            oConnectionSetting.Port = this.cboPort.Text;
            oConnectionSetting.Server = this.cboServer.Text;
            oConnectionSetting.SendByPort = this.rdoSendByPort.Checked;
            oConnectionSetting.CustomPickupLocation = this.chkBoxSpecificPickupFolder.Checked;
            oConnectionSetting.PickupLocation = this.txtPickupFolder.Text;
        }

        private void aboutToolStrip_Click(object sender, EventArgs e)
        {
            Forms.frmAbout frm = new Forms.frmAbout();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://netmailsender.codeplex.com/discussions");
        }

        private void editNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginEditDataGridView(0, 2);
        }

        private void editValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginEditDataGridView(1, 2);
        }

        private void editContentIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginEditDataGridView(3, 1);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxEmailAddress_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtBoxFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxFrom_Enter(object sender, EventArgs e)
        {
            if (txtBoxFrom.Text.Trim() == string.Empty)
            {
                txtBoxFrom.Text = txtBoxUserUpn.Text.Trim();
            }
        }

        private void editInlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginEditDataGridView(4, 1);
        }

        private void dGridHeaders_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dGridHeaders.CurrentCell = dGridHeaders[e.ColumnIndex, e.RowIndex];
            }
        }

        private void dGridAttachments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dGridAttachments.CurrentCell = dGridAttachments[e.ColumnIndex, e.RowIndex];
            }
        }

        private void dGridAttachments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                BeginEditDataGridView(1, 1);
            }
            else if (e.ColumnIndex == 4)
            {
                BeginEditDataGridView(4, 1);
            }
        }

        /// <summary>
        /// function to cell edits in datagridview
        /// </summary>
        /// <param name="cell">the cell that needs to be edited</param>
        /// <param name="dGrid">1 = attachments datagridview, 2 = header datagridview</param>
        public void BeginEditDataGridView(int cell, int dGrid)
        {
            if (dGrid == 1)
            {
                int n = dGridAttachments.CurrentCellAddress.Y;
                dGridAttachments.CurrentCell = dGridAttachments.Rows[n].Cells[cell];
                dGridAttachments.BeginEdit(true);
            }
            else
            {
                int n = dGridHeaders.CurrentCellAddress.Y;
                dGridHeaders.CurrentCell = dGridHeaders.Rows[n].Cells[cell];
                dGridHeaders.BeginEdit(true);
            }

        }

    }
}