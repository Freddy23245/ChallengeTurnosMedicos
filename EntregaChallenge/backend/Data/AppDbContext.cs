using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Models;

namespace TurnosMedicos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Turno> Turnos { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Turno>()
            .Property(t => t.Estado)
            .HasConversion<string>();

        modelBuilder.Entity<Turno>()
            .HasOne(t => t.Paciente)
            .WithMany()
            .HasForeignKey(t => t.PacienteId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Sucursal>().HasData(
            new Sucursal { Id = 1, Nombre = "Palermo",   Direccion = "Av. Santa Fe 3500, CABA"  },
            new Sucursal { Id = 2, Nombre = "Belgrano",  Direccion = "Av. Cabildo 1200, CABA"   },
            new Sucursal { Id = 3, Nombre = "San Telmo", Direccion = "Defensa 850, CABA"        }
        );

        modelBuilder.Entity<Medico>().HasData(
            new Medico { Id = 1, NombreCompleto = "Dr. Alejandro Ríos",      Especialidad = "Clínica Médica",  SucursalId = 1 },
            new Medico { Id = 2, NombreCompleto = "Dra. Valentina Mora",     Especialidad = "Pediatría",       SucursalId = 1 },
            new Medico { Id = 3, NombreCompleto = "Dr. Carlos Benítez",      Especialidad = "Cardiología",     SucursalId = 2 },
            new Medico { Id = 4, NombreCompleto = "Dra. Lucía Ferraro",      Especialidad = "Dermatología",    SucursalId = 2 },
            new Medico { Id = 5, NombreCompleto = "Dr. Martín Gutiérrez",    Especialidad = "Traumatología",   SucursalId = 3 }
        );

        modelBuilder.Entity<Paciente>().HasData(
            new Paciente { Id = 1, NombreCompleto = "Gonzalo Herrera",   DNI = "28445123", Email = "gonzalo.herrera@gmail.com",    Telefono = "1145678901", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 1, 4),      isActive = true },
            new Paciente { Id = 2, NombreCompleto = "Mariana López",     DNI = "31202456", Email = "mariana.lopez@hotmail.com",    Telefono = "1167891234", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 1, 24),     isActive = true },
            new Paciente { Id = 3, NombreCompleto = "Roberto Quintana",  DNI = "25789012", Email = "roberto.q@gmail.com",          Telefono = "1156789012", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 2, 23),     isActive = true },
            new Paciente { Id = 4, NombreCompleto = "Sofía Vargas",      DNI = "35123789", Email = "sofia.vargas@yahoo.com",       Telefono = "1178901234", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 3, 10),     isActive = true },
            new Paciente { Id = 5, NombreCompleto = "Diego Fernández",   DNI = "29876543", Email = "diegofernandez@gmail.com",     Telefono = "1134567890", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 3, 25),     isActive = true },
            new Paciente { Id = 6, NombreCompleto = "Laura Giménez",     DNI = "33456789", Email = "lauragimenez@gmail.com",       Telefono = "1189012345", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 4, 4),      isActive = true },
            new Paciente { Id = 7, NombreCompleto = "Facundo Sosa",      DNI = "27345678", Email = "facundo.sosa@outlook.com",     Telefono = "1112345678", NoShowCount = 0, Bloqueado = true,  FechaBloqueo = new DateTime(2026, 4, 19), createdAt = new DateTime(2025, 10, 27), isActive = true },
            new Paciente { Id = 8, NombreCompleto = "Carolina Méndez",   DNI = "36901234", Email = "carolina.mendez@gmail.com",    Telefono = "1190123456", NoShowCount = 0, Bloqueado = false, FechaBloqueo = null,                     createdAt = new DateTime(2026, 4, 14),     isActive = true }
        );

        modelBuilder.Entity<Turno>().HasData(
            new Turno { Id =  1, PacienteId = 1, MedicoId = 1, FechaHora = new DateTime(2026, 5,  5,  9, 0, 0), Estado = EstadoTurno.Pendiente,  FechaCreacion = new DateTime(2026, 4, 25), Motivo = "Control anual"               },
            new Turno { Id =  2, PacienteId = 2, MedicoId = 3, FechaHora = new DateTime(2026, 4, 30, 11, 0, 0), Estado = EstadoTurno.Confirmado, FechaCreacion = new DateTime(2026, 4, 23), Motivo = "Revisión cardiológica"        },
            new Turno { Id =  3, PacienteId = 2, MedicoId = 2, FechaHora = new DateTime(2026, 3, 14, 10, 0, 0), Estado = EstadoTurno.NoShow,     FechaCreacion = new DateTime(2026, 3,  9), Motivo = "Pediatría seguimiento"        },
            new Turno { Id =  4, PacienteId = 2, MedicoId = 1, FechaHora = new DateTime(2026, 2, 18,  9, 0, 0), Estado = EstadoTurno.NoShow,     FechaCreacion = new DateTime(2026, 2, 13), Motivo = "Consulta general"             },
            new Turno { Id =  5, PacienteId = 3, MedicoId = 4, FechaHora = new DateTime(2026, 5, 12, 15, 0, 0), Estado = EstadoTurno.Pendiente,  FechaCreacion = new DateTime(2026, 4, 26), Motivo = "Revisión de piel"             },
            new Turno { Id =  6, PacienteId = 4, MedicoId = 5, FechaHora = new DateTime(2026, 5, 20, 14, 0, 0), Estado = EstadoTurno.Confirmado, FechaCreacion = new DateTime(2026, 4, 20), Motivo = "Dolor en rodilla derecha"     },
            new Turno { Id =  7, PacienteId = 5, MedicoId = 1, FechaHora = new DateTime(2026, 4, 10, 10, 0, 0), Estado = EstadoTurno.Cancelado,  FechaCreacion = new DateTime(2026, 4,  5), Motivo = "Chequeo general"              },
            new Turno { Id =  8, PacienteId = 6, MedicoId = 2, FechaHora = new DateTime(2026, 5, 15,  9, 0, 0), Estado = EstadoTurno.Pendiente,  FechaCreacion = new DateTime(2026, 4, 27), Motivo = "Control pediátrico"           },
            new Turno { Id =  9, PacienteId = 1, MedicoId = 3, FechaHora = new DateTime(2026, 4,  4, 11, 0, 0), Estado = EstadoTurno.Atendido,   FechaCreacion = new DateTime(2026, 3, 30), Motivo = "Electrocardiograma de rutina" },
            new Turno { Id = 10, PacienteId = 8, MedicoId = 4, FechaHora = new DateTime(2026, 5,  3, 16, 0, 0), Estado = EstadoTurno.Pendiente,  FechaCreacion = new DateTime(2026, 4, 26), Motivo = "Primera consulta"             }

        );
    }
}
