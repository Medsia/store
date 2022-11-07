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


        public async Task<IEnumerable<ProductImgLink>> GetAllOrDefaultByProductIdAsync(int productId)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            var dtos = await dbContext.ProductImages
                                      .Where(img => img.ProductId == productId && img.IsThumbnail == false)
                                      .ToArrayAsync();

            if (dtos == null || dtos.Count() == 0)
                dtos = new ProductImgLinkDto[] { };

            return dtos.Select(ProductImgLink.Mapper.Map)
                       .ToArray();
        }

        public async Task<ProductImgLink> GetImageOrDefaultAsync(int productId, int personalId)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            var dto = await dbContext.ProductImages
                               .SingleOrDefaultAsync(img => img.ProductId == productId && img.IsThumbnail == false && img.PersonalId == personalId);

            if (dto == null)
                dto = new ProductImgLinkDto { Id = 0 };

            return ProductImgLink.Mapper.Map(dto);
        }

        public async Task<ProductImgLink> GetImageOrDefaultAsync(int productId, bool isThumbnail)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductImgLinkRepository));

            var dto = await dbContext.ProductImages
                               .SingleOrDefaultAsync(img => img.ProductId == productId && img.IsThumbnail == isThumbnail);

            if (dto == null)
                dto = new ProductImgLinkDto { Id = 0 };

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
