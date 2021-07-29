using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static System.Net.Mime.MediaTypeNames;/*
using Gma.QrCodeNet.Encoding.Windows.Render;
using Gma.QrCodeNet.Encoding;*/

namespace UTCAPPCMS.MVC.Helpers
{
    public class QRCodeService
    {
        /* protected void btnGenerate_Click()
         {
             string code = txtCode.Text;
             QRCodeGenerator qrGenerator = new QRCodeGenerator();
             QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
             System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
             imgBarCode.Height = 150;
             imgBarCode.Width = 150;
             using (Bitmap bitMap = qrCode.GetGraphic(20))
             {
                 using (MemoryStream ms = new MemoryStream())
                 {
                     bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                     byte[] byteImage = ms.ToArray();
                     System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                     img.Save(Server.MapPath("Images/") + "Test.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                     imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                 }
                 plBarCode.Controls.Add(imgBarCode);
             }
         }*/
      /*  public System.Drawing.Image GenerateQRCode(string content, int size)
        {
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode;
            encoder.TryEncode(content, out qrCode);

            GraphicsRenderer gRenderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Two), System.Drawing.Brushes.Black, System.Drawing.Brushes.White);
            //Graphics g = gRenderer.Draw(qrCode.Matrix);

            MemoryStream ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Bmp, ms);

            var imageTemp = new Bitmap(ms);

            var image = new Bitmap(imageTemp, new System.Drawing.Size(new System.Drawing.Point(size, size)));

            //image.Save("file.bmp", ImageFormat.Bmp);

            return (System.Drawing.Image)image;
        }*/
        public string SaveQRCodeAsImg(string qrText,int id)
        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);

            string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
            string filepath = "wwwroot/Files/QRCodeImgs/file-" + fileGuid +"Id"+id+ ".png";//id location 
            qrCodeData.SaveRawData(filepath, QRCodeData.Compression.Uncompressed);

            QRCodeData qrCodeData1 = new QRCodeData(filepath, QRCodeData.Compression.Uncompressed);

            QRCode qrCode = new QRCode(qrCodeData1);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            string extention = "png";

            string fname = (DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond + "." + extention).ToString();

            var path = Directory.GetCurrentDirectory();

            string targetPath2 = path + "/wwwroot/Files/QRCodeImgs/" + id;
            string saveDBPath = "/Files/QRCodeImgs/" + id + fname;

            if (!Directory.Exists(targetPath2))
            {
                DirectoryInfo di = Directory.CreateDirectory(targetPath2);

            }
            //
            qrCodeImage.Save(Path.Combine(targetPath2, fname));
            return saveDBPath;
        }
/*
        public IActionResult GenerateFile(string qrText)
        {
          QRCodeGenerator qrGenerator = new QRCodeGenerator();
          QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,   QRCodeGenerator.ECCLevel.Q);
  
          string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
  
          qrCodeData.SaveRawData("wwwroot/qrr/file-" + fileGuid + ".qrr", QRCodeData.Compression.Uncompressed);
  
          QRCodeData qrCodeData1 = new QRCodeData("wwwroot/qrr/file-" + fileGuid + ".qrr", QRCodeData.Compression.Uncompressed);
  
          QRCode qrCode = new QRCode(qrCodeData1);
          Bitmap qrCodeImage = qrCode.GetGraphic(20);
          return View(BitmapToBytes(qrCodeImage));
        }*/

        public string GenerateQRImage(string txtQRData)
        {
            byte[] ImageByte;

            QRCodeGenerator _QRCodeGenerator = new QRCodeGenerator();
            QRCodeData _QRCodeData = _QRCodeGenerator.CreateQrCode(txtQRData, QRCodeGenerator.ECCLevel.Q);
            QRCode _QRCode = new QRCode(_QRCodeData);

            Bitmap _QRCodeImage = _QRCode.GetGraphic(20);

            using (MemoryStream stream = new MemoryStream())
            {
                _QRCodeImage.Save(stream, ImageFormat.Png);

                ImageByte = stream.ToArray();
            }

            var baseImage = Convert.ToBase64String(ImageByte);
            
            return baseImage;
        }
    }
}
