using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore
{
    public class OrderSummary
    {
        public int OrderId { set; get; }
        public int EmployeeId { set; get; }
        public int CustomerId { set; get; }
        public DateTime DatePurches { set; get; }
        public decimal TotalPrice { set; get; }
        public decimal Discount { set; get; }
        public decimal Tax { set; get; }
        public decimal TotalAndTax { set; get; }
        public string PaidMethod { set; get; }
        public string CheckNumber { set; get; }
        public string CreditCardNumber { set; get; }
        public string CardExprDate { set; get; }
        public List<OrderItem> Items { set; get; }
    }
}
