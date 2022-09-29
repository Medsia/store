using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF
{
    class CategoryRepository : ICategoryRepository
    {
        private readonly DbContextFactory dbContextFactory;
        public CategoryRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            return dbContext.Categories
                            .AsEnumerable()
                            .Select(Category.Mapper.Map)
                            .ToArray();
        }

        public Category GetCategoryById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            var dto = dbContext.Categories
                               .Single(category => category.Id == id);

            return Category.Mapper.Map(dto);
        }

        public void AddNewItem(Category item)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            dbContext.Categories.Add(Category.Mapper.Map(item));
            dbContext.SaveChanges();
        }

        public void EditExistingItem(Category item)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            dbContext.Categories.Update(Category.Mapper.Map(item));
            dbContext.SaveChanges();
        }

        public void DeleteItem(Category item)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            dbContext.Categories.Remove(Category.Mapper.Map(item));
            dbContext.SaveChanges();
        }
    }
}
