using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
   public class PreliminaryTutor : Tutor
    {
        private Boolean _newMessage;
        private Boolean _studentAccept;
        private Boolean _tutorAccept;

        public double SessionFee;
        public Boolean NewMessage
        {
            get { return _newMessage; }
            set
            {
                _newMessage = value;
                NotifyPropertyChanged("NewMessage");
            }
        }
        public Boolean StudentAccept
        {
            get { return _studentAccept; }
            set
            {
                _studentAccept = value;
                NotifyPropertyChanged("StudentAccept");
            }
        }
        public Boolean TutorAccept
        {
            get { return _tutorAccept; }
            set
            {
                _tutorAccept = value;
                NotifyPropertyChanged("TutorAccept");
            }
        }
    }
}
