using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Models
{
    public class FileModel
    {
        public IFormFile File { set; get; }
    }
}
