using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using managerApp;

namespace stajyerTakip
{
    internal class Context:DbContext
    {
        public Context():base("MySqlConn")
        {
            
        }
        public DbSet<dailyFile> dailyFiles { get; set;}
        public DbSet<intern> interns { get; set;}

    }
}
