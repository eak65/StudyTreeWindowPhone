using App1.Model.Logic;
using App1.StudentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.TutorView.ViewModels
{
    public class SelectedStudentSessionViewModel : DetailStudentStudySessionViewModel
    {
        private StudySession _session;
        public SelectedStudentSessionViewModel(StudySession session) : base(session)
        {
            _session = session;
        }
        public Student Student { get { return _session.Student; }   }

    }
    public class StudentVM : BaseINPC
    {
        private Student _student;
        public StudentVM(Student student)
        {
            _student = student;
        }
        public String FirstName
        {
            get { return _student.FirstName; }
        }
     
    }
}
