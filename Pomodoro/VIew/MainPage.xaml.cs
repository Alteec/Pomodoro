using Pomodoro.Model;
using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace Pomodoro.View
{
    public partial class MainWindow : Window
    {
        PomodoroTimer pomodoro = new PomodoroTimer();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Tick;
            DataContext = pomodoro;
        }
        void Tick(object sender, object e)
        {
            if (pomodoro.Time <= TimeSpan.Zero)
            {
                timer.Stop();
                PlaySound();
                MessageBox.Show($"{pomodoro.CurrentStatus} is finished", "test");
                pomodoro.Active = !pomodoro.Active;
                pomodoro.Time = pomodoro.CurrentStatus == Status.Working ? pomodoro.Work : pomodoro.Rest;
            }
            if (!pomodoro.Active)
            {
                timer.Stop();
            }
            pomodoro.Time = pomodoro.Time.Add(TimeSpan.FromSeconds(-1));

        }

        private void StartWork(object sender, RoutedEventArgs e)
        {
            pomodoro.CurrentStatus = Status.Working;
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
                timer.Stop();
            }
            else
            {
                pomodoro.Active = !pomodoro.Active;
                timer.Start();
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
