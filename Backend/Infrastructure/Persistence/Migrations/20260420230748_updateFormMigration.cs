using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFormMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormResults_FormSubmissionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormResults_Forms_FormId",
                table: "FormResults");

            migrationBuilder.DropForeignKey(
                name: "FK_FormResults_users_UserId",
                table: "FormResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormResults",
                table: "FormResults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FormResults");

            migrationBuilder.RenameTable(
                name: "FormResults",
                newName: "FormSubmission");

            migrationBuilder.RenameIndex(
                name: "IX_FormResults_UserId",
                table: "FormSubmission",
                newName: "IX_FormSubmission_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FormResults_FormId",
                table: "FormSubmission",
                newName: "IX_FormSubmission_FormId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "FormSubmission",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormSubmission",
                table: "FormSubmission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormSubmission_FormSubmissionId",
                table: "Answers",
                column: "FormSubmissionId",
                principalTable: "FormSubmission",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmission_Forms_FormId",
                table: "FormSubmission",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormSubmission_users_UserId",
                table: "FormSubmission",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormSubmission_FormSubmissionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmission_Forms_FormId",
                table: "FormSubmission");

            migrationBuilder.DropForeignKey(
                name: "FK_FormSubmission_users_UserId",
                table: "FormSubmission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormSubmission",
                table: "FormSubmission");

            migrationBuilder.RenameTable(
                name: "FormSubmission",
                newName: "FormResults");

            migrationBuilder.RenameIndex(
                name: "IX_FormSubmission_UserId",
                table: "FormResults",
                newName: "IX_FormResults_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FormSubmission_FormId",
                table: "FormResults",
                newName: "IX_FormResults_FormId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmittedAt",
                table: "FormResults",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FormResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormResults",
                table: "FormResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormResults_FormSubmissionId",
                table: "Answers",
                column: "FormSubmissionId",
                principalTable: "FormResults",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormResults_Forms_FormId",
                table: "FormResults",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormResults_users_UserId",
                table: "FormResults",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
