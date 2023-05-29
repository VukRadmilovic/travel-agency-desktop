using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija.Models
{
    internal class Restaurant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Description { get; set; }
        public string? InfoLink { get; set; }
        public bool IsVeganFriendly { get; set; }
        public bool IsGlutenFreeFriendly { get; set; }

        public Restaurant(string name, double latitude, double longitude, string? description,
            string? infoLink, bool isVeganFriendly, bool isGlutenFreeFriendly)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            InfoLink = infoLink;
            IsVeganFriendly = isVeganFriendly;
            IsGlutenFreeFriendly = isGlutenFreeFriendly;
        }

        public Restaurant() {}
    }
}
