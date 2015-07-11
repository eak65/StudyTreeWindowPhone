using System;
using System.Collections.Generic;
using System.Text;
using App1.Model.Logic;

namespace App1.Handler
{
   public class StudentMessageHandler :UserMessageHandler
    {
  override  public void setUpMessage(TreeMessage message)
    {
        StudySession session =DataManager.shared().getStudySessionFromId(message.SessionId);
        if(session !=null)
        {
        this.addMessageToStudySession(message,session);
       //     setChanged();
       //     notifyObservers();

        }

    }
    }
}
