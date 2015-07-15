using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Student
{
    public class DetailStudentStudySessionViewModel : BaseINPC
    {

        private StudySession _session;
        private ObservableCollection<PrelimTutorVM> _prelimtutors;

        public DetailStudentStudySessionViewModel(StudySession session)
        {
            _session = session;
            _prelimtutors = new ObservableCollection<PrelimTutorVM>();
            foreach(PreliminaryTutor t in _session.PreliminaryTutors)
            {
                _prelimtutors.Add(new PrelimTutorVM(t));
            }
        }

        public String SubjectName
        {
            get { return _session.SubjectName; }
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
    }
}
