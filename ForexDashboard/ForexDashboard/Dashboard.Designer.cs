namespace ForexDashboard
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._getFeedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _getFeedButton
            // 
            this._getFeedButton.Location = new System.Drawing.Point(214, 193);
            this._getFeedButton.Name = "_getFeedButton";
            this._getFeedButton.Size = new System.Drawing.Size(75, 23);
            this._getFeedButton.TabIndex = 0;
            this._getFeedButton.Text = "GetFeed";
            this._getFeedButton.UseVisualStyleBackColor = true;
            this._getFeedButton.Click += new System.EventHandler(this.OnGetFeedButtonClick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 486);
            this.Controls.Add(this._getFeedButton);
            this.Name = "Dashboard";
            this.Text = "Forex Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _getFeedButton;
    }
}

