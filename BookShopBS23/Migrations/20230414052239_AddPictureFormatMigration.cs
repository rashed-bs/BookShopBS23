using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopBS23.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureFormatMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFormat",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureFormat",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFormat",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PictureFormat",
                table: "Authors");
        }
    }
}
