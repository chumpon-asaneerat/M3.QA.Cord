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
    #region CordDenierMoistureWeight

    /// <summary>
    /// The Cord Denier Moisture regain and Weight
    /// </summary>
    public class CordDenierMoistureWeight : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordDenierMoistureWeight() : base()
        {
            YarnWeightBeforeDying = new NRTestProperty();
            // BeforeHeat change need to calc formula for Yarn Weight
            //YarnWeightBeforeDying.ValueChanges = CalculateFormula;

            ContentWeight = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            ContentWeight.ValueChanges = CalculateFormula;

            YarnAndContentWeightAfterDying = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            YarnAndContentWeightAfterDying.ValueChanges = CalculateFormula;

            YarnWeightAfterDying = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            //YarnWeightAfterDying.ValueChanges = CalculateFormula;

            StandardDenierD = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            //StandardDenierD.ValueChanges = CalculateFormula;

            StandardDenierDtex = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            //StandardDenierDtex.ValueChanges = CalculateFormula;

            EquilibriumMoistureContent = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            //EquilibriumMoistureContent.ValueChanges = CalculateFormula;

            Weight = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            //Weight.ValueChanges = CalculateFormula;
        }

        #endregion

        #region Private Methods

        private void CalculateFormula()
        {
            /*
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
            */
        }

        private void UpdateProperties()
        {
            if (null == YarnWeightBeforeDying) YarnWeightBeforeDying = new NRTestProperty();

            YarnWeightBeforeDying.LotNo = LotNo;
            YarnWeightBeforeDying.PropertyNo = PropertyNo;
            YarnWeightBeforeDying.SPNo = SPNo;
            YarnWeightBeforeDying.NoOfSample = NoOfSample;
            YarnWeightBeforeDying.NeedSP = NeedSP;
            YarnWeightBeforeDying.YarnType = YarnType;

            if (null == ContentWeight) ContentWeight = new NRTestProperty();
            ContentWeight.SPNo = SPNo;
            ContentWeight.LotNo = LotNo;
            ContentWeight.PropertyNo = PropertyNo;
            ContentWeight.SPNo = SPNo;
            ContentWeight.NoOfSample = NoOfSample;
            ContentWeight.NeedSP = NeedSP;
            ContentWeight.YarnType = YarnType;

            if (null == YarnAndContentWeightAfterDying) YarnAndContentWeightAfterDying = new NRTestProperty();
            YarnAndContentWeightAfterDying.SPNo = SPNo;
            YarnAndContentWeightAfterDying.LotNo = LotNo;
            YarnAndContentWeightAfterDying.PropertyNo = PropertyNo;
            YarnAndContentWeightAfterDying.SPNo = SPNo;
            YarnAndContentWeightAfterDying.NoOfSample = NoOfSample;
            YarnAndContentWeightAfterDying.NeedSP = NeedSP;
            YarnAndContentWeightAfterDying.YarnType = YarnType;

            if (null == YarnWeightAfterDying) YarnWeightAfterDying = new NRTestProperty();
            YarnWeightAfterDying.SPNo = SPNo;
            YarnWeightAfterDying.LotNo = LotNo;
            YarnWeightAfterDying.PropertyNo = PropertyNo;
            YarnWeightAfterDying.SPNo = SPNo;
            YarnWeightAfterDying.NoOfSample = NoOfSample;
            YarnWeightAfterDying.NeedSP = NeedSP;
            YarnWeightAfterDying.YarnType = YarnType;

            if (null == StandardDenierD) StandardDenierD = new NRTestProperty();
            StandardDenierD.SPNo = SPNo;
            StandardDenierD.LotNo = LotNo;
            StandardDenierD.PropertyNo = PropertyNo;
            StandardDenierD.SPNo = SPNo;
            StandardDenierD.NoOfSample = NoOfSample;
            StandardDenierD.NeedSP = NeedSP;
            StandardDenierD.YarnType = YarnType;

            if (null == StandardDenierDtex) StandardDenierDtex = new NRTestProperty();
            StandardDenierDtex.SPNo = SPNo;
            StandardDenierDtex.LotNo = LotNo;
            StandardDenierDtex.PropertyNo = PropertyNo;
            StandardDenierDtex.SPNo = SPNo;
            StandardDenierDtex.NoOfSample = NoOfSample;
            StandardDenierDtex.NeedSP = NeedSP;
            StandardDenierDtex.YarnType = YarnType;

            if (null == EquilibriumMoistureContent) EquilibriumMoistureContent = new NRTestProperty();
            EquilibriumMoistureContent.SPNo = SPNo;
            EquilibriumMoistureContent.LotNo = LotNo;
            EquilibriumMoistureContent.PropertyNo = PropertyNo;
            EquilibriumMoistureContent.SPNo = SPNo;
            EquilibriumMoistureContent.NoOfSample = NoOfSample;
            EquilibriumMoistureContent.NeedSP = NeedSP;
            EquilibriumMoistureContent.YarnType = YarnType;

            if (null == Weight) Weight = new NRTestProperty();
            Weight.SPNo = SPNo;
            Weight.LotNo = LotNo;
            Weight.PropertyNo = PropertyNo;
            Weight.SPNo = SPNo;
            Weight.NoOfSample = NoOfSample;
            Weight.NeedSP = NeedSP;
            Weight.YarnType = YarnType;

            /*
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
            */
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

        #region YarnWeightBeforeDying/ContentWeight/YarnAndContentWeightAfterDying/YarnWeightAfterDying/StandardDenierD/StandardDenierDtex/EquilibriumMoistureContent/Weight

        public NRTestProperty YarnWeightBeforeDying { get; set; }
        public NRTestProperty ContentWeight { get; set; }
        public NRTestProperty YarnAndContentWeightAfterDying { get; set; }

        public NRTestProperty YarnWeightAfterDying { get; set; }

        public NRTestProperty StandardDenierD { get; set; }
        public NRTestProperty StandardDenierDtex { get; set; }

        public NRTestProperty EquilibriumMoistureContent { get; set; }
        public NRTestProperty Weight { get; set; }

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
        internal static List<CordDenierMoistureWeight> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<CordDenierMoistureWeight> results = new List<CordDenierMoistureWeight>();
            if (null == value)
                return results;

            // For Denier (PropertyNo = 10), Moisture regain (PropertyNo = 11), Weight (PropertyNo = 14)
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

                var inst = new CordDenierMoistureWeight()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 12, // Denier (PropertyNo = 10), Moisture regain (PropertyNo = 11), Weight (PropertyNo = 14)
                    SPNo = SP,
                    NeedSP = true,
                    YarnType = value.YarnType,
                    NoOfSample = noOfSample
                };

                results.Add(inst);

                i++; // increase index
            }

            // For Denier (PropertyNo = 10), Moisture regain (PropertyNo = 11), Weight (PropertyNo = 14)
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
        public static void Clone(CordDenierMoistureWeight src, CordDenierMoistureWeight dst)
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

            NRTestProperty.Clone(src.YarnWeightBeforeDying, dst.YarnWeightBeforeDying);
            NRTestProperty.Clone(src.ContentWeight, dst.ContentWeight);
            NRTestProperty.Clone(src.YarnAndContentWeightAfterDying, dst.YarnAndContentWeightAfterDying);

            NRTestProperty.Clone(src.YarnWeightAfterDying, dst.YarnWeightAfterDying);

            NRTestProperty.Clone(src.StandardDenierD, dst.StandardDenierD);
            NRTestProperty.Clone(src.StandardDenierDtex, dst.StandardDenierDtex);

            NRTestProperty.Clone(src.EquilibriumMoistureContent, dst.EquilibriumMoistureContent);
            NRTestProperty.Clone(src.Weight, dst.Weight);
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets CordDenierMoistureWeight by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordDenierMoistureWeight>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordDenierMoistureWeight>> ret = new NDbResult<List<CordDenierMoistureWeight>>();

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
                List<CordDenierMoistureWeight> results = new List<CordDenierMoistureWeight>();

                var items = Utils.P_GetDenierMoistureWByLot.GetByLot(lotNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new CordDenierMoistureWeight();
                        inst.LotNo = item.LotNo;
                        inst.PropertyNo = 12; // Denier (PropertyNo = 10), Moisture regain (PropertyNo = 11), Weight (PropertyNo = 14)
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

        #endregion
    }

    #endregion
}
