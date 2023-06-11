using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harbor.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_id_Container",
                table: "Movimentacoes",
                column: "id_Container");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Container_id_Container",
                table: "Movimentacoes",
                column: "id_Container",
                principalTable: "Container",
                principalColumn: "id_Container",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Container_id_Container",
                table: "Movimentacoes");

            migrationBuilder.DropIndex(
                name: "IX_Movimentacoes_id_Container",
                table: "Movimentacoes");
        }
    }
}
