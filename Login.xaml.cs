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
        public Login()
        {
            InitializeComponent();
            tbUsername.Focus();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if ( tbUsername.Text.Length == 0 || pbPassword.Password.Length == 0 )
            {
                lblErrorMessage.Content = "Please enter username or password.";
                return;
            }

            if ( isAuthenticated() == "ADMINISTRATOR")
            {
                Admin dialog = new Admin();
                dialog.lblWelcome.Content = "Welcome: " + tbUsername.Text;
                dialog.ShowDialog();
                this.Close();
            }
            else if (isAuthenticated() == "CASHIER" )
            {
                Cashier dialog = new Cashier();
                dialog.ShowDialog();
                this.Close();
            }
            else
            {
                lblErrorMessage.Content = "Invalid Username or Password. Please Try again";
                return;
            }
        }

        private string isAuthenticated()
        {
            // TODO: this method checks the role of user and authenticate him

            //            throw new NotImplementedException();
            //            return "CASHIER";
            return "ADMINISTRATOR";
            //return "";
        }

        private void tbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            lblErrorMessage.Content = "";
        }
    }
}
