using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managerApp
{
    internal class task
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public DateTime TaskEndDate { get; set; }
    }
}
