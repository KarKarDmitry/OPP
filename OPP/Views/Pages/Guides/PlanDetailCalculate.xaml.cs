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
using OPP.ViewClasses;
using ViewModel.ViewModels.Guides;

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для PlanDetailCalculate.xaml
    /// </summary>
    public partial class PlanDetailCalculate : DisposablePage
    {
        PlanDetailCalculateViewModel viewModel;
        public PlanDetailCalculate(Plan plan)
        {
            InitializeComponent();
            
            viewModel = new PlanDetailCalculateViewModel(plan);
            DataContext = viewModel;
        }
    }
}
