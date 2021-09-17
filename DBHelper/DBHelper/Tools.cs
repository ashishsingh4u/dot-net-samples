using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.IO.Compression;
using System.IO;
using System.Windows.Forms;
using PropertyListType = System.Collections.Generic.Dictionary<string, string>;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace DBHelper
{
    /// <summary>
    /// Various utility functions
    /// </summary>
    public abstract class Tools
    {
        [DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc,
        int nXSrc, int nYSrc, int dwRop);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);
        public struct IconInfo
        {
            public bool FIcon;
            public int XHotspot;
            public int YHotspot;
            public IntPtr HbmMask;
            public IntPtr HbmColor;
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        #region Miscellaneous
        /// <summary>
        /// Creates an image from the specified control.
        /// </summary>
        /// <param name="control">The control to create an image of.</param>
        /// <returns>An rendering of the control.</returns>
        public static Image CreateControlImage(Control control)
        {
            return CreateControlImage(control, 0);
        }
        /// <summary>
        /// Creates an image from the specified control taking into account any specified border.
        /// </summary>
        /// <param name="control">The control to create an image of.</param>
        /// <param name="borderSize">Size of any border.</param>
        /// <returns>An rendering of the control.</returns>
        public static Image CreateControlImage(Control control, int borderSize)
        {
            int doubleBorder = 2 * borderSize;
            var size = control.Size;
            var graphics = control.CreateGraphics();
            var memoryImage = new Bitmap(size.Width + doubleBorder, size.Height + doubleBorder, graphics);
            var memoryGraphics = Graphics.FromImage(memoryImage);
            var surfaceDeviceContext = graphics.GetHdc();
            var imageDeviceContext = memoryGraphics.GetHdc();
            BitBlt(imageDeviceContext, 0, 0, control.ClientRectangle.Width + doubleBorder,
            control.ClientRectangle.Height + doubleBorder, surfaceDeviceContext, -borderSize, -borderSize,
            13369376);
            graphics.ReleaseHdc(surfaceDeviceContext);
            memoryGraphics.ReleaseHdc(imageDeviceContext);
            return memoryImage;
        }
        public static Image CreateControlImage(Control control, Rectangle captureRect)
        {
            Size size = captureRect.Size;
            Graphics graphics = control.CreateGraphics();
            var memoryImage = new Bitmap(size.Width, size.Height, graphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr surfaceDeviceContext = graphics.GetHdc();
            IntPtr imageDeviceContext = memoryGraphics.GetHdc();
            BitBlt(imageDeviceContext, 0, 0, control.ClientRectangle.Width,
            control.ClientRectangle.Height, surfaceDeviceContext, captureRect.X, captureRect.Y,
            13369376);
            graphics.ReleaseHdc(surfaceDeviceContext);
            memoryGraphics.ReleaseHdc(imageDeviceContext);
            return memoryImage;
        }
        /// <summary>
        /// Creates a custom cursor from the specified image with the specified hotspot.
        /// </summary>
        /// <param name="image">The cursor image.</param>
        /// <param name="xHotSpot">The x hot spot.</param>
        /// <param name="yHotSpot">The y hot spot.</param>
        /// <returns></returns>
        public static Cursor CreateCustomCursor(Bitmap image, int xHotSpot, int yHotSpot)
        {
            var tmp = new IconInfo();
            GetIconInfo(image.GetHicon(), ref tmp);
            tmp.XHotspot = xHotSpot;
            tmp.YHotspot = yHotSpot;
            tmp.FIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }
        /// <summary>
        /// Converts the specified image to grey scale.
        /// </summary>
        /// <param name="image">The image.</param>
        public static void ConvertImageToGreyScale(Image image)
        {
            // greyscale shear (matrix of floats)
            var colourMatrix = new ColorMatrix(new[]
                                                   {
                                                       new[] {0.3f, 0.3f, 0.3f, 0f, 0f},
                                                       new[] {0.59f, 0.59f, 0.59f, 0f, 0f},
                                                       new[] {0.11f, 0.11f, 0.11f, 0f, 0f},
                                                       new[] {0f, 0f, 0f, 1f, 0f, 0f},
                                                       new[] {0f, 0f, 0f, 0f, 1f, 0f},
                                                       new[] {0f, 0f, 0f, 0f, 0f, 1f}
                                                   });
            using (var gfx = Graphics.FromImage(image))
            {
                var imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colourMatrix);
                gfx.DrawImage(image,
                new Rectangle(0, 0, image.Width, image.Height),
                0, 0, image.Width, image.Height,
                GraphicsUnit.Pixel, imageAttributes);
            }
        }
        /// <summary>
        /// Draws the source image onto the target image.
        /// </summary>
        /// <param name="sourceImage">The source image.</param>
        /// <param name="targetImage">The target image.</param>
        /// <param name="targetBounds">Bounds to draw the target image into.</param>
        public static void SuperImposeImageOnImage(Image sourceImage, Image targetImage, Rectangle targetBounds)
        {
            using (Graphics gfx = Graphics.FromImage(targetImage))
            {
                gfx.DrawImageUnscaled(sourceImage, targetBounds);
            }
        }
        /// <summary>
        /// Creates the move cursor version from the specified move cursor superimposing the default cursor on top.
        /// </summary>
        /// <param name="cursorImage">The move cursor image.</param>
        /// <param name="hotSpot">The cursor hotspot.</param>
        /// <returns>A cursor instance.</returns>
        public static Cursor CreateMoveCursor(Image cursorImage, Point hotSpot)
        {
            var maxWidth = cursorImage.Width + Cursors.Default.Size.Width;
            var maxHeight = cursorImage.Height + Cursors.Default.Size.Height;
            var moveCursorImage = new Bitmap(maxWidth, maxHeight);
            var cursorImageBounds = new Rectangle(0, 0, cursorImage.Width, cursorImage.Height);
            // draw the move cursor image onto the copy cursor image
            SuperImposeImageOnImage(cursorImage, moveCursorImage, cursorImageBounds);
            // add the default cursor
            AddDefaultCursor(moveCursorImage, hotSpot);
            return CreateCustomCursor(moveCursorImage, hotSpot.X, hotSpot.Y);
        }
        /// <summary>
        /// Creates the copy cursor version from the specified move cursor by rendering on a copy indicator at the
        /// hotspot of the of the cursor and superimposing the default cursor on top.
        /// </summary>
        /// <param name="cursorImage">The move cursor image.</param>
        /// <param name="copyIndicatorImage">The image that indicates copy.</param>
        /// <param name="hotSpot">The cursor hotspot.</param>
        /// <returns>A cursor instance.</returns>
        public static Cursor CreateCopyCursor(Image cursorImage, Image copyIndicatorImage, Point hotSpot)
        {
            var maxWidth = cursorImage.Width + copyIndicatorImage.Width + Cursors.Default.Size.Width;
            var maxHeight = cursorImage.Height + Cursors.Default.Size.Height;
            var copyCursorBitmap = new Bitmap(maxWidth, maxHeight);
            var cursorImageBounds = new Rectangle(0, 0, cursorImage.Width, cursorImage.Height);
            // draw the main cursor image
            SuperImposeImageOnImage(cursorImage, copyCursorBitmap, cursorImageBounds);
            // add the default cursor
            var defaultCursorBounds = AddDefaultCursor(copyCursorBitmap, hotSpot);
            // draw the copy indicator onto the copy cursor image to the right of the default cursor
            var indicatorBounds = new Rectangle(defaultCursorBounds.Right,
            defaultCursorBounds.Bottom - copyIndicatorImage.Height,
            copyIndicatorImage.Width, copyIndicatorImage.Height);
            SuperImposeImageOnImage(copyIndicatorImage, copyCursorBitmap, indicatorBounds);
            return CreateCustomCursor(copyCursorBitmap, hotSpot.X, hotSpot.Y);
        }
        private static Rectangle AddDefaultCursor(Image targetImage, Point hotSpot)
        {
            using (var gfx = Graphics.FromImage(targetImage))
            {
                var defaultCursor = Cursors.Default;
                var defaultCursorHotspot = defaultCursor.HotSpot;
                var bounds = new Rectangle(hotSpot.X - defaultCursorHotspot.X, hotSpot.Y - defaultCursorHotspot.Y,
                defaultCursor.Size.Width, defaultCursor.Size.Height);
                defaultCursor.Draw(gfx, bounds);
                return bounds;
            }
        }
        #region Checking the Design mode
        enum ExecutionMode
        {
            Unknown = 0, /*!< Not yet determined */
            Design, /*!< Design mode */
            Runtime, /*!< Runtime */
        }
        static ExecutionMode _executionMode = ExecutionMode.Unknown;
        /*!
        Determine whether we are running in design mode
        \return true if we are in design, false otherwise
        \note This is work-around to overcome problems with System.ComponentModel.Component.DesignMode property which doesn't work in some situations
        GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null would be more generic but doesn't always work
        */
        public static bool IsDesignMode()
        {
            if (_executionMode == ExecutionMode.Unknown)
            {
                _executionMode = (Process.GetCurrentProcess().ProcessName == "devenv") ? ExecutionMode.Design : ExecutionMode.Runtime;
            }
            return _executionMode == ExecutionMode.Design;
        }
        #endregion
        /// <summary>
        /// Helper method to copy from objSource collection with objects of type T or derived from T, into 
        /// objDestination. This method was needed since for the above case we cannot use
        /// objDestination.AddRange(objSource), objDestination and objSource needs to be List<T/> of same type.
        /// </summary>
        /// <typeparam name="T">The type (or derived types) of elements in the objSource to copy.</typeparam>
        /// <param name="objDestination">List to copy the elements into.</param>
        /// <param name="objSource">List whose elements needs to be copies.</param>
        public static void CopyFromCollection<T>(List<T> objDestination, ICollection objSource)
        {
            objDestination.AddRange(objSource.Cast<T>());
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors, Dictionary<string, X509Certificate2> sslCertificates)
        {
            return sslCertificates.Values.Any(certificate.Equals);
        }

        #endregion
        #region IdentityObfruscation
        private const string CryptoSequence = "POIUYTREWQASDFGHJKLMNBVCXZ";
        private const string FullDigitSequence = "FDEBCA9786452310";
        public static string EncryptIdentifier(int identity, int seconds)
        {
            int numberLength = identity.ToString().Length;
            int codeLength = numberLength + 2;
            if (codeLength > 16 || identity < 0)
                return "ERROR";
            string digitSequence = StripSequence(FullDigitSequence, numberLength + 2);
            string format = "D" + numberLength;
            string inputText1 = identity.ToString(format);
            string inputText2 = seconds.ToString("D2");
            string inputText = inputText1 + inputText2;
            var obfruscatedIdentifier = new StringBuilder();
            int location = 13;
            bool forwardDirection = true;
            for (int pos = 0; pos < digitSequence.Length; pos++)
            {
                int digitPos = Int32.Parse(digitSequence[pos].ToString(), NumberStyles.HexNumber);
                int digit = Int32.Parse(inputText[digitPos].ToString());
                if (forwardDirection)
                    location += digit;
                else
                    location -= digit;
                obfruscatedIdentifier.Append(CryptoSequence[location]);
                if (forwardDirection)
                {
                    if (location > CryptoSequence.Length - 10)
                    {
                        forwardDirection = false;
                    }
                }
                else
                {
                    if (location < 10)
                    {
                        forwardDirection = true;
                    }
                }
            }
            return obfruscatedIdentifier.ToString();
        }

        public static int DecryptIdentifier(string obfruscatedIdentifier)
        {
            if (String.IsNullOrEmpty(obfruscatedIdentifier))
                return 0;
            var identifierText = new StringBuilder(obfruscatedIdentifier.Length - 1);
            identifierText.Append('0', obfruscatedIdentifier.Length - 2);
            int location = 13;
            string digitSequence = StripSequence(FullDigitSequence, obfruscatedIdentifier.Length);
            bool forwardDirection = true;
            for (int pos = 0; pos < obfruscatedIdentifier.Length; pos++)
            {
                int digitPos = CryptoSequence.IndexOf(obfruscatedIdentifier[pos]);
                if (digitPos < 0)
                    return -1;
                int result;
                if (forwardDirection)
                {
                    result = digitPos - location;
                    if (digitPos > CryptoSequence.Length - 10)
                    {
                        forwardDirection = false;
                    }
                }
                else
                {
                    result = location - digitPos;
                    if (digitPos < 10)
                    {
                        forwardDirection = true;
                    }
                }
                if (result < 0 || result > 9)
                    return -1;
                location = digitPos;
                int digitSeqPos = Int32.Parse(digitSequence[pos].ToString());
                if (digitSeqPos < obfruscatedIdentifier.Length - 2)
                {
                    identifierText[digitSeqPos] = result.ToString()[0];
                }
            }
            return Int32.Parse(identifierText.ToString());
        }
        private static string StripSequence(string fullDigitSequence, int digits)
        {
            var digitSequence = new StringBuilder();
            for (int pos = 0; pos < fullDigitSequence.Length; pos++)
            {
                int digitPos = Int32.Parse(fullDigitSequence[pos].ToString(), NumberStyles.HexNumber);
                if (digitPos < digits)
                {
                    digitSequence.Append(fullDigitSequence[pos]);
                }
            }
            return digitSequence.ToString();
        }
        #endregion
        #region Data Compression
        /*!
Compresses an input string using the GZipStream class
\param strString is the string to be compressed
\return String is the compressed string
*/
        public static string Compress(string decompressed)
        {
            //Convert unicode string to byte array for processing
            byte[] bufferInput = Encoding.Unicode.GetBytes(decompressed);
            //Remember size of input string, this is required for decompression
            int length = bufferInput.Length;
            //Create memory stream
            var memoryStream = new MemoryStream();
            //Create compression stream for above memory stream
            var memoryStreamCompressed = new GZipStream(memoryStream, CompressionMode.Compress, true);
            //write bytes to compression stream
            memoryStreamCompressed.Write(bufferInput, 0, bufferInput.Length);
            //memoryStreamCompressed stream must be closed before buffer can be read
            memoryStreamCompressed.Close();
            //Create output buffer
            //Read compressed data from memory stream
            byte[] bufferOutput = memoryStream.ToArray();
            //Store compressed buffer in a string, prefix string with original size and a # as a compression indicator
            //We must use Base64 encoding to prevent non-printable characters confusing the database
            String output = length + "#" + Convert.ToBase64String(bufferOutput);
            //Close memory stream
            memoryStream.Close();
            return output;
        }
        /*!
        Decompresses an input string created using Tools.Compress method
        If the input string was not compressed using Tools.Compress then the input string is returned
        \param strString is a compressed string 
        \return String is the decompressed string
        */
        public static string Decompress(string compressed)
        {
            //Check to see if the input string has an integer in the first 10 chars followed by a hash
            int outputBufferSize = 0;
            int compressedIndicatorPos = compressed.IndexOf('#');
            if (compressedIndicatorPos > 0 && compressedIndicatorPos < 10)
            {
                string originalSizeText = compressed.Substring(0, compressedIndicatorPos);
                if (!Int32.TryParse(originalSizeText, out outputBufferSize))
                {
                    compressedIndicatorPos = -1;
                }
            }
            else
            {
                compressedIndicatorPos = -1;
            }
            //If Tools.Compress indicator found then decompress string
            if (compressedIndicatorPos != -1)
            {
                //Decode the Base64 so we are working with the raw compressed data
                byte[] compressedBuffer = Convert.FromBase64String(compressed.Substring(compressedIndicatorPos + 1));
                //Create memory stream
                var memoryStream = new MemoryStream(compressedBuffer);
                //Create decompressed stream
                var memoryStreamDecompressed = new GZipStream(memoryStream, CompressionMode.Decompress);
                //Create output buffer of the original size
                var outputBuffer = new byte[outputBufferSize];
                //Read decompressed data
                memoryStreamDecompressed.Read(outputBuffer, 0, outputBufferSize);
                //Convert bytes back to a string
                String decompressed = Encoding.Unicode.GetString(outputBuffer);
                // Close the streams
                memoryStreamDecompressed.Close();
                memoryStream.Close();
                //Return decompressed string
                return decompressed;
            }
            //Compression identifier not found, just return input string
            return compressed;
        }
        #endregion
        #region Convertions of data
        /*!
Trim the trailing and leading zero ('\0') characters
\param strString String to trim
\return String with trimmed zero characters
\note This is required because string.Trim() doesn't do that
*/
        public static string TrimZeros(string strString)
        {
            string strReturn = strString.Trim('\0');
            int intZero = strReturn.IndexOf("\0");
            if (intZero > 0)
                strReturn = strReturn.Substring(0, intZero);
            return strReturn;
        }
        /*!
        Prepare multiline string for display in the multiline text box
        \param strString String to prepare
        \return String prepared for display in multiline text box
        \note Replaces line breaks with soft line breaks
        */
        public static string PrepareMultilineString(string strString)
        {
            return strString.Replace("\n", "\r\n");
        }
        /*!
        Convert multiline text box text for storing
        \param strString String to convert
        \return String converted for storing
        \note Replaces soft line breaks with line breaks
        */
        public static string ConvertMultilineText(string strString)
        {
            return strString.Replace("\r\n", "\n");
        }
        #region DateTime
        public const string CStrDateFormat = "dd MMM yyyy"; /*!< Standard date format */
        public const string CStrHhTimeFormat = "HH:mm:ss"; /*!< Standard time format, 24 hours, no milliseconds */
        public const string CStrMmMddDateFormat = "MMM dd"; /*!< Standard date format without year */
        public const string CStrDateTimeFormat = CStrDateFormat + " HH:mm:ss:fff"; /*!< Standard datetime format (convert 113) */
        public const string CStrParseDateTimeFormat = "d MMM yyyy H:mm:ss:fff"; /*!< Standard datetime format (convert 113) for parsing as input may come with single digits for days and hours */
        public const string CStrParseDateFormat = "d MMM yyyy";
        // Special strings to represent datetime
        public class DateTimeString
        {
            public const string Yesterday = "YESTERDAY";
            public const string Today = "TODAY";
            public const string Now = "NOW";
            /*!
            Determine whether a given text(non-date, e.g. TODAY) can be translated into DateTime
            \param strText Text to check
            \return true if the string can be translated into DateTime, false otherwise
            */
            public static bool IsDateTimeString(string strText)
            {
                strText = strText.ToUpper();
                return strText == Yesterday ||
                strText == Today ||
                strText == Now;
            }
            /*!
            Check whether a special string should contain time part
            \param strText Text to check
            \return true if text contains time part, false otherwise
            \note Utility for conversion from DateTime to string
            */
            public static bool HasTime(string strText)
            {
                return strText.ToUpper() == Now;
            }
        }
        /*!
        Convert DateTime to string representation
        \param objDate Date to convert
        \param blnAddTime true to add the time part 
        \return String representing the DateTime
        */
        public static string ConvertDateTimeToString(DateTime objDate, bool blnAddTime)
        {
            return objDate.ToString(blnAddTime ? CStrDateTimeFormat : CStrDateFormat, DateTimeFormatInfo.InvariantInfo);
        }
        /*!
        Convert DateTime to string representation
        \param objDate Date to convert
        \param blnAddTime true to add the time part 
        \return String representing the DateTime
        */
        public static string ConvertDateTimeToString(DateTime objDate, string strFormat)
        {
            return objDate.ToString(strFormat, DateTimeFormatInfo.InvariantInfo);
        }
        /*!
        Convert string representation of date and time to DateTime
        \param strDate Date string to convert
        \return Converted date
        */
        public static DateTime ConvertStringToDateTime(string strDate, bool blnAddTime)
        {
            return DateTime.ParseExact(strDate, blnAddTime ? CStrParseDateTimeFormat : CStrParseDateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces);
        }
        /*!
        Translate special date or time string DateTime (in UTC)
        \param strDate Date string to convert
        - TODAY translated to today day
        - NOW translate to current date and time
        - YESTERDAY translated to today-1 day
        \return Converted date
        */
        public static DateTime TranslateStringToDateTime(string strDateString)
        {
            DateTime objRes = DateTime.UtcNow;
            switch (strDateString.ToUpper())
            {
                case DateTimeString.Yesterday:
                    objRes = DateTime.UtcNow.AddDays(-1);
                    break;
                case DateTimeString.Today:
                    objRes = DateTime.UtcNow.Date;
                    break;
                case DateTimeString.Now:
                    objRes = DateTime.UtcNow;
                    break;
                default:
                    Debug.Assert(false, "Unknown datetime string");
                    break;
            }
            return objRes;
        }
        /*!
        Convert string representation of time to DateTime
        \param objTime Time to convert
        \return Converted time string
        */
        public static string ConvertTimeToString(DateTime objTime)
        {
            return objTime.ToString(CStrHhTimeFormat, DateTimeFormatInfo.InvariantInfo);
        }
        /*!
        Convert string representation of time to DateTime
        \param strTime Time string to convert
        \return Converted time
        */
        public static DateTime ConvertStringToTime(string strTime)
        {
            return DateTime.ParseExact(strTime, CStrHhTimeFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces);
        }
        #endregion
        /*!
Convert string to double
\param strNumber String to convert
\param dblResult Return converted number
\param strError Return error message
\return true on success, false otherwise
*/
        public static bool ConvertStringToDouble(string strNumber, out double dblResult, ref string strError)
        {
            bool blnRes = false;
            dblResult = 0;
            if (!String.IsNullOrEmpty(strNumber))
            {
                strNumber = strNumber.Trim();
            }
            if (String.IsNullOrEmpty(strNumber) || strNumber.Length == 0)
            {
                blnRes = true;
            }
            else
            {
                double dblFactor;
                if (DetectNumberShortcuts(strNumber, ref strNumber, out dblFactor))
                {
                    if (Double.TryParse(strNumber, NumberStyles.Any, null, out dblResult))
                    {
                        if (dblFactor != 1)
                        {
                            dblResult *= dblFactor;
                        }
                        blnRes = true;
                    }
                    else
                    {
                        strError = "Unable to convert (" + strNumber + ") to number";
                    }
                }
                else
                {
                    strError = strNumber;
                }
            }
            return blnRes;
        }
        /*!
        Convert string to int
        \param strNumber String to convert
        \param dblResult Return converted number
        \param strError Return error message
        \return true on success, false otherwise
        */
        public static bool ConvertStringToInt(string strNumber, out int intResult, ref string strError)
        {
            bool blnRes = false;
            intResult = 0;
            double dblResult;
            if (ConvertStringToDouble(strNumber, out dblResult, ref strError))
            {
                intResult = (int)dblResult;
                blnRes = true;
            }
            return blnRes;
        }
        /*!
        Convert double value to string formatted for user display
        \param dblNumber Number to convert
        \param intNumberDecimalDigits Number of decimal digits
        \param blnTrimTrailingZeros true to trim the trailing zeros
        \param blnShowGroupSeparator true to show the number group separator
        \return Return converted text
        */
        public static string ConvertDoubleToString(double dblNumber, int intNumberDecimalDigits, bool blnTrimTrailingZeros, bool blnShowGroupSeparator)
        {
            return
            ConvertDoubleToString(dblNumber, intNumberDecimalDigits, blnTrimTrailingZeros, blnShowGroupSeparator, false);
        }

        /*!
        Convert double value to string formatted for user display
        \param dblNumber Number to convert
        \param intNumberDecimalDigits Number of decimal digits
        \param blnTrimTrailingZeros true to trim the trailing zeros
        \param blnShowGroupSeparator true to show the number group separator
        \return Return converted text
        */
        public static string ConvertDoubleToString(double dblNumber, int intNumberDecimalDigits, bool blnTrimTrailingZeros, bool blnShowGroupSeparator, bool blnTrimWholeNumbers)
        {
            var objNumberFormatInfo = new NumberFormatInfo
                                          {
                                              NumberDecimalDigits = intNumberDecimalDigits
                                          };
            if (!blnShowGroupSeparator)
            {
                objNumberFormatInfo.NumberGroupSeparator = "";
            }
            string strRes = dblNumber.ToString("n", objNumberFormatInfo);
            if (blnTrimTrailingZeros || (blnTrimWholeNumbers && (dblNumber - Math.Floor(dblNumber)) < (1 / Math.Pow(10d, intNumberDecimalDigits))))
            {
                int intDecSepPos = strRes.IndexOf(objNumberFormatInfo.NumberDecimalSeparator);
                if (intDecSepPos > -1)
                {
                    strRes = strRes.TrimEnd('0');
                    strRes = strRes.TrimEnd(objNumberFormatInfo.NumberDecimalSeparator.ToCharArray());
                }
            }
            // This is not correct - we loose the symmetry (the empty string doesn't convert back to Double.NaN)
            if (strRes == Double.NaN.ToString())
                strRes = String.Empty;
            return strRes;
        }
        public static string ConvertDoubleToString(double dblNumber, int intNumberDecimalDigits, bool blnTrimTrailingZeros)
        {
            return ConvertDoubleToString(dblNumber, intNumberDecimalDigits, blnTrimTrailingZeros, true);
        }
        /*!
        Convert given object to string using specified format and object type
        \param strFormat Format to be used
        \objVal Object to convert
        \return Formatted object string
        \note Contains multiple exit points to speed up the call
        */
        public static string ToString(string strFormat, object objVal)
        {
            if (objVal.GetType() == typeof(Double))
            {
                return ((double)objVal).ToString(strFormat);
            }
            if (objVal.GetType() == typeof(decimal))
            {
                return ((decimal)objVal).ToString(strFormat);
            }
            if (objVal.GetType() == typeof(DateTime))
            {
                return ((DateTime)objVal).ToString(strFormat);
            }
            return objVal.ToString();
        }
        /*!
        Convert given string to another string using specified data type format
        \param strFormat Format to be used
        \strDataType Original DataType stored in string format in strVal
        \strVal value to be converted to provided format strFormat
        \return Formatted object string
        */
        public static string ToString(string strFormat, string strDataType, object objVal)
        {
            string strResult;
            string strError = "";
            try
            {
                switch (strDataType)
                {
                    case "DOUBLE":
                        double dblValue;
                        if (!ConvertStringToDouble(objVal.ToString(), out dblValue, ref strError))
                        {
                            throw new Exception(strError);
                        }
                        strResult = ToString(strFormat, dblValue);
                        break;
                    case "DATETIME":
                        DateTime objDateTime;
                        try
                        {
                            objDateTime = ConvertStringToDateTime(objVal.ToString(), true);
                        }
                        catch
                        {
                            objDateTime = ConvertStringToDateTime(objVal.ToString(), false);
                        }
                        strResult = ToString(strFormat, objDateTime);
                        break;
                    default:
                        strResult = ToString(strFormat, objVal);
                        break;
                }
            }
            catch (Exception ex)
            {
                strResult = String.Format("Unable to format [{0}] as [{1}] using [{2}]: {3}", objVal.GetType(), strDataType, strFormat, ex.Message);
            }
            return strResult;
        }
        /*!
        Convert object to string and trim
        \param objVal Value to trim
        \return Converted and trimmed value
        */
        public static string Trim(object objVal)
        {
            return objVal.ToString().Trim();
        }
        /*!
        Compare two strings containing numbers
        \param strNumber1 First number string
        \param strNumber2 Second number string
        \return true if the number strings differ, false otherwise
        */
        public static bool CompareNumberStrings(string strNumber1, string strNumber2)
        {
            double dblNumber1;
            double dblNumber2;
            string strError = "";
            if (!ConvertStringToDouble(strNumber1, out dblNumber1, ref strError))
            {
                Debug.Assert(false, strError);
            }
            if (!ConvertStringToDouble(strNumber2, out dblNumber2, ref strError))
            {
                Debug.Assert(false, strError);
            }
            return dblNumber1 != dblNumber2;
        }
        /*!
        Convert string collection to delimited string
        \param objTokenCollection Token collection
        \param strDelimiter Delimiter string
        \return Delimited string
        */
        public static string FormatDelimitedString(StringCollection objTokenCollection, string strDelimiter)
        {
            var objItemString = new StringBuilder();
            foreach (string strToken in objTokenCollection)
            {
                objItemString.Append(strToken);
                objItemString.Append(strDelimiter);
            }
            return objItemString.ToString();
        }
        /// <summary>
        /// Convert string collection to delimited string
        /// </summary>
        /// <param name="tokenCollection">Token collection</param>
        /// <param name="delimiter">Delimiter character</param>
        /// <returns>Delimited string</returns>
        public static string FormatDelimitedString(StringCollection tokenCollection, char delimiter)
        {
            var itemString = new StringBuilder();
            foreach (string token in tokenCollection)
            {
                itemString.Append(token);
                itemString.Append(delimiter);
            }
            return itemString.ToString();
        }
        public static bool IsStrikeDeltaValid(string strike, out string error)
        {
            error = "";
            if (String.IsNullOrEmpty(strike))
                return false;
            string strikeUpper = strike.Trim().ToUpper();
            bool result = true;
            double strikeInDouble;
            if (!Double.TryParse(strikeUpper, out strikeInDouble))
            {
                result = false;
                error = "Strike not is valid format"; // required since input can be any character other than following
                // Check if its a standard format string
                if (strikeUpper == "ATM" || strikeUpper == "ATMS" || strikeUpper == "ATMF" ||
                strikeUpper == "S" || strikeUpper == "F" || strikeUpper == "DN")
                {
                    result = true;
                }
                else
                {
                    // Check if it is a numeric format
                    if (strikeUpper.EndsWith("D") || strikeUpper.EndsWith("F"))
                    {
                        if (Double.TryParse(strikeUpper.Substring(0, strikeUpper.Length - 1), out strikeInDouble))
                        {
                            result = true;
                        }
                    }
                }
            }
            if (result) error = String.Empty;
            return result;
        }
        #endregion
        #region Internal methods
        const char CChrKiloSymbol = 'K';
        const double CDblKiloFactor = 1000.00;
        const char CChrMillionSymbol = 'M';
        const double CDblMillionFactor = 1000000.00;
        const char CChrBillionSymbol = 'B';
        const char CChrBillionSymbol1 = 'Y';
        const double CDblBillionFactor = 1000000000.00;
        const char CChrTrillionSymbol = 'T';
        //const double c_dblTrillionFactor = 1000000000000.00;
        const double CDblTrillionFactor = 1000.00;
        /*!
        Detect the shortcut symbols in the number string
        \param strNumber Number string to expand shortcuts
        \param strResult Return number string with shortcuts characters removed
        \return true on success, false otherwise
        */
        internal static bool DetectNumberShortcuts(string strNumber, ref string strResult, out double dblFactor)
        {
            // Convert to upper case for easier search
            string strNumberString = strNumber.ToUpper();
            // Determine the shortcut(s) positions
            int intKiloPos = strNumberString.IndexOf(CChrKiloSymbol);
            int intMillionPos = strNumberString.IndexOf(CChrMillionSymbol);
            int intBillionPos = strNumberString.IndexOf(CChrBillionSymbol);
            int intBillionPos1 = strNumberString.IndexOf(CChrBillionSymbol1);
            int intTrillionPos = strNumberString.IndexOf(CChrTrillionSymbol);
            // Assign the factor and symbol
            int intShortcutsCount = 0;
            char chrShortcutUsed = ' ';
            dblFactor = 1;
            int intShortcutPos = -1;
            if (intKiloPos > -1)
            {
                intShortcutsCount++;
                intShortcutPos = intKiloPos;
                chrShortcutUsed = CChrKiloSymbol;
                dblFactor = CDblKiloFactor;
            }
            if (intMillionPos > -1)
            {
                intShortcutsCount++;
                intShortcutPos = intMillionPos;
                chrShortcutUsed = CChrMillionSymbol;
                dblFactor = CDblMillionFactor;
            }
            if (intBillionPos > -1)
            {
                intShortcutsCount++;
                intShortcutPos = intBillionPos;
                chrShortcutUsed = CChrBillionSymbol;
                dblFactor = CDblBillionFactor;
            }
            if (intBillionPos1 > -1)
            {
                intShortcutsCount++;
                intShortcutPos = intBillionPos1;
                chrShortcutUsed = CChrBillionSymbol1;
                dblFactor = CDblBillionFactor;
            }
            if (intTrillionPos > -1)
            {
                intShortcutsCount++;
                intShortcutPos = intTrillionPos;
                chrShortcutUsed = CChrTrillionSymbol;
                dblFactor = CDblTrillionFactor;
            }
            // Verify expanded shortcut
            if (intShortcutsCount > 0)
            {
                if (intShortcutsCount == 1)
                {
                    // Make sure that shortcut symbol is the last one
                    if (intShortcutPos < strNumberString.Length - 1)
                    {
                        strResult = "Invalid use of '" + chrShortcutUsed + "' symbol, it should be the last character";
                        return false;
                    }
                    strResult = strNumberString.Replace(chrShortcutUsed.ToString(), "");
                }
                else
                {
                    strResult = String.Format("Invalid use of shortcut symbol, only one of '{0}', '{1}', '{2}', '{3}' , '{4}'should be used.",
                    CChrKiloSymbol, CChrMillionSymbol, CChrBillionSymbol, CChrBillionSymbol1, CChrTrillionSymbol);
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region Comparision of properties value
        /*!
Compare the public (non-indexed) properties. The List is based on 
typeof objExpected objects. For that reason type of objActual should be the same
as objExpected or one of its derived classes
\param objExpected Object with expected values of the properties
\param objActual Object with actual values
\param strError Return error message
\return true if objects are equal, false otherwise
*/
        public static bool CompareProperties(object objExpected, object objActual, ref string strError)
        {
            return CompareProperties(objExpected, objActual, null, ref strError);
        }
        /*!
        Compare the public (non-indexed) properties. The List is based on 
        typeof objExpected objects. For that reason type of objActual should be the same
        as objExpected or one of its derived classes
        \param objExpected Object with expected values of the properties
        \param objActual Object with actual values
        \param objIgnoreProperties List of properties to ignore when comparing
        \param strError Return error message
        \return true if objects are equal, false otherwise
        \note If one object is null then difference is reported, if both objects are null then no difference is reported
        */
        public static bool CompareProperties(object objExpected, object objActual, List<string> objIgnoreProperties, ref string strError)
        {
            bool blnRes = false;
            try
            {
                if (objExpected == null || objActual == null)
                {
                    if (objExpected != null)
                    {
                        throw new Exception("Actual object is null");
                    }
                    if (objActual != null)
                    {
                        throw new Exception("Expected object is null");
                    }
                    // Both are null - considered same
                }
                else
                {
                    // Check the property list
                    if (objExpected.GetType() == typeof(PropertyListType) &&
                    objActual.GetType() == typeof(PropertyListType))
                    {
                        var objExpectedList = objExpected as PropertyListType;
                        var objActualList = objActual as PropertyListType;
                        foreach (string strItem in objExpectedList.Keys)
                        {
                            string strActualValue;
                            if (!objActualList.TryGetValue(strItem, out strActualValue))
                            {
                                throw new Exception("Expected item " + strItem + " not found in the actual property list");
                            }
                            if (objExpectedList[strItem] != strActualValue)
                            {
                                throw new Exception(strItem + " property list value is different");
                            }
                        }
                        foreach (string strItem in objActualList.Keys)
                        {
                            if (!objExpectedList.ContainsKey(strItem))
                            {
                                throw new Exception("Actual item " + strItem + " not found in the expected property list");
                            }
                        }
                    }
                    else
                    {
                        // All public properties
                        PropertyInfo[] objProperties = objExpected.GetType().GetProperties();
                        foreach (PropertyInfo objProperty in objProperties)
                        {
                            if (objIgnoreProperties == null ||
                            !objIgnoreProperties.Contains(objProperty.Name))
                            {
                                if (objProperty.GetValue(objExpected, null) != null || objProperty.GetValue(objActual, null) != null)
                                {
                                    if (!objProperty.GetValue(objExpected, null).Equals(objProperty.GetValue(objActual, null)))
                                    {
                                        throw new Exception(objProperty.Name + " value is different");
                                    }
                                }
                            }
                        }
                    }
                }
                blnRes = true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return blnRes;
        }
        #endregion
        #region Testing support
        public static class GenericEnumConverterTest
        {
            #region Operations
            /// <summary>
            /// Check all converter mappings
            /// </summary>
            /// <param name="enumConverterType">Type of converter</param>
            /// <param name="errors"></param>
            /// <returns>true on success, false otherwise</returns>
            public static bool CheckAll(Type enumConverterType, out string errors)
            {
                var errorList = new List<string>();
                var fieldInfos = enumConverterType.GetFields(BindingFlags.Public | BindingFlags.Static);
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    var enumToDb = new Dictionary<object, object>();
                    var dbToEnum = new Dictionary<object, object>();
                    // Get the converter types information
                    object converter = fieldInfo.GetValue(null);
                    Type enumType = null;
                    Type dbType = null;
                    GetEnumConverterTypes(converter, ref enumType, ref dbType);
                    foreach (object enumValue in Enum.GetValues(enumType))
                    {
                        // Enum to db
                        object dbConverterValue = GetEnumConverterItem(converter, enumType, enumValue);
                        // Db to enum
                        object enumConverterValue = GetEnumConverterItem(converter, dbType, dbConverterValue);
                        // Detect any anomalies
                        if (enumToDb.ContainsKey(enumValue))
                        {
                            errorList.Add(String.Format("Duplicate enum value detected for {0} converter: {1}",
                            fieldInfo.Name, enumValue));
                        }
                        enumToDb[enumValue] = dbConverterValue;
                        if (dbToEnum.ContainsKey(dbConverterValue))
                        {
                            errorList.Add(String.Format("Duplicate DbValue attribute detected for {0} converter: {1}",
                            fieldInfo.Name, enumValue));
                        }
                        dbToEnum[dbConverterValue] = enumConverterValue;
                        if (!enumValue.Equals(enumConverterValue))
                        {
                            errorList.Add(String.Format("Round-trip conversion failed for {0}: Enum[{1}] --> DbByEnum[{2}] --> EnumByDb[{3}]",
                            fieldInfo.Name, enumValue, dbConverterValue, enumConverterValue));
                        }
                    }
                }
                var stringBuilder = new StringBuilder();
                foreach (string error in errorList)
                {
                    stringBuilder.AppendLine(error);
                }
                errors = stringBuilder.ToString();
                // Using errors to detect the problems on purpose as we have to accumulate multiple issues
                return String.IsNullOrEmpty(errors);
            }
            #endregion
            #region Private Methods
            /// <summary>
            /// Get the types used in the enum converter
            /// </summary>
            /// <param name="converter">Converter to analyze</param>
            /// <param name="enumType">Return the enum types</param>
            /// <param name="dbType">Return database type</param>
            static void GetEnumConverterTypes(object converter, ref Type enumType, ref Type dbType)
            {
                Type converterType = converter.GetType();
                Type[] geneticTypes = converterType.GetGenericArguments();
                foreach (Type t in geneticTypes)
                {
                    if (t.BaseType == typeof(Enum))
                    {
                        enumType = t;
                    }
                    else
                    {
                        dbType = t;
                    }
                }
            }
            /// <summary>
            /// Get the item from enum converter
            /// </summary>
            /// <param name="converter">Converter to get the value from</param>
            /// <param name="indexType">Index type (enum type of db type)</param>
            /// <param name="indexValue">Index value for which the indexer value is required</param>
            /// <returns>Converted value for a given indexer</returns>
            static object GetEnumConverterItem(object converter, Type indexType, object indexValue)
            {
                var getItemParams = new[] { indexType };
                var methodInfo = converter.GetType().GetMethod("get_Item", getItemParams);
                var methodParams = new[] { indexValue };
                var itemValue = methodInfo.Invoke(converter, methodParams);
                return itemValue;
            }
            #endregion
        }
        /*!
        Helper method to format the reflection string for a given type
        \param objType Type to create the reflection string for
        \return Formatted reflection string
        */
        public static string FormatReflectionString(Type objType)
        {
            return objType.AssemblyQualifiedName;
        }
        #endregion
        /// <summary>
        /// Converts the date time to an ISO8601 literal that is always safe to pass into SQL Server stored proecure
        /// datetime parameter types. This works regardless of SET DATEFORMAT or SET LANGUAGE settings.
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>ISO8601 datetime literal.</returns>
        public static string ConvertDateTimeToSafeLiteral(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();
            string ipAddress = "";
            if (!string.IsNullOrEmpty(hostName))
            {
                IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
                if (ipAddresses.Length > 0)
                {
                    ipAddress = ipAddresses[0].ToString();
                }
            }
            return ipAddress;
        }
        #region For sending Email
        /// <summary>
        /// Send EMail Using the SMTP Server
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="smtpServer"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendEmailNotification(string fromAddress, string toAddress, string smtpServer, string subject, string body)
        {
            var mailMessage = new MailMessage { From = new MailAddress(fromAddress) };
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            var smtpClient = new SmtpClient { Host = smtpServer, UseDefaultCredentials = true };
            smtpClient.Send(mailMessage);
        }
        #endregion For sending Email
    }

    #region SybaseTools
    /// <summary>
    /// Various utility functions only use for Sybase-specific operations
    /// </summary>
    public abstract class SybaseTools
    {
        #region Data convertions
        public const string CStrDateFormat = "MMM dd yyyy"; /*!< Standard date format */
        public const string CStrHhTimeFormat = "HH:mm:ss";
        public const string CStrMmMddDateFormat = "MMM dd"; /*!< Standard date format without year */
        public const string CStrDateTimeFormat = CStrDateFormat + " hh:mm:ss:ffftt"; /*!< Standard datetime format (convert 109) */
        public const string CStrParseDateTimeFormat = "MMM d yyyy h:mm:ss:ffftt"; /*!< Standard datetime format (convert 109) for parsing as database comes with single digits for days and hours */
        public const string CStrParseDateFormat = "MMM d yyyy";

        /*!
        Convert DateTime to string representation
        \param objDate Date to convert
        \param blnAddTime true to add the time part 
        \return String representing the DateTime
        */
        public static string ConvertDateTimeToString(DateTime objDate, bool blnAddTime)
        {
            return objDate.ToString(blnAddTime ? CStrDateTimeFormat : CStrDateFormat, DateTimeFormatInfo.InvariantInfo);
        }
        /*!
        Convert DateTime to string representation
        \param objDate Date to convert
        \param blnAddTime true to add the time part 
        \return String representing the DateTime
        */
        public static string ConvertDateTimeToString(DateTime objDate, string strFormat)
        {
            return objDate.ToString(strFormat, DateTimeFormatInfo.InvariantInfo);
        }
        public static DateTime ConvertStringToDateTime(string strDate, string strFormat)
        {
            return DateTime.ParseExact(strDate, strFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces);
        }
        /*!
        Convert string representation of date and time to DateTime
        \param strDate Date string to convert
        \return Converted date
        */
        public static DateTime ConvertStringToDateTime(string strDate, bool blnAddTime)
        {
            return DateTime.ParseExact(strDate, blnAddTime ? CStrParseDateTimeFormat : CStrParseDateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces);
        }
        #endregion
    }
    #endregion
}
