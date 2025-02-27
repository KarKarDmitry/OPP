﻿using OPP.AppData.Guides;
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
using OPP.Navigation.Guides;

namespace OPP.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditKitViewModel.xaml
    /// </summary>
    public partial class EditKit : DisposablePage
    {
        EditKitViewModel viewModel;
        public EditKit(Kit kit)
        {
            InitializeComponent();

            viewModel = new EditKitViewModel(kit);
            DataContext = viewModel;
        }

        protected override void DisposeResources()
        {
            viewModel = null;
        }

        private void SaveKit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveKit();
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPartCompositions();
        }

        private void DeletePartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePartComposition();
        }

        private void SaveComposition_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveCompositions();
        }

        private void AddAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddAssemblyComposition();
        }

        private void DeleteAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteAssemblyComposition();
        }

        private void EditPackage_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Kit.Package == null) return;
            GuidesNavigation.NavigateTo(new EditPackage(Package.GetByID(viewModel.Kit.Package.Value)));
        }

        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            GuidesNavigation.NavigateTo(new EditPackage(new Package()));
        }       

        private void EditAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedAssemblyComposition.Assembly == null) return;
            GuidesNavigation.NavigateTo(new EditAssembly(Assembly.GetByID(viewModel.SelectedAssemblyComposition.Assembly.Value)));
        }

        private void AddAssembly_Click(object sender, RoutedEventArgs e)
        {
            GuidesNavigation.NavigateTo(new EditAssembly(new Assembly()));
        }

        private void EditPart_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPartComposition.Part == null) return;
            GuidesNavigation.NavigateTo(new EditPart(Part.GetByID(viewModel.SelectedPartComposition.Part.Value)));
        }

        private void AddPart_Click(object sender, RoutedEventArgs e)
        {            
            GuidesNavigation.NavigateTo(new EditPart(new Part()));
        }

        private void SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
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
