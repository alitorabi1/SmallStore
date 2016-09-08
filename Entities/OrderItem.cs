using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore.Entities
{
    class OrderItem
    {
        public int OrderId;
        public int ProductId;
        public decimal SalePricePerUnit;
        public decimal NumberOfUnit;
        public string ProductName;
    }
}
