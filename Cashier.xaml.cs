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
        public string userName;
        public DateTime loginDate; 

        public Cashier(string user,DateTime d)
        {
            InitializeComponent();
            userName = user;
            loginDate = d;
            this.Title = userName+" entered in "+loginDate.ToString("yyyy-MM-dd HH:mm");
        }

        private void tbProdNameOrBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
