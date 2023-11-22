using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChillsRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeNameinorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Name", "Photo", "Price", "Status" },
                values: new object[,]
                {
                    { new Guid("2fed4630-6f79-4d39-8b2e-ea1ed4a97021"), "Burgers", "Delicious vegan burger.", "Veggie Burger", "Hambuegervagena.png", 8.99m, "enable" },
                    { new Guid("378c7a5f-1601-473e-9107-f0cbcaeaa2b3"), "Burgers", "Delicious burger with meat, lettuce, tomato and cheese.", "Classic Burger", "Hambuerger.png", 5.99m, "enable" },
                    { new Guid("3d298713-520f-405f-9ddc-c02e8ac6a742"), "Desserts", "Delicious cheesecake with cream cheese, fresh cheese", "Cheescake", "cheescake.png", 5.99m, "enable" },
                    { new Guid("5afda841-9152-461b-9fba-7384d5b68183"), "Pasta", "Delicious Lasagna with cream cheese, cheese and red sauce.", "Lasagna", "lasagna.png", 14.99m, "enable" },
                    { new Guid("60895f92-d4ff-4fc3-9f82-3bc52240be6f"), "Pasta", "Delicious Alfredo Pasta with white sauce.", "Alfredo Pasta", "alfredo.png", 14.99m, "enable" },
                    { new Guid("70241cd3-1a21-48c8-8332-85ae64ac1b18"), "Burgers", "Delicious burger with bbq sauce and bacon.", "BBQ Burger", "bbqbuerger.png", 9.99m, "enable" },
                    { new Guid("9e921cda-1d9a-436e-a007-afcfeae0fe42"), "Pasta", "Delicious Spaghetti with red sauce.", "Spaghetti", "spagetti.png", 12.99m, "enable" },
                    { new Guid("c9710902-deeb-4944-becf-4541b0737e58"), "Desserts", "Delicious chocolate cake", "Chocolate Cake", "chocolate.png", 5.99m, "enable" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("2fed4630-6f79-4d39-8b2e-ea1ed4a97021"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("378c7a5f-1601-473e-9107-f0cbcaeaa2b3"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("3d298713-520f-405f-9ddc-c02e8ac6a742"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("5afda841-9152-461b-9fba-7384d5b68183"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("60895f92-d4ff-4fc3-9f82-3bc52240be6f"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("70241cd3-1a21-48c8-8332-85ae64ac1b18"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("9e921cda-1d9a-436e-a007-afcfeae0fe42"));

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: new Guid("c9710902-deeb-4944-becf-4541b0737e58"));

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Orders");

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
        }
    }
}
