using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG4._0
{
    public class VideoControllerModel : INotifyPropertyChanged
    {
        public FGModel Model { get; private set; }
        public VideoControllerModel(FGModel model)
        {
            Model = model;
            Model.PropertyChanged += TheModel_PropertyChanged;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int max;
        public int Max
        {
            get => max;
            set
            {
                max = value;
                NotifyPropertyChanged(nameof(Max));
            }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get => time;
            private set
            {
                time = value;
                NotifyPropertyChanged(nameof(Time));
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

        private int frames;
        public int Frames
        {
            get => frames;
            private set
            {
                frames = value;
                NotifyPropertyChanged(nameof(Frames));
            }
        }


        public void UpdateFile(string path)
        {
            Model.UpdateFile(path);
        }

        public void ChangeTime(int cTime)
        {
            Model.ChangeTime(cTime);
        }

        public void ChangeSpeed(double newSpeed)
        {
            Model.ChangeSpeed(newSpeed);
        }

        public void Start() { Model.Start(); }
        public void Play(){ Model.Play(); }
        public void Pause(){ Model.Pause();}
        private void TheModel_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                Time = Model.Time;
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
            else if (e.PropertyName == "Frames")
            {
                Frames = Model.Frames;
            }
        }

    }
}
