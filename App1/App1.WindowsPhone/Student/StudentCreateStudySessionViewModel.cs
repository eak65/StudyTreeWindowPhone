﻿using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Student
{
    public class StudentCreateStudySessionViewModel : BaseINPC
    {
        private StudySession _session;

        public StudentCreateStudySessionViewModel()
        {
            _session = new StudySession();
            _session.TimeRequested = 60;
        }

        public String SubjecTitle { get; set; }
        public String SelectedCourse
        {
            get { return _session.CourseName; }
            set
            {
                _session.CourseName = value;
                NotifyPropertyChanged("SelectedCourse");
            }
        }

        public StudySession Session
        {
            get { return _session; }
            set
            {
                _session = value;
                NotifyPropertyChanged("Session");
            }
        }

        public int CurrentTime
        {
            get { return _session.TimeRequested; }
            set
            {
                _session.TimeRequested = value;
                NotifyPropertyChanged("CurrentTime");
                NotifyPropertyChanged("DisplayCurrentTime");
            }
        }

        public String DisplayCurrentTime
        {
            get
            {
                return String.Format("Current Time: {0} min", _session.TimeRequested);
            }
            set { }
        }
    }
}
