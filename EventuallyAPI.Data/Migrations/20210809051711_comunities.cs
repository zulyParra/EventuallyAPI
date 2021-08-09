using Microsoft.EntityFrameworkCore.Migrations;

namespace EventuallyAPI.Data.Migrations
{
    public partial class comunities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comunities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    OtherArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Banner = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comunities_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComunitySocialNetworks",
                columns: table => new
                {
                    SocialNetworkId = table.Column<int>(type: "int", nullable: false),
                    ComunityId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComunitySocialNetworks", x => new { x.ComunityId, x.SocialNetworkId });
                    table.ForeignKey(
                        name: "FK_ComunitySocialNetworks_Comunities_ComunityId",
                        column: x => x.ComunityId,
                        principalTable: "Comunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComunitySocialNetworks_SocialNetworks_SocialNetworkId",
                        column: x => x.SocialNetworkId,
                        principalTable: "SocialNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Modelado 3D");

            migrationBuilder.InsertData(
                table: "SocialNetworks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Facebook" },
                    { 2, "Linkedin" },
                    { 3, "Github" },
                    { 4, "Twitter" },
                    { 5, "Instagram" },
                    { 6, "Discord" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comunities_AreaId",
                table: "Comunities",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComunitySocialNetworks_SocialNetworkId",
                table: "ComunitySocialNetworks",
                column: "SocialNetworkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComunitySocialNetworks");

            migrationBuilder.DropTable(
                name: "Comunities");

            migrationBuilder.DropTable(
                name: "SocialNetworks");

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "MOdelado 3D");
        }
    }
}
