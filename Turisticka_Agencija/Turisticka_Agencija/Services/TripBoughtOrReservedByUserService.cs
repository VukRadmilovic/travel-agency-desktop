using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Utils;
using Action = Turisticka_Agencija.Models.Action;

namespace Turisticka_Agencija.Services
{
    class TripBoughtOrReservedByUserService
    {
        public static List<TripBoughtOrReservedByUser> GetAll()
        {
            using var dbContext = new Context();
            return dbContext.TripsBoughtOrReservedByUser.ToList();
        }

        public static bool IsReserved(Trip trip, User user)
        {
            using var dbContext = new Context();
            return dbContext.TripsBoughtOrReservedByUser.Any(t => t.TripId == trip.Id && t.UserId == user.Id && t.Action == Action.Reserved);
        }

        public static bool IsBought(Trip trip, User user)
        {
            using var dbContext = new Context();
            return dbContext.TripsBoughtOrReservedByUser.Any(t => t.TripId == trip.Id && t.UserId == user.Id && t.Action == Action.Bought);
        }

        public static int NumberOfSold(Trip trip)
        {
            using var dbContext = new Context();
            return dbContext.TripsBoughtOrReservedByUser.Count(t => t.TripId == trip.Id && t.Action == Action.Bought);
        }

        public static bool Save(TripBoughtOrReservedByUser t)
        {
            using var dbContext = new Context();
            dbContext.TripsBoughtOrReservedByUser.Add(t);
            dbContext.SaveChanges();
            return true;
        }

        public static bool Delete(TripBoughtOrReservedByUser t)
        {
            using var dbContext = new Context();
            dbContext.Entry(t).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return true;
        }
    }
}
