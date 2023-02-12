using Microsoft.AspNetCore.Mvc;
using API.Singletons;
using NexusFlow.src.models.DB;
using NexusFlow.src.models.DTO;
using System.Security.Cryptography;
using BCrypt.Net;

namespace API.Managers
{
    public static class UserManager
    {
        public static void CreateUser(UserDTO user)
        {

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
    }
}
