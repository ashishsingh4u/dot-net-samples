using System;

namespace ExampleProject.Contracts
{
    /// <summary>
    ///     The time-zone has changed on a view.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    /// <param name="view">View instance</param>
    /// <param name="newZone">New time-zone</param>
    public delegate void TimeZoneChanged(IClockView view, TimeZone newZone);
}
