using System.ComponentModel.DataAnnotations;

namespace TurnosMedicos.Models;

public class Paciente
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string NombreCompleto { get; set; } = string.Empty;
    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"^\d{8}$",ErrorMessage = "El DNI debe tener exactamente 8 números.")]
    [StringLength(8)]
    public string DNI { get; set; } = string.Empty;
    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "Teléfono inválido.")]
    //[RegularExpression(@"^\d+$", ErrorMessage = "El telefono solo debe contener números.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El telefono debe tener exactamente 10 números.")]
    [StringLength(10)]
    public string Telefono { get; set; } = string.Empty;
    public int NoShowCount { get; set; }
    public bool Bloqueado { get; set; }
    public DateTime? FechaBloqueo { get; set; }
    public DateTime createdAt { get; set; }
    public bool isActive { get; set; }
}
