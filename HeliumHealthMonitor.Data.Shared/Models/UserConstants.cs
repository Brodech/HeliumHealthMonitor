namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserConstants
{
    public static List<UserModel> Users = new List<UserModel>()
    {
        new UserModel()
        {
            Id = "1",
            Username = "user1",
            Password = "12345678",
            Role = "Admin"
        },
        new UserModel()
        {
            Id = "2",
            Username = "user2",
            Password = "123456",
            Role = "User"
        }
    };
}
