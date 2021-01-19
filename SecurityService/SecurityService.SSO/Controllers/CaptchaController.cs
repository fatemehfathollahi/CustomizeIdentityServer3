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
            bool requireCaptcha = true;//Convert.ToBoolean( ConfigurationManager.AppSettings["RequireCaptcha"]);
            return requireCaptcha ? 1 : 0;
        }

        public HttpResponseMessage Get()
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
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
            

            //HttpContext.Current.Response.ContentType = "image/jpeg";
            //var mm = HttpContext.Current.Response.OutputStream;
            //bitmap.Save(mm, ImageFormat.Jpeg);


            MemoryStream ms;
            ImageCodecInfo codec = GetEncoderInfo("image/jpeg");
            using (EncoderParameters ep = new EncoderParameters())
            {
                ep.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                // Encode the image
                using ( ms = new MemoryStream())
                {
                    bitmap.Save(ms, codec, ep);

                    // Send the encoded image to the browser
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "image/jpeg";
                    ms.WriteTo(HttpContext.Current.Response.OutputStream);
                }
            }
          
             httpResponseMessage.Content = new StreamContent(ms);
            return httpResponseMessage;

        }

        private  ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void DrawLine(Bitmap bmp)
        {
            var random = new Random();
            for (var i = 0; i < 17; i++)
            {
                var x1 = random.Next(10, Width-10);
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
            IdentityUserService.CaptchaStorage.Add(requestId, salt);
        }

        private int CreateSalt()
        {
            var random = new Random();
            return random.Next(1000, 9999);
        }


        //public ActionResult CaptchaImage(string prefix, bool noisy = true)
        //{
        //    var rand = new Random((int)DateTime.Now.Ticks);
        //    //generate new question
        //    int a = rand.Next(10, 99);
        //    int b = rand.Next(0, 9);
        //    var captcha = string.Format("{0} + {1} = ?", a, b);

        //    //store answer
        //   // Session["Captcha" + prefix] = a + b;

        //    //image stream
        //    FileContentResult img = null;

        //    using (var mem = new MemoryStream())
        //    using (var bmp = new Bitmap(130, 30))
        //    using (var gfx = Graphics.FromImage((Image)bmp))
        //    {
        //        gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        //        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        //        gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

        //        //add noise
        //        if (noisy)
        //        {
        //            int i, r, x, y;
        //            var pen = new Pen(Color.Yellow);
        //            for (i = 1; i < 10; i++)
        //            {
        //                pen.Color = Color.FromArgb(
        //                (rand.Next(0, 255)),
        //                (rand.Next(0, 255)),
        //                (rand.Next(0, 255)));

        //                r = rand.Next(0, (130 / 3));
        //                x = rand.Next(0, 130);
        //                y = rand.Next(0, 30);

        //                gfx.DrawEllipse(pen, x - r, y - r, r, r);
        //            }
        //        }

        //        //add question
        //        gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

        //        //render as Jpeg
        //        bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        img = this.File(mem.GetBuffer(), "image/Jpeg");
        //    }

        //    return img;
        //}

        //public ActionResult CaptchaImage(string prefix)
        //{
        //    return View();
        //}

        #region captcha
        //private string GetRandomText()
        //{
        //    StringBuilder randomText = new StringBuilder();
        //    string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
        //    Random r = new Random();
        //    for (int j = 0; j <= 5; j++)
        //    {
        //        randomText.Append(alphabets[r.Next(alphabets.Length)]);
        //    }
        //    return randomText.ToString();
        //}


        //public ActionResult CustomCaptcha()
        //{
        //    Session["CAPTCHA"] = GetRandomText();
        //    return View();
        //}


        //public FileResult GetCaptchaImage()
        //{
        //    string text = Session["CAPTCHA"].ToString();

        //    //first, create a dummy bitmap just to get a graphics object
        //    Image img = new Bitmap(1, 1);
        //    Graphics drawing = Graphics.FromImage(img);

        //    Font font = new Font("Arial", 15);
        //    //measure the string to see how big the image needs to be
        //    SizeF textSize = drawing.MeasureString(text, font);

        //    //free up the dummy image and old graphics object
        //    img.Dispose();
        //    drawing.Dispose();

        //    //create a new image of the right size
        //    img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
        //    drawing = Graphics.FromImage(img);

        //    Color backColor = Color.SeaShell;
        //    Color textColor = Color.Red;
        //    //paint the background
        //    drawing.Clear(backColor);

        //    //create a brush for the text
        //    Brush textBrush = new SolidBrush(textColor);

        //    drawing.DrawString(text, font, textBrush, 20, 10);

        //    drawing.Save();

        //    font.Dispose();
        //    textBrush.Dispose();
        //    drawing.Dispose();

        //    MemoryStream ms = new MemoryStream();
        //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    img.Dispose();

        //    return File(ms.ToArray(), "image/png");
        //}



        #endregion

    }
}