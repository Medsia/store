
namespace Store.Memory
{
    public class InfoRepository : IInfoRepository
    {
        private Info[] info =
        {
            new Info(1, "Контакты", "МТС: 8(800)555-35-35"),
            new Info(2, "Оплата", "Налом или картой? Калом"),
            new Info(3, "Доставка", "Доставляем Яндекс Едой"),
            new Info(4, "О магазине", "Самый кайфовый магаз"),
        };

        public Info GetContactsInfo()
        {
            return info[0];
        }

        public Info GetPaymentInfo()
        {
            return info[1];
        }

        public Info GetDeliveryInfo()
        {
            return info[2];
        }

        public Info GetAboutInfo()
        {
            return info[3];
        }
    }
}
