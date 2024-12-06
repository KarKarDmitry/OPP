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
using OPP.ViewModels.Guides;
using OPP.ViewModels.Registers;

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для RemainInMonth.xaml
    /// </summary>
    public partial class Remainings: Page
    {
        RemainingsViewModel viewModel = new RemainingsViewModel();
        public Remainings()
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void SaveRemainings_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
