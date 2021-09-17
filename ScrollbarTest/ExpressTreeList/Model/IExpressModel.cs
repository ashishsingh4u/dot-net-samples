using System.ComponentModel;
using System.Drawing;
using ExpressTreeList.Columns;
using ExpressTreeList.Nodes;

namespace ExpressTreeList.Model
{
    interface IExpressModel
    {
        #region Events
        event PropertyChangedEventHandler ModelPropertyChanged;
        #endregion Events

        #region Properties
        ExpressColumns Columns { get; }
        ExpressNodes Nodes { get; }
        Font ColumnFont { get; set; }
        int ColumnGap { set; get; }
        int ColumnHeight { set; get; }
        int NodeHeight { get; set; }
        #endregion Properties

        #region Indexers
        #endregion Indexers

        #region Methods

        int GetTotalColumnWidth();

        int GetTotalNodeHeight();

        #endregion Methods
    }
}