using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Services
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
