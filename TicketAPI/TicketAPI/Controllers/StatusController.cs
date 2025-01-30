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
    public class StatusController : ControllerBase
    {
        private readonly TicketingSystemDbContext _context;
        private readonly IConfiguration _configuration;

        public StatusController(TicketingSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Status.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStatus), new { id = status.StatusId }, status);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            //if (id != status.StatusId)
            //{
              //  return BadRequest("Status ID mismatch");
            //}

            var oldStatus = await _context.Status.FindAsync(id);
            if (oldStatus == null)
            {
                return NotFound();
            }

            oldStatus.NomStatus = status.NomStatus;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating status");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

