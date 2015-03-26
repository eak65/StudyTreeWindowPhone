using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class DataManager
    {
        public string token { get; set; }
     public static DataManager dataManager { get; set; }
     public static DataManager shared()
        {
         if(dataManager==null)
         {
             dataManager = new DataManager();
         }
         return dataManager;
        }


    }
}
