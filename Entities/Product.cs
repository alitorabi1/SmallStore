using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore
{
    class Product
    {
        public int Id { set; get; }
        public string ProductName { set; get; }
        public int CategoryId { set; get; }
        public string Barcode { set; get; }
        public int NumberInStock { set; get; }
        public decimal PurchasePrice { set; get; }
        public decimal SalePrice { set; get; }
        public string Unit { set; get; }
        public decimal SpecialDiscount { set; get; }
        public string ProductImage { set; get; }
    }
}
