using System.ComponentModel.DataAnnotations;

namespace Tourist_Agency.Models
{
    //Nece biti abstract posto entity framework kada vraca base klasu vuce i derived klase, sto ne treba da radi kada
    //samo treba izlistati turisticke atrakcije
    public class Place
    {
        [Key]
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

        public override bool Equals(object? obj)
        {
            return obj is Place && ((Place)obj).Name == Name;
        }
    }
}
