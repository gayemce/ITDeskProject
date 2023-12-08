using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITDeskServer.Migrations
{
    /// <inheritdoc />
    public partial class user_classinda_userid_alaninin_ismi_degistirildi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketFiles_tickets_TicketId",
                table: "TicketFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_Users_AppUserId",
                table: "tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tickets",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tickets");

            migrationBuilder.RenameTable(
                name: "tickets",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_tickets_AppUserId",
                table: "Tickets",
                newName: "IX_Tickets_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketFiles_Tickets_TicketId",
                table: "TicketFiles",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AppUserId",
                table: "Tickets",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketFiles_Tickets_TicketId",
                table: "TicketFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_AppUserId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "tickets");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_AppUserId",
                table: "tickets",
                newName: "IX_tickets_AppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tickets",
                table: "tickets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketFiles_tickets_TicketId",
                table: "TicketFiles",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_Users_AppUserId",
                table: "tickets",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
