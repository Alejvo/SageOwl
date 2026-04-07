using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_SubscriptionId",
                table: "users",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Subscriptions_SubscriptionId",
                table: "users",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Subscriptions_SubscriptionId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_SubscriptionId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "users");
        }
    }
}
