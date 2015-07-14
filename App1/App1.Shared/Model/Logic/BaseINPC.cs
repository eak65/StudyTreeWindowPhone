using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App1.Model.Logic
{
    public class BaseINPC: INotifyPropertyChanged
    {
        // Declare the PropertyChanged event.
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will fire the PropertyChanged event, 
        // passing the source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
