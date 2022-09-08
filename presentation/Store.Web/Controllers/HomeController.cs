﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public HomeController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            return View(categoryRepository.GetAllCategories());
        }

        public IActionResult Delivery()
        {
            return View("Delivery");
        }

        public IActionResult Contacts()
        {
            return View("Contacts");
        }

        public IActionResult About()
        {
            return View("About");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
