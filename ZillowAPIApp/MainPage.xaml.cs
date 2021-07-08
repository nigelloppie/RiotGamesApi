using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ZillowAPIApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ChampionViewModel appViewModel;
        public UserViewModel userViewModel;


        public MainPage()
        {
            this.InitializeComponent();
            appViewModel = new ChampionViewModel(this);
            appViewModel.SelectedChampion = null;
            var bgImage = new BitmapImage(new Uri(this.BaseUri, "Assets/lolbg.jpg"));
            var brush = new ImageBrush();
            brush.ImageSource = bgImage;
            brush.Opacity = 0.5;
            this.Background = brush;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FilterPage));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // show back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }
    }
}
