using System.ComponentModel.DataAnnotations;

namespace HeliumHealthMonitor.Data.Shared.Models;

public class DeviceRegistrationFormModel
{
    [Required]
    [StringLength(24, ErrorMessage = "Der Benutzername darf nicht mehr als 24 Zeichen enthalten.")]
    public string Username { get; set; }

    [Required]
    [StringLength(128, ErrorMessage = "Das Passwort muss zwischen 8 und 128 Zeichen lang sein.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwörter stimmen nicht überein.")]
    public string Password2 { get; set; }
}
