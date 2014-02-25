using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
    }
}
