using System;
using System.Collections.Generic;
using CobaltSoftware.Foundation.ModelViewPresenter.Core;

namespace ExampleProject.Contracts
{
    /// <summary>
    ///     The IClockView represents some form of time-display
    ///     that shows the time, but also allows the user to change
    ///     the time-zone.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public interface  IClockView : IView<IClockView, IClockPresenter>
    {
        /// <summary>
        ///     Data-push point for the list of time-zones to display
        ///     on the view.
        /// </summary>
        IEnumerable<TimeZoneInfo> TimeZones { set; }

        /// <summary>
        ///     Data-push point for current time on the view.
        /// </summary>
        DateTime Time { set; }

        /// <summary>
        ///     Selected time-zone
        /// </summary>
        TimeZoneInfo SelectedTimeZone { set; }
    }
}
