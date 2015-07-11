using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
    public class LoginModel
    {
        public LoginModel(string s)
        {
            Location = s;
        }
        public string Location;
    }
}
