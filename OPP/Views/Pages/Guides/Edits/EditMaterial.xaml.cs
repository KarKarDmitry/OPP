using OPP.AppData.Guides;
using OPP.ViewModels.Guides.Edits;
using OPP.ViewClasses;
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

namespace OPP.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditMaterial.xaml
    /// </summary>
    public partial class EditMaterial : DisposablePage
    {
        EditMaterialViewModel viewModel;
        public EditMaterial(Material material)
        {
            InitializeComponent();

            viewModel = new EditMaterialViewModel(material);
            DataContext = viewModel;
        }

        private void SaveMaterial_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveMaterial();
        }
    }
}
