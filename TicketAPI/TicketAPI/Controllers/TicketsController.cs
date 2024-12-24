using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace TicketAPI.Controllers
{
    [Route("api/TicketsController")]
    [ApiController]
    [Authorize] 

    public class TicketController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration; 


        public TicketController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 

        }

        // GET: api/ticket
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetTickets()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.TypeAppareil)
                .Include(t => t.Etage)
                .Include(t => t.Emplacement)
                .Include(t => t.TypeIntervention)
                .Include(t => t.Utilisateur) // Include the Utilisateur entity
                .Select(t => new TicketDTO
                {
                    TicketId = t.TicketId,
                    Description = t.Description,
                    AppareilNom = t.AppareilNom,
                    MotifDemande = t.MotifDemande,
                    Oralement = t.Oralement,
                    Validation1 = t.Validation1,
                    Validation2 = t.Validation2,
                    NomEtage = t.Etage.NomEtage,
                    NomEmplacement = t.Emplacement.NomEmplacement,
                    NomType = t.TypeIntervention.NomType,
                    NomStatus = t.Status.NomStatus,
                    NomTypeAppareil = t.TypeAppareil.NomTypeAppareil,
                    Email = t.Utilisateur.Email, // Map the email property
                    DateCreation = t.DateCreation == DateTime.MinValue ? DateTime.Now : t.DateCreation // Handle DateCreation formatting
                })
                .ToListAsync();

            return tickets;
        }
















        // GET: api/ticket/5
        [HttpGet("{id}")]
        [Authorize] 

        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.TypeIntervention)
                .Include(t=>t.TypeAppareil)
                .Include(t => t.Utilisateur)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // POST: api/ticket
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ticket>> CreateTicket(CreateTicketDto createTicketDto)
        {
            if (createTicketDto == null)
            {
                return BadRequest("Le ticket est requis.");
            }

            // Retrieve the UtilisateurId from the JWT token
            var utilisateurIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // This will get the user ID from the JWT token
            if (utilisateurIdClaim == null)
            {
                return Unauthorized("User ID is missing from the token.");
            }

            // Convert the claim value to an integer (assuming UtilisateurId is an integer)
            int utilisateurId = int.Parse(utilisateurIdClaim.Value);

            // Create the new Ticket object and associate it with the authenticated user
            var ticket = new Ticket
            {
                Description = createTicketDto.Description,
                StatusId = createTicketDto.StatusId,
                TypeAppareilId= createTicketDto.TypeAppareilId,
                DateCreation = createTicketDto.DateCreation, // Use the provided date
                Oralement = createTicketDto.Oralement,
                EtageId = createTicketDto.EtageId,
                EmplacementId = createTicketDto.EmplacementId,
                MotifDemande = createTicketDto.MotifDemande,
                AppareilNom = createTicketDto.AppareilNom,
                TypeInterventionId = createTicketDto.TypeInterventionId,
                UtilisateurId = utilisateurId // Set the UtilisateurId from the JWT token
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
        }




        // PUT: api/ticket/5
        [HttpPut("{id}")]
        [Authorize] 

        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ticket/5
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
                .Include(t =>t.TypeAppareil)
                .Include(t => t.TypeIntervention)
                .ToListAsync();

            if (!userTickets.Any())
            {
                return NotFound("No tickets found for the logged-in user.");
            }

            return Ok(userTickets);
        }




    }
}