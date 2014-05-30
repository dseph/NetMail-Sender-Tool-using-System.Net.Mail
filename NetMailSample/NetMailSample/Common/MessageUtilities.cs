using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace NetMailSample.Common
{
    public static class MessageUtilities
    {
        public enum addressType
        {
            To,
            Cc,
            Bcc
        };

        /// <summary>
        /// This function validates a single email address with a regular expression
        /// </summary>
        /// <param name="input">the email address that needs to be validated</param>
        /// <returns>A validated email address or an error noting which address is invalid</returns>
        public static string parseEmail(string input)
        {
            Regex r = new Regex(@"^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$");
            if (r.Match(input.Trim()).Success)
            { 
                return input; 
            }
            else
            { 
                throw new ArgumentException("Not an email address - " + input); 
            }
        }

        /// <summary>
        /// This function validates one or more email addresses passed into the function
        /// if none fail, each address gets added indvidually to the mail message object
        /// </summary>
        /// <param name="input">the email address that needs to be validated</param>
        /// <param name="mail">the MailMessage object where the validated email address will be added based on addressType</param>
        /// <param name="mailAddressType">determines which type of mail address field the email address needs to be added</param>
        public static void parseEmails(string input, MailMessage mail, addressType mailAddressType)
        {
            // create a list for the parsed input string to hold valid individual emails
            // split the string and regex each individual email address
            ArrayList al = new ArrayList();
            int failCount = 0;
            Regex r = new Regex(@"^((?:(?:(?:[a-zA-Z0-9][\.\-\+_]?)*)[a-zA-Z0-9])+)\@((?:(?:(?:[a-zA-Z0-9][\.\-_]?){0,62})[a-zA-Z0-9])+)\.([a-zA-Z0-9]{2,6})$");
            string[] emails = input.Split(';');
            foreach (string s in emails)
            {
                string temp = s.Trim();
                if (r.Match(temp).Success) 
                { 
                    al.Add(temp); 
                }
                else 
                { 
                    failCount++; 
                }
            }

            // if we didn't fail validation for an email address,
            // we can now loop through each email and add it to the mail object  
            if (failCount == 0) 
            {
                foreach (object o in al)
                {
                    switch (mailAddressType)
                    {
                        case addressType.To:
                            mail.To.Add(o.ToString());
                            break;
                        case addressType.Cc:
                            mail.CC.Add(o.ToString());
                            break;
                        case addressType.Bcc:
                            mail.Bcc.Add(o.ToString());
                            break;
                        default:
                            throw new ArgumentException("Error with email address - " + o.ToString()); 
                    }
                }
            }
            else 
            { 
                throw new ArgumentException("Not an email address - " + input); 
            }
        }

        /// <summary>
        /// this function will take a string value, 
        /// then convert it to the corresponding Encoding type for the message
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
                default:
                    return Encoding.ASCII;
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