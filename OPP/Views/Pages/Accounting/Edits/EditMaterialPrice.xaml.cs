using OPP.AppData.Guides;
using OPP.AppData.Registers;
using OPP.ViewModels.Registers.Edits;
using OPP.ViewClasses;
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
using System.Configuration;
using OPP.Navigation;

namespace OPP.Views.Pages.Accounting.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditMaterialPrice.xaml
    /// </summary>
    public partial class EditMaterialPrice : DisposablePage
    {
        private EditMaterialPricesViewModel? viewModel;
        private MaterialPrice MaterialPrice;

        public EditMaterialPrice(MaterialPrice materialPrice)
        {
            InitializeComponent();
            DataContext = new EditMaterialPricesViewModel(MaterialPrice);
        }

        private void EditMaterial_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel?.SelectedMaterial == null) return;
            NavigationManager.Navigate(new EditMaterial(viewModel?.SelectedMaterial));
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditMaterial(new Material()));
        }

        private void SaveMaterialPrice_Click(object sender, RoutedEventArgs e)
        {
            viewModel?.SaveMaterialPrice();
        }
    }
}
