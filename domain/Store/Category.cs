using Store.Data;
using System;

namespace Store
{
    public class Category
    {
        private readonly CategoryDto dto;

        public int Id => dto.Id;

        public string Name
        {
            get => dto.Name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Name));

                dto.Name = value.Trim();
            }
        }

        public string ImgLink
        {
            get => dto.ImgLink;
            set => dto.ImgLink = value;
        }

        internal Category(CategoryDto dto)
        {
            this.dto = dto;
        }

        public static class DtoFactory
        {
            public static CategoryDto Create(string name, string imgLink)
            {

                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException(nameof(name));

                return new CategoryDto
                {
                    Name = name,
                    ImgLink = imgLink
                };
            }
        }
        public static class Mapper
        {
            public static Category Map(CategoryDto dto) => new Category(dto);

            public static CategoryDto Map(Category domain) => domain.dto;
        }
    }
}
