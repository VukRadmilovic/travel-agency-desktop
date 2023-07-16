using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tourist_Agency.Models;
using Tourist_Agency.Utils;

namespace Tourist_Agency.Services
{
    internal class PlaceService
    {
        public static List<Place> GetAll()
        {
            using var dbContext = new Context();
            return dbContext.Places.ToList();
        }

        public static bool Save(Place place)
        {
            using var dbContext = new Context();
            dbContext.Places.Add(place);
            dbContext.SaveChanges();
            return true;
        }

        public static bool Delete(Place place)
        {
            using var dbContext = new Context();
            dbContext.Entry(place).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }

        public static bool Update(Place place)
        {
            using var dbContext = new Context();
            dbContext.Entry(place).State = EntityState.Modified;
            dbContext.SaveChanges();
            return true;
        }
    }
}
