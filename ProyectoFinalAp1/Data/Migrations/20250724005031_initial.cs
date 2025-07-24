using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalAp1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarritoCompras_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Avions",
                columns: table => new
                {
                    IdAvion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fabricante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CapacidadPasajeros = table.Column<int>(type: "int", nullable: false),
                    VelocidadMaxima = table.Column<double>(type: "float", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaFabricacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avions", x => x.IdAvion);
                    table.ForeignKey(
                        name: "FK_Avions_Categorias_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria");
                });

            migrationBuilder.CreateTable(
                name: "CarritoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAvion = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarritoCompraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarritoDetalles_Avions_IdAvion",
                        column: x => x.IdAvion,
                        principalTable: "Avions",
                        principalColumn: "IdAvion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoDetalles_CarritoCompras_CarritoCompraId",
                        column: x => x.CarritoCompraId,
                        principalTable: "CarritoCompras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avions_CategoriaIdCategoria",
                table: "Avions",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompras_UsuarioId",
                table: "CarritoCompras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalles_CarritoCompraId",
                table: "CarritoDetalles",
                column: "CarritoCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoDetalles_IdAvion",
                table: "CarritoDetalles",
                column: "IdAvion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoDetalles");

            migrationBuilder.DropTable(
                name: "Avions");

            migrationBuilder.DropTable(
                name: "CarritoCompras");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
