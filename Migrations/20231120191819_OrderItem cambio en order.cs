using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChillsRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemcambioenorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("218ea3ac-8dee-40d9-b276-47684ce31fc0"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("6b043297-e636-4283-a3f9-da8a59510a72"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("6dfe07aa-3fd1-4877-bd20-692646444af2"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("79c240ef-aa36-4e03-85b5-d2f0cb61e648"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("8973d5ea-ee9e-4a93-8b15-db3ca082a3bd"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("cf99a257-6c23-4865-b03f-02e7424ede10"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("d6d0183e-c78f-4667-983c-94a8fc7b6b38"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("f133338c-8f8a-4b39-913f-26c5fa871a83"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MenuItems");

            migrationBuilder.AddColumn<decimal>(
                name: "orderTotal",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "Photo", "Price", "Status" },
                values: new object[,]
                {
                    { new Guid("1efc8489-8acd-4d1c-a6e8-2bcef0c514d4"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", "alfredo.png", 14.99m, "enable" },
                    { new Guid("37fec382-3352-4e47-98c7-cfc99c7527a9"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", "spagetti.png", 12.99m, "enable" },
                    { new Guid("663513f7-9fe4-4874-b3f8-8094ee681b51"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", "Hambuerger.png", 5.99m, "enable" },
                    { new Guid("731104e9-6f67-4903-b1c0-24cf61ccdff6"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", "chocolate.png", 5.99m, "enable" },
                    { new Guid("8eb59f21-ba8e-4d87-acea-73840d0b53d5"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", "lasagna.png", 14.99m, "enable" },
                    { new Guid("b4316847-d474-4639-a790-b53cf7362115"), "Burgers", "Delicious vegan burger.", "Veggie Burger", "Hambuegervagena.png", 8.99m, "enable" },
                    { new Guid("e89fa3cd-641a-4c15-af1c-1013d17819db"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", "bbqbuerger.png", 9.99m, "enable" },
                    { new Guid("fe2f2532-1d22-498c-977b-073460456278"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", "cheescake.png", 5.99m, "enable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("1efc8489-8acd-4d1c-a6e8-2bcef0c514d4"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("37fec382-3352-4e47-98c7-cfc99c7527a9"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("663513f7-9fe4-4874-b3f8-8094ee681b51"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("731104e9-6f67-4903-b1c0-24cf61ccdff6"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("8eb59f21-ba8e-4d87-acea-73840d0b53d5"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("b4316847-d474-4639-a790-b53cf7362115"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("e89fa3cd-641a-4c15-af1c-1013d17819db"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("fe2f2532-1d22-498c-977b-073460456278"));

            migrationBuilder.DropColumn(
                name: "orderTotal",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "OrderId", "Photo", "Price", "Status" },
                values: new object[,]
                {
                    { new Guid("218ea3ac-8dee-40d9-b276-47684ce31fc0"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", null, "cheescake.png", 5.99m, "enable" },
                    { new Guid("6b043297-e636-4283-a3f9-da8a59510a72"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", null, "lasagna.png", 14.99m, "enable" },
                    { new Guid("6dfe07aa-3fd1-4877-bd20-692646444af2"), "Burgers", "Delicious vegan burger.", "Veggie Burger", null, "Hambuegervagena.png", 8.99m, "enable" },
                    { new Guid("79c240ef-aa36-4e03-85b5-d2f0cb61e648"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", null, "Hambuerger.png", 5.99m, "enable" },
                    { new Guid("8973d5ea-ee9e-4a93-8b15-db3ca082a3bd"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", null, "spagetti.png", 12.99m, "enable" },
                    { new Guid("cf99a257-6c23-4865-b03f-02e7424ede10"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", null, "bbqbuerger.png", 9.99m, "enable" },
                    { new Guid("d6d0183e-c78f-4667-983c-94a8fc7b6b38"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", null, "chocolate.png", 5.99m, "enable" },
                    { new Guid("f133338c-8f8a-4b39-913f-26c5fa871a83"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", null, "alfredo.png", 14.99m, "enable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
