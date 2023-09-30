using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Migrations
{
    /// <inheritdoc />
    public partial class OUTRA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacoteServico");

            migrationBuilder.RenameColumn(
                name: "valor",
                table: "Quartos",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "nroHospedes",
                table: "Quartos",
                newName: "NroHospedes");

            migrationBuilder.RenameColumn(
                name: "nroQuarto",
                table: "Quartos",
                newName: "NroQuarto");

            migrationBuilder.AddColumn<int>(
                name: "PacoteId",
                table: "Servicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_PacoteId",
                table: "Servicos",
                column: "PacoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Pacotes_PacoteId",
                table: "Servicos",
                column: "PacoteId",
                principalTable: "Pacotes",
                principalColumn: "IdPacote",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Pacotes_PacoteId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_PacoteId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "PacoteId",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Quartos",
                newName: "valor");

            migrationBuilder.RenameColumn(
                name: "NroHospedes",
                table: "Quartos",
                newName: "nroHospedes");

            migrationBuilder.RenameColumn(
                name: "NroQuarto",
                table: "Quartos",
                newName: "nroQuarto");

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
    }
}
