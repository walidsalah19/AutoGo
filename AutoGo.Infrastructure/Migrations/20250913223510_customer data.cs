using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoGo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class customerdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_ApplicationUserId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_UserId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopReservations_AspNetUsers_ApplicationUserId",
                table: "WorkshopReservations");

            migrationBuilder.DropIndex(
                name: "IX_WorkshopReservations_ApplicationUserId",
                table: "WorkshopReservations");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ApplicationUserId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Workshops");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WorkshopReservations");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Dealers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rentals",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_UserId",
                table: "Rentals",
                newName: "IX_Rentals_customerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Invoices",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                newName: "IX_Invoices_customerId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ApplicationUserId",
                table: "Invoices",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_ApplicationUserId",
                table: "Invoices",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_customerId",
                table: "Invoices",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customers_customerId",
                table: "Rentals",
                column: "customerId",
                principalTable: "Customers",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_ApplicationUserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_customerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customers_customerId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ApplicationUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Rentals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_customerId",
                table: "Rentals",
                newName: "IX_Rentals_UserId");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "Invoices",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_customerId",
                table: "Invoices",
                newName: "IX_Invoices_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Workshops",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Workshops",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Workshops",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Workshops",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "WorkshopReservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Dealers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Dealers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopReservations_ApplicationUserId",
                table: "WorkshopReservations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ApplicationUserId",
                table: "Rentals",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_UserId",
                table: "Invoices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_ApplicationUserId",
                table: "Rentals",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_UserId",
                table: "Rentals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopReservations_AspNetUsers_ApplicationUserId",
                table: "WorkshopReservations",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
