using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turisticka_Agencija.Services;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Models
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
            //TODO: IMPLEMENT
        }

        public void ReserveTrip(Trip trip)
        {
            //TODO: IMPLEMENT
        }


        public void BuyTrip(Trip trip)
        {
            //TODO: IMPLEMENT
        }

        public bool ReservedTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public bool BoughtTrip(Trip trip)
        {
            throw new NotImplementedException();
        }
    }

    public enum UserType
    {
        Agent,
        User
    }
}
