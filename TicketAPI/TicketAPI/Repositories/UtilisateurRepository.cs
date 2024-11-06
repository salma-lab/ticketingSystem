// UtilisateurRepository.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketAPI.Data;
using TicketAPI.Models;

namespace TicketAPI.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly TicketingSystemDbContext _context;

        public UtilisateurRepository(TicketingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
        {
            return await _context.Utilisateurs.Include(u => u.Role).ToListAsync();
        }

        public async Task<Utilisateur> GetUtilisateurByIdAsync(int id)
        {
            return await _context.Utilisateurs.Include(u => u.Role).FirstOrDefaultAsync(u => u.UtilisateurId == id);
        }

        public async Task AddUtilisateurAsync(Utilisateur utilisateur)
        {
            await _context.Utilisateurs.AddAsync(utilisateur);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUtilisateurAsync(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Update(utilisateur);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUtilisateurAsync(int id)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                await _context.SaveChangesAsync();
            }
        }
    }
}
