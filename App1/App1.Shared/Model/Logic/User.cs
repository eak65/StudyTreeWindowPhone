using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using App1.Model.Transfer;
using System.Collections.ObjectModel;

namespace App1.Model.Logic
{
    public class User
    {
        public String token;
        public String studentSponserTokenId;
        public IList creditCards;
        public IList messages;
        public int PersonId;
        public int Fee;
        public int appVerionNumber;
        public String FirstName;
        public String LastName;
        public String Email;
        public String password;
        public String major;
        public int reviewAverage;
        public String UserName;
        public int mobilePhone;
        public String ProfilePhotoUri;
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
