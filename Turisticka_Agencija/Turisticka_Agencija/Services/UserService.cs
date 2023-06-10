using System;
using Turisticka_Agencija.Utils;
using Turisticka_Agencija.Models;
using System.Linq;

namespace Turisticka_Agencija.Services
{
    class UserService
    {
        public static User? LoggedUser = null;
        public static bool IsLoggedIn = false;

        public static (bool Success, User User) TryLogin(LoginDTO loginCredentials)
        {
            using var dbContext = new Context();
            var user = dbContext.Users.FirstOrDefault(user =>
                user.Email == loginCredentials.Email.Trim() && user.Password == loginCredentials.Password.Trim());

            if (user == null)
                return (false, null);

            LoggedUser = user;
            IsLoggedIn = true;
            return (true, user);
        }

        public static (bool Success, User User) TryRegistration(User userInformation)
        {
            using var dbContext = new Context();
            if (dbContext.Users.Any(user => user.Email == userInformation.Email))
                return (false, null);

            userInformation.Name = userInformation.Name.Trim();
            userInformation.Surname = userInformation.Surname.Trim();
            userInformation.Email = userInformation.Email.Trim();
            userInformation.Password = userInformation.Password.Trim();
            userInformation.Type = UserType.User;

            dbContext.Users.Add(userInformation);
            dbContext.SaveChanges();

            LoggedUser = userInformation;
            IsLoggedIn = true;
            return (true, userInformation);
        }

    }
}
