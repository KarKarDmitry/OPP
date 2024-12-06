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
    /// Логика взаимодействия для Boxes.xaml
    /// </summary>
    public partial class Boxes : Page
    {
        BoxesViewModel viewModel;
        public Boxes()
        {
            InitializeComponent();

            viewModel = new BoxesViewModel();
            DataContext = viewModel;
        }

        private void AddBox_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditBox_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedBox == null) return;
            NavigationManager.Navigate(new EditBox(viewModel.SelectedBox));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
