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
    public partial class Login : Window
    {

         Database db;

        public Login()
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
            tbUsername.Focus();


        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if ( tbUsername.Text.Length < 1 || pbPassword.Password.Length <1 )
            {
                MessageBox.Show("Please enter username and password", "User Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            bool isManager;
            try
            {

                if (db.LoginUser(tbUsername.Text, pbPassword.Password, out isManager) && isManager == true)
                {
                    Admin dialog = new Admin();
                    dialog.lblWelcome.Content = "Welcome: " + tbUsername.Text;
                    this.Close();
                    dialog.ShowDialog();
                }
                else if (db.LoginUser(tbUsername.Text, pbPassword.Password, out isManager) && isManager == false)
                {
                    
                    Employee employee = db.GetEmployeeByUserName(tbUsername.Text);
                    Cashier dialog = new Cashier(employee, DateTime.Now);
                    this.Close();
                    dialog.ShowDialog();
                    
                   
                }
                else
                {
                    
                    MessageBox.Show("Invalid Username or Password. Please Try again", "User Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Unable fetch data from database", "database Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                 Environment.Exit(1);
            }
        }

        private void bpPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                btnSubmit.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            
        }
        void MainWindow_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
