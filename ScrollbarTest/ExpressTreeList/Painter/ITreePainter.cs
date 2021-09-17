using System;
using System.Drawing;
using System.Windows.Forms;
using ExpressTreeList.Columns;
using ExpressTreeList.Nodes;

namespace ExpressTreeList.Painter
{
    public interface ITreePainter
    {

        #region Events
        #endregion Events

        #region Properties
        #endregion Properties

        #region Indexers
        #endregion Indexers

        #region Methods

        void DrawHeader(DrawColumnEventArgs args);

        void DrawNode(DrawNodeEventArgs args);

        void DrawNodeCell(DrawNodeEventArgs args);

        #endregion Methods
    }

    public class DrawColumnEventArgs : EventArgs
    {

        #region Enums
        #endregion Enums

        #region Fields
        #endregion Fields

        #region Constructor
        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events
        #endregion Events

        #region Properties

        //Public Properties

        public ExpressColumn Column { get; set; }

        public PaintEventArgs PaintEventArgs { get; set; }

        public Font Font { get; set; }

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

        //Protected/Virtual/Override Methods

        //Private Methods

        #endregion Methods
    }

    public class DrawNodeEventArgs : EventArgs
    {

        #region Enums
        #endregion Enums

        #region Fields
        #endregion Fields

        #region Constructor
        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events
        #endregion Events

        #region Properties

        //Public Properties

        public ExpressNode Node { get; set; }

        public ExpressColumn Column { get; set; }

        public string CellText { get; set; }

        public PaintEventArgs PaintEventArgs { get; set; }

        public Font Font { get; set; }

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

        //Protected/Virtual/Override Methods

        //Private Methods

        #endregion Methods
    }

}
