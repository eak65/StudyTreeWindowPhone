using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App1.Handler;
using App1.Model;
using App1.Model.Logic;
using App1.Model.Transfer;

namespace App1
{
    public class DataManager
    {
        public string token { get; set; }
        private static DataManager dataManager { get; set; }
        public User myself { get; set; }
        public StudentMessageHandler studentMessageHandler;
        public TutorMessageHandler tutorMessageHandler;

        public static DataManager shared()
        {
         if(dataManager==null)
         {
             dataManager = new DataManager();
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
        public StudySession getStudySessionFromId(int id)
    {
        IList<StudySession> joined = this.myself.StudentStudySessions;
          
        var j= joined.Concat(this.myself.TutorStudySessions);
        return j.FirstOrDefault(s => s.StudySessionId == id);
        }
   
         public void DidLogin()
        {
            SignalR.SignalR signalR = new SignalR.SignalR(Constants.url);
            RequestHandler handler = new RequestHandler();
            handler.updateInformation();
            handler.putLoginInformation(new LoginModel("POINT(39.9540 -75.1880)"));
        }
    }
 }