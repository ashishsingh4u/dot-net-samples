using System;
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
    public interface IClockPresenter : IPresenter<IClockPresenter, IClockView>
    {
        /// <summary>
        ///     Set the time-zone.
        /// </summary>
        /// <remarks>
        ///     Called by views via the shared contract.
        /// </remarks>
        /// <param name="timeZone">Time zone to set.</param>
        void ChangeTimeZone(TimeZoneInfo timeZone);
    }
}
