namespace ThreadDemo
{
    partial class Test
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
            this._thread = new System.Windows.Forms.Button();
            this._threadPoolWorker = new System.Windows.Forms.Button();
            this._threadPoolSingle = new System.Windows.Forms.Button();
            this._Cultures = new System.Windows.Forms.ComboBox();
            this._setCulture = new System.Windows.Forms.Button();
            this._crash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _thread
            // 
            this._thread.Location = new System.Drawing.Point(24, 54);
            this._thread.Name = "_thread";
            this._thread.Size = new System.Drawing.Size(95, 23);
            this._thread.TabIndex = 0;
            this._thread.Text = "Thread";
            this._thread.UseVisualStyleBackColor = true;
            this._thread.Click += new System.EventHandler(this._thread_Click);
            // 
            // _threadPoolWorker
            // 
            this._threadPoolWorker.Location = new System.Drawing.Point(24, 112);
            this._threadPoolWorker.Name = "_threadPoolWorker";
            this._threadPoolWorker.Size = new System.Drawing.Size(95, 23);
            this._threadPoolWorker.TabIndex = 0;
            this._threadPoolWorker.Text = "Queue Worker";
            this._threadPoolWorker.UseVisualStyleBackColor = true;
            this._threadPoolWorker.Click += new System.EventHandler(this._threadPoolWorker_Click);
            // 
            // _threadPoolSingle
            // 
            this._threadPoolSingle.Location = new System.Drawing.Point(24, 83);
            this._threadPoolSingle.Name = "_threadPoolSingle";
            this._threadPoolSingle.Size = new System.Drawing.Size(95, 23);
            this._threadPoolSingle.TabIndex = 0;
            this._threadPoolSingle.Text = "Single Object";
            this._threadPoolSingle.UseVisualStyleBackColor = true;
            this._threadPoolSingle.Click += new System.EventHandler(this._threadPoolSingle_Click);
            // 
            // _Cultures
            // 
            this._Cultures.FormattingEnabled = true;
            this._Cultures.Location = new System.Drawing.Point(24, 12);
            this._Cultures.Name = "_Cultures";
            this._Cultures.Size = new System.Drawing.Size(156, 21);
            this._Cultures.TabIndex = 1;
            // 
            // _setCulture
            // 
            this._setCulture.Location = new System.Drawing.Point(186, 12);
            this._setCulture.Name = "_setCulture";
            this._setCulture.Size = new System.Drawing.Size(75, 23);
            this._setCulture.TabIndex = 2;
            this._setCulture.Text = "Set Culture";
            this._setCulture.UseVisualStyleBackColor = true;
            this._setCulture.Click += new System.EventHandler(this._setCulture_Click);
            // 
            // _crash
            // 
            this._crash.Location = new System.Drawing.Point(141, 54);
            this._crash.Name = "_crash";
            this._crash.Size = new System.Drawing.Size(75, 23);
            this._crash.TabIndex = 3;
            this._crash.Text = "Crash";
            this._crash.UseVisualStyleBackColor = true;
            this._crash.Click += new System.EventHandler(this._crash_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._crash);
            this.Controls.Add(this._setCulture);
            this.Controls.Add(this._Cultures);
            this.Controls.Add(this._threadPoolSingle);
            this.Controls.Add(this._threadPoolWorker);
            this.Controls.Add(this._thread);
            this.Name = "Test";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Test_FormClosed);
            this.Load += new System.EventHandler(this.Test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _thread;
        private System.Windows.Forms.Button _threadPoolWorker;
        private System.Windows.Forms.Button _threadPoolSingle;
        private System.Windows.Forms.ComboBox _Cultures;
        private System.Windows.Forms.Button _setCulture;
        private System.Windows.Forms.Button _crash;
    }
}

