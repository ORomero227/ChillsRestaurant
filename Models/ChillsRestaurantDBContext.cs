using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChillsRestaurant.Models
{
    public class ChillsRestaurantDBContext : IdentityDbContext<ApplicationUser>
    {
        public ChillsRestaurantDBContext(DbContextOptions<ChillsRestaurantDBContext> options) : base(options)
        {

        }
    }
}
