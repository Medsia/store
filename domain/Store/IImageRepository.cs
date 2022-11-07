using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Store
{
    public interface IImageRepository
    {
        void SaveImageAsync(IFormFile uploadedImage, string path);
        void EditImageAsync(IFormFile uploadedImage, string path);
        void DeleteImage(string path);
    }
}
