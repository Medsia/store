using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF
{
    class ProductRepository : IProductRepository
    {
        private readonly DbContextFactory dbContextFactory;
        public ProductRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var dtos = await dbContext.Products
                                      .Where(product => product.CategoryId == categoryId)
                                      .ToArrayAsync();


            return dtos.Select(Product.Mapper.Map)
                       .ToArray();
        }

        public async Task<IEnumerable<Product>> GetAllByIdsAsync(IEnumerable<int> productIds)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var dtos = await dbContext.Products
                                      .Where(product => productIds.Contains(product.Id))
                                      .ToArrayAsync();

            return dtos.Select(Product.Mapper.Map)
                       .ToArray();
        }

        public async Task<IEnumerable<Product>> GetAllByTitleAsync(string titlePart)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var parameter = new SqlParameter("@titlePart", titlePart);

            var dtos = await dbContext.Products
                            .FromSqlRaw("SELECT * FROM Products WHERE CONTAINS((Title), @titlePart)",
                                        parameter)
                            .ToArrayAsync();

            return dtos.Select(Product.Mapper.Map)
                       .ToArray();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var dto = await dbContext.Products
                               .SingleAsync(product => product.Id == id);

            return Product.Mapper.Map(dto);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            return dbContext.Products
                            .AsEnumerable()
                            .Select(Product.Mapper.Map)
                            .ToArray();
        }

        public async Task<Product> GetLastCreatedAsync()
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var dto = await dbContext.Products.OrderBy(product => product.Id)
                               .LastAsync();

            return Product.Mapper.Map(dto);
        }

        public void AddNewItem(ProductDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            dbContext.Products.Add(item);
            dbContext.SaveChanges();
        }

        public void EditExistingItem(ProductDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            dbContext.Products.Update(item);
            dbContext.SaveChanges();
        }

        public void DeleteItem(ProductDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            dbContext.Products.Remove(item);
            dbContext.SaveChanges();
        }
    }
}
