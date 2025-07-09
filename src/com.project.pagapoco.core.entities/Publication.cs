using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.project.pagapoco.core.entities
{
    [Table("tbl_publication")]
    public class Publication
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_publication")]
        public long Id { get; set; }

        [Required]
        [Column("code_publication")]
        public long CodePublication { get; set; }

        [Required]
        [StringLength(20)]
        [Column("title", TypeName = "varchar(100)")]
        public string Title { get; set; } = string.Empty;

        [Column("description", TypeName = "varchar(500)")]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [Column("price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }

        [Required]
        [Column("brand", TypeName = "varchar(50)")]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [Column("model", TypeName = "varchar(50)")]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Column("year")]
        public int Year { get; set; }

        [Column("create_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        public Publication() { }

        public Publication(long codePublication, string title, string description, decimal price, string brand, string model, int year, int userId)
        {
            CodePublication = codePublication;
            Title = title;
            Description = description;
            Price = price;
            Brand = brand;
            Model = model;
            Year = year;
            UserId = userId;
        }

        public Publication(string title, string description, decimal price, string brand, string model, int year)
        {
            Title = title;
            Description = description;
            Price = price;
            Brand = brand;
            Model = model;
            Year = year;
        }

    }
}
