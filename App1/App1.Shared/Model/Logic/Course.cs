using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
   public class Course : BaseINPC
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public int UniversityCourseId;
        public Subject Subject { get; set; }
        public Boolean IsUniversityCourse;
    }
}
