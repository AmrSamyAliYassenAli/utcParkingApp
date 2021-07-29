using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTCAPPCMS.DAL.Models;

namespace UTCAPPCMS.MVC.Helpers
{
    public class FileUpload
    {
        private readonly IWebHostEnvironment _iwebHost;
        public FileUpload(IWebHostEnvironment _iwebHost)
        {
            this._iwebHost = _iwebHost;
        }
        // take IFormFile of Logo & Invoice Logo and retutn string logoPath & InvoiceLogoPath to set in database
        public async Task<string> UploadImg (IFormFile ifile,string path)
        {
            string uniqefilename = DateTime.Now.ToString();
            string imgext = Path.GetExtension(ifile.FileName);
            if (imgext == ".jpg" || imgext == ".gif")
            {
                var saveimg = Path.Combine(_iwebHost.WebRootPath, "Images", ifile.FileName);
                var stream = new FileStream(saveimg, FileMode.Create);
                await ifile.CopyToAsync(stream);
                
              //  using (FileStream fs = File.Create(String.Concat(uniqefilename + saveimg))) 
                path = saveimg;
                return path;
            }
            else
            {
                return "";
            }   
        }
    }
}
