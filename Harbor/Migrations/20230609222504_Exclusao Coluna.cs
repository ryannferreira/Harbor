using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harbor.Migrations
{
    /// <inheritdoc />
    public partial class ExclusaoColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Container_ContainerIdContainer",
                table: "Movimentacoes");

            migrationBuilder.DropIndex(
                name: "IX_Movimentacoes_ContainerIdContainer",
                table: "Movimentacoes");

            migrationBuilder.DropColumn(
                name: "ContainerIdContainer",
                table: "Movimentacoes");

            migrationBuilder.RenameColumn(
                name: "nome_Container",
                table: "Movimentacoes",
                newName: "numero_Container");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numero_Container",
                table: "Movimentacoes",
                newName: "nome_Container");

            migrationBuilder.AddColumn<int>(
                name: "ContainerIdContainer",
                table: "Movimentacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ContainerIdContainer",
                table: "Movimentacoes",
                column: "ContainerIdContainer");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Container_ContainerIdContainer",
                table: "Movimentacoes",
                column: "ContainerIdContainer",
                principalTable: "Container",
                principalColumn: "id_Container",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
