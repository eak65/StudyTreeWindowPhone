using App1.Common;
using App1.Model.Logic;
using App1.Model.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
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
    public sealed partial class StudentDetailStudySession : Page
    {
        private StudySession _session;
        private NavigationHelper navigationHelper;
        private StackPanel _activeTutorStack;
        private DetailStudentStudySessionViewModel defaultViewModel;

        public StudentDetailStudySession()
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
        public DetailStudentStudySessionViewModel DefaultViewModel
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
            int sessionid = (int)e.NavigationParameter;
            _session = DataManager.shared().getStudySessionFromId(sessionid);
            if (_session != null)
            {
                defaultViewModel = new DetailStudentStudySessionViewModel(_session);
                this.DataContext = defaultViewModel;
                //if (_session.ActiveTutor == null)
                //{
                //    if(_activeTutorStack != null)
                //    {
                //        _activeTutorStack.Visibility = Visibility.Collapsed;
                //    }
                //}
            }
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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentCreateStudySessionPage));
        }

        private void activeTutorAccept_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            Frame.Navigate(typeof(MainPage));
        }

        private async void prelimTutorAccept_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
            if (_session.ActiveTutor == null)
            {
                Button button = sender as Button;
                Border grid = (button.Parent as Grid).Parent as Border;
                PreliminaryTutor tutor = (grid.DataContext as PrelimTutorVM).Tutor;

                if(tutor != null)
                {
                    AcceptModel model = new AcceptModel(tutor.TutorId, _session.StudentId, _session.StudySessionId);
                    await RequestHandler.Shared().putStudentAccept(model);
                    defaultViewModel.ActiveTutor = tutor;
                    defaultViewModel.PreliminaryTutors.RemoveAt(_session.PreliminaryTutors.IndexOf(tutor));
                    _activeTutorStack.Visibility = Visibility.Visible;
                }
            }
        }

        private void ActiveTutorStackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            _activeTutorStack = sender as StackPanel;
            if(_session != null && _session.ActiveTutor == null)
            {
                _activeTutorStack.Visibility = Visibility.Collapsed;
            }
        }

        private void FindTutorButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
           Frame.Navigate(typeof(StudentNotifyTutorPage), _session.StudySessionId);
        }

        private async void DeleteSessionButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure you want to delete the session?", "Confirmation");
            dialog.Commands.Add(new UICommand("Delete", new UICommandInvokedHandler(CommandHandler)));
            dialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(CommandHandler)));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            await dialog.ShowAsync();
        }

        private async void CommandHandler(IUICommand command)
        {
            var label = command.Label;

            switch (label)
            {
                case "Delete":
                    await RequestHandler.Shared().deleteStudentStudySession(_session.StudySessionId);
                    DataManager.shared().myself.StudentStudySessions.Remove(_session);
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                    else
                    {
                        Frame.Navigate(typeof(StudySessionPage));
                    }
                    break;
                case "Cancel":
                    break;           
            }
        }
    }
}
