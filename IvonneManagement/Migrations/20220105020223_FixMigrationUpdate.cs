using Microsoft.EntityFrameworkCore.Migrations;

namespace IvonneManagement.Migrations
{
    public partial class FixMigrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inquilinos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoInquilino = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquilinos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartamentos",
                columns: table => new
                {
                    IdApt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InquilinoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartamentos", x => x.IdApt);
                    table.ForeignKey(
                        name: "FK_Apartamentos_Inquilinos_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Inquilinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagoMantenimientos",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InquilinoId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    ApartamentoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoMantenimientos", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_PagoMantenimientos_Apartamentos_ApartamentoId",
                        column: x => x.ApartamentoId,
                        principalTable: "Apartamentos",
                        principalColumn: "IdApt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagoMantenimientos_Inquilinos_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Inquilinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentaParqueos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApt = table.Column<int>(type: "int", nullable: false),
                    InquilinoId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentaParqueos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentaParqueos_Apartamentos_IdApt",
                        column: x => x.IdApt,
                        principalTable: "Apartamentos",
                        principalColumn: "IdApt",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentaParqueos_Inquilinos_InquilinoId",
                        column: x => x.InquilinoId,
                        principalTable: "Inquilinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartamentos_InquilinoId",
                table: "Apartamentos",
                column: "InquilinoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoMantenimientos_ApartamentoId",
                table: "PagoMantenimientos",
                column: "ApartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoMantenimientos_InquilinoId",
                table: "PagoMantenimientos",
                column: "InquilinoId");

            migrationBuilder.CreateIndex(
                name: "IX_RentaParqueos_IdApt",
                table: "RentaParqueos",
                column: "IdApt");

            migrationBuilder.CreateIndex(
                name: "IX_RentaParqueos_InquilinoId",
                table: "RentaParqueos",
                column: "InquilinoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagoMantenimientos");

            migrationBuilder.DropTable(
                name: "RentaParqueos");

            migrationBuilder.DropTable(
                name: "Apartamentos");

            migrationBuilder.DropTable(
                name: "Inquilinos");
        }
    }
}
