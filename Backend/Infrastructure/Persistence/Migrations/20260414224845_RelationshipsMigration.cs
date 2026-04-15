using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_users_AuthorId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembership_users_UserId",
                table: "TeamMembership");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_users_UserId",
                table: "Tokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_users_AuthorId",
                table: "Announcements",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembership_users_UserId",
                table: "TeamMembership",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_users_AuthorId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembership_users_UserId",
                table: "TeamMembership");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_users_UserId",
                table: "Tokens");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_users_AuthorId",
                table: "Announcements",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembership_users_UserId",
                table: "TeamMembership",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
