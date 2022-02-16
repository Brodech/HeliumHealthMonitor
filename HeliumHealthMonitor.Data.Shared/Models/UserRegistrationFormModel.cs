using System.ComponentModel.DataAnnotations;

namespace HeliumHealthMonitor.Data.Shared.Models;

public class UserRegistrationFormModel
{
    [Required]
    [StringLength(24, ErrorMessage = "Der Benutzername darf nicht mehr als 24 Zeichen enthalten.")]
    public string Username { get; set; }

    [Required]
    [StringLength(32, ErrorMessage = "Das Passwort muss zwischen 8 und 32 Zeichen lang sein.", MinimumLength = 12)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwörter stimmen nicht überein.")]
    public string Password2 { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Otp { get; set; }
}
