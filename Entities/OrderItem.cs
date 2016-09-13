using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore
{
    public class OrderItem
    {
        public int OrderId {set; get; }
        public int ProductId {set; get; }
        public decimal SalePricePerUnit {set; get; }
        public int NumberOfUnit {set; get; }
        public string ProductName {set; get; }
    }
}
