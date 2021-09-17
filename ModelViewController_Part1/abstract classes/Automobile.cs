using System.Collections;
using ModelViewController_Sample.enumerables;
using ModelViewController_Sample.interfaces;

namespace ModelViewController_Sample
{
	/// <summary>
	/// Summary description for IAutomobile.
	/// </summary>
//	public abstract class Automobile: IVehicleModel
//	{
//		#region "Declarations "
//		private ArrayList aList = new ArrayList();
//
//		private int mintSpeed = 0;
//		private int mintMaxSpeed = 0;
//		private int mintMaxTurnSpeed = 0;
//		private int mintMaxReverseSpeed = 0;
//		private AbsoluteDirection mDirection = AbsoluteDirection.North;
//		private string mstrName = "";
//		#endregion
//
//		#region "Constructor"
//
//		public Automobile(int paramMaxSpeed, int paramMaxTurnSpeed, int paramMaxReverseSpeed, string paramName)
//		{
//			this.mintMaxSpeed = paramMaxSpeed;
//			this.mintMaxTurnSpeed = paramMaxTurnSpeed;
//			this.mintMaxReverseSpeed = paramMaxReverseSpeed;
//			this.mstrName = paramName;
//		}
//
//
//		#endregion
//
//		#region "IVehicleModel Members"
//
//		public void AddObserver(IVehicleView paramView)
//		{
//			aList.Add(paramView);
//		}
//
//		public void RemoveObserver(IVehicleView paramView)
//		{
//			aList.Remove(paramView);
//		}
//
//		public void NotifyObservers()
//		{
//			foreach(IVehicleView view in aList)
//			{
//				view.Update(this);
//			}
//		}
//
//		public string Name
//		{
//			get
//			{
//				return this.mstrName;
//			}
//			set
//			{
//				this.mstrName = value;
//			}
//		}
//
//		public int Speed
//		{
//			get
//			{
//				return this.mintSpeed;
//			}
//			set
//			{
//				this.mintSpeed = value;
//				this.NotifyObservers();
//			}
//		}
//
//		public int MaxSpeed
//		{
//			get
//			{
//				return this.mintMaxSpeed;
//			}
//		}
//
//		public int MaxTurnSpeed
//		{
//			get
//			{
//				return this.mintMaxTurnSpeed;
//			}
//		}
//
//		public int MaxReverseSpeed
//		{
//			get
//			{
//				return this.mintMaxReverseSpeed;
//			}
//		}
//
//		public AbsoluteDirection Direction
//		{
//			get
//			{
//				return this.mDirection;
//			}
//			set
//			{
//				this.mDirection = value;
//				this.NotifyObservers();
//			}
//		}
//
//		public void Execute(IVehicleCommand paramCommand)
//		{
//			paramCommand.Execute(this);
//			this.NotifyObservers();
//		}
//
//
//
//		#endregion
//	}

	public abstract class Automobile: IVehicleModel
	{
		#region "Declarations "
		private readonly ArrayList _aList = new ArrayList();
		private int _mintSpeed;
		private readonly int _mintMaxSpeed;
		private readonly int _mintMaxTurnSpeed;
		private readonly int _mintMaxReverseSpeed;
		private AbsoluteDirection _mDirection = AbsoluteDirection.North;
		private string _mstrName = "";
		#endregion

		#region "Constructor"

	    protected Automobile(int paramMaxSpeed, int paramMaxTurnSpeed, int paramMaxReverseSpeed, string paramName)
		{
			_mintMaxSpeed = paramMaxSpeed;
			_mintMaxTurnSpeed = paramMaxTurnSpeed;
			_mintMaxReverseSpeed = paramMaxReverseSpeed;
			_mstrName = paramName;
		}


		#endregion

		#region "IVehicleModel Members"

		public void AddObserver(IVehicleView paramView)
		{
			_aList.Add(paramView);
		}

		public void RemoveObserver(IVehicleView paramView)
		{
			_aList.Remove(paramView);
		}

		public void NotifyObservers()
		{
			foreach(IVehicleView view in _aList)
			{
				view.Update(this);
			}
		}

		public string Name
		{
			get
			{
				return _mstrName;
			}
			set
			{
				_mstrName = value;
			}
		}

		public int Speed
		{
			get
			{
				return _mintSpeed;
			}
		}

		public int MaxSpeed
		{
			get
			{
				return _mintMaxSpeed;
			}
		}

		public int MaxTurnSpeed
		{
			get
			{
				return _mintMaxTurnSpeed;
			}
		}

		public int MaxReverseSpeed
		{
			get
			{
				return _mintMaxReverseSpeed;
			}
		}

		public AbsoluteDirection Direction
		{
			get
			{
				return _mDirection;
			}
		}

		public void Turn(RelativeDirection paramDirection)
		{
			AbsoluteDirection newDirection;
			
			switch(paramDirection)
			{
				case RelativeDirection.Right:
					newDirection = (AbsoluteDirection)((int)(_mDirection + 1) %4);
					break;
				case RelativeDirection.Left:
					newDirection = (AbsoluteDirection)((int)(_mDirection + 3) %4);
					break;
				case RelativeDirection.Back:
					newDirection = (AbsoluteDirection)((int)(_mDirection + 2) %4);
					break;
				default:
					newDirection = AbsoluteDirection.North;
					break;
			}

			_mDirection = newDirection;

			NotifyObservers();
		}

		public void Accelerate(int paramAmount)
		{

			_mintSpeed += paramAmount;
			if(_mintSpeed >= _mintMaxSpeed) _mintSpeed = _mintMaxSpeed;

			NotifyObservers();
		}

		public void Decelerate(int paramAmount)
		{

			_mintSpeed -= paramAmount;
			if(_mintSpeed <= _mintMaxReverseSpeed) _mintSpeed = _mintMaxReverseSpeed;

			NotifyObservers();
		}


		#endregion


	}

}
