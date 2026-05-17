using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Data;

namespace TurnosMedicos.Controllers;

[ApiController]
[Route("[controller]")]
public class SucursalesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SucursalesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sucursales = await _context.Sucursales.ToListAsync();
        return Ok(sucursales);
    }
}
