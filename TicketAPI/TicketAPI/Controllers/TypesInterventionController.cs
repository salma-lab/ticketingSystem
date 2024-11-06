using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;
using Microsoft.AspNetCore.Authorization;



namespace TicketAPI.Controllers
{
    

    [Route("api/TypesInterventionController")]
    [ApiController]
    [Authorize] // Require authentication for all actions in this controller

    public class TypesInterventionController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration; // Add IConfiguration


        public TypesInterventionController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // Assign it

        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")] // Only Admin can access this

        public async Task<ActionResult<IEnumerable<TypeIntervention>>> GetTypesIntervention()
        {
            return await _context.TypesIntervention.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")] // Only Admin can access this

        public async Task<ActionResult<TypeIntervention>> PostTypeIntervention(TypeIntervention typeIntervention)
        {
            // Vérifie que le modèle est valide
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ajoute le type d'intervention à la base de données
            _context.TypesIntervention.Add(typeIntervention);

            // Sauvegarde les changements dans la base de données
            await _context.SaveChangesAsync();

            // Renvoie le type d'intervention ajouté avec un code de statut 201 Created
            return CreatedAtAction(nameof(GetTypesIntervention), new { id = typeIntervention.TypeInterventionId }, typeIntervention);
        }

    }

}
