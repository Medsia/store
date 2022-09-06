using Microsoft.AspNetCore.Mvc;
using Store.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;

        public CartController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Add(int id)
        {
            var product = productRepository.GetById(id);
            Cart cart;
            if (!HttpContext.Session.TryGetCart(out cart))
                cart = new Cart();

            if (cart.Items.ContainsKey(id))
                cart.Items[id]++;
            else
                cart.Items[id] = 1;

            cart.Amount += product.Price;

            HttpContext.Session.Set(cart);


            return RedirectToAction("Index", "Product", new { id });
        }
    }
}
