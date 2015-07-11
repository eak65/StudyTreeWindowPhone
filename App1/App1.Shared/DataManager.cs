using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App1.Model;
using App1.Model.Logic;

namespace App1
{
    public class DataManager
    {
        public string token { get; set; }
        private static DataManager dataManager { get; set; }
        public User myself { get; set; }

        public static DataManager shared()
        {
         if(dataManager==null)
         {
             dataManager = new DataManager();
             dataManager.myself=new User();
         }
         return dataManager;
        }

        public StudySession getStudySessionFromId(int id)
    {
        IList<StudySession> joined = this.myself.StudentStudySessions;
          
        var j= joined.Concat(this.myself.TutorStudySessions);
        return j.FirstOrDefault(s => s.StudySessionId == id);
        }
    }
}
