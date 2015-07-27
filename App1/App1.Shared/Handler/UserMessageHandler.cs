using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App1.Model.Logic;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace App1.Handler
{
    public abstract class UserMessageHandler
    {
        public abstract void setUpMessage(TreeMessage message);

        protected void addMessageToStudySession(TreeMessage message, StudySession session)
    {
        if(session.TypeCode>1)
        {
            if(session.ActiveTutor.Messages.Last().MessageId< message.MessageId )
           {
            session.ActiveTutor.Messages.Add(message);
           }
        }
        else
        {
            foreach(PreliminaryTutor preliminaryTutor in session.PreliminaryTutors)
            {
                if(preliminaryTutor.TutorId == message.PreliminaryTutorId&&(preliminaryTutor.Messages.Last()==null||preliminaryTutor.Messages.Last().MessageId<message.MessageId))
                {
                        CoreDispatcher dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                        if (!dispatcher.HasThreadAccess)
                        {
                            Task.Run(() => dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { preliminaryTutor.Messages.Add(message); }));
                        }
                        else
                        {
                            preliminaryTutor.Messages.Add(message);
                        }
                        break;
                }
            }
        }
    }
    }
}
