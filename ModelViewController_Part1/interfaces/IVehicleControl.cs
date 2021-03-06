using ModelViewController_Sample.enumerables;

namespace ModelViewController_Sample.interfaces
{
	/// <summary>
	/// Summary description for IVehicleControl.
	/// </summary>
	public interface IVehicleControl
	{
		#region "Methods"

		void SetModel(IVehicleModel paramAuto);
		void SetView(IVehicleView paramView);

		void RequestAccelerate(int paramAmount);
		void RequestDecelerate(int paramAmount);
		void RequestTurn(RelativeDirection paramDirection); 


		#endregion
	}


}
