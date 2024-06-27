#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordTensileStrength

    /// <summary>
    /// The Cord Tensile Strength
    /// </summary>
    public class CordTensileStrength : NRTestProperty
    {
        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected override void CheckSpec()
        {
            if (null == Spec || Spec.SpecId <= 0) 
                return;

            this.N1Out = (N1.HasValue) ? Spec.IsOutOfSpec(N1.Value) : false;
            this.N2Out = (N2.HasValue) ? Spec.IsOutOfSpec(N2.Value) : false;
            this.N3Out = (N3.HasValue) ? Spec.IsOutOfSpec(N3.Value) : false;
            this.N4Out = (N4.HasValue) ? Spec.IsOutOfSpec(N4.Value) : false;
            this.N5Out = (N5.HasValue) ? Spec.IsOutOfSpec(N5.Value) : false;
            this.N6Out = (N6.HasValue) ? Spec.IsOutOfSpec(N6.Value) : false;
            this.N7Out = (N7.HasValue) ? Spec.IsOutOfSpec(N7.Value) : false;

            this.N1R1Out = (N1R1.HasValue) ? Spec.IsOutOfSpec(N1R1.Value) : false;
            this.N1R2Out = (N2R2.HasValue) ? Spec.IsOutOfSpec(N2R2.Value) : false;
            this.N2R1Out = (N2R1.HasValue) ? Spec.IsOutOfSpec(N2R1.Value) : false;
            this.N2R2Out = (N2R2.HasValue) ? Spec.IsOutOfSpec(N2R2.Value) : false;
            this.N3R1Out = (N3R1.HasValue) ? Spec.IsOutOfSpec(N3R1.Value) : false;
            this.N3R2Out = (N3R2.HasValue) ? Spec.IsOutOfSpec(N3R2.Value) : false;
            this.N4R1Out = (N4R1.HasValue) ? Spec.IsOutOfSpec(N4R1.Value) : false;
            this.N4R2Out = (N4R2.HasValue) ? Spec.IsOutOfSpec(N4R2.Value) : false;
            this.N5R1Out = (N5R1.HasValue) ? Spec.IsOutOfSpec(N5R1.Value) : false;
            this.N5R2Out = (N5R2.HasValue) ? Spec.IsOutOfSpec(N5R2.Value) : false;
            this.N6R1Out = (N6R1.HasValue) ? Spec.IsOutOfSpec(N6R1.Value) : false;
            this.N6R2Out = (N6R2.HasValue) ? Spec.IsOutOfSpec(N6R2.Value) : false;
            this.N7R1Out = (N7R1.HasValue) ? Spec.IsOutOfSpec(N7R1.Value) : false;
            this.N7R2Out = (N7R2.HasValue) ? Spec.IsOutOfSpec(N7R2.Value) : false;

            // Raise items events
            this.RaiseNOutChanges();
            this.RaiseR1OutChanges();
            this.RaiseR2OutChanges();
        }

        #endregion

        #region Public Properties

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
        public static void Clone(CordTensileStrength src, CordTensileStrength dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            if (null != dst.Items && null != src.Items)
            {
                int iMax = (dst.Items.Count > src.Items.Count) ? dst.Items.Count : src.Items.Count;
                for (int i = 0; i < iMax; i++) 
                {
                    if (i < dst.Items.Count && i < src.Items.Count)
                    {
                        dst.Items[i].N = src.Items[i].N;
                        dst.Items[i].R1 = src.Items[i].R1;
                        dst.Items[i].R2 = src.Items[i].R2;
                        dst.Items[i].R1Flag = src.Items[i].R1Flag;
                        dst.Items[i].R2Flag = src.Items[i].R2Flag;
                        dst.Items[i].SampleType = src.Items[i].SampleType;
                    }
                }
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="totalN"></param>
        /// <returns></returns>
        internal static List<CordTensileStrength> Create(CordSampleTestData value, 
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<CordTensileStrength> results = new List<CordTensileStrength>();
            if (null == value)
                return results;

            // For Tensile Strength Proepty No = 1
            int noOfSample = (null != totalN) ? totalN.NoSample : 0;
            int alllowSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            // Tensile Strength = 1
            var spec = value.Specs.FindByPropertyNo(1);

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

                var inst = new CordTensileStrength()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 1, // Tensile Strength = 1
                    SPNo = SP,
                    NeedSP = true,
                    Spec = spec,
                    YarnType = value.YarnType,
                    NoOfSample = noOfSample
                };

                results.Add(inst);

                i++; // increase index
            }

            var existItems = (value.MasterId.HasValue) ? GetsByLotNo(value.LotNo).Value() : null;

            // In case No items need to check has some import data
            if (null == existItems || existItems.Count == 0)
            {
                var imports = Utils.Ex_GetTensileDataByLot.Gets(value.LotNo).Value();
                if (null != imports && imports.Count > 0)
                {
                    existItems = new List<CordTensileStrength>();
                    foreach (var item in imports)
                    {
                        var imp = new CordTensileStrength()
                        {
                            LotNo = item.LotNo,
                            PropertyNo = 1, // Tensile Strength = 1
                            SPNo = item.SPNo,
                            NeedSP = true,
                            Spec = spec,
                            YarnType = value.YarnType,
                            NoOfSample = noOfSample,
                            N1 = item.N1,
                            N2 = item.N2,
                            N3 = item.N3,
                            N1R1 = item.N1R1,
                            N1R2 = item.N1R2,
                            N2R1 = item.N2R1,
                            N2R2 = item.N2R2,
                            N3R1 = item.N3R1,
                            N3R2 = item.N3R2
                        };

                        existItems.Add(imp);
                    }
                }
            }

            if (null != existItems && null != results)
            {
                int idx = -1;
                // loop trought all initial results and fill data with the exists on database
                foreach (var item in results)
                {
                    idx = existItems.FindIndex((x) => { return x.SPNo == item.SPNo; });
                    if (idx != -1) 
                    {
                        // need to set because not return from db.
                        existItems[idx].NoOfSample = item.NoOfSample;
                        existItems[idx].YarnType = item.YarnType;
                        existItems[idx].SampleType = item.SampleType;
                        // Clone anther properties
                        Clone(existItems[idx], item);
                    }
                }
            }

            return results;
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets CordTensileStrength by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordTensileStrength>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordTensileStrength>> ret = new NDbResult<List<CordTensileStrength>>();

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
                var items = cnn.Query<CordTensileStrength>("P_GetTensileDataByLot",
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
        public static NDbResult<CordTensileStrength> Save(CordTensileStrength value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTensileStrength> ret = new NDbResult<CordTensileStrength>();

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
            p.Add("@spno", value.SPNo);

            p.Add("@n1", value.N1);
            p.Add("@n2", value.N2);
            p.Add("@n3", value.N3);

            p.Add("@n1r1", value.N1R1);
            p.Add("@n1r2", value.N1R2);
            p.Add("@n2r1", value.N2R1);
            p.Add("@n2r2", value.N2R2);
            p.Add("@n3r1", value.N3R1);
            p.Add("@n3r2", value.N3R2);

            p.Add("@n1r1flag", value.N1R1Flag);
            p.Add("@n1r2flag", value.N1R2Flag);
            p.Add("@n2r1flag", value.N2R1Flag);
            p.Add("@n2r2flag", value.N2R2Flag);
            p.Add("@n3r1flag", value.N3R1Flag);
            p.Add("@n3r2flag", value.N3R2Flag);

            p.Add("@sampletype", value.SampleType);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveTensile", p, commandType: CommandType.StoredProcedure);
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
}
