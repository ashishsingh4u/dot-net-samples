using System;
using System.Drawing;
using ExpressTreeList.Columns;
using ExpressTreeList.Nodes;

namespace ExpressTreeList.View
{
    interface IExpressView
    {
        #region Events

        event EventHandler ViewClosed;

        #endregion Events

        #region Properties

        Font ColumnFont { set; get; }
        int ColumnGap { set; get; }
        int ColumnHeight { set; get; }
        int NodeHeight { get; set; }
        int HorzScrollValue { get; }
        int VertScrollValue { get; }

        #endregion Properties

        #region Indexers
        #endregion Indexers

        #region Methods

        void AddColumn(ExpressColumn column);
        void AddColumns(ExpressColumn[] columns);
        void AddNode(ExpressNode node);
        void AddNodes(ExpressNode[] nodes);
        void RefreshTreeList();
        void SetScrollValues();

        #endregion Methods
    }
}