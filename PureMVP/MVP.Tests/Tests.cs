using System;
using Library;
using MVP;
using NUnit.Framework;

namespace Tests
{
	
	[TestFixture]
	public class Tests
	{
		
		private class StubView : IUserView
		{
			private string email;
			private string userName;

			public event EventHandler DataChanged;
			public event EventHandler Save;

			public string UserName
			{
				get { return userName; }
				set { userName = value; }
			}

			public string Email
			{
				get { return email; }
				set { email = value; }
			}

			public void Show()
			{
				
			}

			public void FireSaveEvent()
			{
				if (Save != null)
				{
					Save(null, EventArgs.Empty);
				}
			}
			
			public void FireDataChanged()
			{
				if (DataChanged != null)
				{
					DataChanged(null, EventArgs.Empty);
				}
			}
		}
		
		private string _userName = "John Jones";
		
		private string _email = "John.Jones@abc.com";
		
		private IUserModel _mock;
		
		[TestFixtureSetUp] 
		public void TestFixtureSetUp()
		{
			_mock = GetMockModel();
		}
		
		private IUserModel GetMockModel()
		{
			IUserModel mockModel = new UserModel();
			
			mockModel.UserName = _userName;
			
			mockModel.Email = _email;
			
			return mockModel;
			
		}
		
		[Test]
		public void DoesViewCorrespondToModel()
		{
			StubView stub = new StubView();
			
			new UserPresenter(_mock, stub);
			
			Assert.AreEqual(stub.UserName,_mock.UserName);
			
			Assert.AreEqual(stub.Email,_mock.Email);
			
		}
		
		[Test]
		public void WhenWeChangeTheViewDoesTheModelUpdate()
		{
			StubView stub = new StubView();
			
			new UserPresenter(_mock, stub);
			
			string newUser = "John Smith";
			
			stub.UserName = newUser;
			
			stub.FireDataChanged();
			
			Assert.AreEqual(newUser,_mock.UserName);
			
		}
		
		
		
	}
}
