using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Shapes;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RollingBall.Models
{
    /// <summary>
    /// Class that describe "wall" game object. Call Ended event when existence time has expired
    /// Represet Model part of MVVM pattern
    /// Inherit Segment
    /// </summary>
    public class Wall : Segment, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        readonly bool _mortal; //if true - launch timer
        public bool mortal { get => _mortal; } 
        private double _life_time; // amount of seconds this wall exists
        public double life_time
        {
            get { return _life_time; }
            private set
            {
                if (_life_time == value)
                    return;
                _life_time = value;
                OnPropertyChanged("life_time");
            }
        }

        public event Action<Wall> Ended;//on timer end event
        public event Action<Wall> updated;//on timer tick event. Currently do not used
        
        //timers
        private DispatcherTimer _deleteTimer;
        private DispatcherTimer _updateTimer;

        /// <summary>
        /// Initialize wall
        /// </summary>
        /// <param name="A">Start segment point</param>
        /// <param name="B">End segment point</param>
        /// <param name="mortal">true if this wall must be destroyed</param>
        /// <param name="life_time">amount of seconds before this wall will be destroyed</param>
        public Wall(Point A, Point B, bool mortal = true, double life_time = 3) : base(A, B)
        {
            this._mortal = mortal;
            this._life_time = life_time;

            if(mortal==true)//set timer
            {
                _deleteTimer = new DispatcherTimer();
                _updateTimer = new DispatcherTimer();

                _deleteTimer.Interval = TimeSpan.FromSeconds(life_time);
                _updateTimer.Interval = TimeSpan.FromSeconds(1);

                _deleteTimer.Tick += DeleteTimerOnTick;
                _updateTimer.Tick += UpdateTimerOnTick;

                _deleteTimer.Start();
                _updateTimer.Start();
            }
        }

        private void UpdateTimerOnTick(object sender, EventArgs e)//timer tick
        {
            life_time--;// used to change opatcity
            //    updated?.Invoke(this);
        }
        private void DeleteTimerOnTick(object sender, EventArgs e)//timer end
        {
            _deleteTimer.Stop();
            _updateTimer.Stop();
            Ended?.Invoke(this);
        }
    }
}
