using System;

namespace MVC
{
    public class Book
    {
        // declare a delegate for the bookpricechanged event
        public delegate void BookPriceChangedHandler(object sender,
        BookPriceChangedEventArgs e);
        
        // declare the bookpricechanged event using the bookpricechangeddelegate
        public event BookPriceChangedHandler BookPriceChanged;
        
        // instance variable for book price
        object _bookPrice;
        
        // property for book price
        public object BookPrice
        {
            set
            {
                // set the instance variable
                _bookPrice=value;
                
                // the price changed so fire the event!
                OnBookPriceChanged();
            }
        }
        
        // method to fire price canged event delegate with proper name
        // this is the method our observers should be implenting!
        protected void OnBookPriceChanged()
        {
            BookPriceChanged(this, new BookPriceChangedEventArgs(_bookPrice));
        }
    }
    public class BookPriceChangedEventArgs : EventArgs
    {
        // instance variable to store the book price
        private readonly object _bookPrice;

        // constructor that sets book price
        public BookPriceChangedEventArgs(object bookPrice)
        {
            _bookPrice = bookPrice;
        }

        // public property for the book price
        public object BookPrice
        {
            get
            {
                return _bookPrice;
            }
        }
    }

}
