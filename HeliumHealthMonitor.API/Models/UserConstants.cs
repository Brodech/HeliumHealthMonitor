using System.Collections.Generic;

namespace ApiDemo_JwtApp.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() {
                Username = "user1", EmailAddress = "jason.admin@email.com",
                Password = "12345678", GivenName = "Jason", Surname = "Bryant", Role = "Administrator"
            },
            new UserModel() {
                Username = "user2", EmailAddress = "elyse.seller@email.com",
                Password = "132456", GivenName = "Elyse", Surname = "Lambert", Role = "User"
            }
        };
    }
}
