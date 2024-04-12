#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Dapper;

using NLib;
using NLib.Controls;
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
            YarnWeightBeforeDrying = new NRTestProperty();
            // BeforeHeat change need to calc formula for Yarn Weight
            YarnWeightBeforeDrying.ValueChanges = CalculateFormula;

            ContentWeight = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            ContentWeight.ValueChanges = CalculateFormula;

            YarnAndContentWeightAfterDrying = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Weight
            YarnAndContentWeightAfterDrying.ValueChanges = CalculateFormula;

            YarnWeightAfterDrying = new NRTestProperty();
            // AfterHeat change need to calc formula for Yarn Type
            YarnWeightAfterDrying.ValueChanges = CalculateDenierFormula;

            StandardDenierD = new NRTestProperty();
            StandardDenierDtex = new NRTestProperty();
            EquilibriumMoistureContent = new NRTestProperty();
            Weight = new NRTestProperty();
        }

        #endregion

        #region Private Methods

        private void CalculateFormula()
        {
            if (null != YarnAndContentWeightAfterDrying && null != ContentWeight && null != YarnWeightAfterDrying)
            {
                // YarnWeightAfterDrying = YarnAndContentWeightAfterDrying – ContentWeight
                YarnWeightAfterDrying.N1 = (YarnAndContentWeightAfterDrying.N1.HasValue ?
                    YarnAndContentWeightAfterDrying.N1 - (ContentWeight.N1.HasValue ? ContentWeight.N1.Value : decimal.Zero) :
                    new decimal?());

                YarnWeightAfterDrying.R1 = (YarnAndContentWeightAfterDrying.R1.HasValue ?
                    YarnAndContentWeightAfterDrying.R1 - (ContentWeight.R1.HasValue ? ContentWeight.R1.Value : decimal.Zero) :
                    new decimal?());

                Raise(() => this.YarnWeightAfterDrying);
            }

            CalculateDenierFormula();
            CalculateMoistureFormula();
            CalculateWeightFormula();
        }

        private void CalculateDenierFormula()
        {
            if (null != YarnWeightAfterDrying && null != StandardDenierD && null != StandardDenierDtex)
            {
                if (!string.IsNullOrWhiteSpace(YarnType) && string.Compare(YarnType, "Polyester", true) == 0)
                {
                    // Polyester : StandardDenierD = YarnWeightAfterDrying * 400.16
                    decimal dFactor = (decimal)400.16;
                    StandardDenierD.N1 = (YarnWeightAfterDrying.N1.HasValue) ? 
                        YarnWeightAfterDrying.N1.Value * dFactor : new decimal?();
                    StandardDenierD.R1 = (YarnWeightAfterDrying.R1.HasValue) ?
                        YarnWeightAfterDrying.R1.Value * dFactor : new decimal?();

                    // Polyester: StandardDenierDtex = YarnWeightAfterDrying * 418
                    decimal dtexFactor = (decimal)418;
                    StandardDenierDtex.N1 = (YarnWeightAfterDrying.N1.HasValue) ?
                        YarnWeightAfterDrying.N1.Value * dtexFactor : new decimal?();
                    StandardDenierDtex.R1 = (YarnWeightAfterDrying.R1.HasValue) ?
                        YarnWeightAfterDrying.R1.Value * dtexFactor : new decimal?();

                    // Raise events
                    Raise(() => this.StandardDenierD);
                    Raise(() => this.StandardDenierDtex);
                }
                else if (!string.IsNullOrWhiteSpace(YarnType) && string.Compare(YarnType, "Nylon", true) == 0)
                {
                    // Nylon: StandardDenierD = YarnWeightAfterDrying * 444.18
                    decimal dFactor = (decimal)444.18;
                    StandardDenierD.N1 = (YarnWeightAfterDrying.N1.HasValue) ?
                        YarnWeightAfterDrying.N1.Value * dFactor : new decimal?();
                    StandardDenierD.R1 = (YarnWeightAfterDrying.R1.HasValue) ?
                        YarnWeightAfterDrying.R1.Value * dFactor : new decimal?();

                    // Nylon: StandardDenierDtex = YarnWeightAfterDrying * 463.98
                    decimal dtexFactor = (decimal)463.98;
                    StandardDenierDtex.N1 = (YarnWeightAfterDrying.N1.HasValue) ?
                        YarnWeightAfterDrying.N1.Value * dtexFactor : new decimal?();
                    StandardDenierDtex.R1 = (YarnWeightAfterDrying.R1.HasValue) ?
                        YarnWeightAfterDrying.R1.Value * dtexFactor : new decimal?();

                    // Raise events
                    Raise(() => this.StandardDenierD);
                    Raise(() => this.StandardDenierDtex);
                }
            }
        }

        private void CalculateMoistureFormula()
        {
            if (null != YarnWeightBeforeDrying && null != YarnWeightAfterDrying && null != EquilibriumMoistureContent)
            {
                // Moisture = ((YarnWeightBeforeDrying - YarnWeightAfterDrying) / 4) * 100
                EquilibriumMoistureContent.N1 = (YarnWeightBeforeDrying.N1.HasValue) ?
                    (YarnWeightBeforeDrying.N1 - ((YarnWeightAfterDrying.N1.HasValue) ? YarnWeightAfterDrying.N1.Value : decimal.Zero) / 4) * 100 : new decimal?();
                EquilibriumMoistureContent.R1 = (YarnWeightBeforeDrying.R1.HasValue) ?
                    (YarnWeightBeforeDrying.R1 - ((YarnWeightAfterDrying.R1.HasValue) ? YarnWeightAfterDrying.R1.Value : decimal.Zero) / 4) * 100 : new decimal?();

                // Raise events
                Raise(() => this.EquilibriumMoistureContent);
            }
        }

        private void CalculateWeightFormula()
        {
            // Weight = YarnWeightAfterDrying  / 22.5
            if (null != YarnWeightAfterDrying && null != Weight)
            {
                decimal wFactor = (decimal)22.5;
                Weight.N1 = (YarnWeightAfterDrying.N1.HasValue) ? YarnWeightAfterDrying.N1.Value / wFactor : new decimal?();
                Weight.R1 = (YarnWeightAfterDrying.R1.HasValue) ? YarnWeightAfterDrying.R1.Value / wFactor : new decimal?();

                // Raise events
                Raise(() => this.Weight);
            }
        }

        private void UpdateProperties()
        {
            UpdateDenierProperties();
            UpdateMoistureProperties();
            UpdateWeightProperties();

            CalculateFormula(); // calculate

            this.Raise(() => this.EnableTest);
        }

        private void UpdateDenierProperties()
        {
            if (null == YarnWeightBeforeDrying) YarnWeightBeforeDrying = new NRTestProperty();
            if (null == YarnWeightBeforeDrying.ValueChanges)
            {
                YarnWeightBeforeDrying.ValueChanges = CalculateFormula;
            }
            YarnWeightBeforeDrying.LotNo = LotNo;
            YarnWeightBeforeDrying.PropertyNo = PropertyNo1; // Denier Property No = 10
            YarnWeightBeforeDrying.SPNo = SPNo;
            YarnWeightBeforeDrying.NoOfSample = NoOfSample;
            YarnWeightBeforeDrying.NeedSP = NeedSP;
            YarnWeightBeforeDrying.YarnType = YarnType;

            if (null == ContentWeight) ContentWeight = new NRTestProperty();
            if (null == ContentWeight.ValueChanges)
            {
                ContentWeight.ValueChanges = CalculateFormula;
            }
            ContentWeight.SPNo = SPNo;
            ContentWeight.LotNo = LotNo;
            ContentWeight.PropertyNo = PropertyNo1; // Denier Property No = 10
            ContentWeight.SPNo = SPNo;
            ContentWeight.NoOfSample = NoOfSample;
            ContentWeight.NeedSP = NeedSP;
            ContentWeight.YarnType = YarnType;

            if (null == YarnAndContentWeightAfterDrying) YarnAndContentWeightAfterDrying = new NRTestProperty();
            if (null == YarnAndContentWeightAfterDrying.ValueChanges)
            {
                YarnAndContentWeightAfterDrying.ValueChanges = CalculateFormula;
            }
            YarnAndContentWeightAfterDrying.SPNo = SPNo;
            YarnAndContentWeightAfterDrying.LotNo = LotNo;
            YarnAndContentWeightAfterDrying.PropertyNo = PropertyNo1; // Denier Property No = 10
            YarnAndContentWeightAfterDrying.SPNo = SPNo;
            YarnAndContentWeightAfterDrying.NoOfSample = NoOfSample;
            YarnAndContentWeightAfterDrying.NeedSP = NeedSP;
            YarnAndContentWeightAfterDrying.YarnType = YarnType;

            if (null == YarnWeightAfterDrying) YarnWeightAfterDrying = new NRTestProperty();
            if (null == YarnWeightAfterDrying.ValueChanges)
            {
                YarnWeightAfterDrying.ValueChanges = CalculateFormula;
            }
            YarnWeightAfterDrying.SPNo = SPNo;
            YarnWeightAfterDrying.LotNo = LotNo;
            YarnWeightAfterDrying.PropertyNo = PropertyNo1; // Denier Property No = 10
            YarnWeightAfterDrying.SPNo = SPNo;
            YarnWeightAfterDrying.NoOfSample = NoOfSample;
            YarnWeightAfterDrying.NeedSP = NeedSP;
            YarnWeightAfterDrying.YarnType = YarnType;

            if (null == StandardDenierD) StandardDenierD = new NRTestProperty();
            StandardDenierD.SPNo = SPNo;
            StandardDenierD.LotNo = LotNo;
            StandardDenierD.PropertyNo = PropertyNo1; // Denier Property No = 10
            StandardDenierD.SPNo = SPNo;
            StandardDenierD.NoOfSample = NoOfSample;
            StandardDenierD.NeedSP = NeedSP;
            StandardDenierD.YarnType = YarnType;

            if (null == StandardDenierDtex) StandardDenierDtex = new NRTestProperty();
            StandardDenierDtex.SPNo = SPNo;
            StandardDenierDtex.LotNo = LotNo;
            StandardDenierDtex.PropertyNo = PropertyNo1; // Denier Property No = 10
            StandardDenierDtex.SPNo = SPNo;
            StandardDenierDtex.NoOfSample = NoOfSample;
            StandardDenierDtex.NeedSP = NeedSP;
            StandardDenierDtex.YarnType = YarnType;
        }

        private void UpdateMoistureProperties()
        {
            if (null == EquilibriumMoistureContent) EquilibriumMoistureContent = new NRTestProperty();
            EquilibriumMoistureContent.SPNo = SPNo;
            EquilibriumMoistureContent.LotNo = LotNo;
            EquilibriumMoistureContent.PropertyNo = PropertyNo2; // Moisture Property No = 11
            EquilibriumMoistureContent.SPNo = SPNo;
            EquilibriumMoistureContent.NoOfSample = NoOfSample;
            EquilibriumMoistureContent.NeedSP = NeedSP;
            EquilibriumMoistureContent.YarnType = YarnType;
        }

        private void UpdateWeightProperties()
        {
            if (null == Weight) Weight = new NRTestProperty();
            Weight.SPNo = SPNo;
            Weight.LotNo = LotNo;
            Weight.PropertyNo = PropertyNo3; // Weight Property No = 14
            Weight.SPNo = SPNo;
            Weight.NoOfSample = NoOfSample;
            Weight.NeedSP = NeedSP;
            Weight.YarnType = YarnType;
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
        /// <summary>Gets or sets Property No 1.</summary>
        public int PropertyNo1
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateDenierProperties();
                });
            }
        }
        /// <summary>Gets or sets Property No 2.</summary>
        public int PropertyNo2
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateMoistureProperties();
                });
            }
        }
        /// <summary>Gets or sets Property No 3.</summary>
        public int PropertyNo3
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateWeightProperties();
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

        #region YarnWeightBeforeDrying/ContentWeight/YarnAndContentWeightAfterDrying/YarnWeightAfterDying/StandardDenierD/StandardDenierDtex/EquilibriumMoistureContent/Weight

        public NRTestProperty YarnWeightBeforeDrying { get; set; }
        public NRTestProperty ContentWeight { get; set; }
        public NRTestProperty YarnAndContentWeightAfterDrying { get; set; }

        public NRTestProperty YarnWeightAfterDrying { get; set; }

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
            Utils.M_GetPropertyTotalNByItem itemDenier,
            Utils.M_GetPropertyTotalNByItem itemMoisture,
            Utils.M_GetPropertyTotalNByItem itemWeight)
        {
            List<CordDenierMoistureWeight> results = new List<CordDenierMoistureWeight>();
            if (null == value)
                return results;

            // For Denier (PropertyNo = 10), Moisture regain (PropertyNo = 11), Weight (PropertyNo = 14)
            int noOfSample = 0;
            if (null != itemDenier) 
                noOfSample = Math.Max(noOfSample, itemDenier.NoSample); // must be 1 sample
            if (null != itemMoisture)
                noOfSample = Math.Max(noOfSample, itemMoisture.NoSample); // must be 1 sample
            if (null != itemWeight)
                noOfSample = Math.Max(noOfSample, itemWeight.NoSample); // must be 1 sample

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
                    PropertyNo1 = 10, // Denier (PropertyNo = 10)
                    PropertyNo2 = 11, // Moisture regain (PropertyNo = 11)
                    PropertyNo3 = 14, // Weight (PropertyNo = 14)
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
                        return x.SPNo == item.SPNo;
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
            dst.PropertyNo1 = src.PropertyNo1;
            dst.PropertyNo2 = src.PropertyNo2;
            dst.PropertyNo3 = src.PropertyNo3;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            NRTestProperty.Clone(src.YarnWeightBeforeDrying, dst.YarnWeightBeforeDrying);
            NRTestProperty.Clone(src.ContentWeight, dst.ContentWeight);
            NRTestProperty.Clone(src.YarnAndContentWeightAfterDrying, dst.YarnAndContentWeightAfterDrying);

            NRTestProperty.Clone(src.YarnWeightAfterDrying, dst.YarnWeightAfterDrying);

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

                var items = Utils.P_GetDenierMoistureWLot.GetByLot(lotNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new CordDenierMoistureWeight();
                        inst.LotNo = item.LotNo;
                        inst.PropertyNo1 = 10; // Denier PropertyNo = 10
                        inst.PropertyNo2 = 11; // Moisture regain PropertyNo = 11
                        inst.PropertyNo3 = 14; // Weight PropertyNo = 14
                        inst.SPNo = item.SPNo;

                        inst.NeedSP = true;
                        //inst.NoOfSample = 1; // ???

                        if (null != inst.YarnWeightBeforeDrying)
                        {
                            inst.YarnWeightBeforeDrying.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.YarnWeightBeforeDrying.N1 = item.YWBDN1;
                            inst.YarnWeightBeforeDrying.R1 = item.YWBDR1;
                        }
                        if (null != inst.ContentWeight)
                        {
                            inst.ContentWeight.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.ContentWeight.N1 = item.CWN1;
                            inst.ContentWeight.R1 = item.CWR1;
                        }
                        if (null != inst.YarnAndContentWeightAfterDrying)
                        {
                            inst.YarnAndContentWeightAfterDrying.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.YarnAndContentWeightAfterDrying.N1 = item.YCWADN1;
                            inst.YarnAndContentWeightAfterDrying.R1 = item.YCWADR1;
                        }
                        if (null != inst.YarnWeightAfterDrying)
                        {
                            inst.YarnWeightAfterDrying.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.YarnWeightAfterDrying.N1 = item.YWADN1;
                            inst.YarnWeightAfterDrying.R1 = item.YWADR1;
                        }
                        if (null != inst.StandardDenierD)
                        {
                            inst.StandardDenierD.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.StandardDenierD.N1 = item.DENIER_D_N1;
                            inst.StandardDenierD.R1 = item.DENIER_D_R1;
                        }
                        if (null != inst.StandardDenierDtex)
                        {
                            inst.StandardDenierD.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.StandardDenierDtex.N1 = item.DENIER_Dtex_N1;
                            inst.StandardDenierDtex.R1 = item.DENIER_Dtex_R1;
                        }
                        if (null != inst.EquilibriumMoistureContent)
                        {
                            inst.EquilibriumMoistureContent.PropertyNo = 11; // Moisture regain PropertyNo = 11
                            inst.EquilibriumMoistureContent.N1 = item.MOISTURE_N1;
                            inst.EquilibriumMoistureContent.R1 = item.MOISTURE_R1;
                        }
                        if (null != inst.Weight)
                        {
                            inst.Weight.PropertyNo = 14; // Weight PropertyNo = 14
                            inst.Weight.N1 = item.WEIGHT_N1;
                            inst.Weight.R1 = item.WEIGHT_R1;
                        }

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
        public static NDbResult<CordDenierMoistureWeight> Save(CordDenierMoistureWeight value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordDenierMoistureWeight> ret = new NDbResult<CordDenierMoistureWeight>();

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

            p.Add("@ywbdn1", (null != value.YarnWeightBeforeDrying) ? value.YarnWeightBeforeDrying.N1 : new decimal?());
            p.Add("@ywbdr1", (null != value.YarnWeightBeforeDrying) ? value.YarnWeightBeforeDrying.R1 : new decimal?());

            p.Add("@cwn1", (null != value.ContentWeight) ? value.ContentWeight.N1 : new decimal?());
            p.Add("@cwr1", (null != value.ContentWeight) ? value.ContentWeight.R1 : new decimal?());

            p.Add("@ycwadn1", (null != value.YarnAndContentWeightAfterDrying) ? value.YarnAndContentWeightAfterDrying.N1 : new decimal?());
            p.Add("@ycwadr1", (null != value.YarnAndContentWeightAfterDrying) ? value.YarnAndContentWeightAfterDrying.R1 : new decimal?());

            p.Add("@ywadn1", (null != value.YarnWeightAfterDrying) ? value.YarnWeightAfterDrying.N1 : new decimal?());
            p.Add("@ywadr1", (null != value.YarnWeightAfterDrying) ? value.YarnWeightAfterDrying.R1 : new decimal?());

            p.Add("@denierdn1", (null != value.StandardDenierD) ? value.StandardDenierD.N1 : new decimal?());
            p.Add("@denierdr1", (null != value.StandardDenierD) ? value.StandardDenierD.R1 : new decimal?());

            p.Add("@denierdtexn1", (null != value.StandardDenierDtex) ? value.StandardDenierDtex.N1 : new decimal?());
            p.Add("@denierdtexr1", (null != value.StandardDenierDtex) ? value.StandardDenierDtex.R1 : new decimal?());

            p.Add("@moisturen1", (null != value.EquilibriumMoistureContent) ? value.EquilibriumMoistureContent.N1 : new decimal?());
            p.Add("@moisturer1", (null != value.EquilibriumMoistureContent) ? value.EquilibriumMoistureContent.R1 : new decimal?());

            p.Add("@weightn1", (null != value.Weight) ? value.Weight.N1 : new decimal?());
            p.Add("@weightr1", (null != value.Weight) ? value.Weight.R1 : new decimal?());


            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveDenierMoistureW", p, commandType: CommandType.StoredProcedure);
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
