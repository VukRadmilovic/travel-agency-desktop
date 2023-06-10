using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija.Models
{
    //Nece biti abstract posto entity framework kada vraca base klasu vuce i derived klase, sto ne treba da radi kada
    //samo treba izlistati turisticke atrakcije
    public class Place
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public string? InfoLink { get; set; }

        public Place(string name, double latitude, double longitude,string address, string? description, string? infoLink)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Description = description;
            InfoLink = infoLink;
        }

        public Place() {}
    }
}
