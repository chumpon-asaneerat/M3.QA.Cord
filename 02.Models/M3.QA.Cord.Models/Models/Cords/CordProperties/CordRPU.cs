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
    #region CordRPU

    /// <summary>
    /// The Cord RPU class.
    /// </summary>
    public class CordRPU : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordRPU() : base()
        {
            BeforeHeat = new NRTestProperty();
            // BeforeHeat change need to calc formula for RPU
            BeforeHeat.ValueChanges = CalculateFormula;

            AfterHeat = new NRTestProperty();
            // AfterHeat change need to calc formula for RPU
            AfterHeat.ValueChanges = CalculateFormula;

            RPU = new decimal?();
        }

        #endregion

        #region Private Methods

        private void CalculateFormula()
        {
            if (null != BeforeHeat && null != AfterHeat)
            {
                // RPU = ( (BF Heat – AF Heat) / AF Heat )*100

                decimal? BF;
                decimal? AF;
                decimal? diff;

                BF = (BeforeHeat.R1.HasValue) ? BeforeHeat.R1.Value : (BeforeHeat.N1.HasValue) ? BeforeHeat.N1.Value : new decimal?();
                AF = (AfterHeat.R1.HasValue) ? AfterHeat.R1.Value : (AfterHeat.N1.HasValue) ? AfterHeat.N1.Value : new decimal?();

                diff = (BF.HasValue && AF.HasValue && (BF.Value - AF.Value > 0)) ? BF.Value - AF.Value : new decimal?();
                RPU = (diff.HasValue && AF.HasValue && AF.Value > 0) ? (diff.Value / AF.Value) * 100 : new decimal?();

                // Raise events
                Raise(() => this.RPU);
            }
        }

        private void UpdateProperties()
        {
            if (null == BeforeHeat) BeforeHeat = new NRTestProperty();

            BeforeHeat.LotNo = LotNo;
            BeforeHeat.PropertyNo = PropertyNo;
            BeforeHeat.SPNo = SPNo;
            BeforeHeat.NoOfSample = NoOfSample;
            BeforeHeat.NeedSP = NeedSP;
            BeforeHeat.YarnType = YarnType;

            if (null == AfterHeat) AfterHeat = new NRTestProperty();
            AfterHeat.SPNo = SPNo;
            AfterHeat.LotNo = LotNo;
            AfterHeat.PropertyNo = PropertyNo;
            AfterHeat.SPNo = SPNo;
            AfterHeat.NoOfSample = NoOfSample;
            AfterHeat.NeedSP = NeedSP;
            AfterHeat.YarnType = YarnType;

            // Check calculate action
            if (null == BeforeHeat.ValueChanges)
            {
                BeforeHeat.ValueChanges = CalculateFormula;
            }
            if (null == AfterHeat.ValueChanges)
            {
                AfterHeat.ValueChanges = CalculateFormula;
            }

            CalculateFormula(); // calculate

            this.Raise(() => this.EnableTest);
        }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets SP No.</summary>
        public int? SPNo
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets Yarn Type.</summary>
        public string YarnType
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }

        #endregion

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }

        #endregion

        #region BeforeHeat/AfterHear/RPU

        public NRTestProperty BeforeHeat { get; set; }
        public NRTestProperty AfterHeat { get; set; }
        public decimal? RPU { get; set; }

        #endregion

        #endregion

        #region Static Methods

        #region Create

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="totalN"></param>
        /// <returns></returns>
        internal static List<CordRPU> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<CordRPU> results = new List<CordRPU>();
            if (null == value)
                return results;

            // For RPU Proepty No = 12
            int noOfSample = (null != totalN) ? totalN.NoSample : 0;
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

                var inst = new CordRPU()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 12, // RPU Proepty No = 12
                    SPNo = SP,
                    NeedSP = true,
                    YarnType = value.YarnType,
                    NoOfSample = noOfSample
                };

                results.Add(inst);

                i++; // increase index
            }

            // For RPU Proepty No = 12
            var existItems = (value.MasterId.HasValue) ? GetsByLotNo(value.LotNo).Value() : null;
            if (null != existItems && null != results)
            {
                int idx = -1;
                // loop trought all initial results and fill data with the exists on database
                foreach (var item in results)
                {
                    idx = existItems.FindIndex((x) =>
                    {
                        return x.SPNo == item.SPNo && x.PropertyNo == item.PropertyNo;
                    });
                    if (idx != -1)
                    {
                        // need to set because not return from db.
                        existItems[idx].NoOfSample = item.NoOfSample;
                        existItems[idx].YarnType = item.YarnType;
                        // Clone anther properties
                        Clone(existItems[idx], item);
                    }
                }
            }

            return results;
        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordRPU src, CordRPU dst)
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

            NRTestProperty.Clone(src.BeforeHeat, dst.BeforeHeat);
            NRTestProperty.Clone(src.AfterHeat, dst.AfterHeat);

            dst.RPU = src.RPU;
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets CordRPU by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordRPU>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordRPU>> ret = new NDbResult<List<CordRPU>>();

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

            try
            {
                List<CordRPU> results = new List<CordRPU>();

                var items = Utils.P_GetRPUByLot.GetByLot(lotNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new CordRPU();
                        inst.LotNo = item.LotNo;
                        inst.PropertyNo = 12; // RPU Proepty No = 12
                        inst.SPNo = item.SPNo;

                        inst.NeedSP = true;
                        //inst.NoOfSample = 1; // ???

                        if (null != inst.BeforeHeat)
                        {
                            inst.BeforeHeat.N1 = item.BFN1;
                            inst.BeforeHeat.R1 = item.BFR1;
                        }
                        if (null != inst.AfterHeat)
                        {
                            inst.AfterHeat.N1 = item.AFN1;
                            inst.AfterHeat.R1 = item.AFR1;
                        }

                        inst.RPU = item.RPU;

                        inst.InputBy = item.InputBy;
                        inst.InputDate = item.InputDate;
                        inst.EditBy = item.EditBy;
                        inst.EditDate = item.EditDate;

                        results.Add(inst);
                    }
                }

                ret.Success(results);
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
        public static NDbResult<CordRPU> Save(CordRPU value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordRPU> ret = new NDbResult<CordRPU>();

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

            p.Add("@bfn1", (null != value.BeforeHeat) ? value.BeforeHeat.N1 : new decimal?());
            p.Add("@bfr1", (null != value.BeforeHeat) ? value.BeforeHeat.R1 : new decimal?());

            p.Add("@afn1", (null != value.AfterHeat) ? value.AfterHeat.N1 : new decimal?());
            p.Add("@afr1", (null != value.AfterHeat) ? value.AfterHeat.R1 : new decimal?());

            p.Add("@rpu", value.RPU);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveRPU", p, commandType: CommandType.StoredProcedure);
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
