using System.IO;
using Microsoft.AspNetCore.Mvc;
using StoringImages.Models;

namespace StoringImages.Controllers
{
    public class HomeController : Controller
    {
        //* Static Properties
        public static ImageHolder ImageHolder = new ImageHolder();

        //* Public Methods
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Upload() => View(new ImageHolder());

        [HttpPost]
        public IActionResult Upload(ImageHolder holder)
        {
            if (holder.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    holder.ImageFile.CopyTo(memoryStream);
                    ImageHolder.ImageArray = memoryStream.ToArray();
                }
            }

            return RedirectToAction(nameof(HomeController.ViewImage));
        }

        public IActionResult ViewImage() => View(ImageHolder);
    }
}