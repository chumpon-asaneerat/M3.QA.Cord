#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;

using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    public class DIPSolitionPHTestData : NInpc
    {
        #region Public Properties

        #region Common

        public string ItemCode { get; set; }
        public string LotNo { get; set; }
        public int? MasterId { get; set; }
        public string Compounds { get; set; }

        public string TestType { get; set; }
        public string LinkType { get; set; }

        public string SendBy { get; set; }
        public DateTime? SendDate { get; set; }

        public DateTime? ForecastFinishDate { get; set; }
        public DateTime? ValidDate { get; set; }

        public DateTime? InputDate { get; set; }
        public string InputBy { get; set; }

        public DateTime? EditDate { get; set; }
        public string EditBy { get; set; }

        #endregion

        #region Test Properties

        public decimal Ph 
        {
            get { return Get<decimal>(); }
            set 
            { 
                Set(value, () => { }); 
            }
        }
        public decimal Temperature
        {
            get { return Get<decimal>(); }
            set
            {
                Set(value, () => { });
            }
        }

        #endregion

        #endregion

        #region Static Methods

        #region Save

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static NDbResult<DIPSolitionPHTestData> Save(DIPSolitionPHTestData value,
            QA.Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPSolitionPHTestData> ret = new NDbResult<DIPSolitionPHTestData>();

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

            string compound;
            if (string.IsNullOrEmpty(value.Compounds))
            {
                compound = null;
            }
            else
            {
                compound = value.Compounds.Contains("Final") ? "Final" : "RF";
            }

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@masterid", value.MasterId);
            p.Add("@compound", compound);
            p.Add("@testtype", value.TestType);
            p.Add("@ph", value.Ph);
            p.Add("@temperature", value.Temperature);
            p.Add("@linktype", value.LinkType);

            p.Add("@user", (null != user) ? user.FullName : null);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("Plc_SavePhMeter", p, commandType: CommandType.StoredProcedure);
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

        #endregion
    }
}
