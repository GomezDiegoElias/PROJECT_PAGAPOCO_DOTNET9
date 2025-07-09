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

        // DbSets: representan las tablas de la base de datos
        public DbSet<User> Users { get; set; } // DbSet para la entidad User

        public DbSet<Publication> Publications { get; set; } // DbSet para la entidad Publication

        // Metodo para obtener la paginacion de publicaciones
        public async Task<PaginatedResponse<Publication>> getPublicationPagination(int pageIndex, int pageSize)
        {

            var publications = new List<Publication>();
            var totalCount = 0;

            // Conexion a traves de la cadena de conexión definida en Database
            using var connection = new SqlConnection(Database.GetConnectionString());

            // Abre la conexión de forma asíncrona
            await connection.OpenAsync();

            // Crea un comando para ejecutar el procedimiento almacenado
            using var command = new SqlCommand("getPublicationPagination", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PageIndex", pageIndex);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            // Agrega un parámetro de salida para el total de filas
            using var reader = await command.ExecuteReaderAsync();

            // Verifica si el lector tiene filas para leer
            while (await reader.ReadAsync())
            {
                // Crea una nueva instancia de Publication y la agrega a la lista
                publications.Add(new Publication
                {
                    Id = reader.GetInt64("Id"),
                    CodePublication = reader.GetInt64("Code"),
                    Title = reader.GetString("Title"),
                    Description = reader.GetString("Description"),
                    Price = reader.GetDecimal("price"),
                    ImageUrl = reader.IsDBNull("ImageUrl") ? null : reader.GetString("ImageUrl"),
                    Brand = reader.GetString("Brand"),
                    Model = reader.GetString("Model"),
                    Year = reader.GetInt32("Year"),
                    CreatedAt = reader.GetDateTime("createAt"),
                    UpdatedAt = reader.GetDateTime("updateAt"),
                    UserId = reader.GetInt32("userId")
                });

                // Obtiene el total solo una vez
                if (totalCount == 0)
                {
                    totalCount = reader.GetInt32("TotalFilas");
                }

            }

            await connection.CloseAsync(); // Cierra la conexión de forma asíncrona

            // Devuelve la respuesta paginada con las publicaciones y el total de filas
            return new PaginatedResponse<Publication>
            {
                Items = publications,
                TotalCount = totalCount
            };

        }

        // Metodo para obtener la paginación de usuarios
        public async Task<PaginatedResponse<User>> getUserPagination(int pageIndex, int pageSize)
        {

            var users = new List<User>();
            var totalCount = 0;

            // Conexion a traves de la cadena de conexión definida en Database
            using var connection = new SqlConnection(Database.GetConnectionString());
            // Abre la conexión de forma asíncrona
            await connection.OpenAsync();

            // Crea un comando para ejecutar el procedimiento almacenado
            using var command = new SqlCommand("getUserPagination", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PageIndex", pageIndex);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            // Agrega un parámetro de salida para el total de filas
            using var reader = await command.ExecuteReaderAsync();

            // Verifica si el lector tiene filas para leer
            while (await reader.ReadAsync())
            {
                // Crea una nueva instancia de User y la agrega a la lista
                users.Add(new User
                {
                    Id = reader.GetInt32("Id"),
                    Dni = reader.GetInt64("Dni"),
                    FirstName = reader.GetString("FirstName"),
                    LastName = reader.IsDBNull("LastName") ? null : reader.GetString("LastName"),
                    Email = reader.GetString("Email"),
                    Password = reader.GetString("Password")
                });

                // Obtiene el total solo una vez
                if (totalCount == 0)
                {
                    totalCount = reader.GetInt32("TotalFilas");
                }
            }

            await connection.CloseAsync(); // Cierra la conexión de forma asíncrona

            // Devuelve la respuesta paginada con los usuarios y el total de filas
            return new PaginatedResponse<User>
            {
                Items = users,
                TotalCount = totalCount
            };
        }

        // Configuración del modelo de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuración de las entidades y sus relaciones
            // Por ejemplo, establecer claves primarias, índices únicos, etc.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Dni)
                .IsUnique();

            modelBuilder.Entity<Publication>()
                .HasIndex(p => p.CodePublication)
                .IsUnique();

            //modelBuilder.Entity<UserPaginationResult>()
            //    .HasNoKey()
            //    .ToView(null);

        }

    }
}
