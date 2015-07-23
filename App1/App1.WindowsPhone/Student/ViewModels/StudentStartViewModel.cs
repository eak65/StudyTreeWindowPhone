using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Student.ViewModels
{
    public class StudentStartViewModel : BaseINPC
    {
        private StudySession _session;
        private int _seconds;
        private int _increaseSeconds = 0;


        public StudentStartViewModel(StudySession s)
        {
            Session = s;
            _seconds = s.TimeRequested * 60;
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

        public String CurrentTime
        {
            get
            {
                TimeSpan s = new TimeSpan(0, 0, _seconds);
                String output = s.ToString(@"hh\:mm\:ss");
                return output;
            }
        }

        public String MinCost
        {
            get
            {
                var output = String.Format("Minimum Cost: ${0:0.0}", _session.ActiveTutor.Fee);
                return output;
            }
        }

        public int IncreaseSeconds
        {
            get
            {
                return _increaseSeconds;
            }
            set
            {
                _increaseSeconds = value;
                NotifyPropertyChanged("IncreaseSeconds");
                NotifyPropertyChanged("IncreaseTime");
                NotifyPropertyChanged("IncreaseCost");
            }
        }

        public String IncreaseTime
        {
            get
            {
                TimeSpan t = new TimeSpan(0, 0, _increaseSeconds);
                return "Increased Time: " + t.ToString(@"hh\:mm\:ss");
            }
        }
        
        public String IncreaseCost
        {
            get
            {
                double cost = _session.ActiveTutor.Fee * ((double)_increaseSeconds / 3600);
                var output = String.Format("Increased Cost: ${0:0.0}", cost);
                return output;
            }
        }
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                NotifyPropertyChanged("Seconds");
                NotifyPropertyChanged("CurrentTime");
            }
        }
    }
}
