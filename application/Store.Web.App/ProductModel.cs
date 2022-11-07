using System.Collections.Generic;

namespace Store.Web.App
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CategoryModel Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailLink { get; set; }
    }
}
