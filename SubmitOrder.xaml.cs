using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.

namespace SmallStore
{
    /// <summary>
    /// Interaction logic for SubmitOrder.xaml
    /// </summary>
    public partial class SubmitOrder : Window
    {
        const decimal TAX_RATE = 0.5M;
        List<OrderItem> orderItems;
        public SubmitOrder(List<OrderItem> items,int customerId,int employeeId,decimal totalDiscount  )
        {
            InitializeComponent();
            orderItems = items;
            lblTotal_Price.Content =TotalPrice()+ totalDiscount + "";
            lblTotalTax.Content= TotalPrice() * TAX_RATE;
            lblTotalAndTax.Content = TotalPrice() + TotalPrice() * TAX_RATE;
            lblTotalDiscount.Content = totalDiscount;
        }
        private decimal TotalPrice() {
            decimal totalPrice=0;
            foreach(OrderItem or in orderItems)
            {
                totalPrice += or.SalePricePerUnit * or.NumberOfUnit;
            }
            return totalPrice;
        }
        
    }
}
