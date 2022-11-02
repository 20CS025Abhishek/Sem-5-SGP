using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_Clubhouse.Migrations
{
    public partial class UpdatedClubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                schema: "Identity",
                table: "ClubMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeader",
                schema: "Identity",
                table: "ClubMember",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ClubModRole",
                schema: "Identity",
                table: "Club",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                schema: "Identity",
                table: "ClubMember");

            migrationBuilder.DropColumn(
                name: "IsLeader",
                schema: "Identity",
                table: "ClubMember");

            migrationBuilder.DropColumn(
                name: "ClubModRole",
                schema: "Identity",
                table: "Club");
        }
    }
}
