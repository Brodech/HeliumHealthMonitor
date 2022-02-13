using System.Collections.Generic;

namespace HeliumHealthMonitor.API.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() {
                Username = "user1",
                Password = "12345678", Role = "Administrator"
            },
            new UserModel() {
                Username = "user2",
                Password = "132456", Role = "User"
            }
        };
    }
}
