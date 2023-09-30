using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Migrations
{
    /// <inheritdoc />
    public partial class maisuma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Pacotes_PacoteIdPacote",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_PacoteIdPacote",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "PacoteIdPacote",
                table: "Servicos");

            migrationBuilder.CreateTable(
                name: "PacoteServico",
                columns: table => new
                {
                    PacotesIdPacote = table.Column<int>(type: "INTEGER", nullable: false),
                    ServicosIdServico = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacoteServico", x => new { x.PacotesIdPacote, x.ServicosIdServico });
                    table.ForeignKey(
                        name: "FK_PacoteServico_Pacotes_PacotesIdPacote",
                        column: x => x.PacotesIdPacote,
                        principalTable: "Pacotes",
                        principalColumn: "IdPacote",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacoteServico_Servicos_ServicosIdServico",
                        column: x => x.ServicosIdServico,
                        principalTable: "Servicos",
                        principalColumn: "IdServico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacoteServico_ServicosIdServico",
                table: "PacoteServico",
                column: "ServicosIdServico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacoteServico");

            migrationBuilder.AddColumn<int>(
                name: "PacoteIdPacote",
                table: "Servicos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_PacoteIdPacote",
                table: "Servicos",
                column: "PacoteIdPacote");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Pacotes_PacoteIdPacote",
                table: "Servicos",
                column: "PacoteIdPacote",
                principalTable: "Pacotes",
                principalColumn: "IdPacote");
        }
    }
}
