using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App1.Handler;
using App1.Model;
using App1.Model.Logic;
using App1.Model.Transfer;
using System.Threading.Tasks;

namespace App1
{
    public class DataManager
    {
        public string token { get; set; }
        private static DataManager dataManager { get; set; }
        public User myself { get; set; }
        public StudentMessageHandler studentMessageHandler;
        public TutorMessageHandler tutorMessageHandler;
        public University University { get; set; }

        public static DataManager shared()
        {
         if(dataManager==null)
         {
             dataManager = new DataManager();
             dataManager.studentMessageHandler = new StudentMessageHandler();
             dataManager.tutorMessageHandler = new TutorMessageHandler();
             dataManager.myself=new User();
         }
         return dataManager;
        }

      
        public void update(UpdateModel updateModel)
        {
        
           shared().myself.studentSponserTokenId = updateModel.StudentTokenId;
           shared().myself.StudentStudySessions = updateModel.Student.StudySessions;
           shared().myself.TutorStudySessions = updateModel.Tutor.StudySessions;
           shared().myself.StudentNotificationList = updateModel.Student.Notifications;
           shared().myself.TutorNotificationList = updateModel.Tutor.Notifications;
           shared().myself.TutorStudySessions = updateModel.Tutor.StudySessions;

        }
        public PreliminaryTutor getTutor(int sessionId, int tutorId)
        {
          StudySession session =  DataManager.shared().myself.StudentStudySessions.Where(s => s.StudySessionId == sessionId).FirstOrDefault();
            if(session == null)
            {
                session = DataManager.shared().myself.TutorStudySessions.Where(s => s.StudySessionId == sessionId).FirstOrDefault();
            }
            if(session.TypeCode>1)
            {
                if (session.ActiveTutor.TutorId == tutorId)
                {
                    return session.ActiveTutor;
                }
                else
                {
                    return null;
                }

            }
            return session.PreliminaryTutors.Where(t => t.TutorId == tutorId).FirstOrDefault();
        }
        public StudySession getStudySessionFromId(int id)
       {
     
            StudySession session = null;  
            session = this.myself.TutorStudySessions.FirstOrDefault(s => s.StudySessionId == id);

            if (session ==null)
            {
                session = this.myself.StudentStudySessions.FirstOrDefault(s => s.StudySessionId == id);
            }
            return session;
           
        }
   
         public async void DidLogin()
        {
            RequestHandler handler = new RequestHandler();
            handler.updateInformation();
            await handler.putLoginInformation(new LoginModel("POINT(-75.1880 39.9540)"));
            SignalR.SignalR signalR = new SignalR.SignalR(Constants.url);
            signalR.Start();
        }
    }
 }