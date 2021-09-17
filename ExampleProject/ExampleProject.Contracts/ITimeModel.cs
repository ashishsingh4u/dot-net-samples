using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ExampleProject.Contracts
{
    /// <summary>
    ///     The ITimeModel interface defines a model of date/time.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public interface ITimeModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     List of available time-zones.
        /// </summary>
        IEnumerable<TimeZoneInfo> AllTimeZones { get; }

        /// <summary>
        ///     Current time-zone
        /// </summary>
        TimeZoneInfo CurrentTimezone { get; set; }

        /// <summary>
        ///     Current time
        /// </summary>
        DateTime CurrentTime { get; }
    }
}
