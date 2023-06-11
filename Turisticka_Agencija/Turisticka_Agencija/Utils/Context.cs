using System.Data.Entity;
using Turisticka_Agencija.Models;

namespace Turisticka_Agencija.Utils
{
    internal class Context : DbContext
    {
        public Context() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectModels;Integrated Security=True") { }
        public DbSet<User>? Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripBoughtOrReservedByUser> TripsBoughtOrReservedByUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripBoughtOrReservedByUser>()
                .HasKey(t => new { t.TripId, t.UserId, t.Action });
        }
    }
}
