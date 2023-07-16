using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tourist_Agency.Models;
using Tourist_Agency.Utils;
using Action = Tourist_Agency.Models.Action;

namespace Tourist_Agency.Services
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

        public static int CountBought(Trip trip, DateTime dateTime)
        {
            using var dbContext = new Context();
            int count = dbContext.TripsBoughtOrReservedByUser.Count(o => o.TripId == trip.Id && o.BuyDate.Month == dateTime.Month && o.BuyDate.Year == dateTime.Year && o.Action == Action.Bought);
            return count;
        }
    }
}
