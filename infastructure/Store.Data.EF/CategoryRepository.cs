﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            var dto = await dbContext.Categories
                               .SingleAsync(category => category.Id == id);

            return Category.Mapper.Map(dto);
        }

        public void AddNewItem(CategoryDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            dbContext.Categories.Add(item);
            dbContext.SaveChanges();
        }

        public void EditExistingItem(CategoryDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            dbContext.Categories.Update(item);
            dbContext.SaveChanges();
        }

        public void DeleteItem(CategoryDto item)
        {
            var dbContext = dbContextFactory.Create(typeof(CategoryRepository));

            dbContext.Categories.Remove(item);
            dbContext.SaveChanges();
        }
    }
}
