using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
    public class AcceptModel
    {
        public AcceptModel(int tutorId, int studentId, int sessionId)
        {
            this.TutorId = tutorId;
            this.StudentId = studentId;
            this.SessionId = sessionId;
        }

        public int TutorId;
        public int StudentId;
        public int SessionId;

    }
}
