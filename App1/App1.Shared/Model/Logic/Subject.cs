using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
   public  class Subject
    {
        public int UniversitySubjectId;
        public String UniversitySubjectName;

        public IList<Course> UniversityCourses;
    }
}
