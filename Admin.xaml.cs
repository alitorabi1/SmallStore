using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void RibbonGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            Login dialog = new Login();
            this.Close();
            dialog.ShowDialog();
        }

        private void RibbonCategory_Click(object sender, RoutedEventArgs e)
        {
            AdminGroup dialog = new AdminGroup();
            dialog.ShowDialog();
        }

        private void RibbonProduct_Click(object sender, RoutedEventArgs e)
        {
            AdminProduct dialog = new AdminProduct();
            dialog.ShowDialog();
        }

        private void RibbonEmployee_Click(object sender, RoutedEventArgs e)
        {
            AdminProfile dialog = new AdminProfile();
            dialog.ShowDialog();
        }

        private void RibbonReport_Click(object sender, RoutedEventArgs e)
        {
            AdminReports dialog = new AdminReports();
            dialog.ShowDialog();
        }

        private void RibbonTutorial_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            //            Microsoft.Office.Interop.Word.Document document = application.Documents.Open("pack://application:,,,/Documents/proposal.doc");
            Microsoft.Office.Interop.Word.Document document = application.Documents.Open(@"C:\Users\lenovo\Documents\Visual Studio 2015\Projects\SmallStore\Documents\proposal.doc");


            //var msWord = new Microsoft.Office.Interop.Word.Application("pack://application:,,,/Documents/proposal.doc");
            //msWord.Visible = true;

            //Documents document = new Documents();
            //document.LoadFromFile(@"E:\Work\Documents\Spire.Doc for .NET.docx");

            //ProcessStartInfo startInfo = new ProcessStartInfo("pack://application:,,,/Documents/proposal.doc");
            //AdminProduct dialog = new AdminProduct();
            //dialog.ShowDialog();
        }

        private void RibbonAbout_Click(object sender, RoutedEventArgs e)
        {
            //AdminProduct dialog = new AdminProduct();
            //dialog.ShowDialog();
        }

        private void RibbonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
