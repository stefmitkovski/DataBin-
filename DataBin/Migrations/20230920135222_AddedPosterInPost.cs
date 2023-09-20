using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBin.Migrations
{
    public partial class AddedPosterInPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Post");
        }
    }
}
