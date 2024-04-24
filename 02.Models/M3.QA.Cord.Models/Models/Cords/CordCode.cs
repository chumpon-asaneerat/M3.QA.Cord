#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    public class CordCode
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
        public static NDbResult<List<CordCode>> Gets(string customername)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordCode>> rets = new NDbResult<List<CordCode>>();

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
                var items = cnn.Query<CordCode>("M_GetCordCodeListByCustomer", p,
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
                rets.data = new List<CordCode>();
            }

            return rets;
        }

        #endregion
    }
}
