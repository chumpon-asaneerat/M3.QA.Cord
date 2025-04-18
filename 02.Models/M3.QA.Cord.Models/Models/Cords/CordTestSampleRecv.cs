﻿#region Using

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
    #region CordTestSampleRecv

    /// <summary>
    /// The Cord Test Sample Recv
    /// </summary>
    public class CordTestSampleRecv : NInpc
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Lot No.
        /// </summary>
        public string LotNo 
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () => 
                {
                    // if empty use LotNo but remove last char.
                    if (string.IsNullOrEmpty(ProductionLot))
                    {
                        if (!string.IsNullOrEmpty(value) && value.Length >= 1)
                        {
                            try
                            {
                                ProductionLot = value.Substring(0, value.Length - 1);
                            }
                            catch (Exception) 
                            {
                                ProductionLot = null;
                            }
                        }
                    }
                });
            }
        }
        /// <summary>
        /// Gets or sets Production Lot.
        /// </summary>
        public string ProductionLot 
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                });
            }
        }
        /// <summary>
        /// Gets or sets Report Production Lot
        /// </summary>
        public string ReportProductionLot
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                });
            }
        }

        public int? MasterId { get; set; }

        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }
        public int TotalSP { get; set; }

        public string DIPMC { get; set; }

        public string ReceiveBy { get; set; }
        public DateTime? ReceiveDate { get; set; }

        // for edit spindle.
        public string Customer { get; set; }
        public string ItemCode { get; set; }
        public string ProductType { get; set; }
        public int NoTestCH { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The CordTestSampleRecv item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordTestSampleRecv> Save(CordTestSampleRecv value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTestSampleRecv> ret = new NDbResult<CordTestSampleRecv>();

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

            int totalSP = 0;
            if (value.SP1.HasValue) totalSP++;
            if (value.SP2.HasValue) totalSP++;
            if (value.SP3.HasValue) totalSP++;
            if (value.SP4.HasValue) totalSP++;
            if (value.SP5.HasValue) totalSP++;
            if (value.SP6.HasValue) totalSP++;
            if (value.SP7.HasValue) totalSP++;

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@productlot", value.ProductionLot);

            p.Add("@reportlot", value.ReportProductionLot);

            p.Add("@MasterId", value.MasterId);

            p.Add("@SP1", value.SP1);
            p.Add("@SP2", value.SP2);
            p.Add("@SP3", value.SP3);
            p.Add("@SP4", value.SP4);
            p.Add("@SP5", value.SP5);
            p.Add("@SP6", value.SP6);
            p.Add("@SP7", value.SP7);

            p.Add("@TotalSP", totalSP);

            p.Add("@DIPMC", string.IsNullOrWhiteSpace(value.DIPMC) ? null : value.DIPMC);

            p.Add("@ReceiveBy", value.ReceiveBy);
            p.Add("@ReceiveDate", value.ReceiveDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveReceiveCord", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");

                // Now save receive SP
                if (ret.ErrNum == 0)
                {
                    if (value.SP1.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP1, value.SP1, new int?(), null);
                    }
                    if (value.SP2.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP2, value.SP2, new int?(), null);
                    }
                    if (value.SP3.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP3, value.SP3, new int?(), null);
                    }
                    if (value.SP4.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP4, value.SP4, new int?(), null);
                    }
                    if (value.SP5.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP5, value.SP5, new int?(), null);
                    }
                    if (value.SP6.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP6, value.SP6, new int?(), null);
                    }
                    if (value.SP7.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            value.ReceiveBy, value.ReceiveDate,
                            value.SP7, value.SP7, new int?(), null);
                    }
                }
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

        public static NDbResult<CordTestSampleRecv> Update(CordTestSampleRecv value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTestSampleRecv> ret = new NDbResult<CordTestSampleRecv>();

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

            int totalSP = 0;
            if (value.SP1.HasValue) totalSP++;
            if (value.SP2.HasValue) totalSP++;
            if (value.SP3.HasValue) totalSP++;
            if (value.SP4.HasValue) totalSP++;
            if (value.SP5.HasValue) totalSP++;
            if (value.SP6.HasValue) totalSP++;
            if (value.SP7.HasValue) totalSP++;

            string user = null != ModelCurrent.User ? ModelCurrent.User.FullName : string.Empty;
            DateTime dt = DateTime.Now;

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@productlot", value.ProductionLot);

            p.Add("@MasterId", value.MasterId);

            p.Add("@SP1", value.SP1);
            p.Add("@SP2", value.SP2);
            p.Add("@SP3", value.SP3);
            p.Add("@SP4", value.SP4);
            p.Add("@SP5", value.SP5);
            p.Add("@SP6", value.SP6);
            p.Add("@SP7", value.SP7);

            p.Add("@TotalSP", totalSP);

            p.Add("@editby", user);
            p.Add("@editdate", dt);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_UpdateReceiveCord", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");

                // Now save receive SP
                if (ret.ErrNum == 0)
                {
                    if (value.SP1.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP1, value.SP1, new int?(), null);
                    }
                    if (value.SP2.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP2, value.SP2, new int?(), null);
                    }
                    if (value.SP3.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP3, value.SP3, new int?(), null);
                    }
                    if (value.SP4.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP4, value.SP4, new int?(), null);
                    }
                    if (value.SP5.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP5, value.SP5, new int?(), null);
                    }
                    if (value.SP6.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP6, value.SP6, new int?(), null);
                    }
                    if (value.SP7.HasValue)
                    {
                        Utils.M_SaveReceiveSP.Save(value.LotNo, value.ProductionLot, value.MasterId.Value,
                            user, dt,
                            value.SP7, value.SP7, new int?(), null);
                    }
                }
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
