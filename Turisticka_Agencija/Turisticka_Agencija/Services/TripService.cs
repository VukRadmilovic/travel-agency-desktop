using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Services
{
    public class TripService
    {
        public static List<Trip> GetAll()
        {
            using var dbContext = new Context();
            return dbContext.Trips.ToList();
        }

        public static bool Save(Trip trip)
        {
            using var dbContext = new Context();
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

        public static bool Update(Trip trip)
        {
            using var dbContext = new Context();
            dbContext.Entry(trip).State = EntityState.Modified;
            dbContext.SaveChanges();
            return true;
        }
    }
}
