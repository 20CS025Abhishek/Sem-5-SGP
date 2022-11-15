using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSE_Clubhouse.Migrations
{
    public partial class AddedCoverImgURLColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImgURL",
                schema: "Identity",
                table: "Club",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImgURL",
                schema: "Identity",
                table: "Club");
        }
    }
}
