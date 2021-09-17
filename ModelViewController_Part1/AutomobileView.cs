using System;
using System.Windows.Forms;

namespace ModelViewController_Sample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FrmAutomobileView : Form
	{

/*
		private ArrayList _aList = new ArrayList();
*/
		private AutoView _autoView1;


		private System.ComponentModel.Container components = null;

		public FrmAutomobileView()
		{
			InitializeComponent();	

			_autoView1.WireUp(new SlowpokeControl(), new Truck("Ford Pickup"));
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}






		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._autoView1 = new ModelViewController_Sample.AutoView();
			this.SuspendLayout();
			// 
			// autoView1
			// 
			this._autoView1.Location = new System.Drawing.Point(8, 8);
			this._autoView1.Name = "_autoView1";
			this._autoView1.Size = new System.Drawing.Size(256, 150);
			this._autoView1.TabIndex = 0;
			this._autoView1.Load += new System.EventHandler(this.AutoView1Load);
			// 
			// frmAutomobileView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(272, 165);
			this.Controls.Add(this._autoView1);
			this.Name = "FrmAutomobileView";
			this.Text = "Automobile View";
			this.Load += new System.EventHandler(this.FrmAutomobileViewLoad);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrmAutomobileView());
		}

		#region "UI Methods"


		private void FrmAutomobileViewLoad(object sender, EventArgs e)
		{
		
		}


		#endregion


		private void AutoView1Load(object sender, EventArgs e)
		{
		
		}


	}
}
