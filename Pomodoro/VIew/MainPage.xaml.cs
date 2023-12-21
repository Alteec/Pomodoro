using Pomodoro.Model;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pomodoro.View
{
    public partial class MainWindow : Window
    {
        PomodoroTimer pomodoro = new PomodoroTimer();
        DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Tick;
            DataContext = pomodoro;
        }
        void Tick(object sender, object e)
        {
            if (pomodoro.Time <= TimeSpan.Zero)
            {
                _timer.Stop();
                PlaySound();
                MessageBox.Show($"{pomodoro.CurrentStatus} is finished", "test");
                pomodoro.Active = !pomodoro.Active;
                pomodoro.Time = pomodoro.CurrentStatus == Status.Working ? pomodoro.Work : pomodoro.Rest;
            }
            if (!pomodoro.Active)
            {
                _timer.Stop();
            }
            pomodoro.Time = pomodoro.Time.Add(TimeSpan.FromSeconds(-1));

        }

        private void StartWork(object sender, RoutedEventArgs e)
        {
            pomodoro.CurrentStatus = Status.Working;
            DisplayStatus.Text=pomodoro.CurrentStatus.ToString();
            try
            {
                pomodoro.Work = TimeSpan.Parse(workUI.Text);
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show($"Invalid");
            }

            pomodoro.Time = pomodoro.Work;
        }

        private void StartRest(object sender, RoutedEventArgs e)
        {
            pomodoro.CurrentStatus = Status.Resting;
            DisplayStatus.Text = pomodoro.CurrentStatus.ToString();

            try
            {
                pomodoro.Rest = TimeSpan.Parse(restUI.Text);
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                MessageBox.Show($"Invalid");
            }
            pomodoro.Time = pomodoro.Rest;
        }

        private void StopResume(object sender, RoutedEventArgs e)
        {
            if (pomodoro.Active)
            {
                pomodoro.Active = !pomodoro.Active;
                _timer.Stop();
            }
            else
            {
                pomodoro.Active = !pomodoro.Active;
                _timer.Start();
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            pomodoro.Time = pomodoro.CurrentStatus == Status.Working ? pomodoro.Work : pomodoro.Rest;
        }
        private void PlaySound()
        {
            SoundPlayer sound = new SoundPlayer(@"C:\Windows\Media\Alarm01.wav");
            sound.Play();
        }
    }
}
