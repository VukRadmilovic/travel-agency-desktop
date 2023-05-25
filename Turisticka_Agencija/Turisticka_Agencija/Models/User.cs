using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turisticka_Agencija.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ime je obavezno polje.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email je obavezno polje.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email adresa mora biti u obliku prvideo@drugideo.trecideo)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje.")]
        [MinLength(8, ErrorMessage = "Lozinka mora da sadrži minimalno 8 karaktera.")]
        public string Password { get; set; }

        public User(string name, string surname, string email, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
        }

        public User() { }
    }
}
