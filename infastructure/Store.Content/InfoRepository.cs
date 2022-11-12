using Store.Data.Content;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Store.Content
{
    public class InfoRepository : IInfoRepository
    {
        public string WebRootPath { private get; set; }
        private string InfoJsonLocation { get => WebRootPath + @"/Content/InfoPagesData.json"; }

        private InfoSO GetJsData()
        {
            InfoSO jsData = (InfoSO)JsonSerializer.Deserialize<InfoSO>(File.ReadAllText(InfoJsonLocation));
            return jsData;
        }

        private void WriteJsData(InfoSO infoSO)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string jsData = JsonSerializer.Serialize<InfoSO>(infoSO, options);
            File.WriteAllText(InfoJsonLocation, jsData);
        }


        public InfoSO GetData()
        {
            return GetJsData();
        }


        public void UpdateContactsData(ContactsSO contactsSO)
        {
            InfoSO infoSO = GetJsData();
            infoSO.Contacts = contactsSO;
            WriteJsData(infoSO);
        }

        public void UpdatePaymentData(PaymentSO paymentSO)
        {
            InfoSO infoSO = GetJsData();
            infoSO.Payment = paymentSO;
            WriteJsData(infoSO);
        }

        public void UpdateDeliveryData(DeliverySO deliverySO)
        {
            InfoSO infoSO = GetJsData();
            infoSO.Delivery = deliverySO;
            WriteJsData(infoSO);
        }
        public void UpdateAboutData(AboutSO aboutSO)
        {
            InfoSO infoSO = GetJsData();
            infoSO.About = aboutSO;
            WriteJsData(infoSO);
        }

    }
}
