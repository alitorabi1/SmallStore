using System;
using System.Collections.Generic;

namespace SmallStore
{
    class ColumnBinder
    {
        public ProductCategory productCategory { get; set; }

        public Product product { get; set; }

        Database db = new Database();

        private string categoryName;
        public string GetCategoryName ()
        {
            return categoryName;
        }
        public void SetCatgoryName(int categoryId)
        {
            List<ProductCategory> pcList = new List<ProductCategory>();
            pcList = db.GetAllCategories();
            foreach (ProductCategory id in pcList)
            {
                if (id.CategoryId == categoryId)
                {
                    categoryName = id.Category;
                }
            }
        }

        public string ProductName { set; get; }
        public int NumberInStock { set; get; }
    }
}
