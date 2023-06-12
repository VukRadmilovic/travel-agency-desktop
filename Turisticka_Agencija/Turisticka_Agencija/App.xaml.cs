using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Windows;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //GenerateRandomTrips();
        }

        private static void GenerateRandomTrips()
        {
            using (var dbContext = new Context())
            {
                // Create and add 5 trips with random data
                for (int i = 0; i < 5; i++)
                {
                    Trip trip = CreateRandomTrip();
                    dbContext.Trips.Add(trip);
                }

                dbContext.SaveChanges();

                foreach (var trip in TripService.GetAll())
                {
                    Console.Write(trip);
                }
            }
        }

        private static Trip CreateRandomTrip()
        {
            Random random = new Random();

            string tripName = "Trip " + random.Next(1000, 9999);
            List<Place> places = GenerateRandomPlaces();
            double price = random.Next(500, 5000);
            Accommodation? accommodation = GenerateRandomAccommodation();
            Restaurant restaurant = GenerateRandomRestaurant();

            List<Accommodation> accommodations = new List<Accommodation>();
            List<Restaurant> restaurants = new List<Restaurant>();

            if (accommodation != null)
            {
                accommodations.Add(accommodation);
            }

            if (restaurant != null)
            {
                restaurants.Add(restaurant);
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            string description = "YES";

            return new Trip(
                id: 0,
                name: tripName,
                start: start,
                end: end,
                price: price,
                description: description,
                places: places,
                accommodations: accommodations,
                restaurants: restaurants
            );
        }

        private static double GenerateRandomLatitude()
        {
            Random random = new Random();
            return random.NextDouble() * (90.0 - (-90.0)) + (-90.0);
        }

        private static double GenerateRandomLongitude()
        {
            Random random = new Random();
            return random.NextDouble() * (180.0 - (-180.0)) + (-180.0);
        }

        private static List<Place> GenerateRandomPlaces()
        {
            List<Place> places = new List<Place>();

            // Add random places to the list
            // You can modify this to generate specific places or use external data sources
            for (int i = 0; i < 3; i++)
            {
                Place place = new Place(
                    name: "Place " + i,
                    latitude: GenerateRandomLatitude(),
                    longitude: GenerateRandomLongitude(),
                    address: "Address " + i,
                    description: "Description " + i,
                    infoLink: "Link " + i
                );

                places.Add(place);
            }

            return places;
        }

        private static Accommodation? GenerateRandomAccommodation()
        {
            Random random = new Random();
            bool hasAccommodation = random.NextDouble() < 0.5; // 50% chance of having accommodation

            if (hasAccommodation)
            {
                return new Accommodation(
                    name: "Accommodation",
                    latitude: GenerateRandomLatitude(),
                    longitude: GenerateRandomLongitude(),
                    address: "Accommodation Address",
                    description: "Accommodation Description",
                    infoLink: "Accommodation Link",
                    starCount: random.Next(1, 5)
                );
            }
            else
            {
                return null;
            }
        }

        private static Restaurant GenerateRandomRestaurant()
        {
            Random random = new Random();

            return new Restaurant(
                name: "Restaurant",
                latitude: GenerateRandomLatitude(),
                longitude: GenerateRandomLongitude(),
                address: "Restaurant Address",
                description: "Restaurant Description",
                infoLink: "Restaurant Link",
                isVeganFriendly: random.NextDouble() < 0.5, // 50% chance of being vegan-friendly
                isGlutenFreeFriendly: random.NextDouble() < 0.5 // 50% chance of being gluten-free friendly
            );
        }
    }
}
