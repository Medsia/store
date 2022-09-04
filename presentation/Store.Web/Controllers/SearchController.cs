using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductRepository productRepository;

        public SearchController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Index(string query)
        {
            var products = productRepository.GetAllByTitle(query);

            return View(products);
        }
    }
}
