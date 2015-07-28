using App1.Common;
using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1.StudentView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudySessionPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private DataManager dataManager = DataManager.shared();
        private ObservableCollection<StudySession> _activeSessions = new ObservableCollection<StudySession>();
        private ListView _sessionListView;

        public StudySessionPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            _sessionListView = StudentStudySessionList;
        }

        public async Task loadInformation()
        {
            foreach (StudySession s in dataManager.myself.StudentStudySessions)
            {
                if (s.TypeCode < 3)
                {
                    _activeSessions.Add(s);
                }
            }

            if (!defaultViewModel.ContainsKey("StudySessions"))
                defaultViewModel.Add("StudySessions", _activeSessions);
            else
                defaultViewModel["StudySessions"] = _activeSessions;
            StResponse res = await RequestHandler.Shared().getStudentSessions();
            reloadSession();
        }

        private void reloadSession()
        {
            try
            {
                ObservableCollection<StudySession> _temp = new ObservableCollection<StudySession>();
                foreach (StudySession s in DataManager.shared().myself.StudentStudySessions)
                {
                    if (s.TypeCode < 3)
                    {
                        _temp.Add(s);
                    }
                }

                _activeSessions = _temp;
                //CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                //if (!dispatcher.HasThreadAccess)
                //{
                //    Task.Run(() => Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { _activeSessions.Clear(); }));
                //    Task.Run(() => Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { _activeSessions.Concat(_temp); }));
                //}
                if (!defaultViewModel.ContainsKey("StudySessions"))
                    defaultViewModel.Add("StudySessions", _activeSessions);
                else
                    defaultViewModel["StudySessions"] = _activeSessions;
            }
            catch (Exception e) { }
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
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

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
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
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            Task.Run(() => loadInformation());       
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AddStudentStudySession_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StudentCreateStudySessionPage));
        }

        private void StudentStudySessionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_sessionListView.SelectedItem != null)
            {
                StudySession selectedSession = _sessionListView.SelectedItem as StudySession;
                this.Frame.Navigate(typeof(StudentDetailStudySession), selectedSession.StudySessionId);
            }
        }
    }
}
