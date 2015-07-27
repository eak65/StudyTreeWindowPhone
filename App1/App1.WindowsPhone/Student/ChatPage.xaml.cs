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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1.StudentView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableCollection<TreeMessage> _allMessages;
        private int _sessionId;
        private int _tutorId;

        public ChatPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            Loaded += ChatPage_Loaded;
        }

        private void ChatPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (_allMessages.Count > 0)
            {
                myChat.ScrollIntoView(myChat.Items[_allMessages.Count - 1]);
            }
        }


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
            Dictionary<string,int> param = e.Parameter as Dictionary<string, int>;
            if(param.ContainsKey("SessionId") && param.ContainsKey("TutorId"))
            {
                _sessionId = param["SessionId"];
                _tutorId = param["TutorId"];
                PreliminaryTutor tutor = DataManager.shared().getTutor(_sessionId, _tutorId);
                _allMessages = tutor.Messages;
                myChat.ItemsSource = _allMessages;
                _allMessages.CollectionChanged += _allMessages_CollectionChanged;
                ChatConversation.KeyDown += ChatConversation_KeyDown;
            }
        }

        private async void ChatConversation_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                String message = ChatConversation.Text;
                StudySession session = DataManager.shared().getStudySessionFromId(_sessionId);
                TreeMessage sentMessage = new TreeMessage(_tutorId, _sessionId, message);
                await RequestHandler.Shared().postStudentMessage(sentMessage);
                _allMessages.Add(sentMessage);
                ChatConversation.Text = "";
            }
        }

        private void _allMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateLayout();
            if(_allMessages.Count > 0)
            {
                CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                if (!dispatcher.HasThreadAccess)
                {
                    Task.Run(() => Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { myChat.ScrollIntoView(myChat.Items[_allMessages.Count - 1]); }));
                }
                else
                {
                    myChat.ScrollIntoView(myChat.Items[_allMessages.Count - 1]);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
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

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            String message = ChatConversation.Text;
            StudySession session = DataManager.shared().getStudySessionFromId(_sessionId);
            TreeMessage sentMessage = new TreeMessage(_tutorId, _sessionId, message);
            await RequestHandler.Shared().postStudentMessage(sentMessage);
            _allMessages.Add(sentMessage);
            ChatConversation.Text = "";
        }
    }
}
