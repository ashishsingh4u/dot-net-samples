using ModelViewController_Sample.enumerables;
using ModelViewController_Sample.interfaces;

namespace ModelViewController_Sample
{
	/// <summary>
	/// Summary description for AutomobileControl.
	/// </summary>
	public class AutomobileControl: IVehicleControl
	{
		private IVehicleModel _model;
		private IVehicleView _view;

		public AutomobileControl(IVehicleModel paramModel, IVehicleView paramView)
		{
			_model = paramModel;
			_view = paramView;
		}

		public AutomobileControl()
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
			if(null != _model)
			{
				_model.Accelerate(paramValue);
				CheckState();
			}
		}

		public void RequestDecelerate(int paramValue)
		{
			if(null != _model)
			{
				_model.Decelerate(paramValue);
				CheckState();
			}
		}

		public void RequestTurn(RelativeDirection paramValue)
		{
			_model.Turn(paramValue);
		}

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

		#endregion
	}
}
