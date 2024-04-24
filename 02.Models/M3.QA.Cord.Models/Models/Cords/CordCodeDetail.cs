#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordCodeDetail

    /// <summary>
    /// The Cord Code Detail class.
    /// </summary>
    public class CordCodeDetail
    {
        #region Public Properties

        /// <summary>Gets or set MasterId.</summary>
        public int? MasterId { get; set; }
        /// <summary>Gets or set CustomerName.</summary>
        public string CustomerName { get; set; }
        /// <summary>Gets or set ItemCode.</summary>
        public string ItemCode { get; set; }
        /// <summary>Gets or set UserName.</summary>
        public string UserName { get; set; }
        
        /// <summary>Gets or set CoaNo.</summary>
        public int CoaNo { get; set; }
        /// <summary>Gets or set FMQC.</summary>
        public string FMQC { get; set; }
        /// <summary>Gets Coa Report Code.</summary>
        public string CoaReportCode
        {
            get { return string.Format("{0} ({1})", CoaNo, FMQC); }
            set { }
        }

        /// <summary>Gets or set ProductType.</summary>
        public string ProductType { get; set; }
        /// <summary>Gets or set ProductName.</summary>
        public string ProductName { get; set; }
        /// <summary>Gets or set YarnType.</summary>
        public string YarnType { get; set; }
        /// <summary>Gets or set YarnCode.</summary>
        public string YarnCode { get; set; }

        /// <summary>Gets or set ELongLoadN.</summary>
        public string ELongLoadN { get; set; }
        /// <summary>Gets or set NoTestCH.</summary>
        public int NoTestCH { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CordCodeDetail>> Gets(string customername)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordCodeDetail>> rets = new NDbResult<List<CordCodeDetail>>();

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
            p.Add("@customername", customername);

            try
            {
                var items = cnn.Query<CordCodeDetail>("M_GetCordCodeDetail", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
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
                rets.data = new List<CordCodeDetail>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The CordCodeDetail item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordCodeDetail> Save(CordCodeDetail value, Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordCodeDetail> ret = new NDbResult<CordCodeDetail>();

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

            p.Add("@masterid", value.MasterId);

            p.Add("@cordcode", value.ItemCode);
            p.Add("@customer", value.CustomerName);
            p.Add("@coano", value.CoaNo);
            p.Add("@fmqc", value.FMQC);
            p.Add("@producttype", value.ProductType);
            p.Add("@productname", value.ProductName);
            p.Add("@yarntype", value.YarnType);
            p.Add("@elonglondn", value.ELongLoadN);
            p.Add("@notestch", value.NoTestCH);
            p.Add("@yarncode", value.YarnCode);

            p.Add("@operator", (null != user) ? user.FullName : null);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveReceiveCord", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
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

        #endregion
    }

    #endregion
}
