using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GaHipHop_API.Migrations
{
    /// <inheritdoc />
    public partial class GaHipHop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Facebook = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instagram = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tiktok = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Shoppee = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Percent = table.Column<float>(type: "float", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Province = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Wards = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AdminId = table.Column<long>(type: "bigint", nullable: true),
                    OrderRequirement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalPrice = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<long>(type: "bigint", nullable: false),
                    DiscountId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductPrice = table.Column<double>(type: "double", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kind",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ColorName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kind", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kind_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KindId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false),
                    OrderPrice = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Kind_KindId",
                        column: x => x.KindId,
                        principalTable: "Kind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "Status" },
                values: new object[,]
                {
                    { 1L, "Category 1", true },
                    { 2L, "Category 2", true }
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Email", "Facebook", "Instagram", "Phone", "Shoppee", "Tiktok" },
                values: new object[] { 1L, "phatdao@gmail.com", "https://www.facebook.com/nguyen.rosie.946", "https://www.instagram.com/ga.hiphop?fbclid=IwZXh0bgNhZW0CMTAAAR29xxUdp_tZ0TqVPfLq5zQVl72SEG7E0SpmIJY8ED62ZmmsKdaTZrc51O4_aem_-LACUZgsYTo5T1fMMgEhjg", "0855005005", "https://shopee.vn/ga.hiphop", "https://www.tiktok.com/@ga.hiphop?fbclid=IwZXh0bgNhZW0CMTAAAR18hmJoFZrOpcozoCaEq74luSuL4Y84xGSBJ3bnlgjRlYhB3xUaRy4YE3Y_aem_494gWYY8v90XRrvald7BQA" });

            migrationBuilder.InsertData(
                table: "Discount",
                columns: new[] { "Id", "ExpiredDate", "Percent", "Status" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 8, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(2433), 0f, true },
                    { 2L, new DateTime(2024, 8, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(2476), 10f, true },
                    { 3L, new DateTime(2024, 8, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(2478), 15f, true },
                    { 4L, new DateTime(2024, 9, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(2485), 20f, true }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1L, "Admin" },
                    { 2L, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "Id", "Address", "Email", "Phone", "Province", "UserName", "Wards" },
                values: new object[] { 1L, "Address 1", "user1@example.com", "123456789", "Province 1", "user1", "Wards 1" });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "Address", "Email", "FullName", "Password", "Phone", "RoleId", "Status", "Username" },
                values: new object[,]
                {
                    { 1L, "Admin Address", "admin@example.com", "Admin User", "$2a$12$W6Fr2O.JNK5pxRlm36q1DeLI.hDM5AhHe..ZnhFhofXKwSNUrMTA.", "123456789", 1L, true, "admin" },
                    { 2L, "Manager Address", "admin@example.com", "Manager User", "$2a$12$W6Fr2O.JNK5pxRlm36q1DeLI.hDM5AhHe..ZnhFhofXKwSNUrMTA.", "123456789", 2L, true, "manager" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "AdminId", "CreateDate", "OrderCode", "OrderRequirement", "PaymentMethod", "Status", "TotalPrice", "UserId" },
                values: new object[] { 1L, 1L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3723), "ORD001", "Requirement 1", "Credit Card", "Confirmed", 100.0, 1L });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "AdminId", "CategoryId", "CreateDate", "DiscountId", "ModifiedDate", "ProductDescription", "ProductName", "ProductPrice", "Status", "StockQuantity" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3650), 1L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3658), "Nilou", "Figure", 50000.0, true, 100 },
                    { 2L, 1L, 1L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3661), 1L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3662), "Shenhe", "Figure", 1000000.0, true, 500 },
                    { 3L, 1L, 1L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3665), 2L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3665), "Shenhe", "Figure", 1000000.0, true, 150 },
                    { 4L, 1L, 2L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3667), 3L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3668), "Shenhe", "Figure", 1000000.0, true, 250 },
                    { 5L, 1L, 2L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3671), 4L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3671), "Shenhe", "Figure", 1000000.0, true, 550 },
                    { 6L, 1L, 2L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3673), 4L, new DateTime(2024, 7, 17, 22, 13, 10, 900, DateTimeKind.Local).AddTicks(3674), "Shenhe", "Figure", 1000000.0, true, 1000 }
                });

            migrationBuilder.InsertData(
                table: "Kind",
                columns: new[] { "Id", "ColorName", "Image", "ProductId", "Quantity", "Status" },
                values: new object[,]
                {
                    { 1L, "Red", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 1L, 50, true },
                    { 2L, "Blue", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 1L, 50, true },
                    { 3L, "White", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 2L, 500, true },
                    { 4L, "Yellow", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 3L, 150, true },
                    { 5L, "Pink", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 4L, 250, true },
                    { 6L, "Purple", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 5L, 550, true },
                    { 7L, "Orange", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 6L, 500, true },
                    { 8L, "Green", "https://firebasestorage.googleapis.com/v0/b/gahiphop-4de10.appspot.com/o/images%2F61104088-d946-4d5b-80fc-427f8ab3690a_GaHipHop.jpg?alt=media&token=a9dca6bb-40a1-4935-aaf8-e10d55820ac2", 6L, 500, true }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "KindId", "OrderId", "OrderPrice", "OrderQuantity" },
                values: new object[] { 1L, 1L, 1L, 100.0, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RoleId",
                table: "Admin",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Kind_ProductId",
                table: "Kind",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AdminId",
                table: "Order",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_KindId",
                table: "OrderDetails",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AdminId",
                table: "Product",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DiscountId",
                table: "Product",
                column: "DiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Kind");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
