using Store.Data.Content;
using System.IO;
using System.Text.Json;

namespace Store.Content
{
    public class InfoRepository : IInfoRepository
    {
        private string InfoJsonLocation { get => @"Content\InfoPagesData.json"; }

        public InfoSO GetJsData()
        {
            InfoSO jsData = (InfoSO)JsonSerializer.Deserialize<InfoSO>(File.ReadAllText(InfoJsonLocation));
            
            return jsData;
        }

        public void WriteJsData()
        {
            //InfoSO jsData = (InfoSO)JsonSerializer.Deserialize("Content/InfoPagesData.json", typeof(InfoSO));
            //return jsData;
        }
    }
}
