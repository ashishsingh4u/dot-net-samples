namespace ForexUpdater
{
    partial class Forex
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
            this._buttonUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _buttonUpdate
            // 
            this._buttonUpdate.Location = new System.Drawing.Point(98, 113);
            this._buttonUpdate.Name = "_buttonUpdate";
            this._buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this._buttonUpdate.TabIndex = 0;
            this._buttonUpdate.Text = "button1";
            this._buttonUpdate.UseVisualStyleBackColor = true;
            this._buttonUpdate.Click += new System.EventHandler(ButtonUpdateClick);
            // 
            // Forex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._buttonUpdate);
            this.Name = "Forex";
            this.Text = "Forex";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonUpdate;
    }
}

