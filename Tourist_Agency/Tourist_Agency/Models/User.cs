using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tourist_Agency.Services;

namespace Tourist_Agency.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public User(string name, string surname, string email, string password, UserType type)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Type = type;
        }

        public User() { }

        public void CancelReservation(Trip trip)
        {
            TripBoughtOrReservedByUser t = new(trip.Id, Id, Action.Reserved, DateTime.Now);
            TripBoughtOrReservedByUserService.Delete(t);
        }

        public void ReserveTrip(Trip trip)
        {
            TripBoughtOrReservedByUser t = new(trip.Id, Id, Action.Reserved, DateTime.Now);
            TripBoughtOrReservedByUserService.Save(t);
        }


        public void BuyTrip(Trip trip)
        {
            TripBoughtOrReservedByUser t = new(trip.Id, Id, Action.Bought, DateTime.Now);
            TripBoughtOrReservedByUserService.Save(t);
        }

        public bool ReservedTrip(Trip trip)
        {
            return TripBoughtOrReservedByUserService.IsReserved(trip, this);
        }

        public bool BoughtTrip(Trip trip)
        {
            return TripBoughtOrReservedByUserService.IsBought(trip, this);
        }
    }

    public enum UserType
    {
        Agent,
        User
    }
}
