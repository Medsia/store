using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        public ContentService(IInfoRepository infoRepository, IProductImgLinkRepository productImgLinkRepository,
                                    IWebHostEnvironment appEnvironment)
        {
            this.infoRepository = infoRepository;
            this.productImgLinkRepository = productImgLinkRepository;

            infoRepository.WebRootPath = appEnvironment.WebRootPath;
        }

        public static string EmptyImageLink = "/Img/Empty.jpg";

        public bool IsImageValid(IFormFile uploadedFile, out string message)
        {
            message = "";

            if (uploadedFile == null)
            {
                message = string.Format("Изображение не загружено.");
                return false;
            }

            if (uploadedFile.ContentType != "image/jpeg" && uploadedFile.ContentType != "image/png")
            {
                message = string.Format("Выбранный файл должен быть типа .jpg или .png.");
                return false;
            }

            if (uploadedFile.Length >= 4194304)
            {
                message = string.Format("Выбранный файл больше 4 МБ.");
                return false;
            }

            return true;
        }


        public async Task<IEnumerable<string>> GetAllImagesByProdIdAsync(int productId)
        {
            var images = await productImgLinkRepository.GetAllOrDefaultByProductIdAsync(productId);

            if (images.Count() == 0)
            {
                var emptyList = new List<string>();
                emptyList.Add(EmptyImageLink);

                return emptyList.ToArray();
            }

            return images.Select(Map)
                        .ToArray();
        }

        public async Task<string> GetThumbnailByProdIdAsync(int productId)
        {
            var image = await productImgLinkRepository.GetImageOrDefaultAsync(productId, true);

            if (image.Id == 0) return EmptyImageLink;

            return Map(image);
        }

        private string Map(ProductImgLink productImgLink)
        {
            return productImgLink.ImgLink;
        }


        public ContactsSO GetContacts()
        {
            var contacts = infoRepository.GetData().Contacts;

            if (string.IsNullOrWhiteSpace(contacts.ImgLink)) contacts.ImgLink = EmptyImageLink;

            return contacts;
        }
        public PaymentSO GetPayment()
        {
            var payment = infoRepository.GetData().Payment;

            if (string.IsNullOrWhiteSpace(payment.ImgLink)) payment.ImgLink = EmptyImageLink;

            return payment;
        }
        public DeliverySO GetDelivery()
        {
            var delivery = infoRepository.GetData().Delivery;

            if (string.IsNullOrWhiteSpace(delivery.ImgLink)) delivery.ImgLink = EmptyImageLink;

            return delivery;
        }
        public AboutSO GetAbout()
        {
            var about = infoRepository.GetData().About;

            if (string.IsNullOrWhiteSpace(about.ImgLink)) about.ImgLink = EmptyImageLink;

            return about;
        }
    }
}
