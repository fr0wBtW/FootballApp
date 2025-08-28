using Microsoft.EntityFrameworkCore;
using FootballApp.Models;

namespace FootballApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) { }
        
        public DbSet<FavoriteMatches> FavoriteMatches { get; set; }
    }
}
