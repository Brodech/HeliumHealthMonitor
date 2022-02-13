namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserConstants
{
    public static List<UserModel> Users = new List<UserModel>()
    {
        new UserModel()
        {
            Id = 1,
            UserName = "user1",
            Password = "12345678",
            Role = "Admin"
        },
        new UserModel()
        {
            Id = 2,
            UserName = "user2",
            Password = "123456",
            Role = "User"
        }
    };
}
