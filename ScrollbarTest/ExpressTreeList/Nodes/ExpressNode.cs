using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ExpressTreeList.Nodes
{ 
    public class ExpressNode : INotifyPropertyChanged
    {

        #region Enums
        #endregion Enums

        #region Fields

        public const int DefaultNodeHeight = 20;

        private readonly List<ICellValue> _cells;

        #endregion Fields

        #region Constructor

        public ExpressNode(params ICellValue[] objects)
        {
            _cells = new List<ICellValue>(objects);
            Bounds = new Rectangle();
        }

        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        //Public Properties

        public Rectangle Bounds { get; set; }

        public List<ICellValue> CellList
        {
            get { return _cells; }
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

        //Protected/Virtual/Override Methods

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, eventArgs);
        }

        //Private Methods

        #endregion Methods

    }

    public class ExpressNodes : IList<ExpressNode>, INotifyPropertyChanged
    {

        #region Enums
        #endregion Enums

        #region Fields

        private readonly List<ExpressNode> _nodes;

        #endregion Fields

        #region Constructor

        public ExpressNodes()
        {
            _nodes = new List<ExpressNode>();
        }

        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        //Public Properties

        public int Count
        {
            get { return _nodes.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<ExpressNode>)_nodes).IsReadOnly; }
        }

        //Protected/Virtual/Override Properties

        //Private Properties

        #endregion Properties

        #region Indexers

        //Public Indexers

        public ExpressNode this[int index]
        {
            get { return _nodes[index]; }
            set
            {
                _nodes[index] = value;
                RaisePropertyChange("this");
            }
        }

        //Protected/Virtual/Override Indexers

        //Private Indexers

        #endregion Indexers

        #region Methods

        //Public Methods
        public void Add(ExpressNode item)
        {
            _nodes.Add(item);
            RaisePropertyChange("Add");
        }

        public void AddRange(ExpressNode[] items)
        {
            _nodes.AddRange(items);
            RaisePropertyChange("AddRange");
        }

        public void Clear()
        {
            _nodes.Clear();
            RaisePropertyChange("Clear");
        }

        public bool Contains(ExpressNode item)
        {
            return _nodes.Contains(item);
        }

        public void CopyTo(ExpressNode[] array, int arrayIndex)
        {
            _nodes.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ExpressNode> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public int IndexOf(ExpressNode item)
        {
            return IndexOf(item);
        }

        public void Insert(int index, ExpressNode item)
        {
            _nodes.Insert(index, item);
        }

        public bool Remove(ExpressNode item)
        {
            var deleted = _nodes.Remove(item);
            RaisePropertyChange("Remove");
            return deleted;
        }

        public void RemoveAt(int index)
        {
            _nodes.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //Protected/Virtual/Override Methods

        protected virtual void RaisePropertyChange(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, args);
        }


        //Private Methods

        #endregion Methods

    }

    public interface ICellValue
    {
        string CellText { get; set; }
    }
}