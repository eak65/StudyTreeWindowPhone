using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
    public class PostSubjectModel
    {
        public PostSubjectModel(String courseName)
        {
            this.CourseName = courseName;
        }

        public int SubjectId;
        public int UniversityCourseId;
        public int UserId;
        public int UniversityId;
        public int Type;
        public String CourseName;
        public String SubjectName;
    }
}
