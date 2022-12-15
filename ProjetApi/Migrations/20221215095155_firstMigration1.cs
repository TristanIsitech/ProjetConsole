using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetApi.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartes");

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    numero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    elementname = table.Column<string>(name: "element_name", type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pv = table.Column<int>(type: "int", nullable: false),
                    evolutionnumero = table.Column<int>(name: "evolution_numero", type: "int", nullable: true),
                    sousevolutionnumero = table.Column<int>(name: "sous_evolution_numero", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.numero);
                    table.ForeignKey(
                        name: "FK_Pokemons_Elements_element_name",
                        column: x => x.elementname,
                        principalTable: "Elements",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemons_Pokemons_evolution_numero",
                        column: x => x.evolutionnumero,
                        principalTable: "Pokemons",
                        principalColumn: "numero");
                    table.ForeignKey(
                        name: "FK_Pokemons_Pokemons_sous_evolution_numero",
                        column: x => x.sousevolutionnumero,
                        principalTable: "Pokemons",
                        principalColumn: "numero");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_element_name",
                table: "Pokemons",
                column: "element_name");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_evolution_numero",
                table: "Pokemons",
                column: "evolution_numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_sous_evolution_numero",
                table: "Pokemons",
                column: "sous_evolution_numero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.CreateTable(
                name: "Cartes",
                columns: table => new
                {
                    numero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    elementname = table.Column<string>(name: "element_name", type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    evolutionnumero = table.Column<int>(name: "evolution_numero", type: "int", nullable: true),
                    sousevolutionnumero = table.Column<int>(name: "sous_evolution_numero", type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pv = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartes", x => x.numero);
                    table.ForeignKey(
                        name: "FK_Cartes_Cartes_evolution_numero",
                        column: x => x.evolutionnumero,
                        principalTable: "Cartes",
                        principalColumn: "numero");
                    table.ForeignKey(
                        name: "FK_Cartes_Cartes_sous_evolution_numero",
                        column: x => x.sousevolutionnumero,
                        principalTable: "Cartes",
                        principalColumn: "numero");
                    table.ForeignKey(
                        name: "FK_Cartes_Elements_element_name",
                        column: x => x.elementname,
                        principalTable: "Elements",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cartes_element_name",
                table: "Cartes",
                column: "element_name");

            migrationBuilder.CreateIndex(
                name: "IX_Cartes_evolution_numero",
                table: "Cartes",
                column: "evolution_numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cartes_sous_evolution_numero",
                table: "Cartes",
                column: "sous_evolution_numero");
        }
    }
}
