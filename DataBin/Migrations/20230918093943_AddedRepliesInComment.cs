﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBin.Migrations
{
    public partial class AddedRepliesInComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId",
                principalTable: "Comment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "Comment");
        }
    }
}
