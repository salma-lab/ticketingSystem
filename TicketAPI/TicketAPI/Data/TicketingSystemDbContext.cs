using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;

namespace TicketAPI.Data

{
    public class TicketingSystemDbContext : DbContext
    {
        public TicketingSystemDbContext(DbContextOptions<TicketingSystemDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TypeIntervention> TypesIntervention { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>()
           .HasOne(u => u.Role)
           .WithMany(r => r.Utilisateurs)
           .HasForeignKey(u => u.RoleId)
           .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete cycles


      
        
        }


       
    }
}
