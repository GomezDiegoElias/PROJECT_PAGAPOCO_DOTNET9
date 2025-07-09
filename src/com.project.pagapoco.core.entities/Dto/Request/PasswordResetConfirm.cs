using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.project.pagapoco.core.entities.Dto.Request
{
    public class PasswordResetConfirm
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
