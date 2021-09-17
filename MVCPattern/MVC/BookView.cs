using System;
using System.Windows.Forms;

namespace MVC
{
    public partial class BookView : Form
    {
        private readonly Book.BookPriceChangedHandler _bookDelegate;
        private readonly Book _book;

        public BookView()
        {
            InitializeComponent();
            //create new book instance
            _book = new Book();

            // create a new delegate, instance and bind it
            // to the observer's bookpricechanged method
            _bookDelegate = new Book.BookPriceChangedHandler(BookPriceChanged);
            //add the delegate to the event
            _book.BookPriceChanged += _bookDelegate;
        }

        public void BookPriceChanged(object sender, BookPriceChangedEventArgs e)
        {
            // update bookprice views
            object bookPrice = e.BookPrice;
            textBox2.Text = bookPrice.ToString();
            label1.Text = bookPrice.ToString();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            // change book price
            _book.BookPrice = textBox1.Text;
        }

    }
}
