using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaHipHop_API.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Admin_AdminId",
                table: "Order");

            migrationBuilder.AlterColumn<long>(
                name: "AdminId",
                table: "Order",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ExpiredDate",
                value: new DateTime(2024, 7, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2542));

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ExpiredDate",
                value: new DateTime(2024, 8, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2558));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 6, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2626));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 6, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2601), new DateTime(2024, 6, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2602) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 6, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2603), new DateTime(2024, 6, 14, 5, 10, 37, 801, DateTimeKind.Local).AddTicks(2604) });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Admin_AdminId",
                table: "Order",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Admin_AdminId",
                table: "Order");

            migrationBuilder.AlterColumn<long>(
                name: "AdminId",
                table: "Order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ExpiredDate",
                value: new DateTime(2024, 6, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ExpiredDate",
                value: new DateTime(2024, 7, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6855));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 5, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6927));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6901), new DateTime(2024, 5, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6903) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 5, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6906), new DateTime(2024, 5, 29, 21, 59, 52, 647, DateTimeKind.Local).AddTicks(6907) });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Admin_AdminId",
                table: "Order",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
