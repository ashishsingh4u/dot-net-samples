using System;
using System.Collections.Generic;
using ExampleProject.Contracts;
using System.ComponentModel;
using System.Timers;

namespace ExampleProject.Model
{
    /// <summary>
    ///     The TimeModel represents a model of date/time with
    ///     internationalization, and provides a simple implementation
    ///     of date/time management, with a periodic tick event
    ///     that notifies listeners of changes in the current time.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public class TimeModel : ITimeModel
    {
        #region Private Fields

        private TimeZoneInfo _timeZone;
        private DateTime _time;
        private readonly Timer _timer;

        #endregion

        #region Constructor(s)

        /// <summary>
        ///     Initialize the time model.
        /// </summary>
        public TimeModel()
        {
            _timeZone = TimeZoneInfo.Local;
            _time = DateTime.Now;
            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        #endregion

        #region Timer Update

        /// <summary>
        ///     When the timer elapses, update the current time.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Set the current time to the right internationalized time.
            _time = TimeZoneInfo.ConvertTime(DateTime.Now, _timeZone);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentTime"));
        }

        #endregion


        #region ITimeModel Members

        /// <summary>
        ///     Current time-zone
        /// </summary>
        public TimeZoneInfo CurrentTimezone
        {
            get
            {
                return _timeZone;
            }
            set
            {
                if (CurrentTimezone != value)
                {
                    _timeZone = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentTimezone"));
                }
            }
        }

        /// <summary>
        ///     Current time
        /// </summary>
        public DateTime CurrentTime
        {
            get 
            {
                return _time;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        ///     Notify listeners of property changes (using Observer pattern).
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region ITimeModel Members

        /// <summary>
        ///     List of time-zones
        /// </summary>
        public IEnumerable<TimeZoneInfo> AllTimeZones
        {
            get {
                return TimeZoneInfo.GetSystemTimeZones();
            }
        }

        #endregion
    }
}
