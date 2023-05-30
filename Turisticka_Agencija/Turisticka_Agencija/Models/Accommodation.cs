using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija.Models
{
    internal class Accommodation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public string? InfoLink { get; set; }
        public int? StarCount { get; set; }

        public Accommodation(string name, double latitude, double longitude,string address, string? description, string? infoLink, int? starCount)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Description = description;
            InfoLink = infoLink;
            StarCount = starCount;
        }
    }
}
