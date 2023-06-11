using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harbor.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    id_Container = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numero_Container = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.id_Container);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    id_Movimentacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Container = table.Column<int>(type: "int", nullable: false),
                    ContainerIdContainer = table.Column<int>(type: "int", nullable: false),
                    nome_Cliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nome_Container = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo_Movimentacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_Termino = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.id_Movimentacao);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Container_ContainerIdContainer",
                        column: x => x.ContainerIdContainer,
                        principalTable: "Container",
                        principalColumn: "id_Container",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ContainerIdContainer",
                table: "Movimentacoes",
                column: "ContainerIdContainer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacoes");

            migrationBuilder.DropTable(
                name: "Container");
        }
    }
}
