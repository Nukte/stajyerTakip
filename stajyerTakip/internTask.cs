using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stajyerTakip
{
    internal class internTask
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int InternID { get; set; }
        public string InternName { get; set; }
        public string TaskStatus { get; internal set; }
    }
}
