using ModelViewController_Sample.enumerables;
using ModelViewController_Sample.interfaces;

namespace ModelViewController_Sample
{
	/// <summary>
	/// Summary description for AutoView.
	/// </summary>
	public class AutoView : System.Windows.Forms.UserControl, IVehicleView
	{
		private System.Windows.Forms.Button _btnLeft;
		private System.Windows.Forms.Button _btnRight;
		private System.Windows.Forms.TextBox _txtAmount;
		private System.Windows.Forms.Button _btnDecelerate;
		private System.Windows.Forms.Button _btnAccelerate;
		private System.Windows.Forms.Label _label1;
		private System.Windows.Forms.ProgressBar _pBar;

		
		private IVehicleControl _control;
		private IVehicleModel _model;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private readonly System.ComponentModel.Container _components;

		public AutoView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		public void WireUp(IVehicleControl paramControl, IVehicleModel paramModel)
		{
			if(_model != null)
			{
				_model.RemoveObserver(this);
			}

			_model = paramModel;
			_control = paramControl;

			_control.SetModel(_model);
			_control.SetView(this);
			_model.AddObserver(this);
		}

		public void UpdateInterface(IVehicleModel auto)
		{
			_label1.Text = auto.Name + " heading " + auto.Direction + " at speed: " + auto.Speed;
			_pBar.Value = (auto.Speed>0)? auto.Speed*100/auto.MaxSpeed : auto.Speed*100/auto.MaxReverseSpeed;
			
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(_components != null)
				{
					_components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._btnLeft = new System.Windows.Forms.Button();
			this._btnRight = new System.Windows.Forms.Button();
			this._txtAmount = new System.Windows.Forms.TextBox();
			this._btnDecelerate = new System.Windows.Forms.Button();
			this._btnAccelerate = new System.Windows.Forms.Button();
			this._label1 = new System.Windows.Forms.Label();
			this._pBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// btnLeft
			// 
			this._btnLeft.Location = new System.Drawing.Point(48, 72);
			this._btnLeft.Name = "_btnLeft";
			this._btnLeft.TabIndex = 11;
			this._btnLeft.Text = "Turn Left";
			this._btnLeft.Click += new System.EventHandler(this.BtnLeftClick);
			// 
			// btnRight
			// 
			this._btnRight.Location = new System.Drawing.Point(128, 72);
			this._btnRight.Name = "_btnRight";
			this._btnRight.TabIndex = 10;
			this._btnRight.Text = "Turn Right";
			this._btnRight.Click += new System.EventHandler(this.BtnRightClick);
			// 
			// txtAmount
			// 
			this._txtAmount.Location = new System.Drawing.Point(88, 40);
			this._txtAmount.Name = "_txtAmount";
			this._txtAmount.Size = new System.Drawing.Size(72, 20);
			this._txtAmount.TabIndex = 9;
			this._txtAmount.Text = "10";
			// 
			// btnDecelerate
			// 
			this._btnDecelerate.Location = new System.Drawing.Point(48, 40);
			this._btnDecelerate.Name = "_btnDecelerate";
			this._btnDecelerate.Size = new System.Drawing.Size(32, 23);
			this._btnDecelerate.TabIndex = 8;
			this._btnDecelerate.Text = "<<";
			this._btnDecelerate.Click += new System.EventHandler(this.BtnDecelerateClick);
			// 
			// btnAccelerate
			// 
			this._btnAccelerate.Location = new System.Drawing.Point(168, 40);
			this._btnAccelerate.Name = "_btnAccelerate";
			this._btnAccelerate.Size = new System.Drawing.Size(32, 23);
			this._btnAccelerate.TabIndex = 7;
			this._btnAccelerate.Text = ">>";
			this._btnAccelerate.Click += new System.EventHandler(this.BtnAccelerateClick);
			// 
			// label1
			// 
			this._label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._label1.Location = new System.Drawing.Point(8, 8);
			this._label1.Name = "_label1";
			this._label1.Size = new System.Drawing.Size(240, 23);
			this._label1.TabIndex = 6;
			this._label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pBar
			// 
			this._pBar.Location = new System.Drawing.Point(8, 104);
			this._pBar.Name = "_pBar";
			this._pBar.Size = new System.Drawing.Size(240, 23);
			this._pBar.TabIndex = 12;
			// 
			// AutoView
			// 
			this.Controls.Add(this._pBar);
			this.Controls.Add(this._btnLeft);
			this.Controls.Add(this._btnRight);
			this.Controls.Add(this._txtAmount);
			this.Controls.Add(this._btnDecelerate);
			this.Controls.Add(this._btnAccelerate);
			this.Controls.Add(this._label1);
			this.Name = "AutoView";
			this.Size = new System.Drawing.Size(256, 136);
			this.Load += new System.EventHandler(this.AutoView_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void AutoView_Load(object sender, System.EventArgs e)
		{
		
		}

		#region IVehicleView Members

		public void DisableAcceleration()
		{
			_btnAccelerate.Enabled = false;
		}

		public void EnableAcceleration()
		{
			_btnAccelerate.Enabled = true;
		}

		public void DisableDeceleration()
		{
			_btnDecelerate.Enabled = false;
		}

		public void EnableDeceleration()
		{
			_btnDecelerate.Enabled = true;
		}

		public void DisableTurning()
		{
			_btnRight.Enabled = _btnLeft.Enabled = false;
		}

		public void EnableTurning()
		{
			_btnRight.Enabled = _btnLeft.Enabled = true;
		}

		public void Update(IVehicleModel paramModel)
		{

			UpdateInterface(paramModel);

		}

		#endregion

		private void BtnAccelerateClick(object sender, System.EventArgs e)
		{
			_control.RequestAccelerate(int.Parse(_txtAmount.Text));
		}

		private void BtnDecelerateClick(object sender, System.EventArgs e)
		{
			_control.RequestDecelerate(int.Parse(_txtAmount.Text));
		}

		private void BtnLeftClick(object sender, System.EventArgs e)
		{
			_control.RequestTurn(RelativeDirection.Left);
		}

		private void BtnRightClick(object sender, System.EventArgs e)
		{
			_control.RequestTurn(RelativeDirection.Right);
		}

	}
}
