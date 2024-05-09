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
    /// <summary>
    /// The Cord Production class.
    /// </summary>
    public class CordProduction
    {
        #region Public Properties

        public int? MasterId { get; set; }
        public string LotNo { get; set; }
        public string ItemCode { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string YarnCode { get; set; }
        public decimal PiNoSL { get; set; }
        public string SpindleNo { get; set; }
        public DateTime? InputDate { get; set; }
        public string InputTestBy { get; set; }

        public int? CoaNo { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<CordProduction>> Gets(
            string lotNo, DateTime? dateform, DateTime? dateto)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordProduction>> ret = new NDbResult<List<CordProduction>>();

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

            p.Add("@lotno", string.IsNullOrWhiteSpace(lotNo) ? null : lotNo);
            p.Add("@dateform", dateform.HasValue ? dateform.Value.Date : new DateTime?());
            p.Add("@dateto", dateto.HasValue ? dateto.Value.Date : new DateTime?());

            try
            {
                var items = cnn.Query<CordProduction>("P_SearchTestCordProduction", p, commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

                ret.Success(data);
                // Set error number/message
                ret.ErrNum = 0;
                ret.ErrMsg = "Success";
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
}
