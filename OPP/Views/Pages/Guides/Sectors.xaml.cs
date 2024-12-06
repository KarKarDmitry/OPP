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
using OPP.Navigation;
using OPP.ViewModels.Guides;
using OPP.ViewModels.Guides.Edits;
using OPP.Views.Pages.Guides.Edits;

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Sectors.xaml
    /// </summary>
    public partial class Sectors : Page
    {
        SectorsViewModel viewModel;
        public Sectors()
        {
            InitializeComponent();
            viewModel = new SectorsViewModel();
            DataContext = viewModel;
        }

        private void AddSector_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Add();
        }

        private void EditSector_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedSector == null) return;

            NavigationManager.Navigate(new EditSector(viewModel.SelectedSector));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }
    }
}
