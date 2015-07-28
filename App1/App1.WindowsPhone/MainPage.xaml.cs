using App1.Common;
using App1.Model;
using App1.StudentView;
using HubApp1.Data;
using System;

using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.TutorView;
using App1.Model.Logic;
using System.Threading.Tasks;
using Windows.UI;

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

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            this.Background = new SolidColorBrush(Color.FromArgb(255, 26, 188, 156));
            this.defaultViewModel["Account"] = DataManager.shared().myself;
        }

        public async Task loadUniversity()
        {
            //if (DataManager.shared().University != null)
            //{
            //    Task<StResponse> responseTask = RequestHandler.Shared().getUniversity();
            //    StResponse response = await responseTask;
            //    DataManager.shared().University = response.Response as University;
            //}
            Task<StResponse> responseTask = RequestHandler.Shared().getUniversity();
            StResponse response = await responseTask;
            DataManager.shared().University = response.Response as University;
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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            await loadUniversity();
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
            Type selectionPage = null;
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            if (itemId.Equals("1"))
            {
                selectionPage = typeof(StudentProfilePage);
            }
            else if (itemId.Equals("2"))
            {
                selectionPage = typeof(TutorStudySessions);

            }
            else if (itemId.Equals("3"))
            {
                selectionPage = typeof(FindStudySession);

            }
            Frame.Navigate(selectionPage, itemId);

        }

        private void logoutButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove("token");
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
