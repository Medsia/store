using System.Collections.Generic;

namespace Store.Data.Content
{
    public class ContactsSO
    {
        public string Title { get; set; }
        public string ImgLink { get; set; }
        public string Location { get; set; }
        public string Worktime { get; set; }
        public string[] Numbers { get; set; }
        public string Additional { get; set; }
    }
}
