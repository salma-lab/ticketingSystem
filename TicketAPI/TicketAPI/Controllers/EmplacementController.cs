using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
    [Route("api/EmplacementController")]
    [ApiController]
    public class EmplacementController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration; // Add IConfiguration


        public EmplacementController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // Assign it


        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Emplacement>>> GetEmplacement()
        {
            return await _context.Emplacements.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]

        public async Task<ActionResult<TypeAppareil>> PostEmplacement(Emplacement emplacement)
        {
            // Vérifie que le modèle est valide
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ajoute le statut à la base de données
            _context.Emplacements.Add(emplacement);

            // Sauvegarde les changements dans la base de données
            await _context.SaveChangesAsync();

            // Renvoie le statut ajouté avec un code de statut 201 Created
            return CreatedAtAction(nameof(GetEmplacement), new { id = emplacement.EmplacementId }, emplacement);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteEmplacement(int id)
        {
            // Find the status by ID
            var emplacement = await _context.Emplacements.FindAsync(id);

            if (emplacement == null)
            {
                // Return a 404 if the status is not found
                return NotFound();
            }

            // Remove the status from the database
            _context.Emplacements.Remove(emplacement);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response (indicating successful deletion)
            return NoContent();
        }
    }
}
