using System;
using GridContract;
namespace Controller
{
    public class GridController : IGridController
    {

        public GridController(IGridModel model, IGridView view)
        {
            if (model == null)
                throw new ArgumentNullException("model", "model is null.");
            if (view == null)
                throw new ArgumentNullException("view", "view is null.");
            Model = model;
            View = view;
        }

        public MVC.Core.IModel Model
        {
            get;
            set;
        }

        public MVC.Core.IView View
        {
            get;
            set;
        }

        public void UpdateView()
        {
            View.UpdateView();
        }
    }
}
