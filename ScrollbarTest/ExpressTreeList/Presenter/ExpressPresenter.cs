using System;
using System.Drawing;
using ExpressTreeList.Columns;
using ExpressTreeList.Model;
using ExpressTreeList.Nodes;
using ExpressTreeList.Painter;
using ExpressTreeList.View;

namespace ExpressTreeList.Presenter
{
    class ExpressPresenter : IExpressPresenter<IExpressView,IExpressModel>
    {
        #region Enums
        #endregion Enums

        #region Fields
        private IExpressModel _model;
        private IExpressView _view;
        private readonly ITreePainter _painter;
        #endregion Fields

        #region Constructor
        public ExpressPresenter()
        {
            _painter = new BasePainter();
        }
        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events
        #endregion Events

        #region Properties

        //Public Properties

        public int ColumnGap
        {
            get { return _model.ColumnGap; }
            set { _model.ColumnGap = value; }
        }

        public int ColumnHeight
        {
            get { return _model.ColumnHeight; }
            set { _model.ColumnHeight = value; }
        }

        public int NodeHeight
        {
            get { return _model.NodeHeight; }
            set { _model.NodeHeight = value; }
        }

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


        public void BindToPresenter(IExpressView view, IExpressModel model)
        {
            _view = view;
            _model = model;
            BindViewEvents(view);
            BindModelEvents(model);
        }

        public void DrawColumns(System.Windows.Forms.PaintEventArgs args)
        {
            int columnX = 0;
            foreach (var column in _model.Columns)
            {
                var rectangle = column.Bounds;
                rectangle.X = columnX;
                rectangle.Height = ColumnHeight;
                column.Bounds = rectangle;
                columnX = rectangle.X + rectangle.Width + ColumnGap;
                _painter.DrawHeader(new DrawColumnEventArgs {Column = column, PaintEventArgs = args, Font = ColumnFont});
            }
        }

        public void DrawNodes(System.Windows.Forms.PaintEventArgs args)
        {
            try
            {
                int nodeAxisY = ColumnHeight;
                int topVisibleNodeIndex = _view.VertScrollValue/NodeHeight;
                int totalVisibleNodes = (args.ClipRectangle.Height/NodeHeight) - 1;
                totalVisibleNodes += topVisibleNodeIndex;
                nodeAxisY += (topVisibleNodeIndex*NodeHeight);
                for (int nodeCtr = topVisibleNodeIndex; nodeCtr < totalVisibleNodes; nodeCtr++)
                {
                    var node = _model.Nodes[nodeCtr];
                    for (var columnCtr = 0; columnCtr < _model.Columns.Count; columnCtr++)
                    {
                        var bounds = _model.Columns[columnCtr].Bounds;
                        bounds.Y += nodeAxisY;
                        bounds.Height = NodeHeight;
                        node.Bounds = bounds;
                        if (columnCtr == 0)
                        {
                            _painter.DrawNode(new DrawNodeEventArgs
                                                  {
                                                      Column = _model.Columns[columnCtr],
                                                      Node = node,
                                                      PaintEventArgs = args,
                                                      Font = ColumnFont,
                                                      CellText = node.CellList[columnCtr].CellText
                                                  });
                        }
                        else
                        {
                            _painter.DrawNodeCell(new DrawNodeEventArgs
                                                      {
                                                          Column = _model.Columns[columnCtr],
                                                          Node = node,
                                                          PaintEventArgs = args,
                                                          Font = ColumnFont,
                                                          CellText = node.CellList[columnCtr].CellText
                                                      });
                        }
                    }
                    nodeAxisY += NodeHeight;
                }
            }
            catch(Exception exception)
            {
                string s = exception.StackTrace;
            }
        }


        public void AddColumn(ExpressColumn column)
        {
            _model.Columns.Add(column);
        }

        public void AddColumns(ExpressColumn[] columns)
        {
            _model.Columns.AddRange(columns);
        }

        public void AddNode(ExpressNode node)
        {
            if(node.CellList.Count != _model.Columns.Count)
                throw new IndexOutOfRangeException();
            _model.Nodes.Add(node);
        }

        public void AddNodes(ExpressNode[] nodes)
        {
            _model.Nodes.AddRange(nodes);
        }

        public int GetTotalColumnWidth()
        {
            return _model.GetTotalColumnWidth();
        }

        public int GetTotalNodeHeight()
        {
            return _model.GetTotalNodeHeight();
        }

        public Font ColumnFont
        {
            get { return _model.ColumnFont; }
            set { _model.ColumnFont = value; }
        }

        //Protected/Virtual/Override Methods

        //Private Methods

        private void BindModelEvents(IExpressModel model)
        {
            model.ModelPropertyChanged += (sender, args) => _view.RefreshTreeList();
        }

        private void BindViewEvents(IExpressView view)
        {
            view.ViewClosed += (sender, args) =>
            {
                _view = null;
                _model = null;
            };
        }

        #endregion Methods
    }
}