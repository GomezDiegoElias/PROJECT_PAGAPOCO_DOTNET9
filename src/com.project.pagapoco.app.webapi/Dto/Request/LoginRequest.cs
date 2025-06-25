using System.ComponentModel.DataAnnotations;

namespace com.project.pagapoco.app.webapi.Dto.Request
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
