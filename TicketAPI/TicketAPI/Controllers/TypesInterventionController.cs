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
    [Authorize]
    public class TypesInterventionController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration;

        public TypesInterventionController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Get all types of intervention
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<TypeIntervention>>> GetTypesIntervention()
        {
            return await _context.TypesIntervention.ToListAsync();
        }

        // Add a new type of intervention
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<TypeIntervention>> PostTypeIntervention(TypeIntervention typeIntervention)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new intervention type to the database
            _context.TypesIntervention.Add(typeIntervention);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the created intervention type with a 201 status code
            return CreatedAtAction(nameof(GetTypesIntervention), new { id = typeIntervention.TypeInterventionId }, typeIntervention);
        }

        // Delete a type of intervention by its ID
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteTypeIntervention(int id)
        {
            // Find the intervention type by ID
            var typeIntervention = await _context.TypesIntervention.FindAsync(id);

            // If not found, return a 404 Not Found
            if (typeIntervention == null)
            {
                return NotFound();
            }

            // Remove the intervention type from the database
            _context.TypesIntervention.Remove(typeIntervention);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response, indicating successful deletion
            return NoContent();
        }
    }
}

