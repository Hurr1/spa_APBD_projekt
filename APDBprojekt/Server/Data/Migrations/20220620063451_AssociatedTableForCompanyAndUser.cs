using Microsoft.EntityFrameworkCore.Migrations;

namespace APDBprojekt.Server.Data.Migrations
{
    public partial class AssociatedTableForCompanyAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCompanies",
                columns: table => new
                {
                    IdApplicationUserCompany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanies", x => x.IdApplicationUserCompany);
                    table.ForeignKey(
                        name: "FK_UserCompanies_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompanies_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_Id",
                table: "UserCompanies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_IdCompany",
                table: "UserCompanies",
                column: "IdCompany");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCompanies");
        }
    }
}
