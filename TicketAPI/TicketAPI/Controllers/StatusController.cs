using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
   

    [Route("api/StatusController")]
    [ApiController]
    [Authorize] 

    public class StatusController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration; // Add IConfiguration


        public StatusController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // Assign it


        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")] 

        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")] 

        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            // Vérifie que le modèle est valide
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ajoute le statut à la base de données
            _context.Status.Add(status);

            // Sauvegarde les changements dans la base de données
            await _context.SaveChangesAsync();

            // Renvoie le statut ajouté avec un code de statut 201 Created
            return CreatedAtAction(nameof(GetStatus), new { id = status.StatusId }, status);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            // Find the status by ID
            var status = await _context.Status.FindAsync(id);

            if (status == null)
            {
                // Return a 404 if the status is not found
                return NotFound();
            }

            // Remove the status from the database
            _context.Status.Remove(status);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response (indicating successful deletion)
            return NoContent();
        }

    }



}
