using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija.Models
{
    public class Trip
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Place> Places { get; set; } // attractions
        public virtual ICollection<Accommodation> Accommodations { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public Trip()
        {

        }

        public Trip(int id, string name, DateTime start, DateTime end, double price, string description)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
            Price = price;
            Description = description;
        }

        public Trip(int id, string name, DateTime start, DateTime end, double price, string description, ICollection<Place> places, ICollection<Accommodation> accommodations, ICollection<Restaurant> restaurants)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
            Price = price;
            Description = description;
            Places = places;
            Accommodations = accommodations;
            Restaurants = restaurants;
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
