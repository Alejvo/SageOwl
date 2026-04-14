using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class formMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormResults_FormResultId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Options_SelectedOptionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Forms_FormId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_SelectedOptionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "OpenQuestionAnswer",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "SelectedOptionId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "FormResultId",
                table: "Answers",
                newName: "FormSubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_FormResultId",
                table: "Answers",
                newName: "IX_Answers_FormSubmissionId");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionType",
                table: "Questions",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FormId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Questions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormResults_FormSubmissionId",
                table: "Answers",
                column: "FormSubmissionId",
                principalTable: "FormResults",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Forms_FormId",
                table: "Questions",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_FormResults_FormSubmissionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Forms_FormId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "FormSubmissionId",
                table: "Answers",
                newName: "FormResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_FormSubmissionId",
                table: "Answers",
                newName: "IX_Answers_FormResultId");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OpenQuestionAnswer",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectedOptionId",
                table: "Answers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_SelectedOptionId",
                table: "Answers",
                column: "SelectedOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_FormResults_FormResultId",
                table: "Answers",
                column: "FormResultId",
                principalTable: "FormResults",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Options_SelectedOptionId",
                table: "Answers",
                column: "SelectedOptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Forms_FormId",
                table: "Questions",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
