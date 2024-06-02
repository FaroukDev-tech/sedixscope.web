using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sedixscope.web.Migrations
{
    /// <inheritdoc />
    public partial class addrowforcommentdescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Comments");
        }
    }
}
