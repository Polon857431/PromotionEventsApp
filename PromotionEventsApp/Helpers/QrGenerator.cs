using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace PromotionEventsApp.Helpers
{
    public class QrGenerator
    {
        public static Bitmap GenerateCode(string code,int pixelsPerModule)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(pixelsPerModule);
        }

        public static byte[] GetCode(string code, int pixelsPerModule)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                GenerateCode(code, pixelsPerModule).Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}