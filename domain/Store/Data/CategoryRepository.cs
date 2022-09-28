using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Category[] categories = TemporaryData.categories;

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();

            //return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            return categories.First(category => category.Id == categoryId);
        }

        public bool AddNewItem(Category item)
        {
            throw new NotImplementedException();

            //int itemDbId = categories.FindIndex(categoriesItem => categoriesItem.Id == item.Id);

            //if (itemDbId != -1)
            //    return false;

            //categories.Add(item);
            //return true;
        }

        public bool EditExistingItem(Category item)
        {

            throw new NotImplementedException();

            //int itemDbId = categories.FindIndex(categoriesItem => categoriesItem.Id == item.Id);

            //if (itemDbId == -1)
            //    return false;

            //categories[itemDbId] = item;
            //return true;
        }

        public bool DeleteItem(Category item)
        {
            throw new NotImplementedException();

            //int itemDbId = categories.FindIndex(categoriesItem => categoriesItem.Id == item.Id);

            //if (itemDbId == -1)
            //    return false;

            //categories.RemoveAt(itemDbId);
            //return true;
        }
    }
}
