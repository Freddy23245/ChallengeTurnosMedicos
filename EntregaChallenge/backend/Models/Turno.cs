using System.ComponentModel.DataAnnotations;

namespace TurnosMedicos.Models;

public class Turno
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El paciente es obligatorio.")]
    public int? PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
    [Required(ErrorMessage = "El medico es obligatorio.")]
    public int MedicoId { get; set; }
    public Medico? Medico { get; set; }
    [Required(ErrorMessage = "El fecha y hora es obligatoria.")]
    public DateTime FechaHora { get; set; }
    public EstadoTurno Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Motivo { get; set; } = string.Empty;
}
