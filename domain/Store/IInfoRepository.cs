
namespace Store
{
    public interface IInfoRepository
    {
        Info GetContactsInfo();
        Info GetPaymentInfo();
        Info GetDeliveryInfo();
        Info GetAboutInfo();
    }
}
