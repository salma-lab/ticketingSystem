namespace TicketAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } // e.g., "Admin", "Worker", "Technician" yeyye
        public ICollection<Utilisateur> Utilisateurs { get; set; }  // Navigation property for users

    }
}
