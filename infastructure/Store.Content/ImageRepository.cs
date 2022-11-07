using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Store.Content
{
    public class ImageRepository : IImageRepository
    {
        public async void SaveImageAsync(IFormFile uploadedImage, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedImage.CopyToAsync(fileStream);
            }
        }

        public void EditImageAsync(IFormFile uploadedImage, string path)
        {
            DeleteImage(path);
            SaveImageAsync(uploadedImage, path);
        }

        public void DeleteImage(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            fileInf.Delete();
        }
    }
}
