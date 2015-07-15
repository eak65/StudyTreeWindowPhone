using App1.Common;
using App1.Model;
using App1.Student;
using HubApp1.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using App1.Model.Transfer;
using App1.SignalR;
using RestSharp.Portable;

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
           this.NavigationCacheMode = NavigationCacheMode.Required;
            SignalR.SignalR signalR = new SignalR.SignalR(Constants.url);

           this.navigationHelper = new NavigationHelper(this);
           this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
           this.navigationHelper.SaveState += this.NavigationHelper_SaveState;


        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        private async  void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

          
          var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
           this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }


        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void LearnSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            Type selectionPage = null;
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if(itemId.Equals("1"))
            {
                selectionPage = typeof(StudentProfilePage);
            }
            else if (itemId.Equals("2"))
            {
                selectionPage = typeof(StudySessionPage);

            }
            else if (itemId.Equals("3"))
            {
                selectionPage = typeof(StudentNotificationsPage);

            }
            Frame.Navigate(selectionPage, itemId);
     
        }

        private void TeachSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;

        }

     



  
    }
}
