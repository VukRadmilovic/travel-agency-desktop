using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tourist_Agency.Models;
using Tourist_Agency.Utils;

namespace Tourist_Agency.Services
{
    class RestaurantService
     {
         public static List<Restaurant> GetAll()
         {
             using var dbContext = new Context();
             return dbContext.Restaurants.ToList();
         }

        public static bool Save(Restaurant restaurant)
        {
            using var dbContext = new Context();
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();
            return true;
        }

        public static bool Delete(Restaurant restaurant)
        {
            using var dbContext = new Context();
            dbContext.Entry(restaurant).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }

        public static bool Update(Restaurant restaurant)
        {
            using var dbContext = new Context();
            dbContext.Entry(restaurant).State = EntityState.Modified;
            dbContext.SaveChanges();
            return true;
        }

    }
}
