#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region Utils class

    public static class Utils
    {
        #region M_GetPropertyTotalNByItem

        public class M_GetPropertyTotalNByItem
        {
            #region Public Properties

            public int MasterId { get; set; }
            public int PropertyNo { get; set; }
            public int NoSample { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<M_GetPropertyTotalNByItem>> Gets(int masterId)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<M_GetPropertyTotalNByItem>> ret = new NDbResult<List<M_GetPropertyTotalNByItem>>();

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

                p.Add("@MasterId", masterId);
                p.Add("@PropertyNo", null);

                try
                {
                    var items = cnn.Query<M_GetPropertyTotalNByItem>("M_GetPropertyTotalNByItem", p, commandType: CommandType.StoredProcedure);
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

            public static NDbResult<M_GetPropertyTotalNByItem> GetByPropertyNo(int masterId, int propertyNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<M_GetPropertyTotalNByItem> ret = new NDbResult<M_GetPropertyTotalNByItem>();

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

                p.Add("@MasterId", masterId);
                p.Add("@PropertyNo", propertyNo);

                try
                {
                    var items = cnn.Query<M_GetPropertyTotalNByItem>("M_GetPropertyTotalNByItem", p, commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList().FirstOrDefault() : null;

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

        #endregion

        #region P_GetAdhesionForceByLot

        public class P_GetAdhesionForceByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SPNo { get; set; }
            public decimal? PeakN1 { get; set; }
            public decimal? PeakN2 { get; set; }
            public decimal? PeakR1 { get; set; }
            public decimal? PeakR2 { get; set; }

            public decimal? AdhesionN1 { get; set; }
            public decimal? AdhesionN2 { get; set; }
            public decimal? AdhesionR1 { get; set; }
            public decimal? AdhesionR2 { get; set; }

            public string InputBy { get; set; }
            public DateTime? InputDate { get; set; }

            public string EditBy { get; set; }
            public DateTime? EditDate { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetAdhesionForceByLot>> GetByLot(string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetAdhesionForceByLot>> ret = new NDbResult<List<P_GetAdhesionForceByLot>>();

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

                if (string.IsNullOrEmpty(lotNo))
                {
                    ret.ParameterIsNull();
                    return ret;
                }

                var p = new DynamicParameters();

                p.Add("@lotNo", lotNo);

                try
                {
                    var items = cnn.Query<P_GetAdhesionForceByLot>("P_GetAdhesionForceByLot", p, commandType: CommandType.StoredProcedure);
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

        #endregion

        #region P_SearchReceiveCord

        public class P_SearchReceiveCord
        {
            #region Public Properties

            public string LotNo { get; set; }
            public string ItemCode { get; set; }
            public int TotalSP { get; set; }
            public string SpindleNo { get; set; }
            public DateTime? ReceiveDate { get; set; }
            public string ReceiveBy { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_SearchReceiveCord>> SearchByDate(
                DateTime? dateFrom = new DateTime?(),
                DateTime? dateTo = new DateTime?())
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_SearchReceiveCord>> ret = new NDbResult<List<P_SearchReceiveCord>>();

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

                p.Add("@dateform", dateFrom);
                p.Add("@dateto", dateTo);

                try
                {
                    var items = cnn.Query<P_SearchReceiveCord>("P_SearchReceiveCord", p, commandType: CommandType.StoredProcedure);
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

        #endregion
    }

    #endregion
}
