using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data
{
    public static class TemporaryData
    {
        public static readonly Category[] categories = new[]
{
            new Category(1, "Значки"),
            new Category(2, "Плакаты"),
            new Category(3, "Брелки"),
            new Category(4, "Аксесуары"),
        };
    }
}
