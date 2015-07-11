using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
    public class User
    {
        public String token;
    public String  studentSponserTokenId;
    public IList<TreeMessage> Messages;
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
    public IList<Subject>StudentSubjects;
    public IList<Subject>TutorSubjects;
    public IList<StudySession>StudentStudySessions;
    public IList<StudySession>TutorStudySessions;
    public IList<STNotification>StudentNotificationList;
    public IList<STNotification>TutorNotificationList;
    public int StudentNotificationBadgeValue;
        public int TutorNotificationBadgeValue;
    }
}
