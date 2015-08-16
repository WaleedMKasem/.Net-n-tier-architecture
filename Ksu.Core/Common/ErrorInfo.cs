using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabia.Core.Common
{
   public class ErrorInfo
   {
       public int ErrorID { get; set; }
       public string ErrorMessage { get; set; }
       public string ErrorDesc { get; set; }
       public string ErrorCode
       {
           get
           {
               return string.Format("ERR{0}", this.ErrorID.ToString().PadLeft(5));
           }
       }
   }
}
