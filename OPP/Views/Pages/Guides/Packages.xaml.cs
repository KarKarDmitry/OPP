using OPP.Navigation;
using OPP.ViewModels.Guides;
using OPP.Views.Pages.Guides.Edits;
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

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Packages.xaml
    /// </summary>
    public partial class Packages : Page
    {
        PackagesViewModel viewModel;
        public Packages()
        {
            InitializeComponent();

            viewModel = new PackagesViewModel();
            DataContext = viewModel;
        }

        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditPackage_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPackage(viewModel.SelectedPackage));
        }

        private void Deleteage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
