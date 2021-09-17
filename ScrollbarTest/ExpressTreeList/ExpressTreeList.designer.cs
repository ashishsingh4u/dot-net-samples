namespace ExpressTreeList
{
    partial class ExpressTreeList
    {
        #region Fields

        protected System.Windows.Forms.HScrollBar _horzScrollBar;
        protected System.Windows.Forms.VScrollBar _vertScrollBar;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._horzScrollBar = new System.Windows.Forms.HScrollBar();
            this._vertScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            //
            // hScrollBar1
            //
            this._horzScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._horzScrollBar.Location = new System.Drawing.Point(0, 184);
            this._horzScrollBar.Name = "hScrollBar1";
            this._horzScrollBar.Size = new System.Drawing.Size(280, 17);
            this._horzScrollBar.TabIndex = 0;
            this._horzScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Scroll);
            //
            // vScrollBar1
            //
            this._vertScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._vertScrollBar.Location = new System.Drawing.Point(280, 0);
            this._vertScrollBar.Name = "vScrollBar1";
            this._vertScrollBar.Size = new System.Drawing.Size(17, 184);
            this._vertScrollBar.TabIndex = 0;
            this._vertScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Scroll);
            //
            // MyTreeList
            //
            this.Controls.Add(this._vertScrollBar);
            this.Controls.Add(this._horzScrollBar);
            this.Name = "MyScrollControl";
            this.Size = new System.Drawing.Size(297, 201);
            this.ResumeLayout(false);
        }

        #endregion Methods
    }
}