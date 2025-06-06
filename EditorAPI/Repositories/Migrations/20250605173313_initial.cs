using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "elementtypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("elementtypes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "presentations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true, defaultValueSql: "'Title'::character varying"),
                    author = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("presentations_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "slides",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    background = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true, defaultValueSql: "'white'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("slides_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "elements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    typeid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("elements_pkey", x => x.id);
                    table.ForeignKey(
                        name: "elements_typeid_fkey",
                        column: x => x.typeid,
                        principalTable: "elementtypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sharedpresentationslides",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    presentationid = table.Column<int>(type: "integer", nullable: true),
                    slideid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sharedpresentationslides_pkey", x => x.id);
                    table.ForeignKey(
                        name: "sharedpresentationslides_presentationid_fkey",
                        column: x => x.presentationid,
                        principalTable: "presentations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "sharedpresentationslides_slideid_fkey",
                        column: x => x.slideid,
                        principalTable: "slides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sharedslideelements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    slideid = table.Column<int>(type: "integer", nullable: true),
                    elementid = table.Column<int>(type: "integer", nullable: true),
                    element_x = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    element_y = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    element_width = table.Column<int>(type: "integer", nullable: true, defaultValue: 100),
                    element_height = table.Column<int>(type: "integer", nullable: true, defaultValue: 30),
                    element_content = table.Column<string>(type: "text", nullable: true, defaultValueSql: "''::text")
                },
                constraints: table =>
                {
                    table.PrimaryKey("sharedslideelements_pkey", x => x.id);
                    table.ForeignKey(
                        name: "sharedslideelements_elementid_fkey",
                        column: x => x.elementid,
                        principalTable: "elements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "sharedslideelements_slideid_fkey",
                        column: x => x.slideid,
                        principalTable: "slides",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_elements_typeid",
                table: "elements",
                column: "typeid");

            migrationBuilder.CreateIndex(
                name: "IX_sharedpresentationslides_slideid",
                table: "sharedpresentationslides",
                column: "slideid");

            migrationBuilder.CreateIndex(
                name: "sharedpresentationslides_presentationid_slideid_idx",
                table: "sharedpresentationslides",
                columns: new[] { "presentationid", "slideid" });

            migrationBuilder.CreateIndex(
                name: "IX_sharedslideelements_elementid",
                table: "sharedslideelements",
                column: "elementid");

            migrationBuilder.CreateIndex(
                name: "sharedslideelements_slideid_elementid_idx",
                table: "sharedslideelements",
                columns: new[] { "slideid", "elementid" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sharedpresentationslides");

            migrationBuilder.DropTable(
                name: "sharedslideelements");

            migrationBuilder.DropTable(
                name: "presentations");

            migrationBuilder.DropTable(
                name: "elements");

            migrationBuilder.DropTable(
                name: "slides");

            migrationBuilder.DropTable(
                name: "elementtypes");
        }
    }
}
