﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Pomodoro.Model
{
    public enum Status
    {
        Working,
        Resting,
    }
    public class PomodoroTimer : INotifyPropertyChanged
    {
        private bool _active = false;
        private TimeSpan _work  = new TimeSpan(0, 25, 0);
        private TimeSpan _rest= new TimeSpan(0, 5, 0);
        private Status _currentStatus  = Status.Working;
        public TimeSpan _time= new TimeSpan(0, 0, 0);
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }

        public TimeSpan Work
        {
            get
            {
                return _work;
            }
            set
            {
                _work = value;
                OnPropertyChanged("Work");
            }
        }
        public TimeSpan Rest
        {
            get
            {
                return _rest;
            }
            set
            {
                _rest = value;
                OnPropertyChanged("Rest");
            }
        }
        public TimeSpan Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        public Status CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}