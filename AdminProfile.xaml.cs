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
    public partial class AdminProfile : Window
    {
        Database db;
        List<Employee> empList = new List<Employee>();
        Employee emp = new Employee();

        public AdminProfile()
        {
            try
            {
                db = new Database();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fatal error: unable to connect to database",
                    "Fatal error", MessageBoxButton.OK, MessageBoxImage.Stop);
                // TODO: write details of the exception to log text file
                Environment.Exit(1);
                //throw e;
            }
            InitializeComponent();
            dpBirthDate.Text = DateTime.Today.ToString();
            dpFireDate.Text = DateTime.Today.ToString();
            dpHireDate.Text = DateTime.Today.ToString();
            try
            {
                empList = db.GetAllEmployees();
                dgEmployee.ItemsSource = empList;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        int currentEmployeeId = 0;

        private void dgEmployee_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            emp = (Employee)dgEmployee.SelectedItem;
            if (emp == null)
            {
                currentEmployeeId = 0;
                return;
            }
            currentEmployeeId = emp.Id;
            lblEmployeeId.Content = emp.Id;
            tbName.Text = emp.FirstName;
            tbFamily.Text = emp.LastName;
            tbPhone.Text = emp.Phone.ToString();
            tbMobile.Text = emp.CellPhone.ToString();
            tbPostalCode.Text = emp.PostalCode;
            tbSIN.Text = emp.SIN;
            tbAddress.Text = emp.Address;
            tbUsername.Text = emp.UserName;
            tbPassword.Password = emp.Password;
            dpBirthDate.Text = emp.BirthDate.ToString();
            dpHireDate.Text = emp.HireDate.ToString();
            dpFireDate.Text = emp.FireDate.ToString();
            tbJobTitle.Text = emp.JobTitle;
            cbIsPermanent.IsChecked = (emp.IsPermenant == 1) ? true : false;
            cbIsManager.IsChecked = (emp.IsManager == 1) ? true : false;
            tbSalary.Text = emp.Salary.ToString();
            imgEmployee.Source = null;

            if (emp.EmployeeImage != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(emp.EmployeeImage);
                ms.Seek(0, System.IO.SeekOrigin.Begin);

                BitmapImage newBitmapImage = new BitmapImage();
                newBitmapImage.BeginInit();
                newBitmapImage.StreamSource = ms;
                newBitmapImage.EndInit();
                imgEmployee.Source = newBitmapImage;
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string firstName = tbName.Text;
            string lastName = tbFamily.Text;
            int phone = int.Parse(tbPhone.Text);
            int cellPhone = int.Parse(tbMobile.Text);
            string postalCode = tbPostalCode.Text;
            string sin = tbSIN.Text;
            string address = tbAddress.Text;
            string userName = tbUsername.Text;
            string password = tbPassword.Password;
            DateTime birthDate = dpBirthDate.SelectedDate.Value;
            DateTime hireDate = dpHireDate.SelectedDate.Value;
            DateTime fireDate = dpFireDate.SelectedDate.Value;
            string jobTitle = tbJobTitle.Text;
            int isPermanent = (cbIsPermanent.IsChecked == true) ? 1 : 0;
            int isManager = (cbIsManager.IsChecked == true) ? 1 : 0;
            decimal salary = decimal.Parse(tbSalary.Text);

            emp = new Employee() { FirstName = firstName, LastName = lastName, Phone = phone, CellPhone = cellPhone, Address = address, PostalCode = postalCode, BirthDate = birthDate, JobTitle = jobTitle, HireDate = hireDate, FireDate = fireDate, Salary = salary, IsPermenant = isPermanent, IsManager = isManager, UserName = userName, Password = password, EmployeeImage = imageData, SIN = sin };
            db.AddEmployeeItem(emp);
            tbName.Text = "";
            tbFamily.Text = "";
            tbPhone.Text = "";
            tbMobile.Text = "";
            tbPostalCode.Text = "";
            tbSIN.Text = "";
            tbAddress.Text = "";
            tbUsername.Text = "";
            tbPassword.Password = "";
            dpBirthDate.Text = "";
            dpHireDate.Text = "";
            dpFireDate.Text = "";
            tbJobTitle.Text = "";
            cbIsPermanent.IsChecked = false;
            cbIsManager.IsChecked = false;
            tbSalary.Text = "";
            imgEmployee.Source = null;
            empList = db.GetAllEmployees();
            dgEmployee.ItemsSource = empList;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            emp = (Employee)dgEmployee.SelectedItem;
            if (emp == null)
            {
                MessageBox.Show("Please select an item to delete",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            db.DeleteEmployeeItemById(emp.Id);
            empList = db.GetAllEmployees();
            dgEmployee.ItemsSource = empList;
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployeeId == 0)
            {
                MessageBox.Show("Please select an item for update",
                    "Invalid action", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            int id = int.Parse(lblEmployeeId.Content.ToString());
            string firstName = tbName.Text;
            string lastName = tbFamily.Text;
            int phone = int.Parse(tbPhone.Text);
            int cellPhone = int.Parse(tbMobile.Text);
            string postalCode = tbPostalCode.Text;
            string sin = tbSIN.Text;
            string address = tbAddress.Text;
            string userName = tbUsername.Text;
            string password = tbPassword.Password;
            DateTime birthDate = dpBirthDate.SelectedDate.Value;
            DateTime hireDate = dpHireDate.SelectedDate.Value;
            DateTime fireDate = dpFireDate.SelectedDate.Value;
            string jobTitle = tbJobTitle.Text;
            int isPermanent = (cbIsPermanent.IsChecked == true) ? 1 : 0;
            int isManager = (cbIsManager.IsChecked == true) ? 1 : 0;
            decimal salary = decimal.Parse(tbSalary.Text);

            emp = new Employee() { Id = id, FirstName = firstName, LastName = lastName, Phone = phone, CellPhone = cellPhone, Address = address, PostalCode = postalCode, BirthDate = birthDate, JobTitle = jobTitle, HireDate = hireDate, FireDate = fireDate, Salary = salary, IsPermenant = isPermanent, IsManager = isManager, UserName = userName, Password = password, EmployeeImage = imageData, SIN = sin };
            db.UpdateEmployeeItem(emp);
            tbName.Text = "";
            tbFamily.Text = "";
            tbPhone.Text = "";
            tbMobile.Text = "";
            tbPostalCode.Text = "";
            tbSIN.Text = "";
            tbAddress.Text = "";
            tbUsername.Text = "";
            tbPassword.Password = "";
            dpBirthDate.Text = "";
            dpHireDate.Text = "";
            dpFireDate.Text = "";
            tbJobTitle.Text = "";
            cbIsPermanent.IsChecked = false;
            cbIsManager.IsChecked = false;
            tbSalary.Text = "";
            imgEmployee.Source = null;
            empList = db.GetAllEmployees();
            dgEmployee.ItemsSource = empList;
        }

        byte[] imageData = { };
        private void btLoadImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();

            FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
            imageData = new byte[fs.Length];
            fs.Read(imageData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            ImageSourceConverter imgs = new ImageSourceConverter();
            imgEmployee.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));
        }

        private void btClearImage_Click(object sender, RoutedEventArgs e)
        {
            imgEmployee.Source = null;
            emp.EmployeeImage = null;
            imageData = null;
        }

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            dgEmployee.UnselectAllCells();
            tbName.Text = "";
            tbFamily.Text = "";
            tbPhone.Text = "";
            tbMobile.Text = "";
            tbPostalCode.Text = "";
            tbSIN.Text = "";
            tbAddress.Text = "";
            tbUsername.Text = "";
            tbPassword.Password = "";
            dpBirthDate.Text = "";
            dpHireDate.Text = "";
            dpFireDate.Text = "";
            tbJobTitle.Text = "";
            cbIsPermanent.IsChecked = false;
            cbIsManager.IsChecked = false;
            tbSalary.Text = "";
            imgEmployee.Source = null;
            imageData = null;
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
