using OPP.AppData.Registers;
using OPP.Navigation;
using OPP.ViewModels.Registers;
using OPP.Views.Pages.Accounting.Edits;
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

namespace OPP.Views.Pages.Accounting
{
    /// <summary>
    /// Логика взаимодействия для MaterialPrices.xaml
    /// </summary>
    public partial class MaterialPrices : Page
    {
        MaterialPricesViewModel viewModel;
        public MaterialPrices()
        {
            InitializeComponent();

            viewModel = new MaterialPricesViewModel();
            DataContext = viewModel;
        }

        private void AddMaterialPrice_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditMaterialPrice(new MaterialPrice()));
        }

        private void EditMaterialPrice_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedMaterialPrice == null) return;
            NavigationManager.Navigate(new EditMaterialPrice(viewModel.SelectedMaterialPrice));
        }

        private void ConfirmPeriodBounds_Click(object sender, RoutedEventArgs e)
        {
            if (!viewModel.PeriodStart.HasValue)
            {
                MessageBox.Show("Не указано начало периода", "Warning", MessageBoxButton.OK);
                return;
            }

            viewModel.GetMaterialPricesByPeriod();

        }
    }
}
