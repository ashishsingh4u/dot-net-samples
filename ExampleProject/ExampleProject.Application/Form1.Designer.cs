namespace ExampleProject.Application
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clockViewControl1 = new ExampleProject.Views.ClockViewControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clockViewControl2 = new ExampleProject.Views.ClockViewControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clockViewControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 121);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First Instance";
            // 
            // clockViewControl1
            // 
            this.clockViewControl1.Location = new System.Drawing.Point(6, 19);
            this.clockViewControl1.Name = "clockViewControl1";
            this.clockViewControl1.Size = new System.Drawing.Size(340, 83);
            this.clockViewControl1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clockViewControl2);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 113);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Another view (also wired to same presenter!)";
            // 
            // clockViewControl2
            // 
            this.clockViewControl2.Location = new System.Drawing.Point(6, 19);
            this.clockViewControl2.Name = "clockViewControl2";
            this.clockViewControl2.Size = new System.Drawing.Size(340, 83);
            this.clockViewControl2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 264);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Single Presenter, Dual Views";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ExampleProject.Views.ClockViewControl clockViewControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ExampleProject.Views.ClockViewControl clockViewControl2;

    }
}