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
        public bool TryLogin(LoginDTO loginCredentials)
        {
            using (var dbContext = new Context())
            {
                if (!dbContext.Users.Any(user =>
                        user.Email == loginCredentials.Email && user.Password == loginCredentials.Password))
                    return false;
                return true;
            }
        }
    }
}
