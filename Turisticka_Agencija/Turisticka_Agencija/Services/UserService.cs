using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turisticka_Agencija.Models;

namespace Turisticka_Agencija.Services
{
    class UserService
    {
        public static bool TryLogin(LoginDTO loginCredentials)
        {
            using (var dbContext = new Context())
            {
                if (!dbContext.Users.Any(user =>
                        user.Email == loginCredentials.Email.Trim() && user.Password == loginCredentials.Password.Trim()))
                    return false;
                return true;
            }
        }


        public static bool TryRegistration(User userInformation)
        {
            using (var dbContext = new Context())
            {
                if (dbContext.Users.Any(user => user.Email == userInformation.Email)) return false;
                userInformation.Name = userInformation.Name.Trim();
                userInformation.Surname = userInformation.Surname.Trim();
                userInformation.Email = userInformation.Email.Trim();
                userInformation.Password = userInformation.Password.Trim();
                dbContext.Users.Add(userInformation);
                dbContext.SaveChanges();
                return true;
            }
        }
    }
}
