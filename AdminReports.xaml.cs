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
    public partial class AdminReports : Window
    {
        public AdminReports()
        {
            InitializeComponent();
            dpFrom.Text = DateTime.Today.ToString();
            dpTo.Text = DateTime.Today.ToString();
        }

        private void rptEmployeeDetail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Login dlg = new Login();
            dlg.ShowDialog();
        }

        private void rptCustomerDetail_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
