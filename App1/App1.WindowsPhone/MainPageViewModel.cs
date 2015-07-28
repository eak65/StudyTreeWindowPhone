using App1.Model.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class MainPageViewModel : BaseINPC
    {
        public String AccountFirstName
        {
            get { return DataManager.shared().myself.FirstName; }
        }

        public String AccountLastName
        {
            get { return DataManager.shared().myself.LastName; }
        }

        public String AccountEmail
        {
            get { return DataManager.shared().myself.Email; }
        }

        public String AccountUserName
        {
            get { return DataManager.shared().myself.UserName; }
        }
    }
}
