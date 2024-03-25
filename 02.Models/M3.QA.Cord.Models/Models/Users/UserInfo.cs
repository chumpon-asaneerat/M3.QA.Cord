#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using Dapper;
using Newtonsoft.Json;
using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    public enum ActiveStatus
    {
        All = -1,
        Active = 1,
        Inactive = 0
    }

    #region UserInfo

    /// <summary>
    /// The UserInfo class.
    /// </summary>
    public class UserInfo : NInpc
    {
        #region Internal Variables

        private int _UserId = 0;

        private string _FullName = string.Empty;
        private string _UserName = string.Empty;
        private string _Password = string.Empty;

        private int _RoleId = 0;
        private string _RoleName = string.Empty;

        private int _Active = 1;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserInfo() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~UserInfo()
        {

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets UserId.
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value;
                    Raise(() => UserId);
                }
            }
        }
        /// <summary>
        /// Gets or sets Full Name.
        /// </summary>
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (_FullName != value)
                {
                    _FullName = value;
                    Raise(() => FullName);
                }
            }
        }
        /// <summary>
        /// Gets or sets User Name.
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    Raise(() => UserName);
                }
            }
        }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    Raise(() => Password);
                }
            }
        }
        /// <summary>
        /// Gets or sets Active.
        /// </summary>
        public int Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    Raise(() => Active);
                }
            }
        }

        /// <summary>
        /// Gets or sets RoleId.
        /// </summary>
        public int RoleId
        {
            get { return _RoleId; }
            set
            {
                if (_RoleId != value)
                {
                    _RoleId = value;
                    Raise(() => RoleId);
                }
            }
        }
        /// <summary>
        /// Gets or sets Role Name.
        /// </summary>
        public string RoleName
        {
            get { return _RoleName; }
            set
            {
                if (_RoleName != value)
                {
                    _RoleName = value;
                    Raise(() => RoleName);
                }
            }
        }

        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public bool CanReset { get; set; } = false;

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets Users.
        /// </summary>
        /// <param name="fullName">Filter full name.</param>
        /// <param name="userName">Filter user name.</param>
        /// <param name="roleName">Filter role name.</param>
        /// <returns>Returns List of userinfo instance.</returns>
        public static NDbResult<List<UserInfo>> Gets(string fullName = null,
            string userName = null, string roleName = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<UserInfo>> rets = new NDbResult<List<UserInfo>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@FullName", fullName);
            p.Add("@UserName", userName);
            p.Add("@RoleName", roleName);
            p.Add("@RoleId", null);
            p.Add("@Active", 1);

            try
            {
                var data = cnn.Query<UserInfo>("GetUsers", p,
                    commandType: CommandType.StoredProcedure).AsList();
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<UserInfo>();
            }

            return rets;
        }
        /// <summary>
        /// Gets User.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>Returns match userinfo instance.</returns>
        public static NDbResult<UserInfo> Get(string userName, string password)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<UserInfo> rets = new NDbResult<UserInfo>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@UserName", userName);
            p.Add("@Password", password);

            try
            {
                var data = cnn.Query<UserInfo>("GetUser", p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // set to null.
                rets.data = null;
            }

            return rets;
        }
        /// <summary>
        /// Gets User.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="active">The active status.</param>
        /// <returns>Returns match userinfo instance.</returns>
        public static NDbResult<UserInfo> Get(int userId, int? active = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<UserInfo> rets = new NDbResult<UserInfo>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@UserId", userId);
            p.Add("@Active", active);

            try
            {
                var data = cnn.Query<UserInfo>("FindUser", p,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // set to null.
                rets.data = null;
            }

            return rets;
        }
        /// <summary>
        /// Search Users.
        /// </summary>
        /// <returns>Returns List of userinfo instance.</returns>
        public static NDbResult<List<UserInfo>> Search(string search = null, ActiveStatus status = ActiveStatus.All,
            int? currUserId = new int?(), int? currRoleId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<UserInfo>> rets = new NDbResult<List<UserInfo>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            string sSearch = string.IsNullOrEmpty(search) ? "" : search.Trim();

            string query = string.Empty;
            query += "SELECT * " + Environment.NewLine;
            query += "  FROM UserInfoView " + Environment.NewLine;
            query += " WHERE (UPPER(LTRIM(RTRIM(FullName))) LIKE '%" + sSearch.Trim().ToUpper() + "%' " + Environment.NewLine;
            query += "    OR UPPER(LTRIM(RTRIM(UserName))) LIKE '%" + sSearch.Trim().ToUpper() + "%') " + Environment.NewLine;

            if (currRoleId.HasValue && currRoleId.Value == 1 && currUserId.HasValue && currUserId.Value == 1)
            {
                // Special Admin
            }
            else if (currRoleId.HasValue && currRoleId.Value == 1)
            {
                // General Admin
                query += "  AND (RoleId >= 1" + Environment.NewLine;
                /*
                if (currUserId.HasValue)
                {
                    query += " OR UserId = " + currUserId.Value.ToString() + Environment.NewLine;
                }
                */
                query += ") " + Environment.NewLine;
            }
            else if (currRoleId.HasValue && currRoleId.Value == 10)
            {
                // Supervisor
                query += "  AND (RoleId >= 10" + Environment.NewLine;
                /*
                if (currUserId.HasValue)
                {
                    query += " OR UserId = " + currUserId.Value.ToString() + Environment.NewLine;
                }
                */
                query += ") " + Environment.NewLine;
            }
            else
            {
                // User
                query += "  AND (RoleId >= 20" + Environment.NewLine;
                /*
                if (currUserId.HasValue)
                {
                    query += " OR UserId = " + currUserId.Value.ToString() + Environment.NewLine;
                }
                */
                query += ") " + Environment.NewLine;
            }

            // Active Status
            if (status == ActiveStatus.Active)
                query += "  AND Active = 1" + Environment.NewLine;
            else if (status == ActiveStatus.Inactive)
                query += "  AND Active = 0" + Environment.NewLine;

            query += " ORDER BY RoleId, UserName " + Environment.NewLine;

            var p = new DynamicParameters();

            try
            {
                var data = cnn.Query<UserInfo>(query, p,
                    commandType: CommandType.Text).AsList();

                if (null != data && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        if (item == null) continue;

                        // Common Logic Check Role if role is less than current user so cannot edit/delete
                        item.CanEdit = false;
                        item.CanDelete = false;
                        item.CanReset = false;

                        // Recheck same role cannot edit/delete
                        if (item.RoleId == 1 && item.UserId == 1 && currUserId.HasValue && currUserId.Value == 1)
                        {
                            if (currRoleId.HasValue && currRoleId.Value == item.RoleId)
                            {
                                // Special Admin
                                item.CanEdit = true;
                                item.CanDelete = false;
                                item.CanReset = false;

                                if (currUserId.HasValue && currUserId.Value == 1)
                                {
                                    item.CanReset = true;
                                    continue; // skip
                                }
                            }
                        }
                        else if (item.RoleId == 1)
                        {
                            // General Admin
                            item.CanDelete = false;
                            item.CanEdit = false;
                            item.CanReset = false;

                            if (currRoleId.HasValue) // Has Role
                            {
                                if (currRoleId.Value == item.RoleId)
                                {
                                    // Same Role and Same User Id
                                    if (currUserId.HasValue &&
                                        (currUserId.Value == item.UserId ||
                                         currUserId.Value == 1))
                                    {
                                        item.CanEdit = true; // current user is admin so can edit this item
                                        item.CanDelete = currUserId.Value == 1; // special admin allow to delete
                                        item.CanReset = currUserId.Value == 1; // special admin allow to reset
                                    }
                                }
                                else if (currRoleId.Value < item.RoleId)
                                {
                                    // All User that has less Role right allow to edit/delete
                                    item.CanEdit = true;
                                    item.CanDelete = true;
                                    item.CanReset = true;
                                }
                            }
                        }
                        else if (item.RoleId >= 10)
                        {
                            // Supervisor
                            item.CanDelete = false;
                            item.CanEdit = false;
                            item.CanReset = false;

                            if (currRoleId.HasValue) // Has Role
                            {
                                if (currRoleId.Value == item.RoleId)
                                {
                                    // Same Role and Same User Id
                                    if (currUserId.HasValue && currUserId.Value == item.UserId)
                                    {
                                        item.CanEdit = true;
                                        item.CanDelete = false;
                                        item.CanReset = true;
                                    }
                                }
                                else if (currRoleId.Value < item.RoleId)
                                {
                                    // All User that has less Role right allow to edit/delete
                                    item.CanEdit = true;
                                    item.CanDelete = true;
                                    item.CanReset = true;
                                }
                            }
                        }
                        else //if (item.RoleId == 20)
                        {
                            // User
                            item.CanDelete = false;
                            item.CanEdit = false;
                            item.CanReset = false;

                            if (currRoleId.HasValue) // Has Role
                            {
                                if (currRoleId.Value == item.RoleId)
                                {
                                    // Same Role and Same User Id
                                    if (currUserId.HasValue && currUserId.Value == item.UserId)
                                    {
                                        item.CanEdit = true;
                                        item.CanDelete = false;
                                        item.CanReset = true;
                                    }
                                }
                                else if (currRoleId.Value < item.RoleId)
                                {
                                    // All User that has less Role right allow to edit/delete
                                    item.CanEdit = true;
                                    item.CanDelete = true;
                                    item.CanReset = true;
                                }
                            }
                        }
                    }
                }

                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<UserInfo>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The UserInfo item to save.</param>
        /// <returns></returns>
        public static NDbResult<UserInfo> Save(UserInfo value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<UserInfo> ret = new NDbResult<UserInfo>();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@RoleId", value.RoleId);
            p.Add("@FullName", value.FullName);
            p.Add("@UserName", value.UserName);
            p.Add("@Password", value.Password);
            p.Add("@UserId", value.UserId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveUser", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set PK
                value.UserId = p.Get<int>("@UserId");
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult Delete(UserInfo value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@UserId", value.UserId);

            try
            {
                cnn.Execute("UPDATE UserInfo SET Active = 0 WHERE UserId = @UserId", p, commandType: CommandType.Text);
                ret.Success();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult Reset(UserInfo value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();
            p.Add("@UserId", value.UserId);

            try
            {
                cnn.Execute("UPDATE UserInfo SET Password = UserName WHERE UserId = @UserId", p, commandType: CommandType.Text);
                ret.Success();
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        #endregion
    }

    #endregion
}
