using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.Model.Logic
{
    public  class StudySession : BaseINPC
    {
        private PreliminaryTutor _activeTutor;
        private Boolean _active;

        public StudySession()
        {
            PreliminaryTutors =  new ObservableCollection<PreliminaryTutor>();
        }

        public int StudySessionId;
        //   public ArrayList<Tutor> Tutors;

        public PreliminaryTutor ActiveTutor {
            get
            {
                return _activeTutor;
            }
            set
            {
                _activeTutor = value;
                NotifyPropertyChanged("ActiveTutor");
            }
        }
        //   @property(nonatomic,strong) NSDate *StartTime;
        //   @property(nonatomic,strong) NSDate *CurrentTime;
        public int StudentId;
        public String SubjectName
        {
            get;
            set;
        }
        public String CourseName { get; set; }
        public Boolean Active
        {
            get { return _active; }
            set
            {
                _active = value;
                NotifyPropertyChanged("Active");
            }
        }
        public Boolean StudentStart;
        public Boolean TutorStart;
       // public double Distance;
      //  public ArrayList<STNotification> Notifications;
        public Boolean StudentRank;
        public Boolean TutorRank;
        public ObservableCollection<PreliminaryTutor> PreliminaryTutors { get; set; }
        public Student Student { get; set; }
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
