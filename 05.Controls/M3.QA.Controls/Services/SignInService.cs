#region Using

using NLib;
using NLib.Models;
using NLib.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
using M3.QA.Models;
//using M3.Services;

#endregion

namespace M3.QA
{
    #region SignInStatus Enum

    /// <summary>
    /// SignInStatus Enum
    /// </summary>
    public enum SignInStatus
    {
        /// <summary>Success.</summary>
        Success = 0,
        /// <summary>UserName Not Found.</summary>
        UserNotFound = 1,
        /// <summary>Invalid Password.</summary>
        InvalidPassword = 2
    }

    #endregion

    #region SignInManager

    /// <summary>
    /// SignInManager class.
    /// </summary>
    public class SignInManager : NSingelton<SignInManager>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SignInManager() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~SignInManager()
        {
            Signout();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sign In.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>Returns SignIn Status.</returns>
        public SignInStatus SignIn(string userName, string password)
        {
            UserInfo oUser = default;
            var Users = UserInfo.Gets(userName: userName).Value();
            if (null != Users && Users.Count > 0)
            {
                oUser = Users[0];
            }

            if (null == oUser)
            {
                // user not exists.
                return SignInStatus.UserNotFound;
            }

            User = UserInfo.Get(userName, password).Value();

            if (null != User)
            {
                // Raise Event.
                UserChanged.Call(this, EventArgs.Empty);
                return SignInStatus.Success;
            }
            else
            {
                return SignInStatus.InvalidPassword;
            }
        }
        /// <summary>
        /// Signout.
        /// </summary>
        public void Signout()
        {
            this.User = null;
            // Raise Event.
            UserChanged.Call(this, EventArgs.Empty);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets current user.
        /// </summary>
        public UserInfo User { get; private set; }

        #endregion

        #region Public Events

        /// <summary>
        /// UserChanged event.
        /// </summary>
        public event EventHandler UserChanged;

        #endregion
    }

    #endregion

    #region VerifyManager

    /// <summary>
    /// VerifyManager class.
    /// </summary>
    public class VerifyManager : NSingelton<VerifyManager>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected VerifyManager() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~VerifyManager()
        {
            
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Vefify.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>Returns SignIn Status.</returns>
        public SignInStatus Verify(string userName, string password)
        {
            User = null; // reset

            UserInfo oUser = default;
            var Users = UserInfo.Gets(userName: userName).Value();
            if (null != Users && Users.Count > 0)
            {
                oUser = Users[0];
            }

            if (null == oUser)
            {
                // user not exists.
                return SignInStatus.UserNotFound;
            }

            User = UserInfo.Get(userName, password).Value();

            if (null != User)
            {
                return SignInStatus.Success;
            }
            else
            {
                return SignInStatus.InvalidPassword;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets current user.
        /// </summary>
        public UserInfo User { get; private set; }

        #endregion
    }

    #endregion
}
