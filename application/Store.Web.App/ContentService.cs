using Store.Data.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class ContentService
    {
        IInfoRepository infoRepository;

        public ContentService(IInfoRepository infoRepository)
        {
            this.infoRepository = infoRepository;
        }

        public ContactsSO GetContacts()
        {
            return infoRepository.GetJsData().Contacts;
        }
        public PaymentSO GetPayment()
        {
            return infoRepository.GetJsData().Payment;
        }
        public DeliverySO GetDelivery()
        {
            return infoRepository.GetJsData().Delivery;
        }
        public AboutSO GetAbout()
        {
            return infoRepository.GetJsData().About;
        }
    }
}
