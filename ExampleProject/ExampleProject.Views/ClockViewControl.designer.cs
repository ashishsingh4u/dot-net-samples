namespace ExampleProject.Views
{
    partial class ClockViewControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Combo_TimeZones = new System.Windows.Forms.ComboBox();
            this.LableTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Combo_TimeZones
            // 
            this.Combo_TimeZones.FormattingEnabled = true;
            this.Combo_TimeZones.Location = new System.Drawing.Point(3, 20);
            this.Combo_TimeZones.Name = "Combo_TimeZones";
            this.Combo_TimeZones.Size = new System.Drawing.Size(311, 21);
            this.Combo_TimeZones.Sorted = true;
            this.Combo_TimeZones.TabIndex = 1;
            this.Combo_TimeZones.SelectionChangeCommitted += new System.EventHandler(this.ComboTimeZonesSelectionChangeCommitted);
            // 
            // LableTime
            // 
            this.LableTime.AutoSize = true;
            this.LableTime.Location = new System.Drawing.Point(3, 54);
            this.LableTime.Name = "LableTime";
            this.LableTime.Size = new System.Drawing.Size(35, 13);
            this.LableTime.TabIndex = 2;
            this.LableTime.Text = "label1";
            // 
            // ClockViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LableTime);
            this.Controls.Add(this.Combo_TimeZones);
            this.Name = "ClockViewControl";
            this.Size = new System.Drawing.Size(340, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Combo_TimeZones;
        private System.Windows.Forms.Label LableTime;
    }
}
