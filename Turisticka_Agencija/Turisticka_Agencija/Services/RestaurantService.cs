using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turisticka_Agencija.Models;

namespace Turisticka_Agencija.Services
{
     class RestaurantService
    {
        public static List<Restaurant> GetAll()
        {
            using (var dbContext = new Context())
            {
                return dbContext.Restaurants.ToList();
            }
        }
    }
}
