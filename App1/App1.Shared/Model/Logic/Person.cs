using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
    public class Person
    {
        public IList<TreeMessage> Messages;
        public double PersonId;
        public String FirstName;
        public String LastName;
        public String Email;
        public String password;
        public TreeMessage lastMessage;
        public double ReviewAverage;
        public String UserName;
       // public IList<Review> Reviews;
        public IList<Subject> Subjects;
        public String ProfilePhotoUri;
        public IList<StudySession> StudySessions;
        public IList<STNotification> Notifications;

        //public boolean newMessage;
        public double NotificationBadgeValue;
    }
}
