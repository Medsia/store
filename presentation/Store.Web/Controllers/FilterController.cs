using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Store.Web.Controllers
{
    public class FilterController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public FilterController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index(int categoryId)
        {
            ViewBag.Category = categoryRepository.GetById(categoryId);
            ViewBag.Products = productRepository.GetAllByCategoryId(categoryId);

            return View();
        }
    }
}
