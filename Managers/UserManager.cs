using Microsoft.AspNetCore.Mvc;
using API.Singletons;
using NexusFlow.src.models.DB;
using NexusFlow.src.models.DTO;
using System.Security.Cryptography;
using BCrypt.Net;
using Data.src.models.DTO;
using MongoDB.Driver;

namespace API.Managers
{
    public static class UserManager
    {
        public static void CreateUser(UserDTO user)
        {

            // perform check to see if username already exists in system

            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, passwordSalt);

            UserDB newUser = new UserDB()
            {
                UserId = Guid.NewGuid().ToString(),
                Firstname = user.Firstname,
                Surname = user.Surname,
                EmailAdress = user.EmailAdress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var user_collection = MongoSingleton.Database.GetCollection<UserDB>(TablesSingleton.Users);
            user_collection.InsertOne(newUser);

            // node insertion -


        }

        public static void LoginUser(LoginDTO logininfo)
        {

            var user_collection = MongoSingleton.Database.GetCollection<UserDB>(TablesSingleton.Users);

            List<UserDB> users = user_collection.Find(x => x.EmailAdress == logininfo.EmailAdress).ToList();

            // Assume only 1 user with email adress exists in system

            if (users.Count == 0)
            {
                // Return 401 Unauthorized status code to indicate that the login failed
                // because there is no user with the provided email address
            }
            else
            {
                var user = users[0];
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(logininfo.Password, user.PasswordSalt);

                if (user.PasswordHash == passwordHash)
                {
                    // Return 200 OK status code to indicate that the login was successful
                    // You should also generate an access token and store it in the database
                    // and attach it to a response cookie to be returned to the client
                    // with an appropriate expiration date/time to limit the user's session
                }
                else
                {
                    // Return 401 Unauthorized status code to indicate that the login failed
                    // because the password provided by the user does not match the password hash stored in the database
                }
            }

        }
    }
}
