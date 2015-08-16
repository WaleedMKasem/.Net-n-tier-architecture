using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arabia.Core
{
    public class ArabiaContext
    {
        public static int UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserId"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["UserId"];
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }

      
    }
}