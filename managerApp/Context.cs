using stajyerTakip;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace managerApp
{
    internal class Context : DbContext
    {
        public Context() : base("MySqlConn")
        {

        }
        public DbSet<dailyFile> dailyFiles { get; set; }
        public DbSet<intern> interns { get; set; }

    }
}
