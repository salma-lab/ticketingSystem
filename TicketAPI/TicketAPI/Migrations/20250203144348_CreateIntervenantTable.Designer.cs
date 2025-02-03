﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketAPI.Data;

#nullable disable

namespace TicketAPI.Migrations
{
    [DbContext(typeof(TicketingSystemDbContext))]
    [Migration("20250203144348_CreateIntervenantTable")]
    partial class CreateIntervenantTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketAPI.Models.Emplacement", b =>
                {
                    b.Property<int>("EmplacementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmplacementId"));

                    b.Property<string>("NomEmplacement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmplacementId");

                    b.ToTable("Emplacements");
                });

            modelBuilder.Entity("TicketAPI.Models.Etage", b =>
                {
                    b.Property<int>("EtageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EtageId"));

                    b.Property<string>("NomEtage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EtageId");

                    b.ToTable("Etages");
                });

            modelBuilder.Entity("TicketAPI.Models.Intervenant", b =>
                {
                    b.Property<int>("IdIntervenant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdIntervenant"));

                    b.Property<string>("NomIntervenant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdIntervenant");

                    b.ToTable("Intervenants");
                });

            modelBuilder.Entity("TicketAPI.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TicketAPI.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("NomStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("TicketAPI.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<string>("AppareilNom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmplacementId")
                        .HasColumnType("int");

                    b.Property<int?>("EtageId")
                        .HasColumnType("int");

                    b.Property<int?>("IdIntervenant")
                        .HasColumnType("int");

                    b.Property<string>("MotifDemande")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Oralement")
                        .HasColumnType("bit");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TypeAppareilId")
                        .HasColumnType("int");

                    b.Property<int>("TypeInterventionId")
                        .HasColumnType("int");

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.Property<bool?>("Validation1")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ValidationTime")
                        .HasColumnType("datetime2");

                    b.HasKey("TicketId");

                    b.HasIndex("EmplacementId");

                    b.HasIndex("EtageId");

                    b.HasIndex("IdIntervenant");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeAppareilId");

                    b.HasIndex("TypeInterventionId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketAPI.Models.TypeAppareil", b =>
                {
                    b.Property<int>("TypeAppareilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeAppareilId"));

                    b.Property<string>("NomTypeAppareil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeAppareilId");

                    b.ToTable("TypeAppareils");
                });

            modelBuilder.Entity("TicketAPI.Models.TypeIntervention", b =>
                {
                    b.Property<int>("TypeInterventionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeInterventionId"));

                    b.Property<string>("NomType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeInterventionId");

                    b.ToTable("TypesIntervention");
                });

            modelBuilder.Entity("TicketAPI.Models.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UtilisateurId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UtilisateurId");

                    b.HasIndex("RoleId");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("TicketAPI.Models.Ticket", b =>
                {
                    b.HasOne("TicketAPI.Models.Emplacement", "Emplacement")
                        .WithMany()
                        .HasForeignKey("EmplacementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketAPI.Models.Etage", "Etage")
                        .WithMany()
                        .HasForeignKey("EtageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketAPI.Models.Intervenant", "Intervenant")
                        .WithMany()
                        .HasForeignKey("IdIntervenant")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketAPI.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("TicketAPI.Models.TypeAppareil", "TypeAppareil")
                        .WithMany()
                        .HasForeignKey("TypeAppareilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketAPI.Models.TypeIntervention", "TypeIntervention")
                        .WithMany()
                        .HasForeignKey("TypeInterventionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketAPI.Models.Utilisateur", "Utilisateur")
                        .WithMany("Tickets")
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Emplacement");

                    b.Navigation("Etage");

                    b.Navigation("Intervenant");

                    b.Navigation("Status");

                    b.Navigation("TypeAppareil");

                    b.Navigation("TypeIntervention");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("TicketAPI.Models.Utilisateur", b =>
                {
                    b.HasOne("TicketAPI.Models.Role", "Role")
                        .WithMany("Utilisateurs")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TicketAPI.Models.Role", b =>
                {
                    b.Navigation("Utilisateurs");
                });

            modelBuilder.Entity("TicketAPI.Models.Utilisateur", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
