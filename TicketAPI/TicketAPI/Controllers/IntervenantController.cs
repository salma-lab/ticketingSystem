using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
    [Route("api/IntervenantController")]
    [Authorize]
    public class IntervenantController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        public IntervenantController(TicketingSystemDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Intervenant>>> getIntervenant()
        {
            return await _context.Intervenants.ToListAsync();
        }
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<Intervenant>> SetIntervenant([FromBody] Intervenant intervenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Intervenants.Add(intervenant);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(getIntervenant), new { id = intervenant.IdIntervenant }, intervenant);
        }



        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EditIntervenant(int id, [FromBody] Intervenant intervenant)
        {
            // Check for ID mismatch
            if (id != intervenant.IdIntervenant)
            {
                return BadRequest("ID de l'intervenant non valide.");
            }

            // Validate NomIntervenant
            if (string.IsNullOrWhiteSpace(intervenant.NomIntervenant))
            {
                return BadRequest("Le nom de l'intervenant ne peut pas être vide.");
            }

            var oldIntervenant = await _context.Intervenants.FindAsync(id);
            if (oldIntervenant == null)
            {
                return NotFound();
            }

            oldIntervenant.NomIntervenant = intervenant.NomIntervenant;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Erreur lors de la mise à jour : {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task <IActionResult> DeleteIntervenant(int id)
        {
            var intervenant = await _context.Intervenants.FindAsync(id);
            if (intervenant == null)
            {
                return NotFound();
            }
            _context.Intervenants.Remove(intervenant);
                await _context.SaveChangesAsync();
            return NoContent();
        }
        

        


    } }
