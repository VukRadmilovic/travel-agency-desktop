using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija
{
    internal class Context : DbContext
    {
        public Context() : base("Data Source=(localdb)\\ProjectModels;Initial Catalog=AgencijaDb;Integrated Security=True") { }
        public DbSet<User> Users { get; set; }  
    }
}
