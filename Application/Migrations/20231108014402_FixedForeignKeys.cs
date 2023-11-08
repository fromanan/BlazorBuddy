using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class FixedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Windows_Tab",
                table: "Tabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Windows_Sessions_Window",
                table: "Windows");

            migrationBuilder.DropIndex(
                name: "IX_Windows_Window",
                table: "Windows");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_Tab",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "Window",
                table: "Windows");

            migrationBuilder.DropColumn(
                name: "Tab",
                table: "Tabs");

            migrationBuilder.AddColumn<int>(
                name: "session_id",
                table: "Windows",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "window_id",
                table: "Tabs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Windows_session_id",
                table: "Windows",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_window_id",
                table: "Tabs",
                column: "window_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Windows_window_id",
                table: "Tabs",
                column: "window_id",
                principalTable: "Windows",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Windows_Sessions_session_id",
                table: "Windows",
                column: "session_id",
                principalTable: "Sessions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Windows_window_id",
                table: "Tabs");

            migrationBuilder.DropForeignKey(
                name: "FK_Windows_Sessions_session_id",
                table: "Windows");

            migrationBuilder.DropIndex(
                name: "IX_Windows_session_id",
                table: "Windows");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_window_id",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "session_id",
                table: "Windows");

            migrationBuilder.DropColumn(
                name: "window_id",
                table: "Tabs");

            migrationBuilder.AddColumn<int>(
                name: "Window",
                table: "Windows",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tab",
                table: "Tabs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Windows_Window",
                table: "Windows",
                column: "Window");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_Tab",
                table: "Tabs",
                column: "Tab");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Windows_Tab",
                table: "Tabs",
                column: "Tab",
                principalTable: "Windows",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Windows_Sessions_Window",
                table: "Windows",
                column: "Window",
                principalTable: "Sessions",
                principalColumn: "id");
        }
    }
}
