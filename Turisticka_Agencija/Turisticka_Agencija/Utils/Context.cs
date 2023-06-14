using System.Data.Entity;
using Turisticka_Agencija.Models;

namespace Turisticka_Agencija.Utils
{
    internal class Context : DbContext
    {
        public Context() : base("Data Source=(localdb)\\ProjectModels;Initial Catalog=AgencijaDb;Integrated Security=True") { }
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

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Places)
                .WithMany()
                .Map(tpt =>
                {
                    tpt.ToTable("TripPlace");
                    tpt.MapLeftKey("TripId");
                    tpt.MapRightKey("PlaceId");
                });

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Accommodations)
                .WithMany()
                .Map(tat =>
                {
                    tat.ToTable("TripAccommodation");
                    tat.MapLeftKey("TripId");
                    tat.MapRightKey("AccommodationId");
                });

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Restaurants)
                .WithMany()
                .Map(trt =>
                {
                    trt.ToTable("TripRestaurant");
                    trt.MapLeftKey("TripId");
                    trt.MapRightKey("RestaurantId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
