using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ExpressTreeList.Columns
{
    public class ExpressColumn : INotifyPropertyChanged
    {

        #region Enums
        #endregion Enums

        #region Fields

        public const int DefaultColumnHeight = 20;
        public const int DefaultColumnGap = 1;
        private const int DefaultColumnWidth = 60;

        private string _name;
        private Rectangle _rectBounds;
        private string _text;

        #endregion Fields

        #region Constructor

        public ExpressColumn(string name)
        {
            _name = name;
            _rectBounds = new Rectangle(0, 0, DefaultColumnWidth, DefaultColumnHeight);
        }

        #endregion Constructor

        #region Delegates
        #endregion Delegates

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        //Public Properties

        public Rectangle Bounds
        {
            get { return _rectBounds; }
            set { _rectBounds = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public int Width
        {
            set
            {
                _rectBounds.Width = value;
                OnPropertyChanged("Bounds");
            }
            get { return _rectBounds.Width; }
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

    public class ExpressColumns : IList<ExpressColumn>, INotifyPropertyChanged
    {

        #region Enums
        #endregion Enums

        #region Fields

        private readonly List<ExpressColumn> _columns;

        #endregion Fields

        #region Constructor

        public ExpressColumns()
        {
            _columns = new List<ExpressColumn>();
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
            get { return _columns.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IList<ExpressColumn>)_columns).IsReadOnly; }
        }

        //Protected/Virtual/Override Properties

        //Private Properties

        #endregion Properties

        #region Indexers

        //Public Indexers

        public ExpressColumn this[int index]
        {
            get { return _columns[index]; }
            set
            {
                _columns[index] = value;
                RaisePropertyChange("this");
            }
        }

        //Protected/Virtual/Override Indexers

        //Private Indexers

        #endregion Indexers

        #region Methods

        //Public Methods
        public void Add(ExpressColumn item)
        {
            _columns.Add(item);
            RaisePropertyChange("Add");
        }

        public void AddRange(ExpressColumn[] items)
        {
            _columns.AddRange(items);
            RaisePropertyChange("AddRange");
        }

        public void Clear()
        {
            _columns.Clear();
            RaisePropertyChange("Clear");
        }

        public bool Contains(ExpressColumn item)
        {
            return _columns.Contains(item);
        }

        public void CopyTo(ExpressColumn[] array, int arrayIndex)
        {
            _columns.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ExpressColumn> GetEnumerator()
        {
            return _columns.GetEnumerator();
        }

        public int IndexOf(ExpressColumn item)
        {
            return IndexOf(item);
        }

        public void Insert(int index, ExpressColumn item)
        {
            _columns.Insert(index, item);
        }

        public bool Remove(ExpressColumn item)
        {
            var deleted = _columns.Remove(item);
            RaisePropertyChange("Remove");
            return deleted;
        }

        public void RemoveAt(int index)
        {
            _columns.RemoveAt(index);
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
}