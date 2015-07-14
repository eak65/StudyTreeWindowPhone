using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
    public class ProfileSettingModel
    {
        public ProfileSettingModel(string firstName, string lastName, string password)
        {
            this.Firstname = firstName;
            this.Lastname = lastName;
            this.Password = password;
        }
        public string Firstname;
        public string Lastname;
        public string Password;
    }
}
