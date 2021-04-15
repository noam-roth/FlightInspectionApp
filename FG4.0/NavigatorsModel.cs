using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG4._0
{
    public class NavigatorsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public FGModel Model { get; private set; }
        public NavigatorsModel(FGModel model)
        {
            Model = model;
            Model.PropertyChanged += TheModel_PropertyChanged;
        }

        double aileron;
        public double Aileron
        {
            get => aileron;
            set
            {
                aileron = value;
                NotifyPropertyChanged(nameof(Aileron));
            }
        }

        double elevator;
        public double Elevator
        {
            get => elevator;
            set
            {
                elevator = value;
                NotifyPropertyChanged(nameof(Elevator));
            }
        }

        double altimeter;
        public double Altimeter
        {
            get => altimeter;
            set
            {
                altimeter = value;
                NotifyPropertyChanged(nameof(Altimeter));
            }
        }

        public double airSpeed;
        public double AirSpeed
        {
            get => airSpeed;
            set
            {
                airSpeed = value;
                NotifyPropertyChanged(nameof(AirSpeed));
            }
        }

        public double flightDirection;

        public double FlightDirection
        {
            get => flightDirection;
            set
            {
                flightDirection = value;
                NotifyPropertyChanged(nameof(FlightDirection));
            }
        }

        public double yaw;
        public double Yaw
        {
            get => yaw;
            set
            {
                yaw = value;
                NotifyPropertyChanged(nameof(Yaw));
            }
        }

        public double roll;
        public double Roll
        {
            get => roll;
            set
            {
                roll = value;
                NotifyPropertyChanged(nameof(Roll));
            }
        }

        public double pitch;
        public double Pitch
        {
            get => pitch;
            set
            {
                pitch = value;
                NotifyPropertyChanged(nameof(Pitch));
            }
        }

        public double rudder;
        public double Rudder
        {
            get => rudder;
            set
            {
                rudder = value;
                NotifyPropertyChanged(nameof(Rudder));
            }
        }

        public double throttle;
        public double Throttle
        {
            get => throttle;
            set
            {
                throttle = value;
                NotifyPropertyChanged(nameof(Throttle));
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TheModel_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Aileron":
                    Aileron = Model.Aileron;
                    break;
                case "Elevator":
                    Elevator = Model.Elevator;
                    break;
                case "Altimeter":
                    Altimeter = Model.Altimeter;
                    break;
                case "AirSpeed":
                    AirSpeed = Model.AirSpeed;
                    break;
                case "FlightDirection":
                    FlightDirection = Model.FlightDirection;
                    break;
                case "Yaw":
                    Yaw = Model.Yaw;
                    break;
                case "Roll":
                    Roll = Model.Roll;
                    break;
                case "Pitch":
                    Pitch = Model.Pitch;
                    break;
                case "Rudder":
                    Rudder = Model.Rudder;
                    break;
                case "Throttle":
                    Throttle = Model.Throttle;
                    break;
                default:
                    break;
            }
        }
    }
}
