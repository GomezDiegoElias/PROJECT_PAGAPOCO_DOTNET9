using System.Data;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Publication> Publications { get; set; }

        public async Task<PaginatedResponse<User>> getUserPagination(int pageIndex, int pageSize)
        {
            var users = new List<User>();
            var totalCount = 0;

            using var connection = new SqlConnection(Database.GetConnectionString());
            await connection.OpenAsync();

            using var command = new SqlCommand("getUserPagination", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PageIndex", pageIndex);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("Id"),
                    Dni = reader.GetInt64("Dni"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.IsDBNull("LastName") ? null : reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    Password = reader.GetString("Password")
                });

                // Obtener el total solo una vez
                if (totalCount == 0)
                {
                    totalCount = reader.GetInt32("TotalFilas");
                }
            }

            return new PaginatedResponse<User>
            {
                Items = users,
                TotalCount = totalCount
            };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Dni)
                .IsUnique();

            modelBuilder.Entity<Publication>()
                .HasIndex(p => p.CodePublication)
                .IsUnique();

            modelBuilder.Entity<UserPaginationResult>()
                .HasNoKey()
                .ToView(null);

        }

    }
}
