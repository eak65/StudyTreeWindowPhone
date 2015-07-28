using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.Model.Logic
{
    public class Person : BaseINPC
    {
        private TreeMessage _lastMessage;
        private ObservableCollection<StudySession> _studySesssions;

        public ObservableCollection<TreeMessage> Messages = new ObservableCollection<TreeMessage>();
        public double PersonId;
        public String FirstName { get; set; }
        public String LastName;
        public String Email { get; set; }
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
        public double ReviewAverage { get; set; }
        public String UserName { get; set; }
       // public IList<Review> Reviews;
        public ObservableCollection<Subject> Subjects;
        public String ProfilePhotoUri;
        public ObservableCollection<StudySession> StudySessions
        {
            get { return _studySesssions; }
            set
            {
                _studySesssions = value;
                NotifyPropertyChanged("StudySessions");
            }
        }
        public ObservableCollection<STNotification> Notifications;

        //public boolean newMessage;
        public double NotificationBadgeValue;
    }
}
