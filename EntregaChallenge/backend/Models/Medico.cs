using System.ComponentModel.DataAnnotations;

namespace TurnosMedicos.Models;

public class Medico
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre completo de medico es obligatorio.")]
    public string NombreCompleto { get; set; } = string.Empty;
    [Required(ErrorMessage = "La especialidad es obligatoria.")]
    public string Especialidad { get; set; } = string.Empty;
    [Required(ErrorMessage = "La sucursal es obligatoria.")]
    public int SucursalId { get; set; }
    public Sucursal? Sucursal { get; set; }
}
