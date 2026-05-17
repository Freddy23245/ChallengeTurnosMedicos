using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Data;

namespace TurnosMedicos.Controllers;

[ApiController]
[Route("[controller]")]
public class MedicosController : ControllerBase
{
    private readonly AppDbContext _context;

    public MedicosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var medicos = await _context.Medicos.Include(m => m.Sucursal).ToListAsync();
        return Ok(medicos);
    }
}
