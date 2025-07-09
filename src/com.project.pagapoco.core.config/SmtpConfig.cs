using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.project.pagapoco.core.config
{
    public class SmtpConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
    }
}
