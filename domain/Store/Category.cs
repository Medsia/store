﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Store
{
    public class Category
    {
        public int Id { get; }
        public string Name { get; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
