using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var model = await productService.GetByIdAsync(id);
            return View(model);
        }
    }
}
