namespace MVC.Core
{
    public interface IController<TView, TModel>
        where TView : class, IView
        where TModel : class, IModel
    {
        TModel Model { get; set; }
        TView View { get; set; }
    }
}
