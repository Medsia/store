using Store.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class ImgLinkItem
    {
        private readonly ImgLinkItemDto dto;

        public int Id => dto.Id;
        public string ImgLink
        {
            get => dto.ImgLink;
            set => dto.ImgLink = value;
        }

        internal ImgLinkItem(ImgLinkItemDto dto)
        {
            this.dto = dto;
        }


        public static class DtoFactory
        {
            public static ImgLinkItemDto Create(int id, string imgLink)
            {

                if (string.IsNullOrWhiteSpace(imgLink))
                    throw new ArgumentException(nameof(imgLink));

                return new ImgLinkItemDto
                {
                    Id = id,
                    ImgLink = imgLink.Trim()
                };
            }
        }
        public static class Mapper
        {
            public static ImgLinkItem Map(ImgLinkItemDto dto) => new ImgLinkItem(dto);

            public static ImgLinkItemDto Map(ImgLinkItem domain) => domain.dto;
        }
    }
}
