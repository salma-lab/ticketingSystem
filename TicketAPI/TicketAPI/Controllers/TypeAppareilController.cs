using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
    [Route("api/TypeAppareilController")]
    [ApiController]
    public class TypeAppareilController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration; // Add IConfiguration


        public TypeAppareilController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // Assign it


        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<TypeAppareil>>> GetTypeAppareil()
        {
            return await _context.TypeAppareils.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]

        public async Task<ActionResult<TypeAppareil>> PostTypeAppareil(TypeAppareil typeAppareil)
        {
            // Vérifie que le modèle est valide
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ajoute le statut à la base de données
            _context.TypeAppareils.Add(typeAppareil);

            // Sauvegarde les changements dans la base de données
            await _context.SaveChangesAsync();

            // Renvoie le statut ajouté avec un code de statut 201 Created
            return CreatedAtAction(nameof(GetTypeAppareil), new { id = typeAppareil.TypeAppareilId }, typeAppareil);
        }




        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> PutType (int id, TypeAppareil typeAppareil)
        {
            var oldappareil = await _context.TypeAppareils.FindAsync(id);
            if (oldappareil == null)
            {
                return NotFound();
            }
            oldappareil.NomTypeAppareil = typeAppareil.NomTypeAppareil;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) 
            { return StatusCode(StatusCodes.Status500InternalServerError, "error updating TypeAppareil"); }
            return NoContent();

        }




        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteAppareil(int id)
        {
            // Find the status by ID
            var typeAppareil = await _context.TypeAppareils.FindAsync(id);

            if (typeAppareil== null)
            {
                // Return a 404 if the status is not found
                return NotFound();
            }

            // Remove the status from the database
            _context.TypeAppareils.Remove(typeAppareil);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response (indicating successful deletion)
            return NoContent();
        }
    }
}
