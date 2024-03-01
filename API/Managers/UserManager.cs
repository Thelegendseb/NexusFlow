using Microsoft.AspNetCore.Mvc;
using API.Singletons;
using NexusFlow.models.DB;
using NexusFlow.models.DTO;
using System.Security.Cryptography;
using BCrypt.Net;
using MongoDB.Driver;

namespace API.Managers
{
    public static class UserManager
    {
        public static IActionResult CreateUser(UserDTO user)
        {

            var user_collection = MongoSingleton.Database.GetCollection<UserDB>(TablesSingleton.Users);

            // perform check to see if username already exists in system

             List<UserDB> users = user_collection.Find(x => x.EmailAddress == user.EmailAddress).ToList();
            // List<UserDB> users = new List<UserDB>();

            if(users.Count > 0)
            {
                // User already contained in system
                return new ConflictResult();
            }

            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, passwordSalt);

            UserDB newUser = new UserDB()
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = user.Firstname,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            NexusNodeDB root = new NexusNodeDB()
            {
                Id = Guid.NewGuid().ToString(),
                // ParentId = null;
                OwnerId = newUser.Id,
                Name = "root",
                Data = "This is your user root, the main folder where all of your information is stored.",
                DataType = 0
            };

            newUser.RootId = root.Id;   

            user_collection.InsertOne(newUser);

            node_collection.InsertOne(root);

            return new OkResult();

        }

        public static LoginResponseDTO LoginUser(LoginDTO logininfo)
        {

            var user_collection = MongoSingleton.Database.GetCollection<UserDB>(TablesSingleton.Users);

            List<UserDB> users = user_collection.Find(x => x.EmailAddress == logininfo.EmailAddress).ToList();

            // Assume only 1 user with email adress exists in system

            LoginResponseDTO response = new LoginResponseDTO();

            if (users.Count == 0)
            {
                response.ResponseCode = 401;
                response.ResponseDescription = "USER_NOT_FOUND";
                // Unauthorized Status Code
            }
            else
            {
                var user = users[0];
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(logininfo.Password, user.PasswordSalt);

                if (user.PasswordHash == passwordHash)
                {
                    // Successfull login

                    response.ResponseCode = 200;
                    response.ResponseDescription = "LOGIN_SUCCESS";

                    // Generate Access Token

                    string token = Guid.NewGuid().ToString();

                    response.AccessToken = token;

                    // Add access Token to database

                    var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

                    AccessTokenDB accessTokenDB = new AccessTokenDB()
                    {
                        Id = token, UserId = user.Id
                    };

                    accesstoken_collection.InsertOne(accessTokenDB);

                }
                else
                {
                    response.ResponseCode = 401;
                    response.ResponseDescription = "INCORRECT_PASSWORD";
                    // Unauthorized Status Code

                }
            }

            return response;

        }

        public static IActionResult LogoutUser(string accesstoken)
        {

            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            accesstoken_collection.DeleteOne(x => x.Id == accesstoken);

            return new OkResult();

        }

        public static IActionResult DeleteUser(string accesstoken)
        {
            // Obtain collections

            var user_collection = MongoSingleton.Database.GetCollection<UserDB>(TablesSingleton.Users);

            var accesstoken_collection = MongoSingleton.Database.GetCollection<AccessTokenDB>(TablesSingleton.AccessTokens);

            var node_collection = MongoSingleton.Database.GetCollection<NexusNodeDB>(TablesSingleton.Nodes);

            AccessTokenDB token = accesstoken_collection.Find(x => x.Id == accesstoken).ToList()[0];

            // Delete all owned nodes

            node_collection.DeleteMany(x => x.OwnerId == token.UserId);

            // Delete user

            user_collection.DeleteOne(x => x.Id == token.UserId);

            // Delete Access Token

            accesstoken_collection.DeleteOne(x => x.Id == accesstoken);

            return new OkResult();

        }
    }
}
