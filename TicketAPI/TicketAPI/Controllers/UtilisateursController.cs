using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketAPI.Data;
using TicketAPI.Models;
using TicketAPI.Services;

namespace TicketAPI.Controllers
{
    [Route("api/UtilisateursController")]
    [ApiController]
    [Authorize] 
    public class UtilisateursController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly PasswordService _passwordService;
        private readonly IConfiguration _configuration; 


        public UtilisateursController(TicketingSystemDbContext context, PasswordService passwordService, IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration; 

        }

        // GET: api/utilisateurs
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")] 
        public async Task<ActionResult<IEnumerable<UtilisateurDTO>>> GetUtilisateurs()
        {
            try
            {
                var utilisateurs = await _context.Utilisateurs
                    .Include(u => u.Role)
                    .Include(u => u.Tickets)
                    .Select(u => new UtilisateurDTO
                    {
                        UtilisateurId = u.UtilisateurId,
                        Nom = u.Nom,
                        Prenom = u.Prenom,
                        Email = u.Email,
                        RoleName = u.Role.RoleName,
                        Tickets = u.Tickets.Select(t => new TicketDTO
                        {
                            TicketId = t.TicketId,
                            Description = t.Description,
                            DateCreation = t.DateCreation,
                            UtilisateurId = t.UtilisateurId
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(utilisateurs);
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // POST: api/utilisateurs
        [HttpPost]
       [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<UtilisateurDTO>> CreateUtilisateur(CreateUtilisateurDTO utilisateurDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the Role exists by RoleId
            var role = await _context.Roles.FindAsync(utilisateurDto.RoleId);
            if (role == null)
            {
                return BadRequest("Invalid RoleId. Please provide a valid role.");
            }

            // Check if the email is unique
            if (await _context.Utilisateurs.AnyAsync(u => u.Email == utilisateurDto.Email))
            {
                return BadRequest("Email is already in use.");
            }

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(utilisateurDto.Password);

            // Create the new Utilisateur object
            var utilisateur = new Utilisateur
            {
                Nom = utilisateurDto.Nom,
                Prenom = utilisateurDto.Prenom,
                Email = utilisateurDto.Email,
                PasswordHash = hashedPassword, 
                RoleId = utilisateurDto.RoleId
            };

            try
            {
                _context.Utilisateurs.Add(utilisateur);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database update error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            // Prepare the created Utilisateur for the response, including the RoleName
            var createdUtilisateur = new UtilisateurDTO
            {
                UtilisateurId = utilisateur.UtilisateurId,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Email = utilisateur.Email,
                RoleName = role.RoleName,
                Tickets = new List<TicketDTO>()
            };

            return CreatedAtAction(nameof(GetUtilisateurs), new { id = createdUtilisateur.UtilisateurId }, createdUtilisateur);
        }

        // GET: api/utilisateurs/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "RequireAdminRole")] 
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs
                .Include(u => u.Role) 
                .Include(u => u.Tickets) 
                .FirstOrDefaultAsync(u => u.UtilisateurId == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            var utilisateurDto = new UtilisateurDTO
            {
                UtilisateurId = utilisateur.UtilisateurId,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Email = utilisateur.Email,
                RoleName = utilisateur.Role.RoleName, // Display RoleName
                Tickets = utilisateur.Tickets.Select(t => new TicketDTO
                {
                    TicketId = t.TicketId,
                    Description = t.Description,
                    DateCreation = t.DateCreation,
                    UtilisateurId = t.UtilisateurId
                }).ToList()
            };

            return Ok(utilisateurDto);
        }

        // PUT: api/utilisateurs/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")] // Only Admin can edit users
        public async Task<ActionResult<UtilisateurDTO>> UpdateUtilisateur(int id, CreateUtilisateurDTO utilisateurDto)
        {
            if (id != utilisateurDto.UtilisateurId)
            {
                return BadRequest("User ID mismatch.");
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            // Update fields
            utilisateur.Nom = utilisateurDto.Nom;
            utilisateur.Prenom = utilisateurDto.Prenom;
            utilisateur.Email = utilisateurDto.Email;

            // Optionally, update the password if provided
            if (!string.IsNullOrEmpty(utilisateurDto.Password))
            {
                utilisateur.PasswordHash = _passwordService.HashPassword(utilisateurDto.Password);
            }

            utilisateur.RoleId = utilisateurDto.RoleId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(utilisateur); // Return the updated utilisateur for confirmation
        }

        // DELETE: api/utilisateurs/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")] // Only Admin can delete users
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent(); // Consider returning a message if needed
        }

        [HttpGet("{id}/tickets")]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetTicketsForUtilisateur(int id)
        {
            // Fetch the utilisateur and include related Tickets, Status, and TypeIntervention
            var utilisateur = await _context.Utilisateurs
                .Include(u => u.Tickets)
                    .ThenInclude(t => t.Status)
                .Include(u => u.Tickets)
                    .ThenInclude(t => t.TypeIntervention)
                .FirstOrDefaultAsync(u => u.UtilisateurId == id);

            // If the user is not found, return 404
            if (utilisateur == null)
            {
                return NotFound("Utilisateur not found");
            }

            // Map the tickets to a DTO
            var tickets = utilisateur.Tickets.Select(t => new TicketDTO
            {
                TicketId = t.TicketId,
                Description = t.Description,
                DateCreation = t.DateCreation,
                NomStatus = t.Status.NomStatus,
                NomType = t.TypeIntervention.NomType,
                AppareilNom = t.AppareilNom,
                Etage = t.Etage,
                Emplacement = t.Emplacement,
                MotifDemande = t.MotifDemande,
                Oralement = t.Oralement,
                UtilisateurId = t.UtilisateurId
            }).ToList();

            // Return the list of tickets
            return Ok(tickets);
        }


        // Method to get profile information for a specific utilisateur - accessible to all authenticated users
        [HttpGet("{id}/profile")]
        public async Task<ActionResult<UtilisateurDTO>> GetUtilisateurProfile(int id)
        {
            var utilisateur = await _context.Utilisateurs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UtilisateurId == id);

            if (utilisateur == null)
            {
                return NotFound("Utilisateur not found");
            }

            var utilisateurDto = new UtilisateurDTO
            {
                UtilisateurId = utilisateur.UtilisateurId,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                Email = utilisateur.Email,
                RoleName = utilisateur.Role.RoleName
            };

            return Ok(utilisateurDto);
        }





        private bool UtilisateurExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        }
    }
}
