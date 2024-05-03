using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Posrtgre.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_catalog.adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    brand_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brand_pkey", x => x.brand_id);
                });

            migrationBuilder.CreateTable(
                name: "color",
                columns: table => new
                {
                    color_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    color_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("color_pkey", x => x.color_id);
                });

            migrationBuilder.CreateTable(
                name: "model_brand",
                columns: table => new
                {
                    model_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand_id = table.Column<int>(type: "integer", nullable: true),
                    model_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("model_brand_pkey", x => x.model_id);
                    table.ForeignKey(
                        name: "brand",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "brand_id");
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand_id = table.Column<int>(type: "integer", nullable: true),
                    model_id = table.Column<int>(type: "integer", nullable: true),
                    color_id = table.Column<int>(type: "integer", nullable: true),
                    year = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("car_pkey", x => x.id);
                    table.ForeignKey(
                        name: "brand_id",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "brand_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "color",
                        column: x => x.color_id,
                        principalTable: "color",
                        principalColumn: "color_id");
                    table.ForeignKey(
                        name: "model_id",
                        column: x => x.model_id,
                        principalTable: "model_brand",
                        principalColumn: "model_id");
                });

            migrationBuilder.CreateIndex(
                name: "fki_color",
                table: "car",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_brand_id",
                table: "car",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_model_id",
                table: "car",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "fki_и",
                table: "model_brand",
                column: "brand_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "model_brand");

            migrationBuilder.DropTable(
                name: "brand");
        }



    }
}
