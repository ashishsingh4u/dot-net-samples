using System;
using System.Drawing;
using System.Windows.Forms;
using ExpressTreeList.Columns;
using ExpressTreeList.Model;
using ExpressTreeList.Nodes;
using ExpressTreeList.Presenter;
using ExpressTreeList.View;

namespace ExpressTreeList
{
    public partial class ExpressTreeList : Control, IExpressView
    {
        #region Enums
        #endregion Enums

        #region Fields

        const int LargeChangeFactor = 10;
        const int Minimum = 0;
        const int SmallChangeFactor = 20;

        private readonly IExpressPresenter<IExpressView, IExpressModel> _presenter;
        private Region _region;

        #endregion Fields

        #region Constructor

        public ExpressTreeList()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserPaint, true);

            _presenter = new ExpressPresenter();
            _presenter.BindToPresenter(this, new ExpressModel());
            Disposed += (sender, args) => { if (ViewClosed != null) ViewClosed(sender, args); };
        }

        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events

        public event EventHandler ViewClosed;

        #endregion Events

        #region Properties

        //Public Properties

        public Font ColumnFont
        {
            get { return _presenter.ColumnFont; }
            set { _presenter.ColumnFont = value; }
        }

        public int ColumnGap
        {
            get { return _presenter.ColumnGap; }
            set { _presenter.ColumnGap = value; }
        }

        public int ColumnHeight
        {
            get { return _presenter.ColumnHeight; }
            set { _presenter.ColumnHeight = value; }
        }

        public int NodeHeight
        {
            get { return _presenter.NodeHeight; }
            set { _presenter.NodeHeight = value; }
        }

        public int HorzScrollValue
        {
            get { return _horzScrollBar.Value; }
        }

        public int VertScrollValue
        {
            get { return _vertScrollBar.Value; }
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

        public void AddColumn(ExpressColumn column)
        {
            _presenter.AddColumn(column);
        }

        public void AddColumns(ExpressColumn[] columns)
        {
            _presenter.AddColumns(columns);
        }

        public void AddNode(ExpressNode node)
        {
            _presenter.AddNode(node);
        }

        public void AddNodes(ExpressNode[] nodes)
        {
            _presenter.AddNodes(nodes);
        }

        public void RefreshTreeList()
        {
            SetScrollValues();
            Invalidate();
        }

        public void SetScrollValues()
        {
            if(_presenter == null || ClientRectangle.Width == 0 || ClientRectangle.Height == 0)
                return;
            int rectWidth = _presenter.GetTotalColumnWidth();
            if (ClientRectangle.Width - _vertScrollBar.Width >= rectWidth)
            {
                _horzScrollBar.Maximum = Minimum;
                _horzScrollBar.Enabled = false;
            }
            else
            {
                _horzScrollBar.Enabled = true;
                _horzScrollBar.Minimum = Minimum;
                int smallChange = (ClientRectangle.Width - _vertScrollBar.Width) / SmallChangeFactor;
                int largeChange = (ClientRectangle.Width - _vertScrollBar.Width) / LargeChangeFactor;
                _horzScrollBar.Maximum = (rectWidth + _vertScrollBar.Width) - ClientRectangle.Width;
                _horzScrollBar.Maximum += largeChange;
                _horzScrollBar.SmallChange = smallChange;
                _horzScrollBar.LargeChange = largeChange;
            }

            int rectHeight = _presenter.GetTotalNodeHeight();
            if (ClientRectangle.Height - _horzScrollBar.Height >= rectHeight)
            {
                _vertScrollBar.Maximum = Minimum;
                _vertScrollBar.Enabled = false;
            }
            else
            {
                _vertScrollBar.Enabled = true;
                _vertScrollBar.Minimum = Minimum;
                int smallChange = (ClientRectangle.Height - _horzScrollBar.Height) / SmallChangeFactor;
                int largeChange = (ClientRectangle.Height - _horzScrollBar.Height) / LargeChangeFactor;
                _vertScrollBar.Maximum = (rectHeight + _horzScrollBar.Height) - ClientRectangle.Height;
                _vertScrollBar.Maximum += largeChange;
                _vertScrollBar.SmallChange = smallChange;
                _vertScrollBar.LargeChange = largeChange;
            }
        }

        //Protected/Virtual/Override Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!DesignMode)
            {
                e.Graphics.Clip = _region;
                e.Graphics.TranslateTransform(-_horzScrollBar.Value, 0);
                _presenter.DrawColumns(e);
                e.Graphics.TranslateClip(0, _presenter.ColumnHeight);
                e.Graphics.TranslateTransform(0, -_vertScrollBar.Value);
                _presenter.DrawNodes(e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_region != null)
                _region.Dispose();
            _region =
                new Region(new Rectangle(0, 0, ClientRectangle.Width - _vertScrollBar.Width,
                                         ClientRectangle.Height - _horzScrollBar.Height));
            base.OnSizeChanged(e);
            SetScrollValues();
            Invalidate();
        }

        //Private Methods

        private void Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        #endregion Methods
    }
}