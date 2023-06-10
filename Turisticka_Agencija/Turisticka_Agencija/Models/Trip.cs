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
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public double EndLatitude { get; set; }
        public double EndLongitude { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Place> Places { get; set; }
        public double Price { get; set; }
        public Accommodation? Accommodation { get; set; }
        public Restaurant Restaurant { get; set; }
        public string Transport { get; set; }

        public Trip()
        {

        }

        public Trip(int id, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude, List<Place> places, double price, Accommodation? accommodation, Restaurant restaurant, string transport, DateTime start, DateTime end)
        {
            Id = id;
            Name = name;
            StartLatitude = startLatitude;
            StartLongitude = startLongitude;
            EndLatitude = endLatitude;
            EndLongitude = endLongitude;
            Places = places;
            Price = price;
            Accommodation = accommodation;
            Restaurant = restaurant;
            Transport = transport;
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
