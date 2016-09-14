using System;

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
        public byte[] ProductImage { set; get; }
    }
}
