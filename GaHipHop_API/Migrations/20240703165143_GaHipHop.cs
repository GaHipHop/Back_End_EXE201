using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GaHipHop_API.Migrations
{
    /// <inheritdoc />
    public partial class GaHipHop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ExpiredDate",
                value: new DateTime(2024, 8, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(7742));

            migrationBuilder.UpdateData(
                table: "Discount",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ExpiredDate",
                value: new DateTime(2024, 9, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(7768));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 7, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(8533));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(8489), new DateTime(2024, 7, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(8491) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(8496), new DateTime(2024, 7, 3, 23, 51, 43, 191, DateTimeKind.Local).AddTicks(8497) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
