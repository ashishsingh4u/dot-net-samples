namespace PgmServer
{
    partial class Server
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
            this._startServerButton = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _startServerButton
            // 
            this._startServerButton.Location = new System.Drawing.Point(92, 114);
            this._startServerButton.Name = "_startServerButton";
            this._startServerButton.Size = new System.Drawing.Size(92, 23);
            this._startServerButton.TabIndex = 0;
            this._startServerButton.Text = "Start Server";
            this._startServerButton.UseVisualStyleBackColor = true;
            this._startServerButton.Click += new System.EventHandler(this.StartServerButtonClick);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(60, 78);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(155, 20);
            this.txtMsg.TabIndex = 1;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this._startServerButton);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _startServerButton;
        private System.Windows.Forms.TextBox txtMsg;
    }
}

