using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kamobi.Services
{
    public class UserInfo
    {
        
        public static string id;
        public static string username;
        public static string displayname;
        public static string phoneNumber;
        public static string passwordHash;

        
        /*
        public static event PropertyChangedEventHandler PropertyChanged;
        public static string id {
            get { return id; }
            set {
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("id"));
                id = value;
            }
        }
        public static string username
        {
            get { return username; }
            set
            {
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("username"));
                username = value;
            }
        }
        public static string displayname
        {
            get { return displayname; }
            set
            {
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("displayname"));
                displayname = value;
            }
        }
        public static string passwordHash
        {
            get { return passwordHash; }
            set
            {
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("passwordHash"));
                passwordHash = value;
            }
        }
        public static string phoneNumber
        {
            get { return phoneNumber; }
            set
            {
                PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("phoneNumber"));
                phoneNumber = value;
            }
        }*/
    }
}
