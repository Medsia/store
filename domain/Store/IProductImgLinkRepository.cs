using Store.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductImgLinkRepository
    {
        Task<IEnumerable<ProductImgLink>> GetAllByProductIdAsync(int productId);
        Task<ProductImgLink> GetThumbnailByProductIdAsync(int productId);
        void AddNewItem(ProductImgLinkDto item);
        void EditExistingItem(ProductImgLinkDto item);
        void DeleteItem(ProductImgLinkDto item);

    }
}
