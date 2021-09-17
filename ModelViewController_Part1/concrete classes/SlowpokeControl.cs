using ModelViewController_Sample.enumerables;
using ModelViewController_Sample.interfaces;

namespace ModelViewController_Sample
{
	/// <summary>
	/// Summary description for SlowpokeControl.
	/// </summary>
	public class SlowpokeControl:IVehicleControl
	{
		private IVehicleModel _model;
		private IVehicleView _view;

		public SlowpokeControl(IVehicleModel paramModel, IVehicleView paramView)
		{
			_model = paramModel;
			_view = paramView;
		}
		public SlowpokeControl()
		{
		}

		#region IVehicleControl Members

		public void SetModel(IVehicleModel paramValue)
		{
			_model = paramValue;
		}

		public void SetView(IVehicleView paramValue)
		{
			_view = paramValue;

		}


		public void RequestAccelerate(int paramValue)
		{
			if(_model != null)
			{
				int amount = paramValue;

				if(amount > 5) amount = 5;
				_model.Accelerate(amount);

				CheckState();
			}
		}

		public void RequestDecelerate(int paramValue)
		{
			if(_model != null)
			{
				int amount = paramValue;

				if(amount > 5) amount = 5;
				_model.Decelerate(amount);

				CheckState();
			}
		}

		public void RequestTurn(RelativeDirection paramValue)
		{
			_model.Turn(paramValue);
		}

		#endregion

		public void CheckState()
		{
			if(_model.Speed >= _model.MaxSpeed)
			{ 
				_view.DisableAcceleration();
			}
			else 
			{
				_view.EnableAcceleration();
			}

			if(_model.Speed <= _model.MaxReverseSpeed)
			{ 
				_view.DisableDeceleration();
			}
			else 
			{
				_view.EnableDeceleration();
			}

			if(_model.Speed >= _model.MaxTurnSpeed) 
			{
				_view.DisableTurning();
			}
			else 
			{
				_view.EnableTurning();
			}
		}

	}
}



