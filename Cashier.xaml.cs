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
        string userName;
        DateTime loginDate;
        Database db;
        List<Product> productL;
        List<OrderItem> orderItemL = new List<OrderItem>();
        decimal totalDiscount;
        public Cashier(string user, DateTime d)
        {
            try
            {
                db = new Database();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fatal error:unable to connect to database", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // Environment.Exit(1);
                throw e;
            }
            InitializeComponent();
            totalDiscount = 0;
            userName = user;
            loginDate = d;
            this.Title = userName + " entered in " + loginDate.ToString("yyyy-MM-dd HH:mm");
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
            if(txtNumberOfItems.Text=="" || txtNumberOfItems.Text==null )txtNumberOfItems.Text = 1+"";
            int numberOfUnit = Convert.ToInt32(txtNumberOfItems.Text);
            p.NumberInStock -= numberOfUnit;
            //  productL. = p.NumberInStock;
            productL.Find(item => item == p).NumberInStock = p.NumberInStock;

            dgProducts.ItemsSource = productL;
            dgProducts.UpdateLayout();
            dgProducts.Items.Refresh();
           
            OrderItem or = new OrderItem() { OrderId = p.Id, ProductName = p.ProductName, NumberOfUnit = numberOfUnit, ProductId = p.Id, SalePricePerUnit = (p.SalePrice) - (p.SalePrice) * p.SpecialDiscount };
            totalDiscount += (p.SalePrice) * p.SpecialDiscount*or.NumberOfUnit;
            orderItemL.Add(or);
            dgOrders.Items.Add(or);
            txtNumberOfItems.Text = 1 + "";


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
            totalDiscount -= (p.SalePrice) * p.SpecialDiscount* or.NumberOfUnit;

            dgProducts.ItemsSource = productL;
            dgProducts.UpdateLayout();
            dgProducts.Items.Refresh();
            // OrderItem or = new OrderItem() { OrderId = p.Id, ProductName = p.ProductName, NumberOfUnit = 1, ProductId = p.Id, SalePricePerUnit = (p.SalePrice) - (p.SalePrice) * p.SpecialDiscount };

            orderItemL.Remove(or);
            dgOrders.Items.Remove(or);
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
            SubmitOrder dialog = new SubmitOrder(orderItemL,0,0, totalDiscount);            
           // this.Close();
            dialog.Show();
        }
    }
}
