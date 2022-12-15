using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetApi.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cartes",
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

            migrationBuilder.CreateTable(
                name: "Faiblesses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    resistancename = table.Column<string>(name: "resistance_name", type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    faiblessename = table.Column<string>(name: "faiblesse_name", type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faiblesses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Faiblesses_Elements_faiblesse_name",
                        column: x => x.faiblessename,
                        principalTable: "Elements",
                        principalColumn: "name");
                    table.ForeignKey(
                        name: "FK_Faiblesses_Elements_resistance_name",
                        column: x => x.resistancename,
                        principalTable: "Elements",
                        principalColumn: "name");
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

            migrationBuilder.CreateIndex(
                name: "IX_Faiblesses_faiblesse_name",
                table: "Faiblesses",
                column: "faiblesse_name");

            migrationBuilder.CreateIndex(
                name: "IX_Faiblesses_resistance_name",
                table: "Faiblesses",
                column: "resistance_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartes");

            migrationBuilder.DropTable(
                name: "Faiblesses");

            migrationBuilder.DropTable(
                name: "Elements");
        }
    }
}
