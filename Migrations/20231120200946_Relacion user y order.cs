using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChillsRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class Relacionuseryorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "Photo", "Price", "Status" },
                values: new object[,]
                {
                    { new Guid("0de945e8-97d4-4cf2-93f9-814fdce954f8"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", "spagetti.png", 12.99m, "enable" },
                    { new Guid("177ec6c6-ff32-46aa-a675-467012ef2a72"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", "chocolate.png", 5.99m, "enable" },
                    { new Guid("36050c6b-2299-499c-94a8-8ff256c05b5f"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", "cheescake.png", 5.99m, "enable" },
                    { new Guid("c666021a-668c-4ec3-916a-4ab50c170ee8"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", "alfredo.png", 14.99m, "enable" },
                    { new Guid("d18abacc-e94c-4932-84e6-9d121d19571f"), "Burgers", "Delicious vegan burger.", "Veggie Burger", "Hambuegervagena.png", 8.99m, "enable" },
                    { new Guid("dd1fee6e-d0a0-4923-a7e4-c0cb3678d174"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", "lasagna.png", 14.99m, "enable" },
                    { new Guid("e4e067bc-5a5c-4b6a-9177-92fea95aa768"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", "bbqbuerger.png", 9.99m, "enable" },
                    { new Guid("ea613d55-197e-4594-b3b3-1f2250f2b3d3"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", "Hambuerger.png", 5.99m, "enable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("0de945e8-97d4-4cf2-93f9-814fdce954f8"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("177ec6c6-ff32-46aa-a675-467012ef2a72"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("36050c6b-2299-499c-94a8-8ff256c05b5f"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c666021a-668c-4ec3-916a-4ab50c170ee8"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("d18abacc-e94c-4932-84e6-9d121d19571f"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("dd1fee6e-d0a0-4923-a7e4-c0cb3678d174"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("e4e067bc-5a5c-4b6a-9177-92fea95aa768"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("ea613d55-197e-4594-b3b3-1f2250f2b3d3"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

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
        }
    }
}
