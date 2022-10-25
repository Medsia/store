using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    class ProductImgLinkRepository : IProductImgLinkRepository
    {
        private readonly DbContextFactory dbContextFactory;
        public ProductImgLinkRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<ProductImgLink>> GetAllByProductIdAsync(int productId)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            var dtos = await dbContext.ProductImages
                                      .Where(img => img.ProductId == productId)
                                      .ToArrayAsync();


            return dtos.Select(ProductImgLink.Mapper.Map)
                       .ToArray();
        }

        public async Task<ProductImgLink> GetThumbnailByProductIdAsync(int productId)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            var dto = await dbContext.ProductImages
                               .SingleOrDefaultAsync(img => img.ProductId == productId && img.IsThumbnail == true);

            if (dto == null) return null;

            return ProductImgLink.Mapper.Map(dto);
        }

        public void AddNewItem(ProductImgLinkDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            dbContext.ProductImages.Add(item);
            dbContext.SaveChanges();
        }

        public void EditExistingItem(ProductImgLinkDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            dbContext.ProductImages.Update(item);
            dbContext.SaveChanges();
        }

        public void DeleteItem(ProductImgLinkDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            dbContext.ProductImages.Remove(item);
            dbContext.SaveChanges();
        }
    }
}
