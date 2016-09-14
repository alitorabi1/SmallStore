using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class AdminProduct : Window
    {
        Database db;
        List<Product> pList = new List<Product>();
        Product p = new Product();

        public AdminProduct()
        {
            try
            {
                db = new Database();
            }
            catch (Exception e)
            {
                // TODO: show a message box
                MessageBox.Show("Fatal error: unable to connect to database",
                    "Fatal error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: write details of the exception to log text file
                Environment.Exit(1);
                //throw e;
            }
            InitializeComponent();
            // create a model object
            //ColumnBinder columnBinder = new ColumnBinder() { CategoryName = db.getCategoryNameById(p.CategoryId) };

            //gcCategory.Binding = new Binding("CategoryName")
            //{
            //    Source = columnBinder,
            //    Mode = BindingMode.TwoWay
            //};

            try
            {
                pList = db.GetAllProducts();
                dgProduct.ItemsSource = pList;
                List<ProductCategory> pcList = new List<ProductCategory>();
                pcList = db.GetAllCategories();
                foreach (ProductCategory id in pcList)
                {
                    if (id.CategoryId == p.CategoryId)
                    {
                        gcCategory.Binding = new Binding("CategoryName")
                        {
                            Source = id.Category,
                            Mode = BindingMode.TwoWay
                        };
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: show a message box
                //MessageBox.Show("Unable to fetch records from database",
                //    "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                MessageBox.Show(e.StackTrace, "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        int currentProductId = 0;

        private void dgProduct_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            p = (Product)dgProduct.SelectedItem;
            if (p == null)
            {
                currentProductId = 0;
                return;
            }
            currentProductId = p.Id;
            lblProductId.Content = p.Id;
            tbProductName.Text = p.ProductName;
            tbCategoryId.Text = p.CategoryId.ToString();
            tbBarcode.Text = p.Barcode;
            tbNumber.Text = p.NumberInStock.ToString();
            tbPurchasePrice.Text = p.PurchasePrice.ToString();
            tbSalesPrice.Text = p.SalePrice.ToString();
            tbUnit.Text = p.Unit;
            tbSpecialDiscount.Text = p.SpecialDiscount.ToString();

            imgProduct.Source = null;
            if (p.ProductImage != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(p.ProductImage);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                BitmapImage newBitmapImage = new BitmapImage();
                newBitmapImage.BeginInit();
                newBitmapImage.StreamSource = ms;
                newBitmapImage.EndInit();
                imgProduct.Source = newBitmapImage;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string product = tbProductName.Text;
            int categoryId = int.Parse(tbCategoryId.Text);
            string barcode = tbBarcode.Text;
            int numberInStock = int.Parse(tbNumber.Text);
            decimal purchasePrice = decimal.Parse(tbPurchasePrice.Text);
            decimal salePrice = decimal.Parse(tbSalesPrice.Text);
            string unit = tbUnit.Text;
            decimal specialDiscount = decimal.Parse(tbSpecialDiscount.Text);
            p = new Product() { ProductName = product, CategoryId = categoryId, Barcode = barcode, NumberInStock = numberInStock, PurchasePrice = purchasePrice, SalePrice = salePrice, Unit = unit, ProductImage = imageData, SpecialDiscount = specialDiscount };
            db.AddProductItem(p);
            tbCategoryId.Text = "";
            tbProductName.Text = "";
            tbPurchasePrice.Text = "";
            tbSalesPrice.Text = "";
            tbBarcode.Text = "";
            tbNumber.Text = "";
            tbUnit.Text = "";
            tbSpecialDiscount.Text = "";
            imgProduct.Source = null;
            pList = db.GetAllProducts();
            dgProduct.ItemsSource = pList;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            p = (Product)dgProduct.SelectedItem;
            if (p == null)
            {
                MessageBox.Show("Please select an item to delete",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            db.DeleteProductItemById(p.Id);
            pList = db.GetAllProducts();
            dgProduct.ItemsSource = pList;
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentProductId == 0)
            {
                MessageBox.Show("Please select an item for update",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            int id = int.Parse(lblProductId.Content.ToString());
            string product = tbProductName.Text;
            int categoryId = int.Parse(tbCategoryId.Text);
            string barcode = tbBarcode.Text;
            int numberInStock = int.Parse(tbNumber.Text);
            decimal purchasePrice = decimal.Parse(tbPurchasePrice.Text);
            decimal salePrice = decimal.Parse(tbSalesPrice.Text);
            string unit = tbUnit.Text;
            decimal specialDiscount = decimal.Parse(tbSpecialDiscount.Text);
            p = new Product() { Id = id, ProductName = product, CategoryId = categoryId, Barcode = barcode, NumberInStock = numberInStock, PurchasePrice = purchasePrice, SalePrice = salePrice, Unit = unit, ProductImage = imageData, SpecialDiscount = specialDiscount };
            db.UpdateProductItem(p);
            tbCategoryId.Text = "";
            tbProductName.Text = "";
            tbPurchasePrice.Text = "";
            tbSalesPrice.Text = "";
            tbBarcode.Text = "";
            tbNumber.Text = "";
            tbUnit.Text = "";
            tbSpecialDiscount.Text = "";
            imgProduct.Source = null;
            pList = db.GetAllProducts();
            dgProduct.ItemsSource = pList;
        }
        byte[] imageData = { };
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();

            FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            imageData = new byte[fs.Length];
            fs.Read(imageData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            ImageSourceConverter imgs = new ImageSourceConverter();
            imgProduct.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));
        }
        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
