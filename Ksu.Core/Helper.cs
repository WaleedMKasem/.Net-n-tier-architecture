using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Security;
using System.Security.Cryptography;
using Microsoft.Practices.ServiceLocation;
using System.Globalization;
using System.Threading;

namespace Arabia.Core
{
    public partial class Helper
    {
        #region Fields
        private static readonly Helper instance = new Helper();
        public string EncryptionKey = "273ece6f97dd844d";

        public static Dictionary<int, string> ProjectStatus = new Dictionary<int, string>() 
        { 
           {0,"منتهى"}
          ,{1,"تحت التنفيذ"}
          ,{2,"متعثر"}
          ,{3,"لم يبدأ التنفيذ"}
        };
        public static Dictionary<int, string> ProgressRateStatus = new Dictionary<int, string>() 
        { 
           {0,"منتظم"}
          ,{1,"تأخير أقل من 25 %"}
          ,{2,"تأخير 25 % فأكثر"}
        };

        #endregion

        #region Properties
        //public static Helper Instance
        //{
        //    get
        //    {
        //        return Helper.instance;
        //    }
        //}
        #endregion

        #region General
        public string Int2Guid(int value)
        {
            return EncryptText(value.ToString());
        }
        public int Guid2Int(string value)
        {
            return int.Parse(DecryptText(value));
        }
        public bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }
        public string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public string GetThisPageUrl(bool includeQueryString)
        {
            string URL = string.Empty;
            if (HttpContext.Current == null)
                return URL;

            if (includeQueryString)
            {
                URL = HttpContext.Current.Request.RawUrl;
            }
            else
            {
                URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            URL = URL.ToLowerInvariant();
            return URL;
        }
        public long DatetimeToUnixTimestamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }
        public DateTime UnixTimestampToDatetime(long unixTimestamp)
        {
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            DateTime dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
        }
        public string SavePicutre(FileUpload fuPicture, string Domain, bool Resize = false, int Width = 0, int Height = 0)
        {
            string PictureUrl = string.Empty;
            if (fuPicture.PostedFile != null && fuPicture.PostedFile.ContentLength > 0)
            {
                string Ext = System.IO.Path.GetExtension(fuPicture.FileName);
                string NewName = System.Guid.NewGuid().ToString();
                string FullName = NewName + Ext;
                PictureUrl = Domain + FullName;
                string FullPathName = HttpContext.Current.Server.MapPath(PictureUrl);
                //Save Image On Server
                System.Drawing.Bitmap Image = new System.Drawing.Bitmap(fuPicture.FileContent);
                if (Resize)
                    Image.GetThumbnailImage(Width, Height, null, IntPtr.Zero).Save(FullPathName, Image.RawFormat);
                else
                    Image.Save(FullPathName, Image.RawFormat);
                Image.Dispose();
            }
            return PictureUrl;
        }
        public string PitureName(FileUpload fuPicture)
        {
            string FullName = string.Empty;
            if (fuPicture.PostedFile != null && fuPicture.PostedFile.ContentLength > 0)
            {
                string Ext = System.IO.Path.GetExtension(fuPicture.FileName);
                string NewName = System.Guid.NewGuid().ToString();
                FullName = NewName + Ext;
            }
            return FullName;
        }
        #endregion

        #region QueryStrings
        public string QueryString(string name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[name] != null)
                result = HttpContext.Current.Request.QueryString[name].ToString();
            return result;
        }
        public int QueryStringInt(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }
        public byte QueryStringByte(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            byte result;
            byte.TryParse(resultStr, out result);
            return result;
        }
        public Guid QueryStringGuid(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            Guid result;
            Guid.TryParse(resultStr, out result);
            return result;
        }
        public int QueryStringIntDecrypt(string name)
        {
            string resultStr = DecryptText(QueryString(name).ToUpperInvariant());
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }
        public int StringInt(string name)
        {
            string resultStr = name.ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }
        public bool QueryStringBool(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            return (resultStr == "YES" || resultStr == "TRUE" || resultStr == "1");
        }
        #endregion

        #region Ensure
        public string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }
        public bool IsDouble(string str)
        {
            double result;
            return double.TryParse(str.Trim(), out result);
        }
        public string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }
        public string EnsureMaximumLength(string str, int maxLength)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
                return str.Substring(0, maxLength);
            else
                return str;
        }
        #endregion

        #region Cookie
        public void SetCookie(string cookieName, string cookieValue, TimeSpan ts)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = HttpContext.Current.Server.UrlEncode(cookieValue);
                DateTime dt = DateTime.Now;
                cookie.Expires = dt.Add(ts);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Gets cookie string
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="decode">Decode cookie</param>
        /// <returns>Cookie string</returns>
        public string GetCookieString(string cookieName, bool decode)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                return string.Empty;
            }
            try
            {
                string tmp = HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
                if (decode)
                    tmp = HttpContext.Current.Server.UrlDecode(tmp);
                return tmp;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets boolean value from cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <returns>Result</returns>
        public bool GetCookieBool(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true).ToUpperInvariant();
            return (str1 == "TRUE" || str1 == "YES" || str1 == "1");
        }

        /// <summary>
        /// Gets integer value from cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <returns>Result</returns>
        public int GetCookieInt(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true);
            if (!String.IsNullOrEmpty(str1))
                return Convert.ToInt32(str1);
            else
                return 0;
        }
        #endregion

        #region Utilities
        public static T ResolveType<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }
        public string Encode(string str)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str));
        }
        public string Decode(string str)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        public string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = EncryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        public string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = EncryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(cipherText);
            }
            catch { return "0"; }
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }
        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }
        public string BuildXmlString(string xmlRootName, string xmlName, int value)
        {
            //<organizations><id>1</id><id>2</id><id>3</id></organizations>
            //<organizations><id>1</id></organizations>
            StringBuilder xmlString = new StringBuilder();

            xmlString.AppendFormat("<{0}>", xmlRootName);
            xmlString.AppendFormat("<" + xmlName + ">{0}</" + xmlName + ">", value);
            xmlString.AppendFormat("</{0}>", xmlRootName);

            return xmlString.ToString();
        }
        public string BuildXmlString(string xmlRootName, string xmlName, List<string> values)
        {
            //<organizations><id>1</id><id>2</id><id>3</id></organizations>
            //<ids><id>1</id></ids>
            StringBuilder xmlString = new StringBuilder();

            xmlString.AppendFormat("<{0}>", xmlRootName);
            foreach (string value in values)
            {
                xmlString.AppendFormat("<" + xmlName + ">{0}</" + xmlName + ">", value);
            }
            xmlString.AppendFormat("</{0}>", xmlRootName);

            return xmlString.ToString();
        }

        #endregion

      
    }
}
