using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
    public  class StudySession
    {

        public int StudySessionId;
        //   public ArrayList<Tutor> Tutors;
        public PreliminaryTutor ActiveTutor { get; set; }
        //   @property(nonatomic,strong) NSDate *StartTime;
        //   @property(nonatomic,strong) NSDate *CurrentTime;
        public int StudentId;
        public String SubjectName;
        public String CourseName;
        public Boolean Active;
        public Boolean StudentStart;
        public Boolean TutorStart;
        public double Distance;
      //  public ArrayList<STNotification> Notifications;
        public Boolean StudentRank;
        public Boolean TutorRank;
        public IList<PreliminaryTutor> PreliminaryTutors;
        public Student Student;
        public String FormattedAddress;
        public int TypeCode;
        public int TimeRequested;
        //  @property(nonatomic,strong) CLLocation *Location;
        public double Latitude;
        public double Longitude;
        public int StudentBadgeValue;



        public PreliminaryTutor getPersonById(int id)
        {
            foreach (PreliminaryTutor tutor in this.PreliminaryTutors)
            {
                if (tutor.TutorId == id)
                {
                    return tutor;
                }
            }
            if (this.ActiveTutor.TutorId == id)
            {
                return this.ActiveTutor;
            }
            return null;
        }
    }
}
