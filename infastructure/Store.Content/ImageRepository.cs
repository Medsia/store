using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Store.Content
{
    public class ImageRepository : IImageRepository
    {
        public async Task SaveImageAsync(IFormFile uploadedImage, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedImage.CopyToAsync(fileStream);
            }
        }

        public async Task EditImageAsync(IFormFile uploadedImage, string path)
        {
            DeleteImage(path);
            await SaveImageAsync(uploadedImage, path);
        }

        public void DeleteImage(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            fileInf.Delete();
        }
    }
}
