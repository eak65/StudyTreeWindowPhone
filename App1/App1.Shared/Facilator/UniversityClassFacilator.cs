using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Facilator
{
    class UniversityClassFacilator
    {
        
        private static UniversityClassFacilator _shared;
        private Course _selectedCourse;

        public delegate void CourseSelectedHandler(Course course);
        public event CourseSelectedHandler CourseAdded;

        public static UniversityClassFacilator Shared
        {
            get
            {
                if(_shared == null)
                {
                    _shared = new UniversityClassFacilator();
                }

                return _shared;
            }
            private set { _shared = value; }
        }

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                OnCourseAdded(value);
            }
        }

        private UniversityClassFacilator() { }

        public void OnCourseAdded(Course course)
        {
            if(CourseAdded != null)
            {
                CourseAdded(course);
            }
        }
    }
}
