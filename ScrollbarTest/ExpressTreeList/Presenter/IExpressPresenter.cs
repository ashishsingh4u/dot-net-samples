using System.Drawing;
using System.Windows.Forms;
using ExpressTreeList.Columns;
using ExpressTreeList.Model;
using ExpressTreeList.Nodes;
using ExpressTreeList.View;

namespace ExpressTreeList.Presenter
{
    interface IExpressPresenter<in TView, in TModel>
        where TView : IExpressView
        where TModel : IExpressModel
    {
        #region Events
        #endregion Events

        #region Properties

        Font ColumnFont { get; set; }
        int ColumnGap { get; set; }
        int ColumnHeight { set; get; }
        int NodeHeight { get; set; }

        #endregion Properties

        #region Indexers
        #endregion Indexers

        #region Methods

        void BindToPresenter(TView view, TModel model);
        void DrawColumns(PaintEventArgs args);
        void DrawNodes(PaintEventArgs args);
        void AddColumn(ExpressColumn column);
        void AddColumns(ExpressColumn[] columns);
        void AddNode(ExpressNode node);
        void AddNodes(ExpressNode[] nodes);
        int GetTotalColumnWidth();
        int GetTotalNodeHeight();

        #endregion Methods
    }
}
