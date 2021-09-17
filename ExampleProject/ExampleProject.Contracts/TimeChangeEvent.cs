using System;

namespace ExampleProject.Contracts
{
    /// <summary>
    ///     The time has changed on the model.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    /// <param name="time">New time</param>
    public delegate void TimeChanged(DateTime time);
}
