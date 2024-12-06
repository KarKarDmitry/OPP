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
using OPP.AppData.Guides;
using OPP.ViewModels.Guides.Edits;
using OPP.ViewClasses;

namespace OPP.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditSector.xaml
    /// </summary>
    public partial class EditSector : DisposablePage
    {
        EditSectorViewModel viewModel { get; set; }
        public EditSector(Sector sector)
        {
            InitializeComponent();

            viewModel = new EditSectorViewModel(sector);
            DataContext = viewModel;
        }

        private void SaveSector_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveSector();
        }
    }
}
