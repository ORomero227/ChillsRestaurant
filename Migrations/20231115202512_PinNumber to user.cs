using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChillsRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class PinNumbertouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "PinNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PinNumber",
                table: "AspNetUsers");

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
    }
}
