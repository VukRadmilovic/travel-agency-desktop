using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Services
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
