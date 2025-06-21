using com.project.pagapoco.core.entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public async Task<List<User>> getUserPagination(int pageIndex, int pageSize)
        {
            var parameters = new[]
            {
                    new SqlParameter("@Param1", pageIndex),
                    new SqlParameter("@Param2", pageSize)
                };

            return await Users
                .FromSqlRaw("EXEC getUserPagination @Param1, @Param2", parameters)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
