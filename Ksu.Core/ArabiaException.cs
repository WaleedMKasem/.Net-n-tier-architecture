using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabia.Core
{
    public class ArabiaException : Exception
    {
        public ArabiaException(string msg)
            : base(msg)
        {
        }
    }
    public class ArabiaDuplicateException : Exception
    {
        public ArabiaDuplicateException(string msg)
            : base(msg)
        {
        }
    }
    public class ArabiaDeleteException : Exception
    {
        public ArabiaDeleteException(string msg)
            : base(msg)
        {
        }
    }
}
