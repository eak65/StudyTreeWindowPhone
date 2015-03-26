using App1.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public MainPage()
        {
           this.InitializeComponent();
            DrawerLayout.InitializeDrawerLayout();
          
        }

        private void burger_Click(object sender, RoutedEventArgs e)
        {

            this.selectBurger(); 
        }
        public void selectBurger()
        {
            if (DrawerLayout.IsDrawerOpen == false)
            {
                DrawerLayout.OpenDrawer();
            }
            else
            {
                DrawerLayout.CloseDrawer();
            }
        }

        private void ProfileTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp));
            this.selectBurger(); 

        }

        private void StudentTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.selectBurger(); 

        }

        private void TutorTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.selectBurger(); 

        }

        private void SettingTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.selectBurger(); 

        }

   
        private void DrawerIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.selectBurger(); 

        }


  
    }
}
