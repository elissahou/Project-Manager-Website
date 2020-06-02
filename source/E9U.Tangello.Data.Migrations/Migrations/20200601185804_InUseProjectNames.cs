using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E9U.Tangello.Data.Migrations.Migrations
{
    public partial class InUseProjectNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InUseProjectNames",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectNameID = table.Column<int>(nullable: false),
                    ProjectDescription = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InUseProjectNames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InUseProjectNames_ProjectNames_ProjectNameID",
                        column: x => x.ProjectNameID,
                        principalTable: "ProjectNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectNameToCategoryMappings_CategoryID",
                table: "ProjectNameToCategoryMappings",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectNameToCategoryMappings_ProjectNameID",
                table: "ProjectNameToCategoryMappings",
                column: "ProjectNameID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryToProjectTypeMappings_CategoryID",
                table: "CategoryToProjectTypeMappings",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryToProjectTypeMappings_ProjectTypeID",
                table: "CategoryToProjectTypeMappings",
                column: "ProjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InUseProjectNames_ProjectNameID",
                table: "InUseProjectNames",
                column: "ProjectNameID");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryToProjectTypeMappings_Categories_CategoryID",
                table: "CategoryToProjectTypeMappings",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryToProjectTypeMappings_ProjectTypes_ProjectTypeID",
                table: "CategoryToProjectTypeMappings",
                column: "ProjectTypeID",
                principalTable: "ProjectTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNameToCategoryMappings_Categories_CategoryID",
                table: "ProjectNameToCategoryMappings",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNameToCategoryMappings_ProjectNames_ProjectNameID",
                table: "ProjectNameToCategoryMappings",
                column: "ProjectNameID",
                principalTable: "ProjectNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryToProjectTypeMappings_Categories_CategoryID",
                table: "CategoryToProjectTypeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryToProjectTypeMappings_ProjectTypes_ProjectTypeID",
                table: "CategoryToProjectTypeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNameToCategoryMappings_Categories_CategoryID",
                table: "ProjectNameToCategoryMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNameToCategoryMappings_ProjectNames_ProjectNameID",
                table: "ProjectNameToCategoryMappings");

            migrationBuilder.DropTable(
                name: "InUseProjectNames");

            migrationBuilder.DropIndex(
                name: "IX_ProjectNameToCategoryMappings_CategoryID",
                table: "ProjectNameToCategoryMappings");

            migrationBuilder.DropIndex(
                name: "IX_ProjectNameToCategoryMappings_ProjectNameID",
                table: "ProjectNameToCategoryMappings");

            migrationBuilder.DropIndex(
                name: "IX_CategoryToProjectTypeMappings_CategoryID",
                table: "CategoryToProjectTypeMappings");

            migrationBuilder.DropIndex(
                name: "IX_CategoryToProjectTypeMappings_ProjectTypeID",
                table: "CategoryToProjectTypeMappings");
        }
    }
}
