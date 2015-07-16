using App1.Common;
using App1.Model.Logic;
using App1.Model.Transfer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace App1.Student
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentCreateStudySessionPage : Page
    {
        private NavigationHelper navigationHelper;
        private StudentCreateStudySessionViewModel defaultViewModel = new StudentCreateStudySessionViewModel();
        public static StudentCreateStudySessionViewModel staticVM;

        public StudentCreateStudySessionPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            defaultViewModel = new StudentCreateStudySessionViewModel();
            this.DataContext = defaultViewModel;
            staticVM = defaultViewModel;
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
        public StudentCreateStudySessionViewModel DefaultViewModel
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
            if (e.PageState != null && e.PageState.ContainsKey("ViewModel"))
            {
                StudentCreateStudySessionViewModel newModel = e.PageState["ViewModel"] as StudentCreateStudySessionViewModel;
                defaultViewModel.CurrentTime = newModel.CurrentTime;
                defaultViewModel.SelectedCourse = newModel.SelectedCourse;
                defaultViewModel.SubjecTitle = newModel.SubjecTitle;
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
            e.PageState.Add("ViewModel", defaultViewModel);
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
            SelectedCourseTextBlock.Visibility = Visibility.Visible;
            //if (e.NavigationMode == NavigationMode.Back) {
            //    Course c = e.Parameter as Course;
            //    if(c != null)
            //    {
            //        defaultViewModel.SelectedCourse = c.Title;
            //        this.navigationHelper.OnNavigatedTo(e);
            //        SelectedCourseTextBlock.Visibility = Visibility.Visible;
            //    }
            //}
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void StudentSelectClassButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UniversityCourseListPage));
        }

        private async void StudentSubmitNewSessionButton_Click(object sender, RoutedEventArgs e)
        {
            StudySessionModel model = new StudySessionModel();
            model.CourseName = defaultViewModel.SelectedCourse;
            model.StudentId = DataManager.shared().myself.PersonId;
            model.Location = "POINT(39.9540 -75.1880)";
            model.TimeRequested = defaultViewModel.CurrentTime.ToString();
            model.SubjectName = defaultViewModel.SubjecTitle;

            await RequestHandler.Shared().postStudentSubject(new PostSubjectModel(model.CourseName));
            RequestHandler.Shared().postStudySession(model);
            Frame.Navigate(typeof(StudySessionPage));
        }

        private void IncreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            defaultViewModel.CurrentTime += 10;
        }

        private void DecreaseTimeButton_Click(object sender, RoutedEventArgs e)
        {
            defaultViewModel.CurrentTime -= 10;
        }
    }
}
