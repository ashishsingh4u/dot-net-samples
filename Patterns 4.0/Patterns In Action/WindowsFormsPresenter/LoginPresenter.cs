using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using WindowsFormsModel;
using WindowsFormsView;

namespace WindowsFormsPresenter
{
    /// <summary>
    /// Login Presenter class.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MVP design pattern.
    /// </remarks>
    public class LoginPresenter : Presenter<ILoginView>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="view">The view</param>
        public LoginPresenter(ILoginView view)
            : base(view)
        {
        }

        /// <summary>
        /// Perform login. Gets data from view and calls model.
        /// </summary>
        public void Login()
        {
            string username = View.UserName;
            string password = View.Password;

            Model.Login(username, password);
        }
    }
}
