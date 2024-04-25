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
    #region CordElongationSubProperty

    /// <summary>
    /// The CordElongationSubProperty class.
    /// </summary>
    public class CordElongationSubProperty : NRTestProperty
    {
        #region Public Properties

        #region LoadN

        /// <summary>Gets is Need Eload.</summary>
        public virtual bool NeedEload { get; set; } = true;
        /// <summary>Gets is show Eload.</summary>
        public Visibility ShowEload
        {
            get { return NeedEload ? Visibility.Visible : Visibility.Hidden; }
            set { }
        }
        /// <summary>Gets Property Text.</summary>
        public virtual string PropertyText { get { return "unknown"; } set { } }
        /// <summary>Gets or sets LoadN.</summary>
        public string LoadN { get; set; }

        #endregion

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #endregion

        #region Static Methods

        #region Clone

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordElongationSubProperty src, CordElongationSubProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.LoadN = src.LoadN;

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
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets CordElongationSubProperty by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordElongationSubProperty>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordElongationSubProperty>> ret = new NDbResult<List<CordElongationSubProperty>>();

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
                var items = cnn.Query<CordElongationSubProperty>("P_GetElongationByLot",
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

        #endregion

        #region Save

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NDbResult<CordElongationSubProperty> Save(CordElongationSubProperty value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordElongationSubProperty> ret = new NDbResult<CordElongationSubProperty>();

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

            p.Add("@loadn", value.LoadN);

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

        #endregion
    }

    #endregion

    #region CordElongationBreakProperty

    /// <summary>
    /// The CordElongationBreakProperty class.
    /// </summary>
    public class CordElongationBreakProperty : CordElongationSubProperty
    {
        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected override void CheckSpec()
        {
            if (null == Spec || Spec.SpecId <= 0)
                return;

            this.NOut1 = (N1.HasValue) ? Spec.IsOutOfSpec(N1.Value) : false;
            this.NOut2 = (N2.HasValue) ? Spec.IsOutOfSpec(N2.Value) : false;
            this.NOut3 = (N3.HasValue) ? Spec.IsOutOfSpec(N3.Value) : false;
            this.NOut4 = (N4.HasValue) ? Spec.IsOutOfSpec(N4.Value) : false;
            this.NOut5 = (N5.HasValue) ? Spec.IsOutOfSpec(N5.Value) : false;
            this.NOut6 = (N6.HasValue) ? Spec.IsOutOfSpec(N6.Value) : false;
            this.NOut7 = (N7.HasValue) ? Spec.IsOutOfSpec(N7.Value) : false;

            this.ROut1 = (R1.HasValue) ? Spec.IsOutOfSpec(R1.Value) : false;
            this.ROut2 = (R2.HasValue) ? Spec.IsOutOfSpec(R2.Value) : false;
            this.ROut3 = (R3.HasValue) ? Spec.IsOutOfSpec(R3.Value) : false;
            this.ROut4 = (R4.HasValue) ? Spec.IsOutOfSpec(R4.Value) : false;
            this.ROut5 = (R5.HasValue) ? Spec.IsOutOfSpec(R5.Value) : false;
            this.ROut6 = (R6.HasValue) ? Spec.IsOutOfSpec(R6.Value) : false;
            this.ROut7 = (R7.HasValue) ? Spec.IsOutOfSpec(R7.Value) : false;

            // Raise items events
            this.RaiseNOutChanges();
            this.RaiseROutChanges();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets is Need Eload.</summary>
        public override bool NeedEload
        {
            get { return false; }
            set { }
        }
        /// <summary>Gets Property Text.</summary>
        public override string PropertyText { get { return "at Break"; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="totalN"></param>
        /// <param name="elongItem"></param>
        /// <returns></returns>
        internal static List<CordElongationBreakProperty> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN, CordElongation elongItem)
        {
            List<CordElongationBreakProperty> results = new List<CordElongationBreakProperty>();

            if (null == value || null == value)
                return results;

            int noOfSample;
            CordElongationBreakProperty inst;

            // For Elongation Break Proepty No = 2
            noOfSample = (null != totalN) ? totalN.NoSample : 0;

            // Elongation Break Proepty No = 2
            var spec = value.Specs.FindByPropertyNo(2);

            inst = new CordElongationBreakProperty()
            {
                LotNo = value.LotNo,
                PropertyNo = 2, // Elongation Break = 2
                SPNo = elongItem.SPNo,
                NeedSP = true,
                Spec = spec,
                NeedEload = false, // Elongation Break not requred SP No
                YarnType = value.YarnType,
                NoOfSample = noOfSample
            };

            results.Add(inst);

            return results;
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
        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected override void CheckSpec()
        {
            if (null == Spec || Spec.SpecId <= 0)
                return;

            this.NOut1 = (N1.HasValue) ? Spec.IsOutOfSpec(N1.Value) : false;
            this.NOut2 = (N2.HasValue) ? Spec.IsOutOfSpec(N2.Value) : false;
            this.NOut3 = (N3.HasValue) ? Spec.IsOutOfSpec(N3.Value) : false;
            this.NOut4 = (N4.HasValue) ? Spec.IsOutOfSpec(N4.Value) : false;
            this.NOut5 = (N5.HasValue) ? Spec.IsOutOfSpec(N5.Value) : false;
            this.NOut6 = (N6.HasValue) ? Spec.IsOutOfSpec(N6.Value) : false;
            this.NOut7 = (N7.HasValue) ? Spec.IsOutOfSpec(N7.Value) : false;

            this.ROut1 = (R1.HasValue) ? Spec.IsOutOfSpec(R1.Value) : false;
            this.ROut2 = (R2.HasValue) ? Spec.IsOutOfSpec(R2.Value) : false;
            this.ROut3 = (R3.HasValue) ? Spec.IsOutOfSpec(R3.Value) : false;
            this.ROut4 = (R4.HasValue) ? Spec.IsOutOfSpec(R4.Value) : false;
            this.ROut5 = (R5.HasValue) ? Spec.IsOutOfSpec(R5.Value) : false;
            this.ROut6 = (R6.HasValue) ? Spec.IsOutOfSpec(R6.Value) : false;
            this.ROut7 = (R7.HasValue) ? Spec.IsOutOfSpec(R7.Value) : false;
        }

        #endregion

        #region Public Properties

        public override bool NeedEload
        {
            get { return true; }
            set { }
        }
        /// <summary>Gets Property Text.</summary>
        public override string PropertyText { get { return "at Load"; } set { } }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="totalN"></param>
        /// <param name="elongItem"></param>
        /// <returns></returns>
        internal static List<CordElongationLoadProperty> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN, CordElongation elongItem)
        {
            List<CordElongationLoadProperty> results = new List<CordElongationLoadProperty>();

            if (null == value || null == value)
                return results;

            int noOfSample;
            CordElongationLoadProperty inst;

            // For Elongation Load Proepty No = 3
            noOfSample = (null != totalN) ? totalN.NoSample : 0;

            string[] elongIds = !string.IsNullOrWhiteSpace(value.ELongLoadN) ?
                value.ELongLoadN.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries) : null;
            if (null != value.ELongLoadN && value.ELongLoadN.Length > 0)
            {
                foreach (string elongId in elongIds)
                {
                    // Elongation Load Proepty No = 3
                    var spec = value.Specs.FindByPropertyNo(3, elongId);

                    inst = new CordElongationLoadProperty()
                    {
                        LotNo = value.LotNo,
                        PropertyNo = 3, // Elongation Load = 3
                        SPNo = elongItem.SPNo,
                        NeedSP = true,
                        NeedEload = true, // Elongation Load requred SP No
                        Spec = spec,
                        NoOfSample = noOfSample,
                        YarnType = value.YarnType,
                        LoadN = elongId
                    };

                    results.Add(inst);
                }
            }

            return results;
        }

        #endregion
    }

    #endregion

    #region CordElongation

    /// <summary>
    /// The Cord Elongation
    /// </summary>
    public class CordElongation
    {
        #region Public Properties

        public string LotNo { get; set; }
        public int? MasterId { get; set; }
        public int? SPNo { get; set; }
        public string ELongLoadN { get; set; }
        public string YarnType { get; set; }

        public List<CordElongationSubProperty> SubProperties { get; set; }

        #endregion

        #region Static Methods

        #region CreateSubProperties

        /// <summary>
        /// Creat Sub Properties.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="breakTotalN"></param>
        /// <param name="loadTotalN"></param>
        /// <param name="elongItem"></param>
        /// <returns></returns>
        private static List<CordElongationSubProperty> CreateSubProperties(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem breakTotalN,
            Utils.M_GetPropertyTotalNByItem loadTotalN,
            CordElongation elongItem)
        {
            List<CordElongationSubProperty> results = new List<CordElongationSubProperty>();

            if (null == value || null == elongItem)
                return results;

            var eBreaks = CordElongationBreakProperty.Create(value, breakTotalN, elongItem);
            var eLoads = CordElongationLoadProperty.Create(value, loadTotalN, elongItem);

            if (null != eBreaks) results.AddRange(eBreaks);
            if (null != eLoads) results.AddRange(eLoads);

            // Sort by SP No/PropetyNo/LoadN.
            return results.OrderBy(o => o.SPNo).ThenBy(o => o.PropertyNo).ThenBy(o => o.LoadN).ToList();
        }

        #endregion

        #region Create

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="breakTotalN"></param>
        /// <param name="loadTotalN"></param>
        /// <returns></returns>
        internal static List<CordElongation> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem breakTotalN,
            Utils.M_GetPropertyTotalNByItem loadTotalN)
        {
            List<CordElongation> results = new List<CordElongation>();

            if (null == value)
                return results;

            int alllowSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            int i = 1;
            int iMaxLimitSP = 7;
            while (i <= iMaxLimitSP)
            {
                if (results.Count >= alllowSP)
                    break; // already reach max allow SP

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
                // Skip SP is null
                if (!SP.HasValue)
                {
                    i++; // increase index and skip to next loop.
                    continue;
                }

                var inst = new CordElongation()
                {
                    LotNo = value.LotNo,
                    MasterId = value.MasterId.Value,
                    SPNo = SP,
                    ELongLoadN = value.ELongLoadN,
                    YarnType = value.YarnType
                };
                // load break/load sub properties.
                inst.SubProperties = CreateSubProperties(value, breakTotalN, loadTotalN, inst);

                results.Add(inst);

                i++; // increase index
            }

            var allItems = results.OrderBy(o => o.SPNo).ToList();

            // Check Exists data
            var exists = (value.MasterId.HasValue) ? CordElongationSubProperty.GetsByLotNo(
                value.LotNo).Value() : null;

            if (null != exists && null != allItems)
            {
                int idx = -1;
                foreach (var item in exists)
                {
                    foreach (var elong in allItems)
                    {
                        if (null == elong.SubProperties || elong.SubProperties.Count <= 0)
                            continue;
                        idx = elong.SubProperties.FindIndex((x) =>
                        {
                            if (x.PropertyNo == 2)
                            {
                                return x.SPNo == item.SPNo &&
                                    x.PropertyNo == item.PropertyNo;
                            }
                            else
                            {
                                return x.SPNo == item.SPNo &&
                                    x.PropertyNo == item.PropertyNo &&
                                    x.LoadN == item.LoadN;
                            }
                        });
                        if (idx != -1)
                        {
                            // need to set because not return from db.
                            item.NoOfSample = elong.SubProperties[idx].NoOfSample;
                            item.YarnType = elong.SubProperties[idx].YarnType;
                            // Clone anther properties
                            CordElongationSubProperty.Clone(item, elong.SubProperties[idx]);
                        }
                    };
                }
            }

            return allItems;
        }

        #endregion

        #endregion
    }

    #endregion
}
