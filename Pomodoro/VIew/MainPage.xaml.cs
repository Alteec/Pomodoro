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
            timer.Tick += TickEvent;
            DataContext = pomodoro;
        }
        private void TickEvent(object sender, object e) //Добавления события для делегата DispatcherTimer
        {
            if (pomodoro.Time <= TimeSpan.Zero)
            {
                timer.Stop();
                PlaySound();
                MessageBox.Show($"{pomodoro.CurrentStatus} is finished", "Finish");
                pomodoro.Active = !pomodoro.Active;
                pomodoro.Time = pomodoro.CurrentStatus == Status.Working ? pomodoro.Work : pomodoro.Rest;
            }
            if (!pomodoro.Active)
            {
                timer.Stop();
                return;
            }
            pomodoro.Time = pomodoro.Time.Add(TimeSpan.FromSeconds(-1));

        }

        private void StartWork(object sender, object e) //Логика события старта режима работы таймера
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

        private void StartRest(object sender, object e) //Логика события старта режима отдыха таймера
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

        private void StopResume(object sender, object e)//Логика события воспроизведения и приостановки таймера
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

        private void Reset(object sender, object e) //Логика события сброса таймера
        {
            pomodoro.Time = pomodoro.CurrentStatus == Status.Working ? pomodoro.Work : pomodoro.Rest;
        }
        private void PlaySound() //Функция для простого воспроизведение звука
        {
            SoundPlayer sound = new SoundPlayer(@"C:\Windows\Media\Alarm01.wav");
            try
            {
                sound.Play();
            }
            catch
            {
                MessageBox.Show($"No sound");
            }

        }
    }
}
