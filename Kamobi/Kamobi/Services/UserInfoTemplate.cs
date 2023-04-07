using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Kamobi.Services
{
    public class UserInfoTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string id;
        public string username
        {
            set
            {
                if (username != value)
                {
                    username = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("username"));
                    }
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

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("displayname"));
                    }
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

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("username"));
                    }
                }
            }
            get
            {
                return phoneNumber;
            }
        }
    }
}
