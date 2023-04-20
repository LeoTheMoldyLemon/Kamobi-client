using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kamobi.Services
{
    public class UserInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        private string _username;
        private string _displayname;
        private string _phoneNumber;
        private string _passwordHash;

        public string id
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(id)));
            }
        }
        public string username
        {
            get => _username;
            set
            {
                _username = value;
                _displayname = value.Substring(0, value.Length - 5);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(username)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(displayname)));
            }
        }
        public string displayname
        {
            get => _displayname;
            set
            {
                _displayname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(displayname)));
            }
        }
        public string phoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(phoneNumber)));
            }
        }
        public string passwordHash
        {
            get => _passwordHash;
            set
            {
                _passwordHash = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(passwordHash)));
            }
        }


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
