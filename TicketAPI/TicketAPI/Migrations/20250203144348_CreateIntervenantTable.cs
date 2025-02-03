using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateIntervenantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomIntervenant",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "IdIntervenant",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Intervenants",
                columns: table => new
                {
                    IdIntervenant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomIntervenant = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervenants", x => x.IdIntervenant);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IdIntervenant",
                table: "Tickets",
                column: "IdIntervenant");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Intervenants_IdIntervenant",
                table: "Tickets",
                column: "IdIntervenant",
                principalTable: "Intervenants",
                principalColumn: "IdIntervenant",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Intervenants_IdIntervenant",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Intervenants");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_IdIntervenant",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IdIntervenant",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "NomIntervenant",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
