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
using OPP.ViewModels.Registers;

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Movements.xaml
    /// </summary>
    public partial class Movements : Page
    {
        MovementsViewModel viewModel;
        public Movements()
        {
            InitializeComponent();
            viewModel = new MovementsViewModel();
            DataContext = viewModel;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

        private void SaveMovements_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
