﻿using System.Data.Entity;


namespace Turisticka_Agencija
{
    internal class Context : DbContext
    {
        public Context() : base("Data Source=(localdb)\\ProjectModels;Initial Catalog=AgencijaDb;Integrated Security=True") { }
        public DbSet<User>? Users { get; set; }  
    }
}
