namespace ScrollbarTest
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class MyScrollControl : UserControl
    {
        #region Fields

        const int LargeChangeFactor = 10;
        const int Minimum = 0;
        const int RectHeight = 500;
        const int RectWidth = 600;
        const int SmallChangeFactor = 20;

        Region _region;

        #endregion Fields

        #region Constructors

        public MyScrollControl()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public void SetScrollBarValues()
        {
            //Set the following scrollbar properties:

            //Minimum: Set to 0

            //SmallChange and LargeChange: Per UI guidelines, these must be set
            //    relative to the size of the view that the user sees, not to
            //    the total size including the unseen part.  In this example,
            //    these must be set relative to the picture box, not to the image.

            //Maximum: Calculate in steps:
            //Step 1: The maximum to scroll is the size of the unseen part.
            //Step 2: Add the size of visible scrollbars if necessary.
            //Step 3: Add an adjustment factor of ScrollBar.LargeChange.

            //Configure the horizontal scrollbar
            //---------------------------------------------
            //if (this.hScrollBar1.Visible)
            //{
            //    this.hScrollBar1.Minimum = 0;
            //    this.hScrollBar1.SmallChange = this.pictureBox1.Width / 20;
            //    this.hScrollBar1.LargeChange = this.pictureBox1.Width / 10;

            //    this.hScrollBar1.Maximum = this.pictureBox1.Image.Size.Width - pictureBox1.ClientSize.Width;  //step 1

            //    if (this.vScrollBar1.Visible) //step 2
            //    {
            //        this.hScrollBar1.Maximum += this.vScrollBar1.Width;
            //    }

            //    this.hScrollBar1.Maximum += this.hScrollBar1.LargeChange; //step 3
            //}

            //Configure the vertical scrollbar
            //---------------------------------------------
            //if (this.vScrollBar1.Visible)
            //{
            //    this.vScrollBar1.Minimum = 0;
            //    this.vScrollBar1.SmallChange = this.pictureBox1.Height / 20;
            //    this.vScrollBar1.LargeChange = this.pictureBox1.Height / 10;

            //    this.vScrollBar1.Maximum = this.pictureBox1.Image.Size.Height - pictureBox1.ClientSize.Height; //step 1

            //    if (this.hScrollBar1.Visible) //step 2
            //    {
            //        this.vScrollBar1.Maximum += this.hScrollBar1.Height;
            //    }

            //    this.vScrollBar1.Maximum += this.vScrollBar1.LargeChange; //step 3
            //}
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!DesignMode)
            {
                e.Graphics.Clip = _region;
                e.Graphics.TranslateTransform(-hScrollBar1.Value, -vScrollBar1.Value);
                e.Graphics.DrawRectangle(Pens.Red, 0, 0, RectWidth, RectHeight);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_region != null)
                _region.Dispose();
            _region = new Region(new Rectangle(0, 0, ClientRectangle.Width - vScrollBar1.Width, ClientRectangle.Height - hScrollBar1.Height));
            base.OnSizeChanged(e);
            SetValues();
        }

        private new void Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        private void SetValues()
        {
            if (ClientRectangle.Width - vScrollBar1.Width >= RectWidth)
                hScrollBar1.Maximum = Minimum;
            else
            {
                hScrollBar1.Minimum = Minimum;
                int smallChange = (ClientRectangle.Width - vScrollBar1.Width) / SmallChangeFactor;
                int largeChange = (ClientRectangle.Width - vScrollBar1.Width) / LargeChangeFactor;
                hScrollBar1.Maximum = (RectWidth + vScrollBar1.Width) - ClientRectangle.Width;
                hScrollBar1.Maximum += largeChange;
                hScrollBar1.SmallChange = smallChange;
                hScrollBar1.LargeChange = largeChange;
            }

            if (ClientRectangle.Height - hScrollBar1.Height >= RectHeight)
                vScrollBar1.Maximum = Minimum;
            else
            {
                vScrollBar1.Minimum = Minimum;
                int smallChange = (ClientRectangle.Height - hScrollBar1.Height) / SmallChangeFactor;
                int largeChange = (ClientRectangle.Height - hScrollBar1.Height) / LargeChangeFactor;
                vScrollBar1.Maximum = (RectHeight + hScrollBar1.Height) - ClientRectangle.Height;
                vScrollBar1.Maximum += largeChange;
                vScrollBar1.SmallChange = smallChange;
                vScrollBar1.LargeChange = largeChange;
            }
        }

        #endregion Methods
    }
}