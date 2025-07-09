using com.project.pagapoco.core.config;
using MimeKit;
using MailKit.Net.Smtp;

namespace com.project.pagapoco.core.business.Service
{
    public class EmailService
    {

        private readonly SmtpConfig _smtpConfig;

        public EmailService(SmtpConfig smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Soporte Pagapoco", _smtpConfig.From));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Recuperación de contraseña";

            message.Body = new TextPart("plain")
            {
                Text = $"Hacé clic en el siguiente enlace para restablecer tu contraseña:\n{resetLink}\nEste enlace expira en 30 minutos."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, _smtpConfig.UseSsl);
            await client.AuthenticateAsync(_smtpConfig.User, _smtpConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

        }
    }
}
