using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.Model.Logic
{
   public  class Subject
    {
        public int UniversitySubjectId;
        public String UniversitySubjectName;

        public ObservableCollection<Course> UniversityCourses;
    }
}
