using com.project.pagapoco.core.business.Service.Imp;
using Microsoft.AspNetCore.Http;
using QRCoder;

namespace com.project.pagapoco.core.business.Service
{
    public class QrLoginService : IQrLoginService
    {

        public string GenerateQrUrl(Guid qrSessionId, HttpRequest request)
        {
            //return $"{request.Scheme}://{request.Host}/api/qr/login?sessionId={qrSessionId}";
            return $"https://gestordepresupuestosmecanica.netlify.app/api/qr/login?sessionId={qrSessionId}";
        }

        public byte[] GenerateQrImage(string qrUrl)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

    }
}
