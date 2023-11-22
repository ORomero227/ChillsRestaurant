using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChillsRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class OrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3521e99e-54af-4cd7-b6e8-d4463038a6cd"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("78204ad1-beb1-46d3-99f2-023ef0cb4597"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("84cadf1d-e464-4173-9a2d-01c4714f42fc"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("a2be83d6-ebd5-4a65-8772-aea94679e979"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("bc9eaf2e-6992-4620-ab85-ee52c3a6eb09"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c159a292-1e39-4555-a1d0-30a3f0a345cc"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c39c496a-2398-4e4d-9b7d-8843598e6564"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("d07a1eb5-39bb-469b-ab99-a655b3989ab9"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "MenuItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KitchenStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

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

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "Photo", "Price", "Status" },
                values: new object[,]
                {
                    { new Guid("3521e99e-54af-4cd7-b6e8-d4463038a6cd"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", "spagetti.png", 12.99m, "enable" },
                    { new Guid("78204ad1-beb1-46d3-99f2-023ef0cb4597"), "Burgers", "Delicious vegan burger.", "Veggie Burger", "Hambuegervagena.png", 8.99m, "enable" },
                    { new Guid("84cadf1d-e464-4173-9a2d-01c4714f42fc"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", "alfredo.png", 14.99m, "enable" },
                    { new Guid("a2be83d6-ebd5-4a65-8772-aea94679e979"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", "bbqbuerger.png", 9.99m, "enable" },
                    { new Guid("bc9eaf2e-6992-4620-ab85-ee52c3a6eb09"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", "lasagna.png", 14.99m, "enable" },
                    { new Guid("c159a292-1e39-4555-a1d0-30a3f0a345cc"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", "Hambuerger.png", 5.99m, "enable" },
                    { new Guid("c39c496a-2398-4e4d-9b7d-8843598e6564"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", "cheescake.png", 5.99m, "enable" },
                    { new Guid("d07a1eb5-39bb-469b-ab99-a655b3989ab9"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", "chocolate.png", 5.99m, "enable" }
                });
        }
    }
}
