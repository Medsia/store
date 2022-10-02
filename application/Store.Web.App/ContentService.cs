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


        public void EditContacts()
        {
            // В ПРОЦЕССЕ
        }
        public void EditPayment()
        {
            // В ПРОЦЕССЕ
        }
        public void EditDelivery()
        {
            // В ПРОЦЕССЕ
        }
        public void EditAbout()
        {
            // В ПРОЦЕССЕ
        }
    }
}
