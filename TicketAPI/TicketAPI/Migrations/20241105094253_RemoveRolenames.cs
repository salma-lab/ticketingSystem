﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRolenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Utilisateurs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
