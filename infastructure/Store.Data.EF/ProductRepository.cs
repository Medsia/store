using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Product> GetAllByCategoryId(IEnumerable<int> categoryIds)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            return dbContext.Products
                            .Where(product => categoryIds.Contains(product.CategoryId))
                            .AsEnumerable()
                            .Select(Product.Mapper.Map)
                            .ToArray();
        }

        public IEnumerable<Product> GetAllByIds(IEnumerable<int> productIds)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            return dbContext.Products
                            .Where(product => productIds.Contains(product.Id))
                            .AsEnumerable()
                            .Select(Product.Mapper.Map)
                            .ToArray();
        }

        public IEnumerable<Product> GetAllByTitle(string titlePart)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var parameter = new SqlParameter("@titlePart", titlePart);
            return dbContext.Products
                            .FromSqlRaw("SELECT * FROM Products WHERE CONTAINS((Title), @titlePart)",
                                        parameter)
                            .AsEnumerable()
                            .Select(Product.Mapper.Map)
                            .ToArray();
        }

        public Product GetById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var dto = dbContext.Products
                               .Single(product => product.Id == id);

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

        public void AddNewItem(Product item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            dbContext.Products.Add(Product.Mapper.Map(item));
            dbContext.SaveChanges();
        }

        public void EditExistingItem(Product item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            dbContext.Products.Update(Product.Mapper.Map(item));
            dbContext.SaveChanges();
        }

        public void DeleteItem(Product item)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            dbContext.Products.Remove(Product.Mapper.Map(item));
            dbContext.SaveChanges();
        }
    }
}
