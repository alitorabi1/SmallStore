using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore
{
    class OrderItem
    {
        public int OrderId {set; get; }
        public int ProductId {set; get; }
        public decimal SalePricePerUnit {set; get; }
        public decimal NumberOfUnit {set; get; }
        public string ProductName {set; get; }
    }
}
