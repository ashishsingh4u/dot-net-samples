namespace ModelViewController_Sample.interfaces
{
	/// <summary>
	/// Summary description for IAutomobileView.
	/// </summary>
	public interface IVehicleView
	{
		#region "Methods"

		void DisableAcceleration();
		void EnableAcceleration();
		void DisableDeceleration();
		void EnableDeceleration();
		void DisableTurning();
		void EnableTurning();

		#endregion

		#region "Observer Pattern Implementation"
		void Update(IVehicleModel paramModel);
		#endregion
	}
}
