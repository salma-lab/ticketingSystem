using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
    public class My_tickets : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration;



        public My_tickets(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        // GET: My_tickets
        [HttpGet("MyTickets")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetMyTickets()
        {
            // Retrieve the UtilisateurId from the JWT token
            var utilisateurIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Adjust claim type if needed
            if (utilisateurIdClaim == null)
            {
                return Unauthorized("User ID is missing from the token.");
            }

            // Convert the claim value to an integer
            if (!int.TryParse(utilisateurIdClaim.Value, out int utilisateurId))
            {
                return BadRequest("Invalid user ID in the token.");
            }

            // Fetch tickets associated with the authenticated user
            var userTickets = await _context.Tickets
                .Where(t => t.UtilisateurId == utilisateurId)
                .Include(t => t.Status) // Include related entities if needed
                .Include(t => t.TypeIntervention)
                .ToListAsync();

            if (!userTickets.Any())
            {
                return NotFound("No tickets found for the logged-in user.");
            }

            return Ok(userTickets);
        }


        // GET: My_tickets/Details/5


        // GET: My_tickets/Create


        // POST: My_tickets/Create




        // POST: My_tickets/Edit/5


        // GET: My_tickets/Delete/5
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }

        // POST: My_tickets/Delete/5

    }
}
