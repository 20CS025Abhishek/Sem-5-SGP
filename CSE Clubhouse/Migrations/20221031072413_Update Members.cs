using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_Clubhouse.Migrations
{
    public partial class UpdateMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "Identity",
                table: "ClubMember",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "Identity",
                table: "ClubMember");
        }
    }
}
