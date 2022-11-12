using Store.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductImgLinkRepository
    {
        Task<IEnumerable<ProductImgLink>> GetAllOrDefaultByProductIdAsync(int productId);
        Task<ProductImgLink> GetImageOrDefaultAsync(int productId, int personalId);
        Task<ProductImgLink> GetImageOrDefaultAsync(int productId, bool isThumbnail);
        void AddNewItem(ProductImgLinkDto item);
        void EditExistingItem(ProductImgLinkDto item);
        void DeleteItem(ProductImgLinkDto item);

    }
}
