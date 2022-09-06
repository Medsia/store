using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public interface ICategoryRepository
    {
        Category[] GetAll();
        Category GetById(int id);
    }
}
