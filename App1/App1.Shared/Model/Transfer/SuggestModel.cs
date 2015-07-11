using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
    public class SuggestModel
    {
        public int TutorId;
        public int SessionId;
        public SuggestModel(double TutorId, int SessionId)
        {
            this.TutorId = (int)TutorId;
            this.SessionId = SessionId;
        }
    }
}
