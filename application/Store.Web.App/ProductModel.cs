﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
