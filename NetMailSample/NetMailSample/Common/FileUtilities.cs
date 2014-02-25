using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace NetMailSample.Common
{   
    public static class FileUtilities
    {
        ///<summary> Valid units of measurement for file sizes</summary>
        public enum SizeType
        {
            Bytes,
            KiloBytes,
            MegaBytes,
            GigaBytes,
            TeraBytes
        }
                
        /// <summary>
        /// Converts size of file from one type to another
        /// </summary>
        /// <param name="ConvertFrom">The SizeType that you are converting from</param>
        /// <param name="ConvertTo">The SizeType that you are converting to</param>
        /// <param name="Value">The value to be converted</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static decimal Convert(SizeType ConvertFrom, SizeType ConvertTo, decimal Value)
        {
            decimal ConvertedValue = 0;

            //First check to see if they entered the same value for the ConvertFrom and ConvertTo parameters
            if (ConvertFrom == ConvertTo)
            {
                return Value; 
            }             
            else
            {
                try
                {
                    switch (ConvertFrom)
                    {
                        case SizeType.Bytes: 
                            switch (ConvertTo)
                            {
                                case SizeType.KiloBytes: 
                                    ConvertedValue = Value / 1024;
                                    break;
                                case SizeType.MegaBytes: 
                                    ConvertedValue = Value / 1024 / 1024;
                                    break;
                                case SizeType.GigaBytes: 
                                    ConvertedValue = Value / 1024 / 1024 / 1024;
                                    break;
                                case SizeType.TeraBytes: 
                                    ConvertedValue = Value / 1024 / 1024 / 1024 / 1024;
                                    break;
                            }
                            break;
                        case SizeType.KiloBytes: 
                            switch (ConvertTo)
                            {
                                case SizeType.Bytes: 
                                    ConvertedValue = Value * 1024;
                                    break;
                                case SizeType.MegaBytes: 
                                    ConvertedValue = Value / 1024;
                                    break;
                                case SizeType.GigaBytes: 
                                    ConvertedValue = Value / 1024 / 1024;
                                    break;
                                case SizeType.TeraBytes: 
                                    ConvertedValue = Value / 1024 / 1024 / 1024;
                                    break;
                            }
                            break;
                        case SizeType.MegaBytes: 
                            switch (ConvertTo)
                            {
                                case SizeType.Bytes: 
                                    ConvertedValue = Value * 1024 * 1024;
                                    break;
                                case SizeType.KiloBytes: 
                                    ConvertedValue = Value * 1024;
                                    break;
                                case SizeType.GigaBytes: 
                                    ConvertedValue = Value / 1024;
                                    break;
                                case SizeType.TeraBytes: 
                                    ConvertedValue = Value / 1024 / 1024;
                                    break;
                            }
                            break;
                        case SizeType.GigaBytes: 
                            switch (ConvertTo)
                            {
                                case SizeType.Bytes:
                                    ConvertedValue = Value * 1024 * 1024 * 1024;
                                    break;
                                case SizeType.KiloBytes:
                                    ConvertedValue = Value * 1024 * 1024;
                                    break;
                                case SizeType.MegaBytes:
                                    ConvertedValue = Value * 1024;
                                    break;
                                case SizeType.TeraBytes:
                                    ConvertedValue = Value / 1024;
                                    break;
                            }
                            break;
                        case SizeType.TeraBytes: 
                            switch (ConvertTo)
                            {
                                case SizeType.Bytes: 
                                    ConvertedValue = Value * 1024 * 1024 * 1024 * 1024;
                                    break;
                                case SizeType.KiloBytes:
                                    ConvertedValue = Value * 1024 * 1024 * 1024;
                                    break;
                                case SizeType.MegaBytes:
                                    ConvertedValue = Value * 1024 * 1024;
                                    break;
                                case SizeType.GigaBytes:
                                    ConvertedValue = Value * 1024;
                                    break;
                            }
                            break;
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            // check if .0 is at the end and remove it
            if (Decimal.Remainder(ConvertedValue, 1) == 0)
                ConvertedValue = Decimal.Truncate(ConvertedValue);
            return ConvertedValue;
        }

        /// <summary>
        /// Method to convert and display the file size with the appropriate label (KB, MB, GB, TB)
        /// </summary>
        /// <param name="myFile">The file you want to convert the size from</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertFileSize(FileInfo myFile)
        {
            Decimal val;

            if (myFile.Length < 1024)
            {
                return myFile.Length + " bytes";
            }
            else if (myFile.Length > 1024 && myFile.Length < 1048576)
            {
                val = Math.Round(Convert(SizeType.Bytes, SizeType.KiloBytes, myFile.Length));
                return val + " KB";
            }
            else if (myFile.Length > 1048576 && myFile.Length < 1073741824)
            {
                val = Math.Round(Convert(SizeType.Bytes, SizeType.MegaBytes, myFile.Length));
                return val + " MB";
            }
            else if (myFile.Length > 1073741824 && myFile.Length < 1099511627776)
            {
                val = Math.Round(Convert(SizeType.Bytes, SizeType.GigaBytes, myFile.Length));
                return val + " GB";
            }
            else
            {
                val = Math.Round(Convert(SizeType.Bytes, SizeType.TeraBytes, myFile.Length));
                return val + " TB";
            }
        }

        /// <summary>
        /// this function converts a string to the corresponding mime content type
        /// typically this is for a file attachment
        /// </summary>
        /// <param name="val">string value to be converted</param>
        /// <returns></returns>
        public static string GetContentType(string val)
        {
            switch (val)
            {
                case "Octet":
                    return MediaTypeNames.Application.Octet.ToString();
                case "Pdf":
                    return MediaTypeNames.Application.Pdf.ToString();
                case "Rtf":
                    return MediaTypeNames.Application.Rtf.ToString();
                case "Soap":
                    return MediaTypeNames.Application.Soap.ToString();
                case "Zip":
                    return MediaTypeNames.Application.Zip.ToString();
                case "Gif":
                    return MediaTypeNames.Image.Gif.ToString();
                case "Jpeg":
                    return MediaTypeNames.Image.Jpeg.ToString();
                case "Tiff":
                    return MediaTypeNames.Image.Tiff.ToString();
                case "Html":
                    return MediaTypeNames.Text.Html.ToString();
                case "Plain":
                    return MediaTypeNames.Text.Plain.ToString();
                case "RichText":
                    return MediaTypeNames.Text.RichText.ToString();
                case "Xml":
                    return MediaTypeNames.Text.Xml.ToString();
                default:
                    return val;
            }
        }
    }
}