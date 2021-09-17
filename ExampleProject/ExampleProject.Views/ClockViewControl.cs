using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExampleProject.Contracts;

namespace ExampleProject.Views
{
    /// <summary>
    ///     The Clock View Control displays time zones and time, and allows
    ///     the user to select a new time zone.
    /// </summary>
    public partial class ClockViewControl : UserControl, IClockView
    {
        public ClockViewControl()
        {
            InitializeComponent();
        }

        #region IClockView Members

        /// <summary>
        ///     Set the time-zones on the display
        /// </summary>
        public IEnumerable<TimeZoneInfo> TimeZones
        {
            set 
            {
                Combo_TimeZones.DataSource = value;
                Combo_TimeZones.DisplayMember = "DisplayName";
                Combo_TimeZones.ValueMember = "DisplayName";
            }
        }

        /// <summary>
        ///     Set the currently displayed time.
        /// </summary>
        public DateTime Time
        {
            set 
            {
                if (LableTime.InvokeRequired)
                    LableTime.Invoke(new Action<Label, String>(UpdateLabel), new Object[] { LableTime, value.ToLongTimeString() });
                else
                    UpdateLabel(LableTime, value.ToLongTimeString());

            }
        }

        /// <summary>
        ///     Selected time-zone
        /// </summary>
        public TimeZoneInfo SelectedTimeZone
        {
            set
            {

                if (LableTime.InvokeRequired)
                    LableTime.Invoke(new Action<ComboBox, TimeZoneInfo>(UpdateComboValue), new Object[] { Combo_TimeZones, value });
                else
                    UpdateComboValue(Combo_TimeZones, value);
            }
        }

        #endregion

        #region Cross-thread invokers

        /// <summary>
        ///     Update a labels value
        /// </summary>
        /// <param name="instance">Label instance</param>
        /// <param name="text">Text</param>
        /// <remarks>Required due to the oddities of WinForms</remarks>
        private static void UpdateLabel(Label instance, String text)
        {
            // Validate parameters
            if (instance == null)
                throw new ArgumentNullException("instance");
            if (String.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            instance.Text = text;
        }

        /// <summary>
        ///     Update a combo-box value
        /// </summary>
        /// <param name="instance">Combo-box instance</param>
        /// <param name="selection">Selection value</param>
        /// <remarks>Required due to the oddities of WinForms</remarks>
        private static void UpdateComboValue(ComboBox instance, Object selection)
        {
            // Validate parameters
            if (instance == null)
                throw new ArgumentNullException("instance");
            if (selection == null)
                throw new ArgumentNullException("selection");

            instance.SelectedItem = selection;
        }

        #endregion

        #region IView<IClockView,IClockPresenter> Members

        private IClockPresenter _presenter;

        /// <summary>
        ///     Attach to presenter
        /// </summary>
        /// <param name="presenter">Presenter</param>
        /// <param name="requiresInitialState">Requires initial state push?</param>
        public void AttachToPresenter(IClockPresenter presenter, bool requiresInitialState)
        {
            // Validate arguments
            if (presenter == null)
                throw new ArgumentNullException("presenter");

            // Detach from any existing presenter
            DetatchFromPresenter();

            // Set our current presenter
            _presenter = presenter;

            // Notify the presenter that we're connecting.
            Presenter.ConnectView(this, requiresInitialState);
        }

        /// <summary>
        ///     Detach from the presenter
        /// </summary>
        public void DetatchFromPresenter()
        {
            lock (this)
            {
                // Disconnect from presenter if needed.
                if (Presenter != null)
                {
                    Presenter.DisconnectView(this);
                    _presenter = null;
                }
            }
        }

        /// <summary>
        ///     Current presenter instance
        /// </summary>
        public IClockPresenter Presenter
        {
            get 
            {
                return _presenter;
            }
        }

        #endregion

        /// <summary>
        ///     When the time zone has changed in the drop-down, raise our time-zone changed
        ///     event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboTimeZonesSelectionChangeCommitted(object sender, EventArgs e)
        {
            // If we've got a presenter, notify it of the time zone shift.
            if (Presenter != null)
                Presenter.ChangeTimeZone(Combo_TimeZones.SelectedItem as TimeZoneInfo);
        }
    }
}
