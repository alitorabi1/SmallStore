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
    public partial class AdminGroup : Window
    {
        Database db;
        List<ProductCategory> pcList = new List<ProductCategory>();

        public AdminGroup()
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
            try
            {
                pcList = db.GetAllCategories();
                dgCategory.ItemsSource = pcList;
            }
            catch (Exception e)
            {
                // TODO: show a message box
                MessageBox.Show("Unable to fetch records from database",
                    "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        int currentCategoryId = 0;

        private void dgCategory_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ProductCategory pc = (ProductCategory)dgCategory.SelectedItem;
            if (pc == null)
            {
                currentCategoryId = 0;
                lblCategoryId.Content = "";
                return;
            }
            currentCategoryId = pc.CategoryId;
            lblCategoryId.Content = pc.CategoryId;
            tbCName.Text = pc.Category;
            tbCDesc.Text = pc.Description;
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string category = tbCName.Text;
            string description = tbCDesc.Text;
            ProductCategory pc = new ProductCategory() { Category = category, Description = description };
            db.AddCategoryItem(pc);
            tbCName.Text = "";
            tbCDesc.Text = "";
            pcList = db.GetAllCategories();
            dgCategory.ItemsSource = pcList;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            ProductCategory pc = (ProductCategory)dgCategory.SelectedItem;
            if (pc == null)
            {
                MessageBox.Show("Please select an item to delete",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            db.DeleteCategoryItemsById(pc.CategoryId);
            pcList = db.GetAllCategories();
            dgCategory.ItemsSource = pcList;
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentCategoryId == 0)
            {
                MessageBox.Show("Please select an item for update",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            int id = int.Parse(lblCategoryId.Content.ToString());
            string category = tbCName.Text;
            string description = tbCDesc.Text;
            ProductCategory pc = new ProductCategory() { CategoryId = id, Category = category, Description = description };
            db.UpdateCategoryItems(pc);
            pcList = db.GetAllCategories();
            currentCategoryId = pc.CategoryId;
            dgCategory.ItemsSource = pcList;
        }

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            dgCategory.UnselectAllCells();
            tbCName.Text = "";
            tbCDesc.Text = "";
        }
    }
}
