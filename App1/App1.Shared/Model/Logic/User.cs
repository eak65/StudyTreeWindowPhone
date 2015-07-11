using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using App1.Model.Transfer;

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
        public IList<Subject> StudentSubjects;
        public IList<Subject> TutorSubjects;
        public IList<StudySession> StudentStudySessions;
        public IList<StudySession> TutorStudySessions;
        public IList<STNotification> StudentNotificationList;
        public IList<STNotification> TutorNotificationList;
        public int StudentNotificationBadgeValue;
        public int TutorNotificationBadgeValue;


        public User()
        {
            this.TutorSubjects = new List<Subject>();
            this.StudentSubjects = new List<Subject>();
            this.StudentStudySessions = new List<StudySession>();
            this.TutorStudySessions = new List<StudySession>();
            this.StudentNotificationList = new List<STNotification>();
            this.TutorNotificationList = new List<STNotification>();

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
