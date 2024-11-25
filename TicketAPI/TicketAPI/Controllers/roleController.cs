using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketAPI.Data;
using TicketAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TicketAPI.Controllers
{
    [Route("api/RoleController")]
    [ApiController]
    //[Authorize]
    public class roleController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration;

        public roleController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Get all role
        [HttpGet]
        //[Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // Add a new role
        [HttpPost]
        //[Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the new roleto the database
            _context.Roles.Add(role);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the created role with a 201 status code
            return CreatedAtAction(nameof(GetRoles), new { id = role.RoleId }, role);
        }

        // Delete a role by its ID
        [HttpDelete("{id}")]
        //[Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            // Find the role by ID
            var role = await _context.Roles.FindAsync(id);

            // If not found, return a 404 Not Found
            if (role == null)
            {
                return NotFound();
            }

            // Remove the role from the database
            _context.Roles.Remove(role);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 204 No Content response, indicating successful deletion
            return NoContent();
        }
    }
}

