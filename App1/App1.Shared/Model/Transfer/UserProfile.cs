using System;
using System.Collections.Generic;
using System.Text;
using App1.Model.Logic;

namespace App1.Model.Transfer
{
    public class UserProfile
    {
        public String StudentTokenId;
        public double IOSVersion;
        public University University;
        public Logic.Student Student;
        public Tutor Tutor;
        public String FirstName;
        public String LastName;
        public String PassWord;
        public String UserName;
        public String Email;
        public String Major;
        public String Location;

        public UserProfile(String firstName, String lastName, String passWord, String userName, String uEmail,
            String major)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PassWord = passWord;
            this.UserName = userName;
            this.Email = uEmail;
            this.Major = major;
            this.Location = "POINT(39.9540 -75.1880)";
        }
    }
}
