#region Using

using System;
using System.CodeDom.Compiler;
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
    #region CordElongationSubProperty

    /// <summary>
    /// The CordElongationSubProperty class.
    /// </summary>
    public class CordElongationSubProperty : CordTestProperty
    {
        /// <summary>Gets is show Eload.</summary>
        public virtual Visibility ShowEload { get { return Visibility.Hidden; } set { } }
        /// <summary>Gets Property Text.</summary>
        public virtual string PropertyText { get { return "unknown"; } set { } }
        /// <summary>Gets or sets ELongLoadN.</summary>
        public string ELongLoadN { get; set; }
    }

    #endregion

    #region CordElongationBreakProperty

    /// <summary>
    /// The CordElongationBreakProperty class.
    /// </summary>
    public class CordElongationBreakProperty : CordElongationSubProperty
    {
        #region Public Properties

        public override Visibility ShowEload { get { return Visibility.Hidden; } set { } }
        /// <summary>Gets Property Text.</summary>
        public override string PropertyText { get { return "at Break"; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="elongItem"></param>
        /// <returns></returns>
        internal static List<CordElongationBreakProperty> Create(CordSampleTestData value,
            CordElongationProperty elongItem)
        {
            List<CordElongationBreakProperty> results = new List<CordElongationBreakProperty>();

            if (null == value || null == value)
                return results;

            int noOfSample;
            CordElongationBreakProperty inst;
            Utils.M_GetPropertyTotalNByItem total;

            // For Elongation Break Proepty No = 2
            total = (value.MasterId.HasValue) ?
                Utils.M_GetPropertyTotalNByItem.GetByItem(value.MasterId.Value, 2).Value() : null;
            noOfSample = (null != total) ? total.NoSample : 0;

            inst = new CordElongationBreakProperty()
            {
                LotNo = value.LotNo,
                PropertyNo = 2, // Elongation Break = 2
                SPNo = elongItem.SPNo,
                NeedSP = false, // Elongation Break not requred SP No
                NoOfSample = noOfSample
            };

            results.Add(inst);

            // Check Exists data
            var existBreaks = (value.MasterId.HasValue) ? GetsByLotNo(
                value.LotNo, value.MasterId.Value).Value() : null;

            if (null != existBreaks && null != results)
            {
                int idx = -1;
                foreach (var item in existBreaks)
                {
                    item.NoOfSample = noOfSample; // need to set because not return from db.

                    idx = results.FindIndex((x) => 
                    { 
                        return x.SPNo == item.SPNo && x.PropertyNo == item.PropertyNo; 
                    });
                    if (idx != -1)
                    {
                        Clone(item, results[idx]);
                    }
                    idx++;
                }
            }

            return results;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordElongationBreakProperty src, CordElongationBreakProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            dst.N1 = src.N1;
            dst.N2 = src.N2;
            dst.N3 = src.N3;
            dst.N4 = src.N4;
            dst.N5 = src.N5;
            dst.N6 = src.N6;
            dst.N7 = src.N7;

            dst.R1 = src.R1;
            dst.R2 = src.R2;
            dst.R3 = src.R3;
            dst.R4 = src.R4;
            dst.R5 = src.R5;
            dst.R6 = src.R6;
            dst.R7 = src.R7;

            /*
            dst.AllowN1 = src.AllowN1;
            dst.AllowN2 = src.AllowN2;
            dst.AllowN3 = src.AllowN3;
            dst.AllowN4 = src.AllowN4;
            dst.AllowN5 = src.AllowN5;
            dst.AllowN6 = src.AllowN6;
            dst.AllowN7 = src.AllowN7;

            dst.AllowR1 = src.AllowR1;
            dst.AllowR2 = src.AllowR2;
            dst.AllowR3 = src.AllowR3;
            dst.AllowR4 = src.AllowR4;
            dst.AllowR5 = src.AllowR5;
            dst.AllowR6 = src.AllowR6;
            dst.AllowR7 = src.AllowR7;

            dst.Avg = src.Avg;
            */
        }

        /// <summary>
        /// Gets CordElongationProperty by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <param name="masterId">The MasterId.</param>
        /// <returns></returns>
        public static NDbResult<List<CordElongationBreakProperty>> GetsByLotNo(string lotNo, int masterId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordElongationBreakProperty>> ret = new NDbResult<List<CordElongationBreakProperty>>();

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

            try
            {
                var items = cnn.Query<CordElongationBreakProperty>("P_GetElongationByLot",
                    p, commandType: CommandType.StoredProcedure).ToList();
                ret.Success(items);
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
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NDbResult<CordElongationBreakProperty> Save(CordElongationBreakProperty value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordElongationBreakProperty> ret = new NDbResult<CordElongationBreakProperty>();

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
            p.Add("@propertyno", value.PropertyNo);
            p.Add("@spno", value.SPNo);

            p.Add("@loadn", null);

            p.Add("@n1", value.N1);
            p.Add("@n2", value.N2);
            p.Add("@n3", value.N3);
            p.Add("@r1", value.R1);
            p.Add("@r2", value.R2);
            p.Add("@r3", value.R3);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveElongation", p, commandType: CommandType.StoredProcedure);
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

    #region CordElongationLoadProperty

    /// <summary>
    /// The CordElongationLoadProperty class.
    /// </summary>
    public class CordElongationLoadProperty : CordElongationSubProperty
    {
        #region Public Properties

        public override Visibility ShowEload { get { return Visibility.Visible; } set { } }
        /// <summary>Gets Property Text.</summary>
        public override string PropertyText { get { return "at Load"; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="elongItem"></param>
        /// <returns></returns>
        internal static List<CordElongationLoadProperty> Create(CordSampleTestData value,
            CordElongationProperty elongItem)
        {
            List<CordElongationLoadProperty> results = new List<CordElongationLoadProperty>();

            if (null == value || null == value)
                return results;

            int noOfSample;
            CordElongationLoadProperty inst;
            Utils.M_GetPropertyTotalNByItem total;

            // For Elongation Load Proepty No = 3
            total = (value.MasterId.HasValue) ?
                Utils.M_GetPropertyTotalNByItem.GetByItem(value.MasterId.Value, 3).Value() : null;
            noOfSample = (null != total) ? total.NoSample : 0;

            string[] elongIds = !string.IsNullOrWhiteSpace(value.ELongLoadN) ?
                value.ELongLoadN.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries) : null;
            if (null != value.ELongLoadN && value.ELongLoadN.Length > 0)
            {
                foreach (string elongId in elongIds)
                {
                    inst = new CordElongationLoadProperty()
                    {
                        LotNo = value.LotNo,
                        PropertyNo = 3, // Elongation Load = 3
                        SPNo = elongItem.SPNo,
                        NeedSP = true, // Elongation Load requred SP No
                        NoOfSample = noOfSample,
                        ELongLoadN = elongId
                    };

                    results.Add(inst);
                }
            }

            // Check Exists data
            var existLoads = (value.MasterId.HasValue) ? GetsByLotNo(
                value.LotNo, value.MasterId.Value).Value() : null;

            if (null != existLoads && null != results)
            {
                int idx = -1;
                foreach (var item in existLoads)
                {
                    item.NoOfSample = noOfSample; // need to set because not return from db.

                    idx = results.FindIndex((x) => 
                    { 
                        return x.SPNo == item.SPNo && 
                            x.PropertyNo == item.PropertyNo &&
                            x.ELongLoadN == item.ELongLoadN; 
                    });
                    if (idx != -1)
                    {
                        Clone(item, results[idx]);
                    }
                    idx++;
                }
            }

            return results;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordElongationLoadProperty src, CordElongationLoadProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.ELongLoadN = src.ELongLoadN;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            dst.N1 = src.N1;
            dst.N2 = src.N2;
            dst.N3 = src.N3;
            dst.N4 = src.N4;
            dst.N5 = src.N5;
            dst.N6 = src.N6;
            dst.N7 = src.N7;

            dst.R1 = src.R1;
            dst.R2 = src.R2;
            dst.R3 = src.R3;
            dst.R4 = src.R4;
            dst.R5 = src.R5;
            dst.R6 = src.R6;
            dst.R7 = src.R7;

            /*
            dst.AllowN1 = src.AllowN1;
            dst.AllowN2 = src.AllowN2;
            dst.AllowN3 = src.AllowN3;
            dst.AllowN4 = src.AllowN4;
            dst.AllowN5 = src.AllowN5;
            dst.AllowN6 = src.AllowN6;
            dst.AllowN7 = src.AllowN7;

            dst.AllowR1 = src.AllowR1;
            dst.AllowR2 = src.AllowR2;
            dst.AllowR3 = src.AllowR3;
            dst.AllowR4 = src.AllowR4;
            dst.AllowR5 = src.AllowR5;
            dst.AllowR6 = src.AllowR6;
            dst.AllowR7 = src.AllowR7;

            dst.Avg = src.Avg;
            */
        }

        /// <summary>
        /// Gets CordElongationLoadProperty by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <param name="masterId">The MasterId.</param>
        /// <returns></returns>
        public static NDbResult<List<CordElongationLoadProperty>> GetsByLotNo(string lotNo, int masterId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordElongationLoadProperty>> ret = new NDbResult<List<CordElongationLoadProperty>>();

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

            try
            {
                var items = cnn.Query<CordElongationLoadProperty>("P_GetElongationByLot",
                    p, commandType: CommandType.StoredProcedure).ToList();
                ret.Success(items);
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
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NDbResult<CordElongationLoadProperty> Save(CordElongationLoadProperty value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordElongationLoadProperty> ret = new NDbResult<CordElongationLoadProperty>();

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
            p.Add("@propertyno", value.PropertyNo);
            p.Add("@spno", value.SPNo);

            p.Add("@loadn", value.ELongLoadN);

            p.Add("@n1", value.N1);
            p.Add("@n2", value.N2);
            p.Add("@n3", value.N3);
            p.Add("@r1", value.R1);
            p.Add("@r2", value.R2);
            p.Add("@r3", value.R3);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveElongation", p, commandType: CommandType.StoredProcedure);
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

    #region CordElongationProperty

    /// <summary>
    /// The CordElongationProperty class.
    /// </summary>
    public class CordElongationProperty
    {
        #region Public Properties

        public string LotNo { get; set; }
        public int? MasterId { get; set; }
        public int? SPNo { get; set; }
        public string ELongLoadN { get; set; }

        public List<CordElongationSubProperty> SubProperties { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creat Sub Properties.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="elongItem"></param>
        /// <returns></returns>
        private static List<CordElongationSubProperty> CreateSubProperties(CordSampleTestData value,
            CordElongationProperty elongItem)
        {
            List<CordElongationSubProperty> results = new List<CordElongationSubProperty>();

            if (null == value || null == elongItem)
                return results;

            var eBreaks = CordElongationBreakProperty.Create(value, elongItem);
            var eLoads = CordElongationLoadProperty.Create(value, elongItem);

            if (null != eBreaks) results.AddRange(eBreaks);
            if (null != eLoads) results.AddRange(eLoads);

            // Sort by SP No/PropetyNo/ELongLoadN.
            return results.OrderBy(o => o.SPNo).ThenBy(o => o.PropertyNo).ThenBy(o => o.ELongLoadN).ToList();
        }
        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static List<CordElongationProperty> Create(CordSampleTestData value)
        {
            List<CordElongationProperty> results = new List<CordElongationProperty>();

            if (null == value)
                return results;

            var maxSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;
            for (int i = 1; i <= maxSP; i++)
            {
                if (i > 7) continue;

                int? SP;
                switch (i)
                {
                    case 1: SP = value.SP1; break;
                    case 2: SP = value.SP2; break;
                    case 3: SP = value.SP3; break;
                    case 4: SP = value.SP4; break;
                    case 5: SP = value.SP5; break;
                    case 6: SP = value.SP6; break;
                    case 7: SP = value.SP7; break;
                    default: SP = new int?(); break;
                }
                var inst = new CordElongationProperty()
                {
                    LotNo = value.LotNo,
                    MasterId = value.MasterId.Value,
                    SPNo = SP,
                    ELongLoadN = value.ELongLoadN
                };
                // load break/load sub properties.
                inst.SubProperties = CreateSubProperties(value, inst);

                results.Add(inst);
            }

            return results.OrderBy(o => o.SPNo).ToList();
        }

        #endregion
    }

    #endregion
}
