using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Controllers
{
    [Route("api/CommentairesController")]
    [ApiController]
    [Authorize] // Require authentication for all actions in this controller

    public class CommentairesController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;

        public CommentairesController(TicketingSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/commentaires
        [HttpGet]
        [Authorize] 

        public async Task<ActionResult<IEnumerable<CommentaireResponseDto>>> GetCommentaires()
        {
            var commentaires = await _context.Commentaires
                .Include(c => c.Utilisateur) // Include the Utilisateur to get the name
                .ToListAsync();

            // Map to the response DTO
            var response = commentaires.Select(c => new CommentaireResponseDto
            {
                CommentaireId = c.CommentaireId,
                TicketId = c.TicketId,
                Contenu = c.Contenu,
                DateCommentaire = c.DateCommentaire,
                Nom = c.Utilisateur.Nom,
                Prenom = c.Utilisateur.Prenom
            }).ToList();

            return response;
        }

        // GET: api/commentaires/5
        [HttpGet("{id}")]
        [Authorize] 

        public async Task<ActionResult<CommentaireDto>> GetCommentaire(int id)
        {
            var commentaire = await _context.Commentaires
                .Include(c => c.Utilisateur) // Include Utilisateur to get the name
                .FirstOrDefaultAsync(c => c.CommentaireId == id);

            if (commentaire == null)
            {
                return NotFound();
            }

            return new CommentaireDto
            {
                CommentaireId = commentaire.CommentaireId,
                TicketId = commentaire.TicketId,
                Contenu = commentaire.Contenu,
                DateCommentaire = commentaire.DateCommentaire,
                UtilisateurId = commentaire.UtilisateurId,
                Nom = commentaire.Utilisateur.Nom,
                Prenom = commentaire.Utilisateur.Prenom
            };
        }

        // POST: api/commentaires
        // POST: api/commentaires
        [HttpPost]
        [Authorize] // Ensure that only authenticated users can add comments
        public async Task<ActionResult<Commentaire>> PostCommentaire(CommentaireDto commentaireDto)
        {
            // Check if the ticket exists
            var ticketExists = await _context.Tickets.AnyAsync(t => t.TicketId == commentaireDto.TicketId);
            if (!ticketExists)
            {
                return BadRequest("Le Ticket spécifié n'existe pas.");
            }

            // Retrieve the authenticated user's ID from the claims (assuming it's stored as 'NameIdentifier')
            var utilisateurId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Assuming the user ID is stored as a claim

            // Create the comment based on the DTO
            var commentaire = new Commentaire
            {
                TicketId = commentaireDto.TicketId,
                Contenu = commentaireDto.Contenu,
                DateCommentaire = DateTime.Now, // Use the current date and time for the comment
                UtilisateurId = utilisateurId // Set the authenticated user's ID
            };

            // Add the comment to the database
            _context.Commentaires.Add(commentaire);
            await _context.SaveChangesAsync();

            // Return the created comment along with a reference to its endpoint
            return CreatedAtAction(nameof(GetCommentaire), new { id = commentaire.CommentaireId }, commentaire);
        }

        [HttpPut("{id}")]
        [Authorize] 

        public async Task<IActionResult> PutCommentaire(int id, Commentaire commentaire)
        {
            if (id != commentaire.CommentaireId)
            {
                return BadRequest();
            }

            _context.Entry(commentaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentaireExists(id))
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

        // DELETE: api/commentaires/5
        [HttpDelete("{id}")]
        [Authorize] 

        public async Task<IActionResult> DeleteCommentaire(int id)
        {
            var commentaire = await _context.Commentaires.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }

            _context.Commentaires.Remove(commentaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/CommentairesController/ticket/{ticketId}
        [HttpGet("ticket/{ticketId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CommentaireResponseDto>>> GetCommentsByTicket(int ticketId)
        {
            var commentaires = await _context.Commentaires
                .Where(c => c.TicketId == ticketId)
                .Include(c => c.Utilisateur) // Include related user info
                .ToListAsync();

            var response = commentaires.Select(c => new CommentaireResponseDto
            {
                CommentaireId = c.CommentaireId,
                TicketId = c.TicketId,
                Contenu = c.Contenu,
                DateCommentaire = c.DateCommentaire,
                Nom = c.Utilisateur.Nom,
                Prenom = c.Utilisateur.Prenom
            }).ToList();

            return Ok(response);
        }




        private bool CommentaireExists(int id)
        {
            return _context.Commentaires.Any(e => e.CommentaireId == id);
        }
    }
}
