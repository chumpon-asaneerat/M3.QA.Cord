﻿#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;
using static M3.QA.Models.Utils;

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
            public decimal? PeakN1R1 { get; set; }
            public decimal? PeakN1R2 { get; set; }
            public decimal? PeakN2R1 { get; set; }
            public decimal? PeakN2R2 { get; set; }

            public decimal? AdhesionN1 { get; set; }
            public decimal? AdhesionN2 { get; set; }
            public decimal? AdhesionN1R1 { get; set; }
            public decimal? AdhesionN1R2 { get; set; }
            public decimal? AdhesionN2R1 { get; set; }
            public decimal? AdhesionN2R2 { get; set; }

            public bool? AdhesionN1R1Flag { get; set; }
            public bool? AdhesionN1R2Flag { get; set; }
            public bool? AdhesionN2R1Flag { get; set; }
            public bool? AdhesionN2R2Flag { get; set; }

            public string SampleType { get; set; }

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

        #region P_GetTwistNumberByLot

        public class P_GetTwistNumberByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int PropertyNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? N1 { get; set; }
            public decimal? N2 { get; set; }
            public decimal? N3 { get; set; }

            public decimal? N1R1 { get; set; }
            public decimal? N1R2 { get; set; }
            public decimal? N2R1 { get; set; }
            public decimal? N2R2 { get; set; }
            public decimal? N3R1 { get; set; }
            public decimal? N3R2 { get; set; }

            public decimal? TMN1 { get; set; }
            public decimal? TMN2 { get; set; }
            public decimal? TMN3 { get; set; }

            public decimal? TMN1R1 { get; set; }
            public decimal? TMN1R2 { get; set; }
            public decimal? TMN2R1 { get; set; }
            public decimal? TMN2R2 { get; set; }
            public decimal? TMN3R1 { get; set; }
            public decimal? TMN3R2 { get; set; }

            public bool? TMN1R1Flag { get; set; }
            public bool? TMN1R2Flag { get; set; }
            public bool? TMN2R1Flag { get; set; }
            public bool? TMN2R2Flag { get; set; }
            public bool? TMN3R1Flag { get; set; }
            public bool? TMN3R2Flag { get; set; }

            public decimal? TCMN1 { get; set; }
            public decimal? TCMN2 { get; set; }
            public decimal? TCMN3 { get; set; }
            
            public decimal? TCMN1R1 { get; set; }
            public decimal? TCMN1R2 { get; set; }
            public decimal? TCMN2R1 { get; set; }
            public decimal? TCMN2R2 { get; set; }
            public decimal? TCMN3R1 { get; set; }
            public decimal? TCMN3R2 { get; set; }

            public bool? TCMN1R1Flag { get; set; }
            public bool? TCMN1R2Flag { get; set; }
            public bool? TCMN2R1Flag { get; set; }
            public bool? TCMN2R2Flag { get; set; }
            public bool? TCMN3R1Flag { get; set; }
            public bool? TCMN3R2Flag { get; set; }

            public string SampleType { get; set; }
            public string InputBy { get; set; }
            public DateTime? InputDate { get; set; }

            public string EditBy { get; set; }
            public DateTime? EditDate { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetTwistNumberByLot>> GetByLot(string lotNo, int propertyNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetTwistNumberByLot>> ret = new NDbResult<List<P_GetTwistNumberByLot>>();

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
                p.Add("@propertyno", propertyNo);

                try
                {
                    var items = cnn.Query<P_GetTwistNumberByLot>("P_GetTwistNumberByLot", p, commandType: CommandType.StoredProcedure);
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

        #region P_GetRPUByLot

        public class P_GetRPUByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? BFN1 { get; set; }
            public decimal? BFN1R1 { get; set; }
            public decimal? BFN1R2 { get; set; }

            public decimal? AFN1 { get; set; }
            public decimal? AFN1R1 { get; set; }
            public decimal? AFN1R2 { get; set; }

            public decimal? RPUN1 { get; set; }
            public decimal? RPUN1R1 { get; set; }
            public decimal? RPUN1R2 { get; set; }

            public bool? RPUN1R1Flag { get; set; }
            public bool? RPUN1R2Flag { get; set; }

            public string SampleType { get; set; }

            public string InputBy { get; set; }
            public DateTime? InputDate { get; set; }

            public string EditBy { get; set; }
            public DateTime? EditDate { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetRPUByLot>> GetByLot(string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetRPUByLot>> ret = new NDbResult<List<P_GetRPUByLot>>();

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
                    var items = cnn.Query<P_GetRPUByLot>("P_GetRPUByLot", p, commandType: CommandType.StoredProcedure);
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

        #region P_GetShrinkageByLot

        public class P_GetShrinkageByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? LBHN1 { get; set; }
            public decimal? LBHN2 { get; set; }
            public decimal? LBHN3 { get; set; }

            public decimal? LBHN1R1 { get; set; }
            public decimal? LBHN1R2 { get; set; }
            public decimal? LBHN2R1 { get; set; }
            public decimal? LBHN2R2 { get; set; }
            public decimal? LBHN3R1 { get; set; }
            public decimal? LBHN3R2 { get; set; }
            public bool? LBHN1R1Flag { get; set; }
            public bool? LBHN1R2Flag { get; set; }
            public bool? LBHN2R1Flag { get; set; }
            public bool? LBHN2R2Flag { get; set; }
            public bool? LBHN3R1Flag { get; set; }
            public bool? LBHN3R2Flag { get; set; }

            public decimal? LAHN1 { get; set; }
            public decimal? LAHN2 { get; set; }
            public decimal? LAHN3 { get; set; }

            public decimal? LAHN1R1 { get; set; }
            public decimal? LAHN1R2 { get; set; }
            public decimal? LAHN2R1 { get; set; }
            public decimal? LAHN2R2 { get; set; }
            public decimal? LAHN3R1 { get; set; }
            public decimal? LAHN3R2 { get; set; }
            public bool? LAHN1R1Flag { get; set; }
            public bool? LAHN1R2Flag { get; set; }
            public bool? LAHN2R1Flag { get; set; }
            public bool? LAHN2R2Flag { get; set; }
            public bool? LAHN3R1Flag { get; set; }
            public bool? LAHN3R2Flag { get; set; }

            public decimal? SKN1 { get; set; }
            public decimal? SKN2 { get; set; }
            public decimal? SKN3 { get; set; }

            public decimal? SKN1R1 { get; set; }
            public decimal? SKN1R2 { get; set; }
            public decimal? SKN2R1 { get; set; }
            public decimal? SKN2R2 { get; set; }
            public decimal? SKN3R1 { get; set; }
            public decimal? SKN3R2 { get; set; }

            public bool? SKN1R1Flag { get; set; }
            public bool? SKN1R2Flag { get; set; }
            public bool? SKN2R1Flag { get; set; }
            public bool? SKN2R2Flag { get; set; }
            public bool? SKN3R1Flag { get; set; }
            public bool? SKN3R2Flag { get; set; }

            public string SampleType { get; set; }

            public string InputBy { get; set; }
            public DateTime? InputDate { get; set; }

            public string EditBy { get; set; }
            public DateTime? EditDate { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetShrinkageByLot>> GetByLot(string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetShrinkageByLot>> ret = new NDbResult<List<P_GetShrinkageByLot>>();

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
                    var items = cnn.Query<P_GetShrinkageByLot>("P_GetShrinkageByLot", p, commandType: CommandType.StoredProcedure);
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

        #region P_GetDenierMoistureWLot

        public class P_GetDenierMoistureWLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? YWBDN1 { get; set; }
            public decimal? YWBDN1R1 { get; set; }
            public decimal? YWBDN1R2 { get; set; }

            public decimal? CWN1 { get; set; }
            public decimal? CWN1R1 { get; set; }
            public decimal? CWN1R2 { get; set; }

            public decimal? YCWADN1 { get; set; }
            public decimal? YCWADN1R1 { get; set; }
            public decimal? YCWADN1R2 { get; set; }

            public decimal? YWADN1 { get; set; }
            public decimal? YWADN1R1 { get; set; }
            public decimal? YWADN1R2 { get; set; }

            public decimal? DENIER_D_N1 { get; set; }
            public decimal? DENIER_D_N1R1 { get; set; }
            public decimal? DENIER_D_N1R2 { get; set; }
            public bool? DENIER_D_N1R1Flag { get; set; }
            public bool? DENIER_D_N1R2Flag { get; set; }

            public decimal? DENIER_Dtex_N1 { get; set; }
            public decimal? DENIER_Dtex_N1R1 { get; set; }
            public decimal? DENIER_Dtex_N1R2 { get; set; }
            public bool? DENIER_Dtex_N1R1Flag { get; set; }
            public bool? DENIER_Dtex_N1R2Flag { get; set; }

            public decimal? MOISTURE_N1 { get; set; }
            public decimal? MOISTURE_N1R1 { get; set; }
            public decimal? MOISTURE_N1R2 { get; set; }
            public bool? MOISTURE_N1R1Flag { get; set; }
            public bool? MOISTURE_N1R2Flag { get; set; }

            public decimal? WEIGHT_N1 { get; set; }
            public decimal? WEIGHT_N1R1 { get; set; }
            public decimal? WEIGHT_N1R2 { get; set; }
            public bool? WEIGHT_N1R1Flag { get; set; }
            public bool? WEIGHT_N1R2Flag { get; set; }

            public string SampleType { get; set; }

            public string InputBy { get; set; }
            public DateTime? InputDate { get; set; }

            public string EditBy { get; set; }
            public DateTime? EditDate { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetDenierMoistureWLot>> GetByLot(string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetDenierMoistureWLot>> ret = new NDbResult<List<P_GetDenierMoistureWLot>>();

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
                    var items = cnn.Query<P_GetDenierMoistureWLot>("P_GetDenierMoistureWLot", p, commandType: CommandType.StoredProcedure);
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

        #region P_SearchReceiveSolution

        public class P_SearchReceiveSolution
        {
            #region Public Properties

            public string LotNo { get; set; }
            public string ItemCode { get; set; }
            public string Compound { get; set; }
            public DateTime? ReceiveDate { get; set; }
            public string ReceiveBy { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_SearchReceiveSolution>> SearchByDate(
                DateTime? dateFrom = new DateTime?(),
                DateTime? dateTo = new DateTime?())
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_SearchReceiveSolution>> ret = new NDbResult<List<P_SearchReceiveSolution>>();

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
                    var items = cnn.Query<P_SearchReceiveSolution>("P_SearchReceiveSolution", p, commandType: CommandType.StoredProcedure);
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

        #region M_CheckSolutionLotReceive

        public class M_CheckSolutionLotReceive
        {
            #region Public Properties

            public string ItemCode { get; set; }
            public string LotNo { get; set; }
            public int? MasterId { get; set; }
            public string Compound { get; set; }
            public string SendBy { get; set; }
            public DateTime? SendDate { get; set; }
            public DateTime? ForecastFinishDate { get; set; }
            public DateTime? ValidDate { get; set; }

            public DateTime? SaveDate { get; set; }
            public string SaveBy { get; set; }

            public decimal? PhN { get; set; }
            public decimal? PhR { get; set; }

            public decimal? TempturatureN { get; set; }
            public decimal? TempturatureR { get; set; }

            public decimal? ViscosityN { get; set; }
            public decimal? ViscosityR { get; set; }


            public decimal? TSCN1 { get; set; }
            public decimal? TSCN2 { get; set; }
            public decimal? TSCR1 { get; set; }
            public decimal? TSCR3 { get; set; }

            public decimal? BeakerWN1 { get; set; }
            public decimal? BeakerWN2 { get; set; }
            public decimal? BeakerWR1 { get; set; }
            public decimal? BeakerWR2 { get; set; }

            public decimal? BeakerW_BHN1 { get; set; }
            public decimal? BeakerW_BHN2 { get; set; }
            public decimal? BeakerW_BHR1 { get; set; }
            public decimal? BeakerW_BHR2 { get; set; }

            public decimal? BeakerW_AHN1 { get; set; }
            public decimal? BeakerW_AHN2 { get; set; }
            public decimal? BeakerW_AHR1 { get; set; }
            public decimal? BeakerW_AHR2 { get; set; }

            public DateTime? InputDate { get; set; }
            public string InputBy { get; set; }

            public DateTime? EditDate { get; set; }
            public string EditBy { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<M_CheckSolutionLotReceive>> Gets(
                string lotNo, string compound)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<M_CheckSolutionLotReceive>> ret = new NDbResult<List<M_CheckSolutionLotReceive>>();

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

                p.Add("@lotno", lotNo);
                p.Add("@compound", compound);

                try
                {
                    var items = cnn.Query<M_CheckSolutionLotReceive>("M_CheckSolutionLotReceive", p, commandType: CommandType.StoredProcedure);
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

        #region M_GetReportTestSpecByMasterid

        public class M_GetReportTestSpecByMasterid
        {
            #region Public Properties

            public string ItemCode { get; set; }
            public string PropertName { get; set; }
            public int NoSample { get; set; }
            public int? MasterId { get; set; }
            public int PropertyNo { get; set; }
            public int SpecId { get; set; }
            public string SpecDesc { get; set; }

            public string UnitId { get; set; }
            public string UnitDesc { get; set; }

            public string OptionId { get; set; }
            public string OptionDesc { get; set; }

            public decimal? VCenter { get; set; }
            public decimal? VMin { get; set; }
            public decimal? VMax { get; set; }

            public string UnitReport { get; set; }
            public string TestMethod { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<M_GetReportTestSpecByMasterid>> Gets(
                int? masterId)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<M_GetReportTestSpecByMasterid>> ret = new NDbResult<List<M_GetReportTestSpecByMasterid>>();

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

                p.Add("@masterid", masterId);

                try
                {
                    var items = cnn.Query<M_GetReportTestSpecByMasterid>("M_GetReportTestSpecByMasterid", p, commandType: CommandType.StoredProcedure);
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

        #region P_GetTestDetailByProperty

        public class P_GetTestDetailByProperty
        {
            #region Public Properties

            public string ItemCode { get; set; }
            public string LotNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? N1 { get; set; }
            public decimal? N2 { get; set; }
            public decimal? N3 { get; set; }

            public string LoadN { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetTestDetailByProperty>> Gets(
                string lotNo, int propertyno, string unitid)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetTestDetailByProperty>> ret = new NDbResult<List<P_GetTestDetailByProperty>>();

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

                p.Add("@lotNo", lotNo);
                p.Add("@propertyno", propertyno);
                p.Add("@unit", unitid);

                try
                {
                    var items = cnn.Query<P_GetTestDetailByProperty>("P_GetTestDetailByProperty", p, commandType: CommandType.StoredProcedure);
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

        #region Ex_GetTensileDataByLot

        public class Ex_GetTensileDataByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? N1 { get; set; }
            public decimal? N2 { get; set; }
            public decimal? N3 { get; set; }

            public decimal? N1R1 { get; set; }
            public decimal? N1R2 { get; set; }
            public decimal? N2R1 { get; set; }
            public decimal? N2R2 { get; set; }
            public decimal? N3R1 { get; set; }
            public decimal? N3R2 { get; set; }

            public bool? N1R1Flag { get; set; }
            public bool? N1R2Flag { get; set; }
            public bool? N2R1Flag { get; set; }
            public bool? N2R2Flag { get; set; }
            public bool? N3R1Flag { get; set; }
            public bool? N3R2Flag { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<Ex_GetTensileDataByLot>> Gets(
                string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<Ex_GetTensileDataByLot>> ret = new NDbResult<List<Ex_GetTensileDataByLot>>();

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

                p.Add("@lotNo", lotNo);

                try
                {
                    var items = cnn.Query<Ex_GetTensileDataByLot>("Ex_GetTensileDataByLot", p, commandType: CommandType.StoredProcedure);
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

        #region Ex_GetElongationByLot

        public class Ex_GetElongationByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int PropertyNo { get; set; }

            public int? SPNo { get; set; }
            public string LoadN { get; set; }

            public decimal? N1 { get; set; }
            public decimal? N2 { get; set; }
            public decimal? N3 { get; set; }

            public decimal? N1R1 { get; set; }
            public decimal? N1R2 { get; set; }
            public decimal? N2R1 { get; set; }
            public decimal? N2R2 { get; set; }
            public decimal? N3R1 { get; set; }
            public decimal? N3R2 { get; set; }

            public bool? N1R1Flag { get; set; }
            public bool? N1R2Flag { get; set; }
            public bool? N2R1Flag { get; set; }
            public bool? N2R2Flag { get; set; }
            public bool? N3R1Flag { get; set; }
            public bool? N3R2Flag { get; set; }

            public string SampleType { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<Ex_GetElongationByLot>> Gets(
                string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<Ex_GetElongationByLot>> ret = new NDbResult<List<Ex_GetElongationByLot>>();

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

                p.Add("@lotNo", lotNo);

                try
                {
                    var items = cnn.Query<Ex_GetElongationByLot>("Ex_GetElongationByLot", p, commandType: CommandType.StoredProcedure);
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

        #region Ex_GetAdhesionDataByLot

        public class Ex_GetAdhesionDataByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SPNo { get; set; }

            public decimal? PeakN1 { get; set; }
            public decimal? PeakN2 { get; set; }
            public decimal? PeakN1R1 { get; set; }
            public decimal? PeakN1R2 { get; set; }
            public decimal? PeakN2R1 { get; set; }
            public decimal? PeakN2R2 { get; set; }

            public decimal? AdhesionN1 { get; set; }
            public decimal? AdhesionN2 { get; set; }
            public decimal? AdhesionN1R1 { get; set; }
            public decimal? AdhesionN1R2 { get; set; }
            public decimal? AdhesionN2R1 { get; set; }
            public decimal? AdhesionN2R2 { get; set; }

            public string SampleType { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<Ex_GetAdhesionDataByLot>> Gets(
                string lotNo, string sampleType)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<Ex_GetAdhesionDataByLot>> ret = new NDbResult<List<Ex_GetAdhesionDataByLot>>();

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

                p.Add("@lotNo", lotNo);

                try
                {
                    var items = cnn.Query<Ex_GetAdhesionDataByLot>("Ex_GetAdhesionDataByLot", p, commandType: CommandType.StoredProcedure);
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

        #region M_SaveReceiveSP

        public class M_SaveReceiveSP
        {
            #region Static Methods

            public static NDbResult Save(string lotNo, string productlot,
                string receiveBy, DateTime? receiveDate,
                int? sp, int? groupsp, int? retestsp = new int?(), string remark = null)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult ret = new NDbResult();

                if (string.IsNullOrWhiteSpace(lotNo))
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
                p.Add("@LotNo", lotNo);
                p.Add("@productlot", productlot);

                p.Add("@sp", sp);
                p.Add("@groupsp", groupsp);
                p.Add("@retestsp", retestsp);

                p.Add("@receiveremark", string.IsNullOrEmpty(remark) ? null : remark);
                p.Add("@receiveby", receiveBy);
                p.Add("@receivedate", receiveDate);

                p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                try
                {
                    cnn.Execute("M_SaveReceiveSP", p, commandType: CommandType.StoredProcedure);
                    ret.Success();
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

        #region P_GetActiveSPByLot

        public class P_GetActiveSPByLot : NInpc
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int? SP { get; set; }
            public int? GroupSP { get; set; }
            public string SampleType { get; set; } // S = Sample, F = Full CH
            public int? RetestSP { get; set; } // ?????

            // use for runtime edit and save only not return from SP.
            public int? RetestSP1 { get; set; }
            public int? RetestSP2 { get; set; }

            public string Remark { get; set; }

            public Visibility EnableVisibility
            {
                get { return (IsEnableRetest) ? Visibility.Collapsed : Visibility.Visible; }
                set { }
            }

            public Visibility CancelVisibility
            {
                get { return (IsEnableRetest) ? Visibility.Visible : Visibility.Collapsed; }
                set { }
            }

            public Visibility RetestVisibility
            {
                get { return (IsEnableRetest) ? Visibility.Visible : Visibility.Collapsed; }
                set { }
            }

            public bool IsEnableRetest { get; private set; }

            #endregion

            #region Public Methods

            public void EnableRetest()
            {
                IsEnableRetest = true;
                Raise(() => this.IsEnableRetest);

                Raise(() => this.RetestVisibility);
                Raise(() => this.EnableVisibility);
                Raise(() => this.CancelVisibility);

                Raise(() => this.RetestSP1);
                Raise(() => this.RetestSP2);
            }

            public void CancelRetest()
            {
                IsEnableRetest = false;
                Raise(() => this.IsEnableRetest);

                Raise(() => this.RetestVisibility);
                Raise(() => this.EnableVisibility);
                Raise(() => this.CancelVisibility);

                Raise(() => this.RetestSP1);
                Raise(() => this.RetestSP2);
            }

            #endregion

            #region Static Methods

            public static NDbResult<List<P_GetActiveSPByLot>> Gets(
                string lotNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<P_GetActiveSPByLot>> ret = new NDbResult<List<P_GetActiveSPByLot>>();

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

                p.Add("@lotNo", lotNo);

                try
                {
                    var items = cnn.Query<P_GetActiveSPByLot>("P_GetActiveSPByLot", p, commandType: CommandType.StoredProcedure);
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

        #region M_GetSolutionList

        public class M_GetSolutionList
        {
            #region Public Properties

            public int MasterId { get; set; }
            public string Customer { get; set; }
            public string ItemCode { get; set; }
            public string UserName { get; set; }

            public int Coa { get; set; }
            public string FMQC { get; set; }

            public string ProductType { get; set; }
            public string YarnType { get; set; }
            public string ElongLoadN { get; set; }

            public int NoTestCH { get; set; }
            public string YarnCode { get; set; }
            public int ValidDays { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<M_GetSolutionList>> Gets()
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<M_GetSolutionList>> ret = new NDbResult<List<M_GetSolutionList>>();

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

                try
                {
                    var items = cnn.Query<M_GetSolutionList>("M_GetSolutionList", p, commandType: CommandType.StoredProcedure);
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

        #region Plc_GetPhTemperatureByLot

        public class Plc_GetPhTemperatureByLot
        {
            #region Public Properties

            public string LotNo { get; set; }
            public int MasterId { get; set; }
            public string Compound { get; set; }
            public string TestType { get; set; }
            public string LinkType { get; set; }

            public decimal? Ph { get; set; }
            public decimal? Tempturature { get; set; }

            public string TestBy { get; set; }
            public DateTime? TestDate { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<List<Plc_GetPhTemperatureByLot>> Gets(string lotNo, string coupound)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<List<Plc_GetPhTemperatureByLot>> ret = new NDbResult<List<Plc_GetPhTemperatureByLot>>();

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
                p.Add("@lotNo", lotNo);
                p.Add("@compound", coupound);

                try
                {
                    var items = cnn.Query<Plc_GetPhTemperatureByLot>("Plc_GetPhTemperatureByLot", p, commandType: CommandType.StoredProcedure);
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

    #region Utils ExtensionMethods

    public static class UtilsExtensionMethods
    {
        #region Find By Property No

        /// <summary>
        /// Find By Property No.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="propertyNo"></param>
        /// <returns></returns>
        public static M_GetReportTestSpecByMasterid FindByPropertyNo(this List<M_GetReportTestSpecByMasterid> items,
            int propertyNo, string elongN = null)
        {
            if (null == items || items.Count <= 0)
                return null;
            if (string.IsNullOrEmpty(elongN))
            {
                return items.Find((x) => { return x.PropertyNo == propertyNo; });
            }
            else
            {
                return items.Find((x) =>
                {
                    return x.PropertyNo == propertyNo && string.Compare(x.UnitId, elongN, true) == 0;
                });
            }
        }

        #endregion
    }

    #endregion

    #region 

    public static class ModelCurrent
    {
        public static UserInfo User { get; set; } 
    }

    #endregion
}
