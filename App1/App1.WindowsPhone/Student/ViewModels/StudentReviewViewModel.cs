using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.StudentView.ViewModels
{
    public class StudentReviewViewModel : BaseINPC
    {
        private double _reviewScore;

        public StudentReviewViewModel()
        {
            _reviewScore = 0;
        }

        public double ReviewScore
        {
            get { return _reviewScore; }
            set
            {
                _reviewScore = value;
                NotifyPropertyChanged("ReviewScore");
            }
        }

    }
}
