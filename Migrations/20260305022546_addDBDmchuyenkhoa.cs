using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace his_backend.Migrations
{
    /// <inheritdoc />
    public partial class addDBDmchuyenkhoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ck_trieuchung",
                schema: "benhnhan");

            migrationBuilder.DropTable(
                name: "dmtrieuchungbenh",
                schema: "benhnhan");

            migrationBuilder.AddColumn<string>(
                name: "mota_trieuchung",
                schema: "benhnhan",
                table: "dmchuyenkhoa",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mota_trieuchung",
                schema: "benhnhan",
                table: "dmchuyenkhoa");

            migrationBuilder.CreateTable(
                name: "dmtrieuchungbenh",
                schema: "benhnhan",
                columns: table => new
                {
                    matc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    trieuchung = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dmtrieuchungbenh", x => x.matc);
                });

            migrationBuilder.CreateTable(
                name: "ck_trieuchung",
                schema: "benhnhan",
                columns: table => new
                {
                    mack = table.Column<int>(type: "integer", nullable: false),
                    matc = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ck_trieuchung", x => new { x.mack, x.matc });
                    table.ForeignKey(
                        name: "fk_ck_trieuchung_mack",
                        column: x => x.mack,
                        principalSchema: "benhnhan",
                        principalTable: "dmchuyenkhoa",
                        principalColumn: "mack",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ck_trieuchung_matc",
                        column: x => x.matc,
                        principalSchema: "benhnhan",
                        principalTable: "dmtrieuchungbenh",
                        principalColumn: "matc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ck_trieuchung_matc",
                schema: "benhnhan",
                table: "ck_trieuchung",
                column: "matc");
        }
    }
}
