using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.project.pagapoco.core.entities.Dto
{
    public class UserPaginationResult
    {
        public int Id { get; set; }
        public long Dni { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Fila { get; set; }
        public int TotalFilas { get; set; }
    }
}
