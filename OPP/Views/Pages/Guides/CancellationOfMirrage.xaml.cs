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

namespace OPP.Views.Pages.Guides
{
    /// <summary>
    /// Логика взаимодействия для CancellationOfMirrage.xaml
    /// </summary>
    public partial class CancellationOfMirrage : Page
    {
        public CancellationOfMirrage()
        {
            InitializeComponent();
        }

        private async void CancelMirrage_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Эту операцию может отменить только администратор базы данных!\r\nВы уверены?", "Подтверждение операции", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No) return;



        }

        private void AddMirragelRow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMirragelRow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
