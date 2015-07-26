using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
   public class TutorSessionModel
    {
        public TutorSessionModel(int _sessionId)
        {
            this.sessionId = _sessionId;
        }
       public int sessionId { get; set; }
    }
}
