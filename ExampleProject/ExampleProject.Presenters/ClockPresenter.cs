using System;
using System.Linq;
using CobaltSoftware.Foundation.ModelViewPresenter.Support;
using ExampleProject.Contracts;
using System.ComponentModel;
using System.Diagnostics;

namespace ExampleProject.Presenters
{
    /// <summary>
    ///     The ClockPresenter is a simple IClockPresenter implementation
    ///     that controls and coordinates the display of date/time on
    ///     it's associated views.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public class ClockPresenter : PresenterBase<IClockPresenter, IClockView>, IClockPresenter
    {
        #region Private Fields

        private readonly ITimeModel _modelInstance;

        #endregion

        #region Constructor(s)

        /// <summary>
        ///    Initialize a new clock presenter, showing some aspect of a model.
        /// </summary>
        /// <param name="timeModel">Time model to observe/update</param>
        public ClockPresenter(ITimeModel timeModel)
        {
            // Validate arguments
            if (timeModel == null)
                throw new ArgumentNullException("timeModel");

            // Set local copy
            _modelInstance = timeModel;

            // Start observing
            _modelInstance.PropertyChanged += ModelInstancePropertyChanged;
        }

        #endregion

        #region Model Observation

        /// <summary>
        ///     A property of the model has changed
        /// </summary>
        /// <param name="sender">Sending Object</param>
        /// <param name="e">Event arguments</param>
        void ModelInstancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {                
                case "CurrentTimezone":
                    // Time-zone has been updated on the model
                    lock (Views)
                    {
                        // Set the time-zone on each view.
                        Views.ToList().ForEach(x => x.SelectedTimeZone = _modelInstance.CurrentTimezone);
                    }
                    break;

                case "CurrentTime":
                    lock (Views)
                    {
                        // Set the time on each view
                        Views.ToList().ForEach(x => x.Time = _modelInstance.CurrentTime);
                    }

                    break;

                default:
                    // We're not listening for this property to change!
                    break;

            }
        }

        #endregion

        #region IClockPresenter Implementation

        /// <summary>
        ///     When a view has changed it's timezone, update the model.
        /// </summary>
        public void ChangeTimeZone(TimeZoneInfo timeZone)
        {
            // Test invariants
            Debug.Assert(_modelInstance != null, "Model should always be available.");
            
            // Update model
            _modelInstance.CurrentTimezone = timeZone;
        }

        #endregion
        
        #region PresenterBase Implementation

        /// <summary>
        ///     Refresh a view with the initial state.
        /// </summary>
        /// <param name="viewInstance">View instance being wired up</param>
        protected override void RefreshView(IClockView viewInstance)
        {
            // Validate arguments
            if (viewInstance == null)
                throw new ArgumentNullException("viewInstance");

            // Push the list of all time-zones through
            viewInstance.TimeZones = _modelInstance.AllTimeZones;

            // Set the current timezone, as per the model
            viewInstance.SelectedTimeZone = _modelInstance.CurrentTimezone;
        }

        /// <summary>
        ///     Obtain a reference to ourselves.
        /// </summary>
        /// <remarks>
        ///     Required by the PresenterBase class, since it cannot supply
        ///     it's own subclassed generic type. It's a curious perversion
        ///     of generics for some.
        /// </remarks>
        /// <returns>This instance (as IClockPresenter)</returns>
        protected override IClockPresenter GetPresenterEndpoint()
        {
            return this;
        }

        #endregion
    }
}
