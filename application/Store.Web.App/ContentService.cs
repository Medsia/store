using Store.Data;
using Store.Data.Content;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class ContentService
    {
        private readonly IInfoRepository infoRepository;
        private readonly IProductImgLinkRepository productImgLinkRepository;

        private static readonly string EmptyImageLink = "/Img/Empty.jpg";
        private static readonly ProductImgLinkModel EmptyImageModel = new ProductImgLinkModel { ImgLink = EmptyImageLink, IsThumbnail = true };

        public ContentService(IInfoRepository infoRepository, IProductImgLinkRepository productImgLinkRepository)
        {
            this.infoRepository = infoRepository;
            this.productImgLinkRepository = productImgLinkRepository;
        }


        public async Task<IEnumerable<ProductImgLinkModel>> GetAllImagesByProdIdAsync(int productId)
        {
            var images = await productImgLinkRepository.GetAllByProductIdAsync(productId);

            if (images == null)
            {
                var emptyList = new List<ProductImgLinkModel>();
                emptyList.Add(EmptyImageModel);

                return emptyList.ToArray();
            }

            return images.Select(Map)
                        .ToArray();
        }

        public async Task<ProductImgLinkModel> GetThumbnailByProdIdAsync(int productId)
        {
            var image = await productImgLinkRepository.GetThumbnailByProductIdAsync(productId);

            if (image == null) return EmptyImageModel;

            return Map(image);
        }

        private ProductImgLinkModel Map(ProductImgLink productImgLink)
        {
            return new ProductImgLinkModel
            {
                ImgLink = productImgLink.ImgLink,
                IsThumbnail = productImgLink.IsThumbnail
            };
        }


        public ContactsSO GetContacts()
        {
            return infoRepository.GetData().Contacts;
        }
        public PaymentSO GetPayment()
        {
            return infoRepository.GetData().Payment;
        }
        public DeliverySO GetDelivery()
        {
            return infoRepository.GetData().Delivery;
        }
        public AboutSO GetAbout()
        {
            return infoRepository.GetData().About;
        }
    }
}
