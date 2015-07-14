using System;
using System.Collections.Generic;
using System.Text;
using App1.Model.Logic;

namespace App1.Model
{
   public class CourseModel
    {
        public Boolean isUniversityCourse;
        public int CourseId;
        public String CourseName;
        public String CourseDescription;
        //    public int Subject_SubjectId;


        public Course getCourse()
        {
           
            Course c = new Course();
            c.Title = this.CourseName;
            c.Description = this.CourseDescription;
            c.UniversityCourseId = this.CourseId;
            c.IsUniversityCourse = this.isUniversityCourse;
            return c;
        }
    }
}
