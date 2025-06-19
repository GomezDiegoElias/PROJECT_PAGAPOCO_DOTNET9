using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace com.project.pagapoco.core.entities
{
    [Table("tbl_user")]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("first_name", TypeName = "varchar(50)"), NotNull]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "varchar(50)")]
        public string LastName { get; set; }
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(150)"), NotNull]
        public string Password { get; set; }

        public User() { }
        public User(int id, string firstName, string lastName, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

    }
}
