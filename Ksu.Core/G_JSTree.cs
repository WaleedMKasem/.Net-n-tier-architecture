using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Core
{
    public class G_JSTree
    {
        public int ServerId { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string parent { get; set; }

        public G_a_attr a_attr { get; set; }
        public G_state state { get; set; }
    }
    public class G_state
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }
    public class G_a_attr
    {
        public string Class { get; set; }
        
    }

}
