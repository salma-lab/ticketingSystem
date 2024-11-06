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
    [Authorize] // Require authentication for all actions in this controller

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
        [Authorize(Policy = "RequireAdminRole")] // Only Admin can access this

        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")] // Only Admin can access this

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

    }

}
