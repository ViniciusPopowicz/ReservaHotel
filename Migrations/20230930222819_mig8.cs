using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    IdHotel = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false),
                    Pais = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    NumQuartos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.IdHotel);
                });

            migrationBuilder.CreateTable(
                name: "Pacotes",
                columns: table => new
                {
                    IdPacote = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ValorPacote = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacotes", x => x.IdPacote);
                });

            migrationBuilder.CreateTable(
                name: "Quartos",
                columns: table => new
                {
                    NroQuarto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NroHospedes = table.Column<int>(type: "INTEGER", nullable: false),
                    valor = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quartos", x => x.NroQuarto);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    IdVoucher = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Desconto = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.IdVoucher);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    IdServico = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    ValorServico = table.Column<float>(type: "REAL", nullable: false),
                    PacoteIdPacote = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.IdServico);
                    table.ForeignKey(
                        name: "FK_Servicos_Pacotes_PacoteIdPacote",
                        column: x => x.PacoteIdPacote,
                        principalTable: "Pacotes",
                        principalColumn: "IdPacote");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataReserva = table.Column<string>(type: "TEXT", nullable: false),
                    DataCheckIn = table.Column<string>(type: "TEXT", nullable: false),
                    DataCheckOut = table.Column<string>(type: "TEXT", nullable: false),
                    QuartoNroQuarto = table.Column<int>(type: "INTEGER", nullable: false),
                    HotelIdHotel = table.Column<int>(type: "INTEGER", nullable: false),
                    PacoteIdPacote = table.Column<int>(type: "INTEGER", nullable: false),
                    ClienteCpf = table.Column<string>(type: "TEXT", nullable: false),
                    VoucherIdVoucher = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorReserva = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteCpf",
                        column: x => x.ClienteCpf,
                        principalTable: "Clientes",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Hotels_HotelIdHotel",
                        column: x => x.HotelIdHotel,
                        principalTable: "Hotels",
                        principalColumn: "IdHotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Pacotes_PacoteIdPacote",
                        column: x => x.PacoteIdPacote,
                        principalTable: "Pacotes",
                        principalColumn: "IdPacote",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Quartos_QuartoNroQuarto",
                        column: x => x.QuartoNroQuarto,
                        principalTable: "Quartos",
                        principalColumn: "NroQuarto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Vouchers_VoucherIdVoucher",
                        column: x => x.VoucherIdVoucher,
                        principalTable: "Vouchers",
                        principalColumn: "IdVoucher",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteCpf",
                table: "Reservas",
                column: "ClienteCpf");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HotelIdHotel",
                table: "Reservas",
                column: "HotelIdHotel");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_PacoteIdPacote",
                table: "Reservas",
                column: "PacoteIdPacote");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_QuartoNroQuarto",
                table: "Reservas",
                column: "QuartoNroQuarto");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VoucherIdVoucher",
                table: "Reservas",
                column: "VoucherIdVoucher");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_PacoteIdPacote",
                table: "Servicos",
                column: "PacoteIdPacote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Quartos");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Pacotes");
        }
    }
}
