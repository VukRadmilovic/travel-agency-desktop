using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Services
{
    public class TripService
    {
        public static List<Trip> GetAll()
        {
            using var dbContext = new Context();
            List<Trip> trips = dbContext.Trips
                .Include(t => t.Accommodations)
                .Include(t => t.Restaurants)
                .Include(t => t.Places)
                .ToList();
            return trips;
        }

        public static bool Save(Trip trip)
        {
            using var dbContext = new Context();

            // Check if places already exist in the database
            for (int i = trip.Places.Count - 1; i >= 0; i--)
            {
                var place = trip.Places.ElementAt(i);
                var existingPlace = dbContext.Places.FirstOrDefault(p => p.Name == place.Name);
                if (existingPlace != null)
                {
                    // Place already exists, so replace it with the existing one
                    dbContext.Entry(place).State = EntityState.Detached;
                    trip.Places.Remove(place);
                    trip.Places.Add(existingPlace);
                }
            }

            dbContext.Trips.Add(trip);
            dbContext.SaveChanges();
            return true;
        }

        public static bool Delete(Trip trip)
        {
            using var dbContext = new Context();
            dbContext.Entry(trip).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }

        public static bool Update(Trip updatedTrip)
        {
            using (var context = new Context())
            {
                var existingTrip = context.Trips
                    .Include(t => t.Places)
                    .Include(t => t.Accommodations)
                    .Include(t => t.Restaurants)
                    .SingleOrDefault(t => t.Id == updatedTrip.Id);

                if (existingTrip != null)
                {
                    // Update the scalar properties of the trip
                    existingTrip.Name = updatedTrip.Name;
                    existingTrip.Start = updatedTrip.Start;
                    existingTrip.End = updatedTrip.End;
                    existingTrip.Price = updatedTrip.Price;
                    existingTrip.Description = updatedTrip.Description;

                    // Update the Places collection
                    existingTrip.Places.Clear();
                    foreach (var place in updatedTrip.Places)
                    {
                        var existingPlace = context.Places.Find(place.Name);
                        if (existingPlace != null)
                            existingTrip.Places.Add(existingPlace);
                    }

                    // Update the Accommodations collection
                    existingTrip.Accommodations.Clear();
                    foreach (var accommodation in updatedTrip.Accommodations)
                    {
                        var existingAccommodation = context.Accommodations.Find(accommodation.Id);
                        if (existingAccommodation != null)
                            existingTrip.Accommodations.Add(existingAccommodation);
                    }

                    // Update the Restaurants collection
                    existingTrip.Restaurants.Clear();
                    foreach (var restaurant in updatedTrip.Restaurants)
                    {
                        var existingRestaurant = context.Restaurants.Find(restaurant.Id);
                        if (existingRestaurant != null)
                            existingTrip.Restaurants.Add(existingRestaurant);
                    }

                    // Save the changes to the database
                    context.SaveChanges();
                }
            }

            return true;
        }
    }
}
