﻿using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProductService productService;
        private readonly ContentService contentService;

        public SearchController(ProductService productService, ContentService contentService)
        {
            this.productService = productService;
            this.contentService = contentService;
        }

        public async Task<IActionResult> Index(string query)
        {
            if (query == null)
                return RedirectToAction("Index", "Home");
            else
            {
                var products = await productService.GetAllByQueryAsync(query);

                return View("Index", products);
            }
            
        }
        public async Task<IActionResult> SearchByCategory(int categoryId)
        {
            var products = await productService.GetAllByQueryAsync(categoryId);

            return View("Index", products);
        }
    }
}
