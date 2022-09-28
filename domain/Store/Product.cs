using Store.Data;
using System;

namespace Store
{
    public class Product
    {
        private readonly ProductDto dto;

        public int Id => dto.Id;
        public int CategoryId
        {
            get => dto.CategoryId;
            set => dto.CategoryId = value;
        }
        

        public string Title
        {
            get => dto.Title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Title));

                dto.Title = value.Trim();
            }
        }

        public string Description
        {
            get => dto.Description;
            set => dto.Description = value;
        }

        public decimal Price
        {
            get => dto.Price;
            set => dto.Price = value;
        }

        internal Product(ProductDto dto)
        {
            this.dto = dto;
        }

        public static class DtoFactory
        {
            public static ProductDto Create(int categoryId,
                                         string title,
                                         string description,
                                         decimal price)
            {           

                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException(nameof(title));

                return new ProductDto
                {
                    CategoryId = categoryId,
                    Title = title.Trim(),
                    Description = description?.Trim(),
                    Price = price,
                };
            }
        }
        public static class Mapper
        {
            public static Product Map(ProductDto dto) => new Product(dto);
      
            public static ProductDto Map(Product domain) => domain.dto;
        }
    }
}
