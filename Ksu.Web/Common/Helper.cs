using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace Arabia.Web.Common
{
    public static class Helper
    {
        #region Fields

        public static int pageSize = 5;
        public static string noResultMessage = "لا توجد بيانات.";
        public static int minLength = 1;
       
        #endregion
    

      

        public static byte[] GetPictureBits(this HttpPostedFileBase postedFile)
        {
            Stream fs = postedFile.InputStream;
            int size = postedFile.ContentLength;
            byte[] img = new byte[size];
            fs.Read(img, 0, size);
            return img;
        }
    }
}