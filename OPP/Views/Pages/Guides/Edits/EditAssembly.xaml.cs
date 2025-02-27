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
using System.Reflection;
using Assembly = OPP.AppData.Guides.Assembly;
using OPP.Environment;
using OPP.Navigation;

namespace OPP.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditAssembly.xaml
    /// </summary>
    public partial class EditAssembly : DisposablePage
    {
        private EditAssemblyViewModel viewModel;
        public EditAssembly(Assembly assembly)
        {
            InitializeComponent();
            viewModel = new EditAssemblyViewModel(assembly);
            DataContext = viewModel;

            AssemblyPlane.Source = Assembly.GetImageAsBitmap(viewModel.Assembly);
        }

        protected override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditCoatingType_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Assembly.Coating != null)
            {
                NavigationManager.Navigate(new EditCoating(Coating.GetByID(viewModel.Assembly.Coating.Value)));
            }
        }

        private void AddCoatingType_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditCoating(new Coating()));
        }

        private void DeleteAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteAssemblyComposition();
        }

        private void AddAssemblyCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddAssemblyComposition();
        }

        private void SaveCompositions_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveCompositionsAsync();
        }

        private void SaveAssembly_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveAssemblyAsync();
        }

        private void AssemblyComposition_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
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

        private void AssemblyDetailCompositions_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedCells.Count > 0)
            {
                var selectedCell = dataGrid.SelectedCells[0];

                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromItem(selectedCell.Item) as DataGridRow;

                if (row != null)
                {
                    var column = selectedCell.Column;

                    dataGrid.CurrentCell = new DataGridCellInfo(selectedCell.Item, column);
                    dataGrid.BeginEdit();
                }
            }
        }

        private void EditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedPartComposition != null)
            {
                NavigationManager.Navigate(new EditPart(Part.GetByID(viewModel.SelectedPartComposition.Part.Value)));
            }
        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditPart(new Part()));
        }

        private void AddAssemblies_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(new EditAssembly(new Assembly()));
        }

        private void EditAssemblies_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedAssemblyComposition.Subassembly != null)
            {
                NavigationManager.Navigate(new EditAssembly(Assembly.GetByID(viewModel.SelectedAssemblyComposition.Subassembly.Value)));
            }
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddPartCompositions();
        }

        private void DeletePartRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeletePartComposition();
        }

        private void AddToleranceRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddTolerance();
        }

        private void DeleteToleranceRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteTolerance();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            var byteImage = ImageWorker.TakeImageOrCompasAsBytes();
            var byteMap = Assembly.SetImage(viewModel.Assembly, byteImage);
            if (byteMap != null) AssemblyPlane.Source = byteMap;
        }
    }
}
