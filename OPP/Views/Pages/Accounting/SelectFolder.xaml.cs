using OPP.Navigation;
using OPP.ViewClasses;
using OPP.Views.Pages.Guides;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OPP.Views.Pages.Accounting
{
    /// <summary>
    /// Логика взаимодействия для SelectFolder.xaml
    /// </summary>
    public partial class SelectFolder : Page
    {
        public SelectFolder()
        {
            InitializeComponent();

            AccountingNavigation.NavigateTo += OnNavigateToPage;
            AccountingNavigation.NavigateGoBack += OnNavigateGoBack;

        }

        private void OnNavigateToPage(Page page)
        {
            if (ContentFrame.NavigationService.Content != null)
            {
                Storyboard fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];

                EventHandler fadeOutCompletedHandler = null;
                fadeOutCompletedHandler = (s, e) =>
                {
                    fadeOutStoryboard.Completed -= fadeOutCompletedHandler;

                    ContentFrame.NavigationService.Navigate(page);

                    Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
                    fadeInStoryboard.Begin();
                };

                fadeOutStoryboard.Completed += fadeOutCompletedHandler;

                fadeOutStoryboard.Begin();
            }
            else
            {
                ContentFrame.NavigationService.Navigate(page);
            }
        }

        private void OnNavigateGoBack()
        {
            if (!ContentFrame.NavigationService.CanGoBack) return;

            Storyboard fadeOutStoryboard = (Storyboard)this.Resources["FadeOutStoryboard"];

            EventHandler fadeOutCompletedHandler = null;
            fadeOutCompletedHandler = (s, e) =>
            {
                fadeOutStoryboard.Completed -= fadeOutCompletedHandler;
                ContentFrame.NavigationService.GoBack();

                if (ContentFrame.NavigationService.Content is DisposablePage previewsPage)
                {

                }

                Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
                fadeInStoryboard.Begin();
            };

            fadeOutStoryboard.Completed += fadeOutCompletedHandler;

            fadeOutStoryboard.Begin();
        }

        private void GlobalGuides_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GlobalGuideViewPage"));
        }

        private void Boxes_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("GuidesBoxesPage"));
        }

        private void MaterialPrices_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate(App.GetPage("AccoutingMaterialPricesPage"));
        }
    }
}
