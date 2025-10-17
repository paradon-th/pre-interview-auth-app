using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PreAuthBe.Migrations
{
    /// <inheritdoc />
    public partial class SeedThaiUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a9b-8c7d-123456abcdef"), new DateTime(2025, 10, 17, 11, 19, 11, 841, DateTimeKind.Utc).AddTicks(2794), "somying.j@system.io", "สมหญิง", "ใจดี", "$2a$11$c0b57bHqaQ/WcgcMHagaROg5BGK5GyLDr1keMYgvucQXmoB4HXE.W", "Admin", "somying_admin" },
                    { new Guid("b2c3d4e5-f6a7-4b8c-9d6e-234567abcdef"), new DateTime(2025, 10, 17, 11, 19, 11, 841, DateTimeKind.Utc).AddTicks(2817), "somchai.r@email.com", "สมชาย", "รักไทย", "$2a$11$c0b57bHqaQ/WcgcMHagaROg5BGK5GyLDr1keMYgvucQXmoB4HXE.W", "User", "somchai_user" },
                    { new Guid("e5f6a7b8-c9d0-4c5b-af4f-345678abcdef"), new DateTime(2025, 10, 17, 11, 19, 11, 841, DateTimeKind.Utc).AddTicks(2820), "ekachai@superuser.com", "เอกชัย", "ราชา", "$2a$11$c0b57bHqaQ/WcgcMHagaROg5BGK5GyLDr1keMYgvucQXmoB4HXE.W", "Admin", "ekachai_admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a9b-8c7d-123456abcdef"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b8c-9d6e-234567abcdef"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4c5b-af4f-345678abcdef"));
        }
    }
}
