using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;

namespace com.project.pagapoco.core.business.Service
{
    public interface IAuthService
    {
        public Task<AuthResponse> Authenticate(string email, string password);
    }
}
