using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Data;
using TurnosMedicos.Helpers;
using TurnosMedicos.Models;

namespace TurnosMedicos.Controllers;

[ApiController]
[Route("[controller]")]
public class TurnosController : ControllerBase
{
    private readonly AppDbContext _context;

    public TurnosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var turnos = await _context.Turnos
            .Include(t => t.Paciente)
            .Include(t => t.Medico)
            .ToListAsync();
        return Ok(turnos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var turno = await _context.Turnos
            .Include(t => t.Paciente)
            .Include(t => t.Medico)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (turno == null) return NotFound();
        return Ok(turno);
    }

    [HttpPost]
    public async Task<IActionResult> CrearTurno([FromBody] Turno turno)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var paciente = await _context.Pacientes.FindAsync(turno.PacienteId);
        if (paciente == null)
            return NotFound(new { mensaje = "Paciente no encontrado." });
      
        //pregunto si esta bloqueado
        if (paciente.Bloqueado)
        {

            //aca valido si el paciente esta bloquedado con fecha de bloqueo y si la fecha de bloqueo supera los 30 dias
            //valido tambien si el paciente llega estar bloqueado sin tener fechabloqueo

            if (!paciente.FechaBloqueo.HasValue || (paciente.FechaBloqueo.HasValue && paciente.FechaBloqueo.Value.AddDays(30) > DateTime.Now))
            {
                return BadRequest(new { mensaje = "El paciente se encuentra bloqueado para agendar turnos online." });
            }
            paciente.Bloqueado = false;
            paciente.FechaBloqueo = null;
        }
            
        var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == turno.MedicoId);
        if (!medicoExiste)
            return NotFound(new { mensaje = "Médico no encontrado." });

        var turnoConflicto = await _context.Turnos.AnyAsync(t =>
            t.MedicoId == turno.MedicoId &&
            t.FechaHora == turno.FechaHora &&
            t.Estado != EstadoTurno.Cancelado);
        if (turnoConflicto)
            return BadRequest(new { mensaje = "El médico ya tiene un turno en ese horario." });

        turno.FechaCreacion = DateTime.UtcNow;
        turno.Estado = EstadoTurno.Pendiente;

        //se previene error de trackeo tambien si se agrega un usuario que no existe que lo cree
        //ya que si se envia la entidad medico o paciente con id 0 crea un nuevo
        turno.Paciente = null;
        turno.Medico = null;
        _context.Turnos.Add(turno);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = turno.Id }, turno);
    }

    [HttpGet("cancelar/{id}")]
    public async Task<IActionResult> CancelarTurno(int id)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null) return NotFound();

        //agrego esta validacion por que si el turno esta cancelado sigue contando los noshow
        if (turno.Estado == EstadoTurno.Cancelado)
           return BadRequest(new {mensaje="No se puede cancelar el turno por que ya esta cancelado" });
        else
        {
            if(turno.Estado == EstadoTurno.Atendido || turno.Estado == EstadoTurno.NoShow)
            {
               return BadRequest(new { mensaje = "El turno no se puede cancelar por que tiene estado noshow o atendido"});
            }

        }

        if (turno.FechaHora - DateTime.Now < TimeSpan.FromHours(23))
            return BadRequest(new { mensaje = "No se puede cancelar con menos de 24 horas de anticipación." });

        //busco al paciente por el turno
        var paciente = await _context.Pacientes.FindAsync(turno.PacienteId);
        //valido si no existe el paciente
        if (paciente == null) return NotFound();

        //le agrego la cantidad de noshow 
        paciente.NoShowCount += 1;

        //valido que si tiene 3 o mas noshow y que no este bloqueado haga el bloqueo del paciente
        if (paciente.NoShowCount >= 3 && !paciente.Bloqueado)
        {
            paciente.Bloqueado = true;
            paciente.NoShowCount = 0;
            paciente.FechaBloqueo = DateTime.Now;
        }
        
        turno.Estado = EstadoTurno.Cancelado;
        await _context.SaveChangesAsync();
        return Ok(turno);
    }

    [HttpPost("{id}/ausencia")]
    public async Task<IActionResult> MarcarAusencia(int id)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null) return NotFound();

        //se marco esta validacion ya que si el turno tenia ausencia el noshow seguia contando
        if (turno.Estado == EstadoTurno.NoShow)
            return BadRequest(new {mensaje= "El turno ya fue marcado con ausencia" });
        else
        {
            if (turno.Estado == EstadoTurno.Atendido || turno.Estado == EstadoTurno.Cancelado)
            {
               return BadRequest(new {mensaje= "El turno no se puede cancelar por que tiene estado Cancelado o atendido" });
            }
        }
            
        if (!turno.FechaHora.IsWithinCancellationWindow())
            return BadRequest(new { mensaje = "La ausencia solo puede registrarse dentro de las 24 horas del turno." });

        //busco al paciente por el turno
        var paciente = await _context.Pacientes.FindAsync(turno.PacienteId);
        //valido si no existe el paciente
        if (paciente == null) return NotFound();

        //le agrego la cantidad de noshow 
        paciente.NoShowCount += 1;

        //valido que si tiene 3 o mas noshow y que no este bloqueado haga el bloqueo del paciente
        if (paciente.NoShowCount >= 3 && !paciente.Bloqueado)
        {
            paciente.Bloqueado = true;
            paciente.NoShowCount = 0;
            paciente.FechaBloqueo = DateTime.Now;
        }
        turno.Estado = EstadoTurno.NoShow;
        turno.Paciente = null;
        await _context.SaveChangesAsync();
        return Ok(turno);
    }

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoRequest request)
    {
        var turno = await _context.Turnos.FindAsync(id);
        if (turno == null) return NotFound();

        turno.Estado = request.Estado;
        await _context.SaveChangesAsync();
        return Ok(turno);
    }
}

public class ActualizarEstadoRequest
{
    public EstadoTurno Estado { get; set; }
}
