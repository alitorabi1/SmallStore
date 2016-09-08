using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore.Entities
{
    class OrderSummary
    {
        public int OrderId;
        public int EmployeeId;
        public int CustomerId;
        public DateTime DatePurches;
        public decimal TotalPrice;
        public decimal Discount;
        public decimal Tax;
        public decimal TotalAndTax;
        public string PaidMethod;
    }
}
