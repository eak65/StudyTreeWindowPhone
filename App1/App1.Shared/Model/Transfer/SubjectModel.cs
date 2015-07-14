using System;
using System.Collections.Generic;
using System.Text;
using App1.Model.Logic;
using System.Collections.ObjectModel;

namespace App1.Model.Transfer
{
   public class SubjectModel
    {
        public int SubjectId;
        public String SubjectName;
        public IList<CourseModel> Courses;



        public Subject getSubject()
        {
            Subject subject= new Subject();
            subject.UniversitySubjectName=this.SubjectName;
            subject.UniversitySubjectId=this.SubjectId;
            subject.UniversityCourses =  new ObservableCollection<Course>();

            foreach(CourseModel model in this.Courses)
                 {
                subject.UniversityCourses.Add(model.getCourse());
                }
            return subject;
        }
    }
}
