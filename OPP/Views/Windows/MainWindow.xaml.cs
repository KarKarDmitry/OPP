﻿using OPP.Views.Pages.Guides;
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

using P_PDB = OPP.Views.Pages.Guides;
using P_Accounting = OPP.Views.Pages.Accounting;
using System.Reflection;

using OPP.AppData.Guides;
using OPP.ViewModels.Guides;
using Assembly = OPP.AppData.Guides.Assembly;
using OPP.Views.Windows;
using OPP.Views.Pages;
using OPP.ViewModels.Registers;
using OPP.Navigation;

namespace OPP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            // Загружаем данные и ждем завершения перед отображением окна
            LoadDataAsync();

            new _GeneralGuidesViewModel().SubscribeEvents();
            new _GeneralRegistersViewModel().SubscribeEvents();

        }

        private async Task LoadDataAsync()
        {
            // Создаем список задач для параллельной загрузки данных
            var tasks = new List<Task>();

            tasks.AddRange(new List<Task>
            {
                Task.Run(() => Coating.GetAll()),
                Task.Run(() => AppData.Guides.Label.GetAll()),
                Task.Run(() => Manual.GetAll()),
                Task.Run(() => Blank.GetAll()),
                Task.Run(() => Box.GetAll()),
                Task.Run(() => Package.GetAll()),
                Task.Run(() => Part.GetAll()),
                Task.Run(() => Assembly.GetAll()),
                Task.Run(() => Kit.GetAll()),
                Task.Run(() => Lock.GetAll()),
                Task.Run(() => Packaging.GetAll()),
                Task.Run(() => AssemblyAssemblyComposition.GetAll()),
                Task.Run(() => AssemblyPartComposition.GetAll()),
                Task.Run(() => KitAssemblyComposition.GetAll()),
                Task.Run(() => KitPartComposition.GetAll()),
                Task.Run(() => LockAssemblyComposition.GetAll()),
                Task.Run(() => LockPartComposition.GetAll()),
                Task.Run(() => PackagingAssemblyComposition.GetAll()),
                Task.Run(() => PackagingPartComposition.GetAll()),
                Task.Run(() => PackagingLockComposition.GetAll()),
                Task.Run(() => PartComposition.GetAll())
            });

            // Await all tasks to complete
            await Task.WhenAll(tasks);

        }


        private void PDP_Button_Checked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(App.GetNavigationPage<P_PDB.SelectFolder>("GuidesSelectFolderPage"));
            NavigationManager.SetGuidesContext();
        }

        private void Accounting_Button_Checked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(App.GetNavigationPage<P_Accounting.SelectFolder>("AccountingSelectFolderPage"));
            NavigationManager.SetAccountingContext();
        }

        private void TechnicalBureau_Button_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
