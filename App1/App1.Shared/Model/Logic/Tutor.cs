using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
    public class Tutor : Person
    {
        private double _fee;


        public int TutorId;
        public String description;
         public Boolean newMessage;
        public double Fee
        {
            get { return _fee; }
            set
            {
                _fee = value;
                NotifyPropertyChanged("Fee");
            }
        }
        public String Major;
        public double TutorBadgeValue;
        public double StudentBadgeValue;
     //   public double Distance { get; set; }
    }
}
