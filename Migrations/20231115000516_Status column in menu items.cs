using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChillsRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Statuscolumninmenuitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3aad9ec8-214e-4d1e-bff7-42f89bdf9c25"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("77c66861-ff97-4fd7-9f31-8db1781088c6"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7999ee28-28e1-4052-a316-3069543adc3c"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("7f2871e4-ffe6-4ba1-8046-1549c86900cd"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("a2984f54-af8f-4dc3-b65f-f08760886339"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c0a125c4-3b9c-413d-9756-b4a783f80cac"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c6d5dfbf-332a-4e6e-a0c3-0d9476c54f7e"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("fde86925-0fa4-45b9-a8fa-41e9e65d2a1b"));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "Photo", "Price", "Status" },
                values: new object[,]
                {
                    { new Guid("2a9d9a4f-40f9-4385-b867-98f37917fd85"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", "alfredo.png", 14.99m, "enable" },
                    { new Guid("62a94a19-0175-4de1-bac0-7e71dc6bce86"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", "spagetti.png", 12.99m, "enable" },
                    { new Guid("65aa863f-d9f5-4fb1-9731-f70c4a890b13"), "Burgers", "Delicious vegan burger.", "Veggie Burger", "Hambuegervagena.png", 8.99m, "enable" },
                    { new Guid("6a21ace7-1873-41c0-8701-ab863adababd"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", "chocolate.png", 5.99m, "enable" },
                    { new Guid("8b2f2da7-7eb5-4fcd-8c5b-5748171613bc"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", "bbqbuerger.png", 9.99m, "enable" },
                    { new Guid("ab881908-f59d-48c9-8553-6e1426193958"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", "lasagna.png", 14.99m, "enable" },
                    { new Guid("b077cabe-3492-4d08-8dfd-965d3dcac6d3"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", "cheescake.png", 5.99m, "enable" },
                    { new Guid("b295486f-ae34-48d7-abf8-c3ad9b14085e"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", "Hambuerger.png", 5.99m, "enable" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("2a9d9a4f-40f9-4385-b867-98f37917fd85"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("62a94a19-0175-4de1-bac0-7e71dc6bce86"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("65aa863f-d9f5-4fb1-9731-f70c4a890b13"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("6a21ace7-1873-41c0-8701-ab863adababd"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("8b2f2da7-7eb5-4fcd-8c5b-5748171613bc"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("ab881908-f59d-48c9-8553-6e1426193958"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("b077cabe-3492-4d08-8dfd-965d3dcac6d3"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("b295486f-ae34-48d7-abf8-c3ad9b14085e"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MenuItems");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "Photo", "Price" },
                values: new object[,]
                {
                    { new Guid("3aad9ec8-214e-4d1e-bff7-42f89bdf9c25"), "Burgers", "Delicious vegan burger.", "Veggie Burger", "Hambuegervagena.png", 8.99m },
                    { new Guid("77c66861-ff97-4fd7-9f31-8db1781088c6"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", "bbqbuerger.png", 9.99m },
                    { new Guid("7999ee28-28e1-4052-a316-3069543adc3c"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", "lasagna.png", 14.99m },
                    { new Guid("7f2871e4-ffe6-4ba1-8046-1549c86900cd"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", "cheescake.png", 5.99m },
                    { new Guid("a2984f54-af8f-4dc3-b65f-f08760886339"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", "chocolate.png", 5.99m },
                    { new Guid("c0a125c4-3b9c-413d-9756-b4a783f80cac"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", "spagetti.png", 12.99m },
                    { new Guid("c6d5dfbf-332a-4e6e-a0c3-0d9476c54f7e"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", "alfredo.png", 14.99m },
                    { new Guid("fde86925-0fa4-45b9-a8fa-41e9e65d2a1b"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", "Hambuerger.png", 5.99m }
                });
        }
    }
}
