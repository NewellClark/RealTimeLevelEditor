using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddedOwnerNavigationPropertyAndOwnerIdProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Levels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Levels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_OwnerId",
                table: "Levels",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_AspNetUsers_OwnerId",
                table: "Levels",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_AspNetUsers_OwnerId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_OwnerId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Levels");
        }
    }
}
