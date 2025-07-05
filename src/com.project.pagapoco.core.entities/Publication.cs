using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.project.pagapoco.core.entities
{
    [Table("tbl_publication")]
    public class Publication
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("code_publication")]
        public long CodePublication { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
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

    }
}
