using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.StudentView
{
    public class UniversityCoursesListViewModel : BaseINPC
    {
        private University _university;
        private ObservableCollection<Course> _courses;
        private ObservableCollection<Course> _displayCourses;
        private String _searchPhrase = "";

        public UniversityCoursesListViewModel(University uni)
        {
            _university = uni;
            _courses = new ObservableCollection<Course>();
            foreach(Subject s in uni.UniversitySubjects)
            {
                foreach(Course c in s.UniversityCourses)
                {
                    c.Subject = s;
                    _courses.Add(c);
                }
            }
            _displayCourses = _courses;
        }

        public ObservableCollection<Course> DisplayCourses
        {
            get { return _displayCourses; }
            set
            {
                _displayCourses = value;
                NotifyPropertyChanged("DisplayCourses");
            }
        }

        public String SearchPhrase
        {
            get { return _searchPhrase; }
            set
            {
                _searchPhrase = value;
                if(_searchPhrase == "")
                {
                    _displayCourses = _courses;
                }
                else
                {
                    var result = _courses.Where(w => w.Title.ToLower().Contains(value.ToLower()));
                    _displayCourses = new ObservableCollection<Course>(result);
                }
                NotifyPropertyChanged("SearchPhrase");
                NotifyPropertyChanged("DisplayCourses");
            }
        }
    }
}
