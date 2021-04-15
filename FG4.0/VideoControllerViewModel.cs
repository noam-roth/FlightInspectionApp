using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG4._0
{
    public class VideoControllerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public VideoControllerModel Model { get; private set; }
        public VideoControllerViewModel(VideoControllerModel model)
        {
            Model = model;
            Model.PropertyChanged += TheModel_PropertyChanged;
            Time = "00:00:00";
            Frames = 0;
            Max = 1;
        }

        private int maxValVideoSlider;
        public int Max
        {
            get => maxValVideoSlider;
            set
            {
                maxValVideoSlider = value;
                NotifyPropertyChanged(nameof(Max));
            }
        }

        private string time;
        public string Time
        {
            get => time;
            private set
            {
                time = value;
                NotifyPropertyChanged(nameof(Time));
            }
        }

        private int frames;
        public int Frames
        {
            get => frames;
            set
            {
                frames = value;
                NotifyPropertyChanged(nameof(Frames));
            }
        }


        private double speed;
        public double Speed
        {
            get => speed;
            set
            {
                speed = value;
                NotifyPropertyChanged(nameof(Speed));
            }
        }

        private bool isPlay;
        public bool IsPlay
        {
            get => isPlay;
            set
            {
                isPlay = value;
                NotifyPropertyChanged(nameof(IsPlay));
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFile(string path)
        {
            Model.UpdateFile(path);
        }

        public void Start()
        {
            Model.Start();
        }

        public void Play()
        {
            Model.Play();
        }

        public void Pause()
        {
            Model.Pause();
        }

        public void ChangeSpeed(double newSpeed)
        {
            Model.ChangeSpeed(newSpeed);
        }

        public void ChangeTime(int cTime)
        {
            if (frames + cTime > 0 && frames + cTime < maxValVideoSlider)
            {
                Model.ChangeTime(frames + cTime);
            }
            else if (frames + cTime < 0)
            {
                Model.ChangeTime(0);
            }
            else
            {
                Model.ChangeTime(maxValVideoSlider);
            }
        }

        public void ChangeTimeSlider(int cTime)
        {
            Model.ChangeTime(cTime);
        }


        private void TheModel_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                Time = Model.Time.ToString();
            }
            else if (e.PropertyName == "Frames")
            {
                Frames = Model.Frames;
            }
            else if (e.PropertyName == "Speed")
            {
                Speed = Model.Speed;
            }
            else if (e.PropertyName == "IsPlay")
            {
                IsPlay = Model.IsPlay;
            }
            else if (e.PropertyName == "Max")
            {
                Max = Model.Max;
            }
        }
    }
}
