using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.Model.Logic
{
    public class Person : BaseINPC
    {
        private TreeMessage _lastMessage;

        public ObservableCollection<TreeMessage> Messages;
        public double PersonId;
        public String FirstName;
        public String LastName;
        public String Email;
        public String password;
        public TreeMessage LastMessage
        {
            get { return _lastMessage; }
            set
            {
                _lastMessage = value;
                NotifyPropertyChanged("LastMessage");
            }
        }
        public double ReviewAverage;
        public String UserName;
       // public IList<Review> Reviews;
        public ObservableCollection<Subject> Subjects;
        public String ProfilePhotoUri;
        public ObservableCollection<StudySession> StudySessions;
        public ObservableCollection<STNotification> Notifications;

        //public boolean newMessage;
        public double NotificationBadgeValue;
    }
}
