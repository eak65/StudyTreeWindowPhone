using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using App1.Model.Transfer;
using System.Collections.ObjectModel;

namespace App1.Model.Logic
{
    public class User : BaseINPC
    {
        private String _firstName;
        private String _lastName;
        private String _email;
        private String _userName;
        private String _profileUri;

        public String token;
        public String studentSponserTokenId;
        public IList creditCards;
        public IList messages;
        public int PersonId;
        public int Fee;
        public int appVerionNumber;
        public String FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public String LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public String Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public String password;
        public String major;
        public int reviewAverage;
        public String UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        public int mobilePhone;
        public String ProfilePhotoUri
        {
            get { return _profileUri; }
            set
            {
                _profileUri = value;
                NotifyPropertyChanged("ProfilePhotoUri");
            }
        }
        public int notificationBadgeValue;
        public ObservableCollection<Subject> StudentSubjects;
        public ObservableCollection<Subject> TutorSubjects;
        public ObservableCollection<StudySession> StudentStudySessions;
        public ObservableCollection<StudySession> TutorStudySessions;
        public ObservableCollection<STNotification> StudentNotificationList;
        public ObservableCollection<STNotification> TutorNotificationList;
        public int StudentNotificationBadgeValue;
        public int TutorNotificationBadgeValue;


        public User()
        {
            this.TutorSubjects = new ObservableCollection<Subject>();
            this.StudentSubjects = new ObservableCollection<Subject>();
            this.StudentStudySessions = new ObservableCollection<StudySession>();
            this.TutorStudySessions = new ObservableCollection<StudySession>();
            this.StudentNotificationList = new ObservableCollection<STNotification>();
            this.TutorNotificationList = new ObservableCollection<STNotification>();

        }
        public void updateAccountInformation(LoginResponse response)
        {
            FirstName = response.FirstName;
            LastName = response.LastName;
            Email = response.Email;
            UserName = response.UserName;
            ProfilePhotoUri = response.ProfilePhotoUri;
            PersonId = response.PersonId;
            Fee = response.Fee;
        }
    }
}
