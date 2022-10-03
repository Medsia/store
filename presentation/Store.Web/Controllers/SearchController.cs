using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using System.Collections.Generic;

namespace Store.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProductService productService;

        public SearchController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(string query)
        {
            var products = productService.GetAllByQuery(query);

            return View("Index", products);
        }
        public IActionResult SearchByCategory(int categoryId)
        {
            var products = productService.GetAllByQuery(categoryId);

            return View("Index", products);
        }
    }
}
