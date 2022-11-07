
namespace Store.Data
{
    public class ProductImgLinkDto
    {
        public int Id { get; set; }
        public string ImgLink { get; set; }
        public int ProductId { get; set; }
        public int PersonalId { get; set; }
        public bool IsThumbnail { get; set; }
    }
}
