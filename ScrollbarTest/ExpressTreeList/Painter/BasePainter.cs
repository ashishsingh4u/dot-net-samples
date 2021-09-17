using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ExpressTreeList.Painter
{
    class BasePainter : ITreePainter
    {

        #region Enums

        private State ButtonStatus
        {
            set { _status = value; }
        }

        private enum State
        {
            Normal,
            Hover,
            Click
        }

        [Flags]
        private enum RectangleCorners
        {
            None = 0, TopLeft = 1, TopRight = 2,
            BottomLeft = 4, BottomRight = 8,
// ReSharper disable UnusedMember.Local
            All = TopLeft | TopRight | BottomLeft | BottomRight
// ReSharper restore UnusedMember.Local
        }

        #endregion Enums

        #region Fields

        private readonly Color _headerBackColor = Color.WhiteSmoke;
        private readonly Color _headerForeColor = Color.Black;
        private readonly Pen _separator;
        private State _status;

        #endregion Fields



        #region Constructor

        public BasePainter()
        {
            _separator = new Pen(Color.Silver) {DashStyle = DashStyle.Dot, DashOffset = 0.5f};
        }

        #endregion Constructor

        #region Delegates

        #endregion Delegates

        #region Events

        #endregion Events

        #region Properties

        //Public Properties

        private int _radius = 6;

        public int RoundedCornerRadius
        {
            get { return _radius; }
            set { _radius = value; ButtonStatus = _status; }
        }
        
        private bool _antialias = true;

        public bool FontAntiAlias
        {
            get { return _antialias; }
            set { _antialias = value; ButtonStatus = _status; }
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

        public void DrawHeader(DrawColumnEventArgs args)
        {
            //ControlPaint.DrawBorder3D(args.PaintEventArgs.Graphics, args.Column.Bounds, Border3DStyle.Raised);
            //args.PaintEventArgs.Graphics.DrawString(args.Column.Text, args.Font, Brushes.Black, args.Column.Bounds);
            var bmp = DrawButton(new Rectangle(0, 0, args.Column.Bounds.Width, args.Column.Bounds.Height), args.Column.Text, args.Font);
            if (bmp != null)
            {
                args.PaintEventArgs.Graphics.DrawImage(bmp, args.Column.Bounds, new Rectangle(0, 0, args.Column.Bounds.Width, args.Column.Bounds.Height), GraphicsUnit.Pixel);
                bmp.Dispose();
            }
        }

        public void DrawNode(DrawNodeEventArgs args)
        {
            args.PaintEventArgs.Graphics.DrawRectangle(_separator, args.Node.Bounds);
            args.PaintEventArgs.Graphics.DrawString(args.CellText, args.Font, Brushes.Black, args.Node.Bounds);
        }

        public void DrawNodeCell(DrawNodeEventArgs args)
        {
            args.PaintEventArgs.Graphics.DrawRectangle(_separator, args.Node.Bounds);
            args.PaintEventArgs.Graphics.DrawString(args.CellText, args.Font, Brushes.Black, args.Node.Bounds);
        }

        //Protected/Virtual/Override Methods

        //Private Methods

        private Bitmap DrawButton(Rectangle rectBar, string text, Font font)
        {
            var bmp = new Bitmap(rectBar.Width, rectBar.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                DrawGlass(gr, rectBar, text, font);
            }
            return bmp;
        }

        private void DrawGlass(Graphics gr, Rectangle rectBar, string text, Font font)
        {
            // Some calculations
            if (rectBar.Height <= 0) rectBar.Height = 1;
            int nAlphaStart = 185 + 5 * rectBar.Width / 24,
                nAlphaEnd = 10 + 4 * rectBar.Width / 24;
            if (nAlphaStart > 255) nAlphaStart = 255;
            else if (nAlphaStart < 0) nAlphaStart = 0;
            if (nAlphaEnd > 255) nAlphaEnd = 255;
            else if (nAlphaEnd < 0) nAlphaEnd = 0;
            Color colorBacklight;
            Color colorFillBk;
            Color colorBorder;

            switch (_status)
            {
                case State.Click:
                    colorBacklight = GetDarkerColor(_headerBackColor, 20);
                    colorFillBk = GetDarkerColor(_headerBackColor, 40);
                    colorBorder = GetDarkerColor(_headerBackColor, 60);
                    break;
                case State.Hover:
                    colorBacklight = GetLighterColor(_headerBackColor, 5);
                    colorFillBk = GetLighterColor(_headerBackColor, 10);
                    colorBorder = GetDarkerColor(_headerBackColor, 100);
                    break;
// ReSharper disable RedundantCaseLabel
                case State.Normal:
// ReSharper restore RedundantCaseLabel
                default:
                    colorBacklight = _headerBackColor;
                    colorFillBk = GetDarkerColor(_headerBackColor, 85);
                    colorBorder = GetDarkerColor(_headerBackColor, 100);
                    break;
            }

            var colorBacklightEnd = Color.FromArgb(50, 0, 0, 0);
            var colorGlowStart = Color.FromArgb(nAlphaEnd, 255, 255, 255);
            var colorGlowEnd = Color.FromArgb(nAlphaStart, 255, 255, 255);

            // Create gradient path
            var er = new RectangleF(rectBar.Left - (rectBar.Width), rectBar.Top - (rectBar.Height / 2), rectBar.Width * 3, rectBar.Height * 4);
            var rctPath = new GraphicsPath();
            rctPath.AddEllipse(er);
            // Create gradient
            var pgr = new PathGradientBrush(rctPath)
                          {
// ReSharper disable PossibleLossOfFraction
                              CenterPoint = new PointF(rectBar.Width/2, rectBar.Height),
// ReSharper restore PossibleLossOfFraction
                              CenterColor = colorBacklight,
                              SurroundColors = new[] {colorBacklightEnd}
                          };
            // Create glow
            var rectBarPath = CreateRoundedPath(rectBar.X, rectBar.Y, rectBar.Width - 1, rectBar.Height - 1, _radius, RectangleCorners.None);
            var rectBarPathHalf = CreateRoundedPath(rectBar.X, rectBar.Y, rectBar.Width - 1, (rectBar.Height - 1) / 2, _radius, RectangleCorners.None);
            var rectGlow = new Rectangle(rectBar.Left, rectBar.Top, rectBar.Width, rectBar.Height / 2);
            var brGlow = new LinearGradientBrush(
                new PointF(rectGlow.Left, rectGlow.Height + 1), new PointF(rectGlow.Left, rectGlow.Top - 1),
                colorGlowStart, colorGlowEnd);
            // Draw the button
            gr.FillRectangle(new SolidBrush(SystemColors.Control), rectBar);
            gr.FillPath(new SolidBrush(colorFillBk), rectBarPath);
            gr.FillPath(pgr, rectBarPath);
            gr.FillPath(brGlow, rectBarPathHalf);
            gr.DrawPath(new Pen(colorBorder, 1), rectBarPath);
            var stringFormat = new StringFormat();
            const ContentAlignment temp = ContentAlignment.TopLeft;
            switch (temp)
            {

                case ContentAlignment.TopLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    stringFormat.Alignment = StringAlignment.Far;
                    stringFormat.LineAlignment = StringAlignment.Far;
                    break;

            }

            var drawBrush = new SolidBrush(_headerForeColor);
            if (_antialias)
                gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gr.DrawString(text, font, drawBrush, rectBar, stringFormat);

        }

        private static Color GetDarkerColor(Color color, byte intensity)
        {
            int r = color.R - intensity;
            int g = color.G - intensity;
            int b = color.B - intensity;
            if (r > 255 || r < 0) r *= -1;
            if (g > 255 || g < 0) g *= -1;
            if (b > 255 || b < 0) b *= -1;
            return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        }

        private static Color GetLighterColor(Color color, byte intensity)
        {
            int r = color.R + intensity;
            int g = color.G + intensity;
            int b = color.B + intensity;
            if (r > 255 || r < 0) r *= -1;
            if (g > 255 || g < 0) g *= -1;
            if (b > 255 || b < 0) b *= -1;
            return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        }

        private static GraphicsPath CreateRoundedPath(int x, int y, int width, int height, int radius, RectangleCorners corners)
        {

            int xw = x + width - 1;
            int yh = y + height - 1;
            int xwr = xw - radius;
            int yhr = yh - radius;
            int xr = x + radius;
            int yr = y + radius;
            int r2 = radius * 2;
            int xwr2 = xw - r2;
            int yhr2 = yh - r2;

            var p = new GraphicsPath();
            p.StartFigure();

            //Top Left Corner

            if ((RectangleCorners.TopLeft & corners) == RectangleCorners.TopLeft)
            {
                p.AddArc(x, y, r2, r2, 180, 90);
            }
            else
            {
                p.AddLine(x, yr, x, y);
                p.AddLine(x, y, xr, y);
            }

            //Top Edge

            p.AddLine(xr, y, xwr, y);

            //Top Right Corner

            if ((RectangleCorners.TopRight & corners) == RectangleCorners.TopRight)
            {
                p.AddArc(xwr2, y, r2, r2, 270, 90);
            }
            else
            {
                p.AddLine(xwr, y, xw, y);
                p.AddLine(xw, y, xw, yr);
            }

            //Right Edge

            p.AddLine(xw, yr, xw, yhr);

            //Bottom Right Corner

            if ((RectangleCorners.BottomRight & corners) == RectangleCorners.BottomRight)
            {
                p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
            }
            else
            {
                p.AddLine(xw, yhr, xw, yh);
                p.AddLine(xw, yh, xwr, yh);
            }

            //Bottom Edge

            p.AddLine(xwr, yh, xr, yh);

            //Bottom Left Corner

            if ((RectangleCorners.BottomLeft & corners) == RectangleCorners.BottomLeft)
            {
                p.AddArc(x, yhr2, r2, r2, 90, 90);
            }
            else
            {
                p.AddLine(xr, yh, x, yh);
                p.AddLine(x, yh, x, yhr);
            }

            //Left Edge

            p.AddLine(x, yhr, x, yr);
            p.CloseFigure();
            return p;
        }

        #endregion Methods
    }

}


