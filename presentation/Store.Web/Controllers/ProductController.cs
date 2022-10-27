using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        private readonly ContentService contentService;

        public ProductController(ProductService productService, ContentService contentService)
        {
            this.productService = productService;
            this.contentService = contentService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var model = await productService.GetByIdAsync(id);

            ViewBag.Images = await contentService.GetAllImagesByProdIdAsync(id);

            return View(model);
        }
    }
}
