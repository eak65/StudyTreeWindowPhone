using App1.Common;
using App1.Model.Transfer;
using App1.Student.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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

namespace App1.Student
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentStartSessionPage : Page
    {
        private NavigationHelper navigationHelper;
        private StudentStartViewModel defaultViewModel;
        private int _sessionId;
        private DispatcherTimer _timer;
        private Boolean _hasStarted = false;

        public StudentStartSessionPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
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
        public StudentStartViewModel DefaultViewModel
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
            if(e.NavigationMode == NavigationMode.New)
            {
                _sessionId = (int)e.Parameter;
                defaultViewModel = new StudentStartViewModel(DataManager.shared().getStudySessionFromId(_sessionId));
                this.DataContext = defaultViewModel;
                _timer = new DispatcherTimer();
                _timer.Tick += _timer_Tick;
                _timer.Interval = new TimeSpan(0, 0, 1);
            }
        }

        private void _timer_Tick(object sender, object e)
        {
            if (defaultViewModel.Seconds > 0)
            {
                defaultViewModel.Seconds -= 1;
            }
            else
            {
                _timer.Stop();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void StartSessionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_hasStarted)
            {
                _timer.Start();
                Button b = sender as Button;
                b.Content = "Stop";
                _hasStarted = true;
            }
            else
            {
                _timer.Stop();
            }
        }

        private async void SubmitUpdateTimeButton_Click(object sender, RoutedEventArgs e)
        {
            TimerModel model = new TimerModel(defaultViewModel.IncreaseSeconds, _sessionId, defaultViewModel.Session.StudentId);
            await RequestHandler.Shared().putStudentUpdateTimer(model);
            defaultViewModel.Seconds += defaultViewModel.IncreaseSeconds;
            defaultViewModel.IncreaseSeconds = 0;
        }

        private void DecreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if(defaultViewModel.IncreaseSeconds > 600)
            {
                defaultViewModel.IncreaseSeconds -= 10 * 60;
            }
            else if(defaultViewModel.IncreaseSeconds > 0)
            {
                defaultViewModel.IncreaseSeconds = 0;
            }
        }

        private void IncreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            defaultViewModel.IncreaseSeconds += 10 * 60;
        }
    }
}
