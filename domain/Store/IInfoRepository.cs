using Store.Data.Content;

namespace Store
{
    public interface IInfoRepository
    {
        InfoSO GetData();
        void UpdateContactsData(ContactsSO contactsSO);
        void UpdatePaymentData(PaymentSO paymentSO);
        void UpdateDeliveryData(DeliverySO deliverySO);
        void UpdateAboutData(AboutSO aboutSO);
    }
}
