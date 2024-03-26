#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;

#endregion

namespace M3.QA.Models
{
    public class CordCode
    {
        #region Public Properties

        public int MasterId { get; set; }
        public string Customer { get; set; }
        public string ItemCode { get; set; }
        public string UserName { get; set; }
        public int CoaNo { get; set; }
        public string FMQC { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string YarnType { get; set; }

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
