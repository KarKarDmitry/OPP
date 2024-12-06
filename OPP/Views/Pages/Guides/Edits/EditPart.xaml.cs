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
using OPP.Environment;
using OPP.Navigation.Guides;

namespace OPP.Views.Pages.Guides.Edits
{
    /// <summary>
    /// Логика взаимодействия для EditPart.xaml
    /// </summary>
    public partial class EditPart : DisposablePage
    {
        EditPartViewModel viewModel;
        public EditPart(Part part)
        {
            InitializeComponent();

            viewModel = new EditPartViewModel(part);
            DataContext = viewModel;

            PartPlane.Source = Part.GetImageAsBitmap(viewModel.Part);
        }

        protected override void DisposeResources()
        {
            viewModel = null;
        }

        private void EditCoatingType_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Part.Coating == null) return;

            GuidesNavigation.Navigate(new EditCoating(Coating.GetByID(viewModel.Part.Coating.Value)));
        }

        private void AddCoatingType_Click(object sender, RoutedEventArgs e)
        {
            GuidesNavigation.Navigate(new EditCoating(new Coating()));
        }

        private void AddPartCompositionRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddBlankCompositions();
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteBlankComposition();
        }

        private void EditDetail_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedBlankComposition?.Blank == null) return;

            GuidesNavigation.Navigate(new EditBlank(Blank.GetByID(viewModel.SelectedBlankComposition.Blank.Value)));
        }

        private void AddDetail_Click(object sender, RoutedEventArgs e)
        {
            GuidesNavigation.Navigate(new EditBlank(new Blank()));
        }

        private void SaveComposition_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveCompositions();
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

        private void SavePart_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SavePart();
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
            var byteMap = Part.SetImage(viewModel.Part, byteImage);
            if (byteMap != null) PartPlane.Source = byteMap;
        }
    }
}
