using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija.Models
{
    public class Restaurant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public string? InfoLink { get; set; }
        public bool IsVeganFriendly { get; set; }
        public bool IsGlutenFreeFriendly { get; set; }

        public Restaurant(string name, double latitude, double longitude,string address, string? description,
            string? infoLink, bool isVeganFriendly, bool isGlutenFreeFriendly)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Description = description;
            InfoLink = infoLink;
            IsVeganFriendly = isVeganFriendly;
            IsGlutenFreeFriendly = isGlutenFreeFriendly;
        }

        public Restaurant() {}

        public override bool Equals(object? obj)
        {
            return obj is Restaurant && ((Restaurant)obj).Name == Name;
        }
    }
}
