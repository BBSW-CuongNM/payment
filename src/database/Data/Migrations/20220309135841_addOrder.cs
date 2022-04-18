using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    public partial class addOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "VnPay_ATM");

            migrationBuilder.AlterColumn<bool>(
                name: "Enable",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    TokenValue = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    FirebaseToken = table.Column<string>(type: "text", nullable: true),
                    DataTransactionId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "WALLET",
                columns: new[] { "PartnerId", "SortIndex" },
                values: new object[] { "", 6 });

            migrationBuilder.InsertData(
                table: "PaymentDestionations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DataTransactionId", "ExternalId", "Group", "Image", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "Name", "OtherName", "ParentId", "PartnerId", "SortIndex" },
                values: new object[] { "ATM", null, null, "", "ATM", "", "https://s3.cloud.cmctelecom.vn/bank/637806135041944131ABBANK.svg", false, null, null, "Thanh toán qua ATM", "", "", "", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DeleteData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "ATM");

            migrationBuilder.AlterColumn<bool>(
                name: "Enable",
                table: "Users",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "PaymentDestionations",
                keyColumn: "Id",
                keyValue: "WALLET",
                columns: new[] { "PartnerId", "SortIndex" },
                values: new object[] { "VnPay", 5 });

            migrationBuilder.InsertData(
                table: "PaymentDestionations",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DataTransactionId", "ExternalId", "Group", "Image", "IsDeleted", "LastUpdatedAt", "LastUpdatedBy", "Name", "OtherName", "ParentId", "PartnerId", "SortIndex" },
                values: new object[] { "VnPay_ATM", null, null, "", "ATM", "", "https://s3.cloud.cmctelecom.vn/bank/637806135041944131ABBANK.svg", false, null, null, "Thanh toán qua ATM", "", "", "VnPay", 1 });
        }
    }
}
