using System.ComponentModel;
using System.Drawing;
using System.Linq;
using ExpressTreeList.Columns;
using ExpressTreeList.Nodes;

namespace ExpressTreeList.Model
{
    class ExpressModel : IExpressModel
    {
        #region Enums
        #endregion Enums

        #region Fields
        public event PropertyChangedEventHandler ModelPropertyChanged;
        private readonly ExpressColumns _columns;
        private readonly ExpressNodes _nodes;
        #endregion Fields

        #region Constructor

        public ExpressModel()
        {
            _columns = new ExpressColumns();
            _columns.PropertyChanged +=
                (sender, args) => { if (ModelPropertyChanged != null) ModelPropertyChanged(sender, args); };
            _nodes = new ExpressNodes();
            _nodes.PropertyChanged +=
                (sender, args) => { if (ModelPropertyChanged != null) ModelPropertyChanged(sender, args); };
            ColumnFont = new Font(FontFamily.GenericSansSerif, 8.25f);
            ColumnGap = ExpressColumn.DefaultColumnGap;
            ColumnHeight = ExpressColumn.DefaultColumnHeight;
            NodeHeight = ExpressNode.DefaultNodeHeight;
        }

        #endregion Constructor

        #region Delegates

        #endregion Delegates

        #region Events

        #endregion Events

        #region Properties

        //Public Properties

        public ExpressColumns Columns { get { return _columns; } }

        public ExpressNodes Nodes { get { return _nodes; } }

        public Font ColumnFont { get; set; }

        public int ColumnGap { get; set; }

        public int ColumnHeight { get; set; }

        public int NodeHeight { get; set; }

        //Protected/Virtual/Override Properties

        //Private Properties

        #endregion Properties

        #region Indexers

        //Public Indexers

        //Protected/Virtual/Override Indexers

        //Private Indexers

        #endregion Indexers

        #region Methods

        //Public Methods

        public int GetTotalColumnWidth()
        {
            int width = Columns.Sum(expressColumn => expressColumn.Width);
            width += (Columns.Count * ColumnGap) - 1; //Adding Column Gap.
            return width;
        }

        public int GetTotalNodeHeight()
        {
#warning Has to be modified
            return (Nodes.Count*NodeHeight) + ColumnHeight;
        }

        //Protected/Virtual/Override Methods

        //Private Methods

        #endregion Methods
    }
}