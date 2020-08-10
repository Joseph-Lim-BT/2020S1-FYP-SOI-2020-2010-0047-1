using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmarterTomorrow.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetImage(string id)
        {
            
            var path = Path.Combine("/Images", id + ".jpg");
            return base.File(path, "image/jpeg");
        }
        public IActionResult Magic()
        {
            return View();
        }
    }
}