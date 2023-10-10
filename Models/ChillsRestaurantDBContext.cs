using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChillsRestaurant.Models
{
    public class ChillsRestaurantDBContext : IdentityDbContext<ApplicationUser>
    {
        public ChillsRestaurantDBContext(DbContextOptions<ChillsRestaurantDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Classic Burger",
                    Price = 5.99m,
                    Category = "Burgers",
                    Photo = "Hambuerger.png",
                    Description= "Delicious burger with meat, lettuce, tomato and cheese."
                },
                new MenuItem 
                {
                    Id = Guid.NewGuid(),
                    Name = "Veggie Burger",
                    Price = 8.99m,
                    Category = "Burgers",
                    Photo = "Hambuegervagena.png",
                    Description = "Delicious vegan burger."
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "BBQ Burger",
                    Price = 9.99m,
                    Category = "Burgers",
                    Photo = "bbqbuerger.png",
                    Description = "Delicious burger with bbq sauce and bacon."
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Spaghetti",
                    Price = 12.99m,
                    Category = "Pasta",
                    Photo = "spagetti.png",
                    Description = "Delicious Spaghetti with red sauce."
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Alfredo Pasta",
                    Price = 14.99m,
                    Category = "Pasta",
                    Photo = "alfredo.png",
                    Description = "Delicious Alfredo Pasta with white sauce."
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Lasagna",
                    Price = 14.99m,
                    Category = "Pasta",
                    Photo = "lasagna.png",
                    Description = "Delicious Lasagna with cream cheese, cheese and red sauce.",
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Cheescake",
                    Price = 5.99m,
                    Category = "Desserts",
                    Photo = "cheescake.png",
                    Description = "Delicious cheesecake with cream cheese, fresh cheese",
                },
                new MenuItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Chocolate Cake",
                    Price = 5.99m,
                    Category = "Desserts",
                    Photo = "chocolate.png",
                    Description = "Delicious chocolate cake"
                }
            );


            base.OnModelCreating(builder);
        }

        public DbSet<MenuItem> MenuItems { get; set; }

    }
}
