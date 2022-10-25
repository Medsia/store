using Store.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class ProductImgLink
    {
        private readonly ProductImgLinkDto dto;

        public int Id => dto.Id;
        public string ImgLink
        {
            get => dto.ImgLink;
            set => dto.ImgLink = value;
        }

        public int ProductId
        {
            get => dto.ProductId;
            set => dto.ProductId = value;
        }

        public bool IsThumbnail
        {
            get => dto.IsThumbnail;
            set => dto.IsThumbnail = value;
        }

        internal ProductImgLink(ProductImgLinkDto dto)
        {
            this.dto = dto;
        }


        public static class DtoFactory
        {
            public static ProductImgLinkDto Create(int id, string imgLink, int productId, bool isThumbnail)
            {

                if (string.IsNullOrWhiteSpace(imgLink))
                    throw new ArgumentException(nameof(imgLink));

                return new ProductImgLinkDto
                {
                    Id = id,
                    ImgLink = imgLink.Trim(),
                    ProductId = productId,
                    IsThumbnail = isThumbnail
                };
            }
        }
        public static class Mapper
        {
            public static ProductImgLink Map(ProductImgLinkDto dto) => new ProductImgLink(dto);

            public static ProductImgLinkDto Map(ProductImgLink domain) => domain.dto;
        }
    }
}
