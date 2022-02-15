using System.ComponentModel.DataAnnotations;

namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserLoginFormModel
{
    [Required(ErrorMessage = "Der Benutzername darf nicht leer sein.")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Das Passwort darf nicht leer sein.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;
}
