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

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


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
            p.NumberInStock -=1;
            //  productL. = p.NumberInStock;
            productL.Find(item => item == p).NumberInStock = p.NumberInStock;
            dgProducts.ItemsSource = productL;
            OrderItem or = new OrderItem() { OrderId = p.Id, ProductName = p.ProductName,NumberOfUnit=1, ProductId = p.Id, SalePricePerUnit = (p.SalePrice) - (p.SalePrice) * p.SpecialDiscount };

            orderItemL.Add(or);            
            dgOrders.Items.Add(or);
       

        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            dgOrders.ItemsSource = orderItemL;
        }
    }
}
