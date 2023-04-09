using Kamobi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kamobi.Models
{
    public class UserInfoTemplate : INotifyPropertyChanged
    {
        public UserInfoTemplate()
        {
            this.id = UserInfo.id;
            this.username = UserInfo.username;
            this.passwordHash = UserInfo.passwordHash;
            this.displayname = UserInfo.displayname;
            this.phoneNumber = UserInfo.phoneNumber;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public string id;
        public string username
        {
            set
            {
                if (username != value)
                {
                    username = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("username"));
                }
            }
            get
            {
                return username;
            }
        }
        public string displayname
        {
            set
            {
                if (displayname != value)
                {
                    displayname = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("displayname"));
                }
            }
            get
            {
                return displayname;
            }
        }
        public string passwordHash;
        public string phoneNumber
        {
            set
            {
                if (phoneNumber != value)
                {
                    phoneNumber = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("username"));
                }
            }
            get
            {
                return phoneNumber;
            }
        }
    }
}
