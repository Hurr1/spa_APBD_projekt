using Microsoft.EntityFrameworkCore.Migrations;

namespace APDBprojekt.Server.Data.Migrations
{
    public partial class Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    IdAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.IdAddress);
                });

            migrationBuilder.CreateTable(
                name: "Brandings",
                columns: table => new
                {
                    BrandingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brandings", x => x.BrandingId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandingId = table.Column<int>(type: "int", nullable: true),
                    AddressIdAddress = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_AddressIdAddress",
                        column: x => x.AddressIdAddress,
                        principalTable: "Addresses",
                        principalColumn: "IdAddress",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Brandings_BrandingId",
                        column: x => x.BrandingId,
                        principalTable: "Brandings",
                        principalColumn: "BrandingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressIdAddress",
                table: "Companies",
                column: "AddressIdAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BrandingId",
                table: "Companies",
                column: "BrandingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Brandings");
        }
    }
}
