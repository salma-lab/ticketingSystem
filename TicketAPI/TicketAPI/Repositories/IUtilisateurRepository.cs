// IUtilisateurRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketAPI.Models;

namespace TicketAPI.Repositories
{
    public interface IUtilisateurRepository
    {
        Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync();
        Task<Utilisateur> GetUtilisateurByIdAsync(int id);
        Task AddUtilisateurAsync(Utilisateur utilisateur);
        Task UpdateUtilisateurAsync(Utilisateur utilisateur);
        Task DeleteUtilisateurAsync(int id);
    }
}
