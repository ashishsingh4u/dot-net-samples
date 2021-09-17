namespace CobaltSoftware.Foundation.ModelViewPresenter.Core
{
    /// <summary>
    ///     The IView interface defines a passive view in the Model-View-Presenter
    ///     pattern. The view registers itself with a presenter (as controlled by
    ///     an orchestration mechanism) and requests it's state. The view should
    ///     expose events that are consumed by the presenter for observation and 
    ///     change tracking in response to interactions.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <list>
    ///             <listheader>Version History</listheader>
    ///             <item>10 October, 2009 - Steve Gray - Initial Draft</item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         This implementation uses self-constrained generics to allow
    ///         bi-directional strong coupling of interface types for both
    ///         presenter and view, allowing mutual strong references.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TViewContract">View contract type</typeparam>
    /// <typeparam name="TPresenterContract">Presenter contract type</typeparam>
    public interface IView<TViewContract, TPresenterContract>
        where TViewContract : IView<TViewContract, TPresenterContract>
        where TPresenterContract : IPresenter<TPresenterContract, TViewContract>
    {
        /// <summary>
        ///     Attach this view to a presenter.
        /// </summary>
        /// <param name="presenter">Presenter instance</param>
        /// <param name="requiresInitialState">Does this view require initial state to be pushed in?</param>
        /// <remarks>
        ///     <para>
        ///         This is the initiating action for view/presenter
        ///         wireup. This method should, in turn, cause the view to call
        ///         ConnectView on the presenter. The ConnectView, conversely
        ///         will inspect the Presenter property of the view to ensure
        ///         that the two are in agreement (preventing the operation
        ///         occuring in reverse).
        ///     </para>
        ///     <para>
        ///         If the view is already attached to another presenter then
        ///         the view will be detached from that presenter first and
        ///         fire the appropriate disconnect events, before attempting
        ///         to connect to the new view.
        ///     </para>
        ///     <para>
        ///         If attempting to-reattach to its existing presenter there is
        ///         no work carried out (i.e. initial connection events do
        ///         not fire again).
        ///     </para>
        /// </remarks>
        void AttachToPresenter(TPresenterContract presenter, bool requiresInitialState);

        /// <summary>
        ///     Detatch from any current presenter.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This is the initiating operation for disconnection. A view
        ///         has this method called, which will in turn cause DisconnectView
        ///         to be called on the presente.r
        ///     </para>
        ///     <para>
        ///         If the view is not attached to any presenter then this is
        ///         a null operation with no effect/error.
        ///     </para>
        /// </remarks>
        void DetatchFromPresenter();
        /// <summary>
        ///     Presenter this view is associated with.
        /// </summary>
        TPresenterContract Presenter { get; }
    }
}
