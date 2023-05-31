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
