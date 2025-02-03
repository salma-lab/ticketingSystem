using Microsoft.EntityFrameworkCore;
using TicketAPI.Models;

namespace TicketAPI.Data

{
    public class TicketingSystemDbContext : DbContext
    {
        public TicketingSystemDbContext(DbContextOptions<TicketingSystemDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TypeIntervention> TypesIntervention { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TypeAppareil> TypeAppareils { get; set; }
        public DbSet<Intervenant> Intervenants { get; set; }
        public DbSet<Emplacement> Emplacements { get; set; }
        public DbSet<Etage> Etages{ get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TypeAppareil)
                .WithMany() // Si `TypeAppareil` n'a pas de collection `Tickets`
                .HasForeignKey(t => t.TypeAppareilId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Etage)
                .WithMany() // Si `TypeAppareil` n'a pas de collection `Tickets`
                .HasForeignKey(t => t.EtageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
              .HasOne(t => t.Intervenant)
              .WithMany() // Si `TypeAppareil` n'a pas de collection `Tickets`
              .HasForeignKey(t => t.IdIntervenant)
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Utilisateur>()
               .HasOne(u => u.Role)
               .WithMany(r => r.Utilisateurs)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
    .Property(t => t.DateCreation)
    .HasColumnType("datetime")
    .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }

    }



    }

