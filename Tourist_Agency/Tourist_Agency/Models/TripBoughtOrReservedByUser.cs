using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourist_Agency.Models
{
    public class TripBoughtOrReservedByUser
    {
        [Key, Column(Order=0)]
        public int TripId { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        [Key, Column(Order = 2)]
        public Action Action { get; set; }
        [Key, Column(Order = 3)]
        public DateTime BuyDate {  get; set; }

        public TripBoughtOrReservedByUser(int tripId, int userId, Action action, DateTime buyDate)
        {
            TripId = tripId;
            UserId = userId;
            Action = action;
            BuyDate = buyDate;
        }

        public TripBoughtOrReservedByUser()
        {

        }
    }

    public enum Action
    {
        Reserved,
        Bought
    }
}
