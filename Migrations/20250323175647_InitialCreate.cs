using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrinquedosAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id_categoria = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome_categoria = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Brinquedos",
                columns: table => new
                {
                    Id_brinquedo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome_brinquedo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Tipo_brinquedo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Classificacao = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Tamanho = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Id_categoria = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Id_estoque = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brinquedos", x => x.Id_brinquedo);
                    table.ForeignKey(
                        name: "FK_Brinquedos_Categorias_Id_categoria",
                        column: x => x.Id_categoria,
                        principalTable: "Categorias",
                        principalColumn: "Id_categoria",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id_estoque = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Quantidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Faixa = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Id_brinquedo = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id_estoque);
                    table.ForeignKey(
                        name: "FK_Estoques_Brinquedos_Id_brinquedo",
                        column: x => x.Id_brinquedo,
                        principalTable: "Brinquedos",
                        principalColumn: "Id_brinquedo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brinquedos_Id_categoria",
                table: "Brinquedos",
                column: "Id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_Id_brinquedo",
                table: "Estoques",
                column: "Id_brinquedo",
                unique: true,
                filter: "\"Id_brinquedo\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "Brinquedos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
