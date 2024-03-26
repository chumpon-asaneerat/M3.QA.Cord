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
    public class DIPSolutionSampleRecv : NInpc
    {
        #region Public Properties

        public string LotNo { get; set; }
        public int? MasterId { get; set; }

        public string Compound { get; set; }

        public string SendBy 
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () => 
                {
                    Raise(() => this.SendBy);
                });
            }

        }
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? ForecastFinishDate { get; set; }

        public string SaveBy { get; set; }
        public DateTime? SaveDate { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The DIPSolutionSampleRecv item to save.</param>
        /// <returns></returns>
        public static NDbResult<DIPSolutionSampleRecv> Save(DIPSolutionSampleRecv value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPSolutionSampleRecv> ret = new NDbResult<DIPSolutionSampleRecv>();

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

            p.Add("@LotNo", value.LotNo);
            p.Add("@MasterId", value.MasterId);

            p.Add("@Compound", value.Compound);

            p.Add("@SendBy", value.SendBy);
            p.Add("@SendDate", value.SendDate);

            p.Add("@ReceiveDate", value.ReceiveDate);
            p.Add("@ForecastFinishDate", value.ForecastFinishDate);

            p.Add("@SaveBy", value.SaveBy);
            p.Add("@SaveDate", value.SaveDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveReceiveSolution", p, commandType: CommandType.StoredProcedure);
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
}
