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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmallStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Cashier : Window
    {
        Employee employee;
        DateTime loginDate;
        Database db;
        List<Product> productL;
        List<OrderItem> orderItemL = new List<OrderItem>();
        decimal totalDiscount;
        int customerId = 1;
       // Database database;
        public Cashier(Employee e, DateTime d)
        {
            try
            {
                db = new Database();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Fatal error:unable to connect to database", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // Environment.Exit(1);
                throw exp;
            }
            InitializeComponent();
            employee = e;
            totalDiscount = 0;

            loginDate = d;
            this.Title = e.FirstName + " " + e.LastName + " entered in " + loginDate.ToString("yyyy-MM-dd HH:mm");
            //    orderItemL = new List<OrderItem>();

        }

        private void tbProdNameOrBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            productL = db.GetAllProductByNameOrBarcode(tbProdNameOrBarcode.Text);
            dgProducts.ItemsSource = productL;


        }


        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            // tbProdNameOrBarcode.Text = dgOrders.Items.Count+"";
            btnAdd.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem == null) return;
            Product p = (Product)dgProducts.SelectedItem;
            
            if (txtNumberOfItems.Text == "" || txtNumberOfItems.Text == null ) txtNumberOfItems.Text = 1 + "";
           
                int numberOfUnit = Convert.ToInt32(txtNumberOfItems.Text);
            if (p.NumberInStock < numberOfUnit || p.NumberInStock<=0 || numberOfUnit<=0) return;
            p.NumberInStock -= numberOfUnit;
            //  productL. = p.NumberInStock;
            productL.Find(item => item == p).NumberInStock = p.NumberInStock;

            dgProducts.ItemsSource = productL;
            dgProducts.UpdateLayout();
            dgProducts.Items.Refresh();

            OrderItem or = new OrderItem() {  ProductName = p.ProductName, NumberOfUnit = numberOfUnit, ProductId = p.Id, SalePricePerUnit = (p.SalePrice) - (p.SalePrice) * p.SpecialDiscount };
            totalDiscount += (p.SalePrice) * p.SpecialDiscount * or.NumberOfUnit;
            orderItemL.Add(or);
            dgOrders.Items.Add(or);
            txtNumberOfItems.Text = 1 + "";



            if (dgOrders.Items.Count > 0) btnSubmitOrder.IsEnabled = true;


        }


        private void txtNumberOfItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNumberOfItems.Text, "^[0-9]*$"))
            {
                txtNumberOfItems.Text = string.Empty;
            }

        }



        private void DgOrders_removeItem_doubleClick(object sender, RoutedEventArgs e)
        {

            btnDelete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem == null) return;

            OrderItem or = (OrderItem)dgOrders.SelectedItem;
            Product p = productL.Find(item => item.ProductName == or.ProductName && item.Id == or.ProductId);

            productL.Find(item => item.ProductName == or.ProductName && item.Id == or.ProductId).NumberInStock += or.NumberOfUnit;
            totalDiscount -= (p.SalePrice) * p.SpecialDiscount * or.NumberOfUnit;

            dgProducts.ItemsSource = productL;
            dgProducts.UpdateLayout();
            dgProducts.Items.Refresh();
            // OrderItem or = new OrderItem() { OrderId = p.Id, ProductName = p.ProductName, NumberOfUnit = 1, ProductId = p.Id, SalePricePerUnit = (p.SalePrice) - (p.SalePrice) * p.SpecialDiscount };

            orderItemL.Remove(or);
            dgOrders.Items.Remove(or);
            if (dgOrders.Items.Count <= 0) btnSubmitOrder.IsEnabled = false;
        }
        private void dgProductsMouseUp(object o, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is ScrollViewer)
            {
                ((DataGrid)o).UnselectAll();
            }
        }
        private void dgOrdersMouseUp(object o, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is ScrollViewer)
            {
                ((DataGrid)o).UnselectAll();
            }
        }

        private void btnSubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            Payment dialog = new Payment(orderItemL, 0, 0, totalDiscount, employee.Id, customerId);
            // this.Close();
            dialog.ShowDialog();
            ResetDatagrids();
        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
        public  void ResetDatagrids()
        {
            orderItemL.Clear();
            dgOrders.Items.Clear();
            dgOrders.UpdateLayout();
            dgOrders.Items.Refresh();

        }


    }
}
