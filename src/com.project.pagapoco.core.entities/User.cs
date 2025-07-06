using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace com.project.pagapoco.core.entities
{
    [Table("tbl_user")]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("dni")]
        public long Dni { get; set; }

        [Required]
        [Column("first_name", TypeName = "varchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name", TypeName = "varchar(50)")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("password", TypeName = "varchar(150)")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("salt", TypeName = "varchar(50)")]
        public string Salt { get; set; }

        public ICollection<Publication> Publications { get; set; }

        public User() { }

        public User(long dni, string firstName, string lastName, string email, string password)
        {
            Dni = dni;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}
