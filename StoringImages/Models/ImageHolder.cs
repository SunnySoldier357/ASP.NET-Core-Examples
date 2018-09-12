using Microsoft.AspNetCore.Http;

namespace StoringImages.Models
{
    public class ImageHolder
    {
        // Public Properties
        public byte[] ImageArray { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}