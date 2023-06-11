﻿using System;
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
        public double Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Place> Places { get; set; } // attractions
        public virtual ICollection<Accommodation> Accommodations { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public int QuantitySold { get; set; }

        public Trip()
        {

        }

        public Trip(int id, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude, DateTime start, DateTime end, double price, string description, int quantitySold)
        {
            Id = id;
            Name = name;
            StartLatitude = startLatitude;
            StartLongitude = startLongitude;
            EndLatitude = endLatitude;
            EndLongitude = endLongitude;
            Start = start;
            End = end;
            Price = price;
            Description = description;
            QuantitySold = quantitySold;
        }

        public Trip(int id, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude, DateTime start, DateTime end, double price, string description, ICollection<Place> places, ICollection<Accommodation> accommodations, ICollection<Restaurant> restaurants, int quantitySold)
        {
            Id = id;
            Name = name;
            StartLatitude = startLatitude;
            StartLongitude = startLongitude;
            EndLatitude = endLatitude;
            EndLongitude = endLongitude;
            Start = start;
            End = end;
            Price = price;
            Description = description;
            Places = places;
            Accommodations = accommodations;
            Restaurants = restaurants;
            QuantitySold = quantitySold;
        }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
