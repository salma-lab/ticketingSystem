using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;
using Microsoft.AspNetCore.Authorization;



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
            return await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.TypeIntervention)
                .Include(t => t.Commentaires)
                .Include(t => t.Utilisateur) // Include the Utilisateur entity
                .Select(t => new TicketDTO
                {
                    TicketId = t.TicketId,
                    Description = t.Description,
                    AppareilNom = t.AppareilNom,
                    Etage = t.Etage,
                    Emplacement = t.Emplacement,
                    MotifDemande = t.MotifDemande,
                    Oralement = t.Oralement,
                    NomType = t.TypeIntervention.NomType,
                    NomStatus = t.Status.NomStatus,
                    Email = t.Utilisateur.Email // Map the email property
                })
                .ToListAsync();
        }
















        // GET: api/ticket/5
        [HttpGet("{id}")]
        [Authorize] 

        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Status)
                .Include(t => t.TypeIntervention)
                .Include(t => t.Commentaires)
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

            var ticket = new Ticket
            {
                Description = createTicketDto.Description,
                StatusId = createTicketDto.StatusId,
                Oralement=createTicketDto.Oralement,
                Etage=createTicketDto.Etage,
                Emplacement=createTicketDto.Emplacement,
                MotifDemande=createTicketDto.MotifDemande,
                AppareilNom=createTicketDto.AppareilNom,
                TypeInterventionId = createTicketDto.TypeInterventionId,
                UtilisateurId = createTicketDto.UtilisateurId 

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
    }
}