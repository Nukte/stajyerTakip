using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace stajyerTakip
{
    internal class dailyFile
    {
        public int ID { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public byte[] data { get; set; }
        public int whoId { get; set; }
        public DateTime uploadDate { get; set; }
    }
}
