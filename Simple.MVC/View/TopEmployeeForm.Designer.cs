namespace Simple.MVC.View
{
    partial class TopEmployeeForm
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
            this.ShowButton = new System.Windows.Forms.Button();
            this.TopEmployeeNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(12, 12);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(75, 23);
            this.ShowButton.TabIndex = 0;
            this.ShowButton.Text = "Show";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // TopEmployeeNameLabel
            // 
            this.TopEmployeeNameLabel.AutoSize = true;
            this.TopEmployeeNameLabel.Location = new System.Drawing.Point(12, 50);
            this.TopEmployeeNameLabel.Name = "TopEmployeeNameLabel";
            this.TopEmployeeNameLabel.Size = new System.Drawing.Size(0, 13);
            this.TopEmployeeNameLabel.TabIndex = 1;
            // 
            // TopEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.TopEmployeeNameLabel);
            this.Controls.Add(this.ShowButton);
            this.Name = "TopEmployeeForm";
            this.Text = "c";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Label TopEmployeeNameLabel;
    }
}