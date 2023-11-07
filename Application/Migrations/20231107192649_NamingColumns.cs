using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class NamingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentWindow",
                table: "Windows");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Windows",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Windows",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsIncognito",
                table: "Windows",
                newName: "is_incognito");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Tabs",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tabs",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tabs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Sessions",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Sessions",
                newName: "created");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sessions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SessionType",
                table: "Sessions",
                newName: "session_type");

            migrationBuilder.RenameColumn(
                name: "LastChange",
                table: "Sessions",
                newName: "last_change");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Windows",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Windows",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_incognito",
                table: "Windows",
                newName: "IsIncognito");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Tabs",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Tabs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tabs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Sessions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Sessions",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "session_type",
                table: "Sessions",
                newName: "SessionType");

            migrationBuilder.RenameColumn(
                name: "last_change",
                table: "Sessions",
                newName: "LastChange");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentWindow",
                table: "Windows",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
