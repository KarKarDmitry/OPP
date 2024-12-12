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
using OPP.ViewModels.Guides.Edits;

namespace OPP.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditPlanViewModel.xaml
    /// </summary>
    public partial class EditPlan : DisposablePage
    {
        EditPlanViewModel viewModel;
        public EditPlan(Plan plan)
        {
            InitializeComponent();
            viewModel = new EditPlanViewModel(plan);
            DataContext = viewModel;
        }

        private void AddPlanDetailRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPlanDetail();
        }

        private void DeletePlanDetailRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePlanDetail();
        }

        private void SavePlan_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SavePlanAsync();
        }

        private void PlanDetails_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedCells.Count > 0)
            {
                // Получаем первую выделенную ячейку
                var selectedCell = dataGrid.SelectedCells[0];

                // Получаем объект строки, содержащий выбранную ячейку
                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromItem(selectedCell.Item) as DataGridRow;

                if (row != null)
                {
                    // Получаем колонку с ячейкой
                    var column = selectedCell.Column;

                    // Программно включаем редактирование ячейки
                    dataGrid.CurrentCell = new DataGridCellInfo(selectedCell.Item, column);
                    dataGrid.BeginEdit();
                }
            }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true;
            }
        }

    }
}
