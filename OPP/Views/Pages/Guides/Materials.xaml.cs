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
    /// Логика взаимодействия для Materials.xaml
    /// </summary>
    public partial class Materials : Page
    {
        MaterialsViewModel viewModel;
        public Materials()
        {
            InitializeComponent();

            viewModel = new MaterialsViewModel();
            DataContext = viewModel;
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedMaterial == null) return;
            NavigationManager.Navigate(new EditMaterial(viewModel.SelectedMaterial));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
