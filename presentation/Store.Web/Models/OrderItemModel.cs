using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
    public class OrderItemModel
    {
        public int ProductId { get; set; }

        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
