using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tourist_Agency.Models;
using Tourist_Agency.Utils;

namespace Tourist_Agency.Services
{
    internal class AccommodationService
    {
        public static List<Accommodation> GetAll()
        {
            using var dbContext = new Context();
            return dbContext.Accommodations.ToList();
        }

        public static bool Save(Accommodation accommodation)
        {
            using var dbContext = new Context();
            dbContext.Accommodations.Add(accommodation);
            dbContext.SaveChanges();
            return true;
        }

        public static bool Delete(Accommodation accommodation)
        {
            using var dbContext = new Context();
            dbContext.Entry(accommodation).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }

        public static bool Update(Accommodation accommodation)
        {
            using var dbContext = new Context();
            dbContext.Entry(accommodation).State = EntityState.Modified;
            dbContext.SaveChanges();
            return true;
        }
    }
}
