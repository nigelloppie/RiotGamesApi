using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZillowAPIApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterPage : Page
    {
        public UserViewModel userViewModel;
        public UserModel userModel;
        public FilterPage()
        {
            this.InitializeComponent(); 
            userViewModel = new UserViewModel("", this);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // show back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            // navigate if possible
            SystemNavigationManager.GetForCurrentView().BackRequested += About_BackRequested;
        }

        private void About_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            e.Handled = true;
        }

        private void seachBtn_Click(object sender, RoutedEventArgs e)
        {
            userViewModel = new UserViewModel(seachBar.Text, this);
        }
    }
}
