using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebPages;
using IdentityServer3.Core.ViewModels;
using SecurityService.SSO.IdentityService;

namespace SecurityService.SSO.Controllers
{
   
    public class CaptchaController : ApiController
    {
        private const int Width = 200;
        private const int Height = 34;

        public int Get(string isactive)
        {
            var requireCaptcha = ConfigurationManager.AppSettings["RequireCaptcha"].AsBool();
            return 1;//requireCaptcha ? 1 : 0;
        }

        public HttpResponse Get()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            Graphics gfxCaptchaImage = Graphics.FromImage(bitmap);
            gfxCaptchaImage.PageUnit = GraphicsUnit.Pixel;
            gfxCaptchaImage.SmoothingMode = SmoothingMode.HighQuality;
            gfxCaptchaImage.Clear(Color.White);

            var salt = CreateSalt();
            StoreSalt(salt);


            var randomString = "          " + salt;

            var format = new StringFormat();
            var faLcid = new System.Globalization.CultureInfo("fa-IR").LCID;
            format.SetDigitSubstitution(faLcid, StringDigitSubstitute.National);
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            format.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            Font font = new Font("Tahoma", 18);


            GraphicsPath path = new GraphicsPath();


            path.AddString(randomString,
                font.FontFamily,
                (int)font.Style,
                (gfxCaptchaImage.DpiY * font.SizeInPoints / 72),
                new Rectangle(0, 0, Width, Height), format);



            DrawLine(bitmap);

            gfxCaptchaImage.DrawPath(Pens.Crimson, path);
            var distortion = new Random().Next(-8, 8);
            using (var copy = (Bitmap)bitmap.Clone())
            {
                for (var y = 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        var newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
                        var newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
                        if (newX < 0 || newX >= Width) newX = 0;
                        if (newY < 0 || newY >= Height) newY = 0;
                        bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }


            gfxCaptchaImage.DrawImage(bitmap, new Point(0, 0));

            gfxCaptchaImage.Flush();


            HttpContext.Current.Response.ContentType = "image/jpeg";
            bitmap.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);

            return HttpContext.Current.Response;
        }

        private void DrawLine(Bitmap bmp)
        {
            var random = new Random();
            for (var i = 0; i < 17; i++)
            {
                var x1 = random.Next(10, Width - 10);
                var y1 = random.Next(10, Height - 10);
                var x2 = random.Next(10, Width - 10);
                var y2 = random.Next(10, Height - 10);
                var randomColor = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                var blackPen = new Pen(randomColor, 1);
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }
            }
        }

        private void StoreSalt(int salt)
        {
            var query = HttpContext.Current.Request.UrlReferrer?.Query;
            var requestId = query?.Substring(8, query.Length - 8) ?? "";
            IdentityUserService.CaptchaStorage.Remove(requestId);
            IdentityUserService.CaptchaStorage.Add(requestId, salt.ToString());
        }

        private int CreateSalt()
        {
            var random = new Random();
            return random.Next(1000, 9999);
        }

    }
}