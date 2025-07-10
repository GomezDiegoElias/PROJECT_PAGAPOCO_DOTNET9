using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace com.project.pagapoco.core.business.Service.Imp
{
    public interface IQrLoginService
    {
        string GenerateQrUrl(Guid qrSessionId, HttpRequest request);
        byte[] GenerateQrImage(string qrUrl);
    }
}
