using com.project.pagapoco.core.entities;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
