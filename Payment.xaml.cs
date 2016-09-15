using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmallStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    enum PaymentMethod
    {
        Select_Method,
        Cash,
        CreditCard,
        DebitCard,
        Cheque
    }
    enum CardType
    {
        Master,
        Visa
    }
    public partial class Payment : Window
    {
        const decimal TAX_RATE = 0.5M;
        List<OrderItem> orderItems;
        int cashierID;
        int customerId;
        Database db;
        public Payment(List<OrderItem> items, int customerId, int employeeId, decimal totalDiscount, int cashID, int custId)
        {
            try
            {
                db = new Database();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fatal error:unable to connect to database. " + e.Message, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // Environment.Exit(1);
                //throw e;
            }
            InitializeComponent();
            cashierID = cashID;
            customerId = custId;


            foreach (PaymentMethod p in Enum.GetValues(typeof(PaymentMethod)))
            {
                cmbPaymenthMethod.Items.Add(p);
            }
            cmbPaymenthMethod.SelectedIndex = 0;

            orderItems = items;
            dgOrders.ItemsSource = orderItems;
            lblTotal_Price.Content = TotalPrice() + totalDiscount + "";
            lblTotalTax.Content = TotalPrice() * TAX_RATE;
            lblTotalAndTax.Content = TotalPrice() + TotalPrice() * TAX_RATE;
            lblTotalDiscount.Content = totalDiscount;
            FillYear();
            FillMonth();


        }
        private void FillYear()
        {
            int dt = DateTime.Now.Year;
            for (int i = 0; i <= 20; i++)
            {
                cmbYear.Items.Add(dt + i);
            }
            cmbYear.SelectedIndex = 0;

        }
        private void FillMonth()
        {

            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(String.Format("{0:00}", i));
            }
            cmbMonth.SelectedIndex = 0;

        }
        private decimal TotalPrice()
        {
            decimal totalPrice = 0;
            foreach (OrderItem or in orderItems)
            {
                totalPrice += or.SalePricePerUnit * or.NumberOfUnit;
            }
            return totalPrice;
        }

        private void cmbPaymenthMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPaymenthMethod.SelectedItem == null) { btnSubmitOrder.IsEnabled = false; return; }
            if (cmbPaymenthMethod.SelectedValue.ToString() == PaymentMethod.CreditCard.ToString())
            {
                cmbCardType.IsEnabled = true;
                cmbYear.IsEnabled = true;
                cmbMonth.IsEnabled = true;
                lblCardNumber.IsEnabled = true;
                lblCardType.IsEnabled = true;
                lblExpirationDate.IsEnabled = true;
                lblSecurityCode.IsEnabled = true;
                lblYYMM.IsEnabled = true;
                txtCardNumber.IsEnabled = true;
                txtSecurityCode.IsEnabled = true;
                lblCheckNumber.IsEnabled = false;
                txtCheckNumber.IsEnabled = false;


            }
            else if (cmbPaymenthMethod.SelectedValue.ToString() == PaymentMethod.Cheque.ToString())
            {
                cmbCardType.IsEnabled = false;
                cmbYear.IsEnabled = false;
                cmbMonth.IsEnabled = false;
                lblCardNumber.IsEnabled = false;
                lblCardType.IsEnabled = false;
                lblExpirationDate.IsEnabled = false;
                lblSecurityCode.IsEnabled = false;
                lblYYMM.IsEnabled = false;
                txtCardNumber.IsEnabled = false;
                txtSecurityCode.IsEnabled = false;
                lblCheckNumber.IsEnabled = true;
                txtCheckNumber.IsEnabled = true;


            }
            else
            {
                cmbCardType.IsEnabled = false;
                cmbYear.IsEnabled = false;
                cmbMonth.IsEnabled = false;
                lblCardNumber.IsEnabled = false;
                lblCardType.IsEnabled = false;
                lblExpirationDate.IsEnabled = false;
                lblSecurityCode.IsEnabled = false;
                lblYYMM.IsEnabled = false;
                txtCardNumber.IsEnabled = false;
                txtSecurityCode.IsEnabled = false;
                lblCheckNumber.IsEnabled = false;
                txtCheckNumber.IsEnabled = false;

            }
            btnSubmitOrder.IsEnabled = true;

        }
        public static bool HasCreditCardNumber(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b4[0-9]{12}(?:[0-9]{3})?\b|\b5[1-5][0-9]{14}\b|\b3[47][0-9]{13}\b|\b3(?:0[0-5]|[68][0-9])[0-9]{11}\b|\b6(?:011|5[0-9]{2})[0-9]{12}\b|\b(?:2131|1800|35\d{3})\d{11}\b");
            if (matches.Count > 0)
            {
                return true;
            }
            return false;
        }
        private void btnSubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPaymenthMethod.SelectedIndex == 0)
            {
                MessageBox.Show("please choose payment method");
                return;
            }
            if (cmbPaymenthMethod.SelectedItem.ToString() == PaymentMethod.CreditCard.ToString())
            {

                if (!HasCreditCardNumber(txtCardNumber.Text))
                {
                    MessageBox.Show("Card Number is invalid");
                    return;
                }
                if(txtSecurityCode.Text.Length<3)
                {
                    MessageBox.Show("Security Number is invalid");
                    return;
                }

            }
            if(cmbPaymenthMethod.SelectedItem.ToString() == PaymentMethod.Cheque.ToString())
            {
                if (txtSecurityCode.Text.Length < 3)
                {
                    MessageBox.Show("Check Number is invalid");
                    return;
                }
            }


            OrderSummary orderSum = new OrderSummary();
            orderSum.Items = orderItems;
            orderSum.EmployeeId = cashierID;
            orderSum.CustomerId = 1;
            orderSum.PaidMethod = cmbPaymenthMethod.Text;
            orderSum.Tax = Convert.ToDecimal(lblTotalTax.Content);
            orderSum.TotalAndTax = Convert.ToDecimal(lblTotalAndTax.Content);
            orderSum.TotalPrice = Convert.ToDecimal(lblTotal_Price.Content);
            orderSum.DatePurches = DateTime.Now;
            orderSum.Discount = Convert.ToDecimal(lblTotalDiscount.Content);
            orderSum.CheckNumber = "";
            orderSum.CreditCardNumber = "";
            orderSum.CardExprDate = "";
           if( db.Transaction_OrderSubmit(orderSum, cashierID, customerId))
            {
                Receipt receipt = new Receipt(orderItems, lblTotal_Price.Content, lblTotalAndTax.Content, lblTotalDiscount.Content, lblTotalTax.Content);
            }

        }
    }
}
