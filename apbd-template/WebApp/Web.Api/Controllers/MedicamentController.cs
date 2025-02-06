using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Context;
using WebApp.Models;

namespace Web.Api.Controllers;

[Route("api/medicaments")]
[ApiController]
public class MedicamentController : ControllerBase
{
    private readonly WebAppDbContext _context;

    public MedicamentController(WebAppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medicament>>> GetMedicaments()
    {
        return await _context.Medicaments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Medicament>> GetMedicament(int id)
    {
        var medicament = await _context.Medicaments.FindAsync(id);
        if (medicament == null) return NotFound();
        return medicament;
    }

    [HttpPost]
    public async Task<ActionResult<Medicament>> PostMedicament(Medicament medicament)
    {
        _context.Medicaments.Add(medicament);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMedicament), new { id = medicament.IdMedicament }, medicament);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedicament(int id, Medicament medicament)
    {
        if (id != medicament.IdMedicament) return BadRequest();

        _context.Entry(medicament).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicament(int id)
    {
        var medicament = await _context.Medicaments.FindAsync(id);
        if (medicament == null) return NotFound();

        _context.Medicaments.Remove(medicament);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
