using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrinquedosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFornecedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id_fornecedor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome_fornecedor = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Nome_representante = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CNPJ = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id_fornecedor);
                });

            migrationBuilder.CreateTable(
                name: "BrinquedoFornecedores",
                columns: table => new
                {
                    BrinquedoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FornecedorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrinquedoFornecedores", x => new { x.BrinquedoId, x.FornecedorId });
                    table.ForeignKey(
                        name: "FK_BrinquedoFornecedores_Brinquedos_BrinquedoId",
                        column: x => x.BrinquedoId,
                        principalTable: "Brinquedos",
                        principalColumn: "Id_brinquedo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrinquedoFornecedores_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id_fornecedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrinquedoFornecedores_FornecedorId",
                table: "BrinquedoFornecedores",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_CNPJ",
                table: "Fornecedores",
                column: "CNPJ",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrinquedoFornecedores");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
