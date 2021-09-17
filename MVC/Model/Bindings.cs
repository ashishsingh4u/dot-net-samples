using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Model
{
    #region SortableSearchable List

    [Serializable]
    public class SortableSearchableList<T> : BindingList<T>, ITypedList
    {
        private ListSortDirection _sortDirectionValue;
        private PropertyDescriptor _sortPropertyValue;
        private ArrayList _sortedList;
        private ArrayList _unsortedItems;
        [NonSerialized]
        private readonly PropertyDescriptorCollection _properties;

        public SortableSearchableList()
        {
            // Get the 'shape' of the list. 
            // Only get the public _properties marked with Browsable = true.
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(
                typeof(T),
                new Attribute[] { new BrowsableAttribute(true) });

            // Sort the _properties.
            _properties = pdc.Sort();
        }

        public override void EndNew(int itemIndex)
        {
            if (_sortPropertyValue != null && itemIndex > 0
                && itemIndex == Count - 1)
                ApplySortCore(_sortPropertyValue, _sortDirectionValue);
            base.EndNew(itemIndex);


        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortPropertyValue; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirectionValue; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        public void RemoveSort()
        {
            RemoveSortCore();
        }

        protected override void ApplySortCore(PropertyDescriptor prop,
            ListSortDirection direction)
        {
            _sortedList = new ArrayList();

            // Check to see if the property type we are sorting by implements
            // the IComparable interface.
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType != null)
            {
                // If so, set the SortPropertyValue and SortDirectionValue.
                _sortPropertyValue = prop;
                _sortDirectionValue = direction;

                _unsortedItems = new ArrayList(Count);

                // Loop through each item, adding it the the sortedItems ArrayList.
                foreach (Object item in Items)
                {
                    _sortedList.Add(prop.GetValue(item));
                    _unsortedItems.Add(item);
                }

                // Call Sort on the ArrayList.
                _sortedList.Sort();

                // Check the sort direction and then copy the sorted items
                // back into the list.
                if (direction == ListSortDirection.Descending)
                    _sortedList.Reverse();

                for (int i = 0; i < Count; i++)
                {
                    int position = Find(prop.Name, _sortedList[i]);
                    if (position != i)
                    {
                        T temp = this[i];
                        this[i] = this[position];
                        this[position] = temp;
                    }
                }
                // Raise the ListChanged event so bound controls refresh their
                // values.
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
                // If the property type does not implement IComparable, let the user
                // know.
                throw new NotSupportedException("Cannot sort by " + prop.Name + ". This" +
                    prop.PropertyType + " does not implement IComparable");
        }

        protected override void RemoveSortCore()
        {
            // Ensure the list has been sorted.
            if (_unsortedItems != null)
            {
                // Loop through the unsorted items and reorder the
                // list per the unsorted list.
                for (int i = 0; i < _unsortedItems.Count; )
                {
                    int position = Find(SortPropertyCore.Name,
                        _unsortedItems[i].GetType().
                        GetProperty(SortPropertyCore.Name).
                        GetValue(_unsortedItems[i], null));
                    if (position >= 0 && position != i)
                    {
                        object temp = this[i];
                        this[i] = this[position];
                        this[position] = (T)temp;
                        i++;
                    }
                    else if (position == i)
                        i++;
                    else
                        // If an item in the unsorted list no longer exists, delete it.
                        _unsortedItems.RemoveAt(i);
                }
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            // Get the property info for the specified property.
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);

            if (key != null)
            {
                // Loop through the the items to see if the key
                // value matches the property value.
                for (int i = 0; i < Count; ++i)
                {
                    T item = Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }

        public int Find(string property, object key)
        {
            // Check the _properties for a property with the specified name.
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            // If there is not a match, return -1 otherwise pass search to
            // FindCore method.
            if (prop == null)
                return -1;
            return FindCore(prop, key);
        }

        #region ITypedList Implementation

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            PropertyDescriptorCollection pdc;

            if (listAccessors != null && listAccessors.Length > 0)
            {
                // Return child list shape.
                pdc = ListBindingHelper.GetListItemProperties(listAccessors[0].PropertyType);
            }
            else
            {
                // Return _properties in sort order.
                pdc = _properties;
            }

            return pdc;
        }

        // This method is only used in the design-time framework 
        // and by the obsolete DataGrid control.
        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return typeof(T).Name;
        }

        #endregion

    }
    #endregion
}
