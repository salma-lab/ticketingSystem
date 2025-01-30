using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
    [Route("api/EtageController")]
    [ApiController]
    public class EtageController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration; // Add IConfiguration


        public EtageController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // Assign it


        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Etage>>> GetEtage()
        {
            return await _context.Etages.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]

        public async Task<ActionResult<Etage>> PostEmplacement(Etage etage)
        {
            // Vérifie que le modèle est valide
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ajoute le statut à la base de données
            _context.Etages.Add(etage);

            // Sauvegarde les changements dans la base de données
            await _context.SaveChangesAsync();

            // Renvoie le statut ajouté avec un code de statut 201 Created
            return CreatedAtAction(nameof(GetEtage), new { id = etage.EtageId }, etage);
        }




        [HttpPut("{id}")]
        [Authorize(Policy ="RequireAdminRoole")]
        public async Task<IActionResult> PutEtage(int id, Etage etage)
        {
            var oldEtage = await _context.Etages.FindAsync(id);
            if (oldEtage == null)
            {
                return NotFound();
            }
            oldEtage.NomEtage = etage.NomEtage;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }




        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteEtage(int id)
        {
            // Find the status by ID
            var etage = await _context.Etages.FindAsync(id);

            if (etage == null)
            {
                // Return a 404 if the status is not found
                return NotFound();
            }

            // Remove the status from the database
            _context.Etages.Remove(etage);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response (indicating successful deletion)
            return NoContent();
        }
    }
}
