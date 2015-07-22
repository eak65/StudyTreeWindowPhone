using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace App1.Student
{
    public class DetailStudentStudySessionViewModel : BaseINPC
    {

        private StudySession _session;
        private ObservableCollection<PrelimTutorVM> _prelimtutors;
        private Visibility _activeTutorVisibility;

        public DetailStudentStudySessionViewModel(StudySession session)
        {
            _session = session;
            _prelimtutors = new ObservableCollection<PrelimTutorVM>();
            foreach(PreliminaryTutor t in _session.PreliminaryTutors)
            {
                _prelimtutors.Add(new PrelimTutorVM(t));
            }
            if (_session.ActiveTutor == null)
                _activeTutorVisibility = Visibility.Collapsed;
            else
                _activeTutorVisibility = Visibility.Visible;
        }

        public String SubjectName
        {
            get { return _session.SubjectName; }
        }

        public PreliminaryTutor ActiveTutor
        {
            get { return _session.ActiveTutor; }
            set
            {
                _session.ActiveTutor = value;
                ActiveTutorVisibility = Visibility.Visible;
                NotifyPropertyChanged("ActiveTutor");
                NotifyPropertyChanged("ActiveTutorName");
                NotifyPropertyChanged("DisplayActiveTutorFee");
            }
        }

        public Visibility ActiveTutorVisibility
        {
            get
            {
                return _activeTutorVisibility;
            }
            set
            {
                _activeTutorVisibility = value;
                NotifyPropertyChanged("ActiveTutorVisibility");
            }
        }
        
        public String CourseName
        {
            get { return _session.CourseName; }
        }

        public String DisplayTimeRequested
        {
            get { return String.Format("Current Time: {0} min", _session.TimeRequested); }
        }

        public String ActiveTutorName
        {
            get
            {
                if (_session.ActiveTutor != null)
                {
                    return _session.ActiveTutor.FirstName;
                }
                return "";
            }
        }

        public String DisplayActiveTutorFee
        {
            get
            {
                if (_session.ActiveTutor != null)
                {
                    return _session.ActiveTutor.SessionFee.ToString("C", new CultureInfo("en-us"));
                }
                return "";
            }
        }

        public ObservableCollection<PrelimTutorVM> PreliminaryTutors
        {
            get { return _prelimtutors; }
        }
    }

    public class PrelimTutorVM : BaseINPC
    {
        private PreliminaryTutor _tutor;

        public PrelimTutorVM(PreliminaryTutor tutor)
        {
            _tutor = tutor;
        }

        public String TutorName
        {
            get { return _tutor.FirstName; }
        }

        public String DisplayTutorFee
        {
            get { return _tutor.SessionFee.ToString("C", new CultureInfo("en-us")); }
        }

        public PreliminaryTutor Tutor
        {
            get { return _tutor; }
        }
    }
}
