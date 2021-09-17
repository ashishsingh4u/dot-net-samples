namespace PgmClient
{
    partial class Client
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
            this._receiveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _receiveButton
            // 
            this._receiveButton.Location = new System.Drawing.Point(87, 108);
            this._receiveButton.Name = "_receiveButton";
            this._receiveButton.Size = new System.Drawing.Size(89, 23);
            this._receiveButton.TabIndex = 0;
            this._receiveButton.Text = "Receive data";
            this._receiveButton.UseVisualStyleBackColor = true;
            this._receiveButton.Click += new System.EventHandler(this.ReceiveButtonClick);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._receiveButton);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _receiveButton;
    }
}

