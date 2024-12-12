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
using OPP.AppData.Registers;
using OPP.Navigation;
using OPP.ViewModels.Guides;
using OPP.Views.Pages.Guides.Edits;

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для Plans.xaml
    /// </summary>
    public partial class Plans : Page
    {
        PlansViewModel viewModel;
        public Plans()
        {
            InitializeComponent();
            viewModel = new PlansViewModel();
            DataContext = viewModel;
        }

        private void AddPlan_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPlan(new Plan()));
        }

        private void EditPlan_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPlan == null) return;

            NavigationManager.Navigate(new EditPlan(viewModel.SelectedPlan));
        }

        private void DeletePlan_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Delete();
        }

        private void CalculatePlan_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPlan == null) return;

            NavigationManager.Navigate(new PlanDetailCalculate(viewModel.SelectedPlan));
        }
    }
}
