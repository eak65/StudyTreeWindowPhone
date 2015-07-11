using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Logic
{
    public class TreeMessage
    {
        public int MessageId;
        public int SenderId;
        public String SenderName;
        public int StudentId;
        public String MessageString;
        public int TutorId; // use when you send to server
        public int PreliminaryTutorId;
        public int SessionId;

        public TreeMessage(int tutorId, int sessionId, String message)
        {
            this.TutorId = tutorId;
            this.SessionId = sessionId;
            this.MessageString = message;
            this.SenderId = DataManager.shared().myself.PersonId;
        }

        public Boolean isIncoming()
        {
            if (this.SenderId == DataManager.shared().myself.PersonId)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
