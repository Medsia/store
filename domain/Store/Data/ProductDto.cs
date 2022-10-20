using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public List<ImgLinkItemDto> ImgLinks { get; set; }
    }
}
