﻿/****************************** Module Header ******************************\
Module Name:  MessageUtilities.cs
Project:      NetMailSender
Copyright (c) Microsoft Corporation.

This class has helper code for the different messaging related functionality

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace NetMailSample.Common
{
    public static class MessageUtilities
    {
        // mail address message type enum
        public enum addressType
        {
            To,
            Cc,
            Bcc
        };

      
        // This function will set a text list of addresses to a recipient collection on a message.
        // MailMessage oMessage - the message to add the recipients to
        // string sRecipientType - the type of recipient to add (To, CC, BCC)
        // string sAddressString - the string which contains 1+ mail addresses. Addresses can be smtp or a mix of stmp and name.

        public static bool SetRecipientsFromString(ref MailMessage oMessage, string sRecipientType, string sAddressString)
        {
            bool bRet = false;

            char[] arrAddressDelimiter = { ';' };
            string[] sAddresses = sAddressString.Split(arrAddressDelimiter);
            string sSmtpAddress = string.Empty;
            string sRecipientName = string.Empty;
            string sWork = string.Empty;
            char[] carrEndAddress = { '>', ')' };
            char[] carrStartAddress = { '<', ')' };
            char[] junk = { '<', '>', '(', ' ', ';' };
            int iEndAddress = 0;
            int iStartAddress = 0;
            string sCurrentAddress = string.Empty;



            try
            {
                foreach (string sAddress in sAddresses)
                {

                    sCurrentAddress = sAddress;

                    iStartAddress = sAddress.IndexOfAny(carrStartAddress);
                    iEndAddress = sAddress.IndexOfAny(carrEndAddress);

                    sSmtpAddress = string.Empty;
                    sRecipientName = string.Empty;
                    if (sAddress.Contains(">") || sAddress.Contains(")"))
                    {
                        sWork = sAddress.Substring(iStartAddress, iEndAddress - iStartAddress);
                        sSmtpAddress = sWork.Trim(junk);

                        sWork = sWork.Substring(0, iStartAddress);
                        sRecipientName = sWork.Trim(junk);
                    }
                    else
                    {
                        sWork = sAddress.Trim(junk);
                        if (sWork.Length != 0)
                            sSmtpAddress = sAddress.Trim(junk);

                    }

                    if (sRecipientName.Length != 0)
                    {
                        if (sSmtpAddress.Length != 0)
                        {
                            switch (sRecipientType)
                            {
                                case "To":
                                    oMessage.To.Add(new MailAddress(sSmtpAddress, sRecipientName));
                                    break;
                                case "CC":
                                    oMessage.CC.Add(new MailAddress(sSmtpAddress, sRecipientName));
                                    break;
                                case "BCC":
                                    oMessage.Bcc.Add(new MailAddress(sSmtpAddress, sRecipientName));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (sSmtpAddress.Length != 0)
                        {
                            switch (sRecipientType)
                            {
                                case "To":
                                    oMessage.To.Add(new MailAddress(sSmtpAddress));
                                    break;
                                case "CC":
                                    oMessage.CC.Add(new MailAddress(sSmtpAddress));
                                    break;
                                case "BCC":
                                    oMessage.Bcc.Add(new MailAddress(sSmtpAddress));
                                    break;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n\r\nAddress: " + sCurrentAddress, "Error Parsing Address");
            }

            bRet = true;

            return bRet;

        }

        /// <summary>
        /// Depending on the mail address type, add each mail address from the collection to the mail message
        /// </summary>
        /// <param name="mail">This is the MailMessage object from the main form</param>
        /// <param name="mailAddrCol">This is the Collection of addresses that need to be added</param>
        /// <param name="mailAddressType">type of mail address to be added</param>
        public static void AddSmtpToMailAddressCollection(MailMessage mail, MailAddressCollection mailAddrCol, addressType mailAddressType)
        {
            foreach (MailAddress ma in mailAddrCol)
            {
                if (mailAddressType == addressType.To)
                {
                    mail.To.Add(ma.Address);
                }
                else if (mailAddressType == addressType.Cc)
                {
                    mail.CC.Add(ma.Address);
                }
                else
                {
                    mail.Bcc.Add(ma.Address);
                }

                
            }
        }

        /// <summary>
        /// this function converts the Encoding type for the message
        /// </summary>
        /// <param name="encodingVal">string value to be converted</param>
        /// <returns></returns>
        public static Encoding GetEncodingValue(string encodingVal)
        {
            switch (encodingVal)
            {
                case "UTF8":
                    return Encoding.UTF8;
                case "Unicode":
                    return Encoding.Unicode;
                case "UTF32":
                    return Encoding.UTF32;
                case "UTF7":
                    return Encoding.UTF7;
                case "ASCII":
                    return Encoding.ASCII;
                default:
                    return Encoding.Default;
            }
        }

        /// <summary>
        /// this function will convert the string value to the corresponding TransferEncoding type
        /// </summary>
        /// <param name="tEncoding">string value to be converted</param>
        /// <returns></returns>
        public static TransferEncoding GetTransferEncoding(string tEncoding)
        {
            switch (tEncoding)
            {
                case "QuotedPrintable":
                    return TransferEncoding.QuotedPrintable;
                case "SevenBit":
                    return TransferEncoding.SevenBit;
                case "Base64":
                    return TransferEncoding.Base64;
                default:
                    return TransferEncoding.Unknown;
            }
        }

        /// <summary>
        /// this function will return the status code description for an smtp exception
        /// </summary>
        /// <param name="sCode">this is the status code from the smtp exception</param>
        /// <returns></returns>
        public static string GetSmtpStatusCodeDescription(string sCode)
        {
            switch (sCode)
            {
                case "GeneralFailure":
                    return "The transaction could not occur. You receive this error when the specified SMTP host cannot be found.";
                case "BadCommandSequence":
                    return "The commands were sent in the incorrect sequence.";
                case "CannotVerifyUserWillAttemptDelivery":
                    return "The specified user is not local, but the receiving SMTP service accepted the message and attempted to deliver it. This status code is defined in RFC 1123, which is available at http://www.ietf.org.";
                case "ClientNotPermitted":
                    return "The client was not authenticated or is not allowed to send mail using the specified SMTP host.";
                case "CommandNotImplemented":
                    return "The SMTP service does not implement the specified command.";
                case "CommandParameterNotImplemented":
                    return "The SMTP service does not implement the specified command parameter.";
                case "CommandUnrecognized":
                    return "The SMTP service does not recognize the specified command.";
                case "ExceededStorageAllocation":
                    return "The message is too large to be stored in the destination mailbox.";
                case "HelpMessage":
                    return "A Help message was returned by the service.";
                case "InsufficientStorage":
                    return "The SMTP service does not have sufficient storage to complete the request.";
                case "LocalErrorInProcessing":
                    return "The SMTP service cannot complete the request. This error can occur if the client's IP address cannot be resolved (that is, a reverse lookup failed). You can also receive this error if the client domain has been identified as an open relay or source for unsolicited e-mail (spam). For details, see RFC 2505, which is available at http://www.ietf.org.";
                case "MailboxBusy":
                    return "The destination mailbox is in use.";
                case "MailboxNameNotAllowed":
                    return "The syntax used to specify the destination mailbox is incorrect.";
                case "MailboxUnavailable":
                    return "The destination mailbox was not found or could not be accessed.";
                case "MustIssueStartTlsFirst":
                    return "The SMTP server is configured to accept only TLS connections, and the SMTP client is attempting to connect by using a non-TLS connection. The solution is for the user to set EnableSsl=true on the SMTP Client.";
                case "ServiceClosingTransmissionChannel":
                    return "The SMTP service is closing the transmission channel.";
                case "ServiceNotAvailable":
                    return "The SMTP service is not available; the server is closing the transmission channel.";
                case "ServiceReady":
                    return "The SMTP service is ready.";
                case "StartMailInput":
                    return "The SMTP service is ready to receive the e-mail content.";
                case "SyntaxError":
                    return "The syntax used to specify a command or parameter is incorrect.";
                case "SystemStatus":
                    return "A system status or system Help reply.";
                case "TransactionFailed":
                    return "The transaction failed.";
                case "UserNotLocalTryAlternatePath":
                    return "The user mailbox is not located on the receiving server. You should resend using the supplied address information.";
                case "UserNotLocalWillForward":
                    return "The user mailbox is not located on the receiving server; the server forwards the e-mail.";
                default:
                    return "The email was successfully sent to the SMTP service.";
            }
        }
    }
}