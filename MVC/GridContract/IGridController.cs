using System;
using MVC.Core;

namespace GridContract
{
    public interface IGridController : IController<IView, IModel>
    {
        void UpdateView();
    }
}
