#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using Dapper;

using NLib;
using NLib.Controls;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordShrinkagePct

    /// <summary>
    /// The Cord Shrinkage % class.
    /// </summary>
    public class CordShrinkagePct : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordShrinkagePct() : base()
        {
            LengthBeforeHeat = new NRTestProperty();
            // LengthBeforeHeat change need to calc formula for Shrinkage %
            LengthBeforeHeat.ValueChanges = CalculateFormula;

            LengthAfterHeat = new NRTestProperty();
            // LengthAfterHeat change need to calc formula for Shrinkage %
            LengthAfterHeat.ValueChanges = CalculateFormula;

            PctShrinkage = new NRTestProperty();
        }

        #endregion

        #region Private Methods

        private void CheckSpec()
        {
            if (null != Spec && null != LengthBeforeHeat && null != LengthAfterHeat && null != PctShrinkage)
            {
                // Check Shrinkage%.
                PctShrinkage.N1Out = (PctShrinkage.N1.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N1.Value) : false;
                PctShrinkage.N2Out = (PctShrinkage.N2.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N2.Value) : false;
                PctShrinkage.N3Out = (PctShrinkage.N3.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N3.Value) : false;

                PctShrinkage.N1R1Out = (PctShrinkage.N1R1.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N1R1.Value) : false;
                PctShrinkage.N1R2Out = (PctShrinkage.N1R2.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N1R2.Value) : false;
                PctShrinkage.N2R1Out = (PctShrinkage.N2R1.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N2R1.Value) : false;
                PctShrinkage.N2R2Out = (PctShrinkage.N2R2.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N2R2.Value) : false;
                PctShrinkage.N3R1Out = (PctShrinkage.N3R1.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N3R1.Value) : false;
                PctShrinkage.N3R2Out = (PctShrinkage.N3R2.HasValue) ? Spec.IsOutOfSpec(PctShrinkage.N3R2.Value) : false;

                // set out of range flag to LengthBeforeHeat object
                LengthBeforeHeat.N1Out = PctShrinkage.N1Out;
                LengthBeforeHeat.N2Out = PctShrinkage.N2Out;
                LengthBeforeHeat.N3Out = PctShrinkage.N3Out;

                LengthBeforeHeat.N1R1Out = PctShrinkage.N1R1Out;
                LengthBeforeHeat.N1R2Out = PctShrinkage.N1R2Out;
                LengthBeforeHeat.N2R1Out = PctShrinkage.N2R1Out;
                LengthBeforeHeat.N2R2Out = PctShrinkage.N2R2Out;
                LengthBeforeHeat.N3R1Out = PctShrinkage.N3R1Out;
                LengthBeforeHeat.N3R2Out = PctShrinkage.N3R2Out;

                // set out of range flag to LengthAfterHeat object
                LengthAfterHeat.N1Out = PctShrinkage.N1Out;
                LengthAfterHeat.N2Out = PctShrinkage.N2Out;
                LengthAfterHeat.N3Out = PctShrinkage.N3Out;

                LengthAfterHeat.N1R1Out = PctShrinkage.N1R1Out;
                LengthAfterHeat.N1R2Out = PctShrinkage.N1R2Out;
                LengthAfterHeat.N2R1Out = PctShrinkage.N2R1Out;
                LengthAfterHeat.N2R2Out = PctShrinkage.N2R2Out;
                LengthAfterHeat.N3R1Out = PctShrinkage.N3R1Out;
                LengthAfterHeat.N3R2Out = PctShrinkage.N3R2Out;

                // Raise items events
                PctShrinkage.RaiseNOutChanges();
                PctShrinkage.RaiseR1OutChanges();
                PctShrinkage.RaiseR2OutChanges();

                LengthBeforeHeat.RaiseNOutChanges();
                LengthBeforeHeat.RaiseR1OutChanges();
                LengthBeforeHeat.RaiseR2OutChanges();

                LengthAfterHeat.RaiseNOutChanges();
                LengthAfterHeat.RaiseR1OutChanges();
                LengthAfterHeat.RaiseR2OutChanges();
            }
        }

        private void CalculateFormula()
        {
            if (null != LengthBeforeHeat && null != LengthAfterHeat && null != PctShrinkage)
            {
                // Shrinkage % = ( (Length BF Heat – Length AF Heat) * 2
                PctShrinkage.N1 = 2 * (LengthBeforeHeat.N1.HasValue ? 
                    LengthBeforeHeat.N1.Value - (LengthAfterHeat.N1.HasValue ? LengthAfterHeat.N1.Value : decimal.Zero) : 
                    new decimal?());
                PctShrinkage.N2 = 2 * (LengthBeforeHeat.N2.HasValue ?
                    LengthBeforeHeat.N2.Value - (LengthAfterHeat.N2.HasValue ? LengthAfterHeat.N2.Value : decimal.Zero) :
                    new decimal?());
                PctShrinkage.N3 = 2 * (LengthBeforeHeat.N3.HasValue ?
                    LengthBeforeHeat.N3.Value - (LengthAfterHeat.N3.HasValue ? LengthAfterHeat.N3.Value : decimal.Zero) :
                    new decimal?());

                PctShrinkage.N1R1 = 2 * (LengthBeforeHeat.N1R1.HasValue ?
                    LengthBeforeHeat.N1R1.Value - (LengthAfterHeat.N1R1.HasValue ? LengthAfterHeat.N1R1.Value : decimal.Zero) :
                    new decimal?());
                PctShrinkage.N1R2 = 2 * (LengthBeforeHeat.N1R2.HasValue ?
                    LengthBeforeHeat.N1R2.Value - (LengthAfterHeat.N1R2.HasValue ? LengthAfterHeat.N1R2.Value : decimal.Zero) :
                    new decimal?());
                PctShrinkage.N2R1 = 2 * (LengthBeforeHeat.N2R1.HasValue ?
                    LengthBeforeHeat.N2R1.Value - (LengthAfterHeat.N2R1.HasValue ? LengthAfterHeat.N2R1.Value : decimal.Zero) :
                    new decimal?());
                PctShrinkage.N2R2 = 2 * (LengthBeforeHeat.N2R2.HasValue ?
                    LengthBeforeHeat.N2R2.Value - (LengthAfterHeat.N2R2.HasValue ? LengthAfterHeat.N2R2.Value : decimal.Zero) :
                    new decimal?());
                PctShrinkage.N3R1 = 2 * (LengthBeforeHeat.N3R1.HasValue ?
                    LengthBeforeHeat.N3R1.Value - (LengthAfterHeat.N3R1.HasValue ? LengthAfterHeat.N3R1.Value : decimal.Zero) :
                    new decimal?());
                PctShrinkage.N3R2 = 2 * (LengthBeforeHeat.N3R2.HasValue ?
                    LengthBeforeHeat.N3R2.Value - (LengthAfterHeat.N3R2.HasValue ? LengthAfterHeat.N3R2.Value : decimal.Zero) :
                    new decimal?());

                // Raise events
                Raise(() => this.PctShrinkage);

                CheckSpec(); // Check Spec
            }
        }

        private void UpdateProperties()
        {
            if (null == LengthBeforeHeat) LengthBeforeHeat = new NRTestProperty();

            LengthBeforeHeat.LotNo = LotNo;
            LengthBeforeHeat.PropertyNo = PropertyNo;
            LengthBeforeHeat.SPNo = SPNo;
            LengthBeforeHeat.NoOfSample = NoOfSample;
            LengthBeforeHeat.NeedSP = NeedSP;
            LengthBeforeHeat.YarnType = YarnType;
            LengthBeforeHeat.SampleType = SampleType;

            // Check calculate action
            if (null == LengthBeforeHeat.ValueChanges)
            {
                LengthBeforeHeat.ValueChanges = CalculateFormula;
            }

            if (null == LengthAfterHeat) LengthAfterHeat = new NRTestProperty();
            LengthAfterHeat.SPNo = SPNo;
            LengthAfterHeat.LotNo = LotNo;
            LengthAfterHeat.PropertyNo = PropertyNo;
            LengthAfterHeat.SPNo = SPNo;
            LengthAfterHeat.NoOfSample = NoOfSample;
            LengthAfterHeat.NeedSP = NeedSP;
            LengthAfterHeat.YarnType = YarnType;
            LengthAfterHeat.SampleType = SampleType;
            // Check calculate action
            if (null == LengthAfterHeat.ValueChanges)
            {
                LengthAfterHeat.ValueChanges = CalculateFormula;
            }

            if (null == PctShrinkage) PctShrinkage = new NRTestProperty();
            PctShrinkage.SPNo = SPNo;
            PctShrinkage.LotNo = LotNo;
            PctShrinkage.PropertyNo = PropertyNo;
            PctShrinkage.SPNo = SPNo;
            PctShrinkage.NoOfSample = NoOfSample;
            PctShrinkage.NeedSP = NeedSP;
            PctShrinkage.YarnType = YarnType;
            PctShrinkage.SampleType = SampleType;

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
        /// <summary>Gets or sets Sample Type.</summary>
        public string SampleType
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

        public Visibility RequestVisibility
        {
            get
            {
                var isFullCH = SampleType == "F";

                var usr = ModelCurrent.User;
                var allowReq = (null != usr && usr.Active == 1 && usr.RoleId <= 10);

                return (!isFullCH && allowReq) ? Visibility.Visible : Visibility.Collapsed;
            }
            set { }
        }

        #endregion

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

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

        #region LengthBeforeHeat/LengthAfterHear/Shrinkage%

        public NRTestProperty LengthBeforeHeat { get; set; }
        public NRTestProperty LengthAfterHeat { get; set; }
        public NRTestProperty PctShrinkage { get; set; }

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
        internal static List<CordShrinkagePct> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<CordShrinkagePct> results = new List<CordShrinkagePct>();
            if (null == value)
                return results;

            // For Shrinkage% Proepty No = 6
            int noOfSample = (null != totalN) ? totalN.NoSample : 0;
            int alllowSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            // Shrinkage% Proepty No = 6
            var spec = value.Specs.FindByPropertyNo(6);

            int i = 1;
            int iMaxLimitSP = 7;
            while (i <= iMaxLimitSP)
            {
                if (results.Count >= alllowSP)
                    break; // already reach max allow SP

                int? SP;
                string sampleType;
                switch (i)
                {
                    case 1:
                        SP = value.SP1;
                        sampleType = value.SampleType1;
                        break;
                    case 2:
                        SP = value.SP2;
                        sampleType = value.SampleType2;
                        break;
                    case 3:
                        SP = value.SP3;
                        sampleType = value.SampleType3;
                        break;
                    case 4:
                        SP = value.SP4;
                        sampleType = value.SampleType4;
                        break;
                    case 5:
                        SP = value.SP5;
                        sampleType = value.SampleType5;
                        break;
                    case 6:
                        SP = value.SP6;
                        sampleType = value.SampleType6;
                        break;
                    case 7:
                        SP = value.SP7;
                        sampleType = value.SampleType7;
                        break;
                    default:
                        SP = new int?();
                        sampleType = null;
                        break;
                }
                // Skip SP is null
                if (!SP.HasValue)
                {
                    i++; // increase index and skip to next loop.
                    continue;
                }

                var inst = new CordShrinkagePct()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 6, // Shrinkage% Proepty No = 6
                    SPNo = SP,
                    SampleType = sampleType,
                    NeedSP = true,
                    Spec = spec,
                    YarnType = value.YarnType,
                    NoOfSample = noOfSample
                };

                results.Add(inst);

                i++; // increase index
            }

            // For Shrinkage% Proepty No = 6
            var existItems = (value.MasterId.HasValue) ? GetsByLotNo(value.LotNo).Value() : null;
            if (null != existItems && null != results)
            {
                int idx = -1;
                // loop trought all initial results and fill data with the exists on database
                foreach (var item in results)
                {
                    idx = existItems.FindIndex((x) =>
                    {
                        return x.SPNo == item.SPNo && x.PropertyNo == item.PropertyNo && 
                            x.SampleType == item.SampleType;
                    });
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

            // re checking.
            if (null != results)
            {
                foreach (var item in results) item.CalculateFormula(); // calculate to update UI
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
        public static void Clone(CordShrinkagePct src, CordShrinkagePct dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;
            dst.SampleType = src.SampleType;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            NRTestProperty.Clone(src.LengthBeforeHeat, dst.LengthBeforeHeat);
            NRTestProperty.Clone(src.LengthAfterHeat, dst.LengthAfterHeat);
            NRTestProperty.Clone(src.PctShrinkage, dst.PctShrinkage);
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets CordShrinkagePct by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordShrinkagePct>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordShrinkagePct>> ret = new NDbResult<List<CordShrinkagePct>>();

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
                List<CordShrinkagePct> results = new List<CordShrinkagePct>();

                var items = Utils.P_GetShrinkageByLot.GetByLot(lotNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new CordShrinkagePct();
                        inst.LotNo = item.LotNo;
                        inst.SPNo = item.SPNo;
                        inst.SampleType = item.SampleType;

                        inst.PropertyNo = 6; // Shrinkage% Proepty No = 6

                        inst.NeedSP = true;
                        //inst.NoOfSample = 1; // ???

                        if (null != inst.LengthBeforeHeat)
                        {
                            inst.LengthBeforeHeat.SPNo = item.SPNo;
                            inst.LengthBeforeHeat.SampleType = item.SampleType;

                            inst.LengthBeforeHeat.N1 = item.LBHN1;
                            inst.LengthBeforeHeat.N2 = item.LBHN2;
                            inst.LengthBeforeHeat.N3 = item.LBHN3;

                            inst.LengthBeforeHeat.N1R1 = item.LBHN1R1;
                            inst.LengthBeforeHeat.N1R2 = item.LBHN1R2;
                            inst.LengthBeforeHeat.N2R1 = item.LBHN2R1;
                            inst.LengthBeforeHeat.N2R2 = item.LBHN2R2;
                            inst.LengthBeforeHeat.N3R1 = item.LBHN3R1;
                            inst.LengthBeforeHeat.N3R2 = item.LBHN3R2;

                            inst.LengthBeforeHeat.N1R1Flag = item.LBHN1R1Flag.HasValue ? item.LBHN1R1Flag.Value : false;
                            inst.LengthBeforeHeat.N1R2Flag = item.LBHN1R2Flag.HasValue ? item.LBHN1R2Flag.Value : false;
                            inst.LengthBeforeHeat.N2R1Flag = item.LBHN2R1Flag.HasValue ? item.LBHN2R1Flag.Value : false;
                            inst.LengthBeforeHeat.N2R2Flag = item.LBHN2R2Flag.HasValue ? item.LBHN2R2Flag.Value : false;
                            inst.LengthBeforeHeat.N3R1Flag = item.LBHN3R1Flag.HasValue ? item.LBHN3R1Flag.Value : false;
                            inst.LengthBeforeHeat.N3R2Flag = item.LBHN3R2Flag.HasValue ? item.LBHN3R2Flag.Value : false;
                        }
                        if (null != inst.LengthAfterHeat)
                        {
                            inst.LengthAfterHeat.SPNo = item.SPNo;
                            inst.LengthAfterHeat.SampleType = item.SampleType;

                            inst.LengthAfterHeat.N1 = item.LAHN1;
                            inst.LengthAfterHeat.N2 = item.LAHN2;
                            inst.LengthAfterHeat.N3 = item.LAHN3;

                            inst.LengthAfterHeat.N1R1 = item.LAHN1R1;
                            inst.LengthAfterHeat.N1R2 = item.LAHN1R2;
                            inst.LengthAfterHeat.N2R1 = item.LAHN2R1;
                            inst.LengthAfterHeat.N2R2 = item.LAHN2R2;
                            inst.LengthAfterHeat.N3R1 = item.LAHN3R1;
                            inst.LengthAfterHeat.N3R2 = item.LAHN3R2;

                            inst.LengthAfterHeat.N1R1Flag = item.LAHN1R1Flag.HasValue ? item.LAHN1R1Flag.Value : false;
                            inst.LengthAfterHeat.N1R2Flag = item.LAHN1R2Flag.HasValue ? item.LAHN1R2Flag.Value : false;
                            inst.LengthAfterHeat.N2R1Flag = item.LAHN2R1Flag.HasValue ? item.LAHN2R1Flag.Value : false;
                            inst.LengthAfterHeat.N2R2Flag = item.LAHN2R2Flag.HasValue ? item.LAHN2R2Flag.Value : false;
                            inst.LengthAfterHeat.N3R1Flag = item.LAHN3R1Flag.HasValue ? item.LAHN3R1Flag.Value : false;
                            inst.LengthAfterHeat.N3R2Flag = item.LAHN3R2Flag.HasValue ? item.LAHN3R2Flag.Value : false;
                        }

                        if (null != inst.PctShrinkage)
                        {
                            inst.PctShrinkage.SPNo = item.SPNo;
                            inst.PctShrinkage.SampleType = item.SampleType;

                            inst.PctShrinkage.N1 = item.SKN1;
                            inst.PctShrinkage.N2 = item.SKN2;
                            inst.PctShrinkage.N3 = item.SKN3;

                            inst.PctShrinkage.N1R1 = item.SKN1R1;
                            inst.PctShrinkage.N1R2 = item.SKN1R2;
                            inst.PctShrinkage.N2R1 = item.SKN2R1;
                            inst.PctShrinkage.N2R2 = item.SKN2R2;
                            inst.PctShrinkage.N3R1 = item.SKN3R1;
                            inst.PctShrinkage.N3R2 = item.SKN3R2;

                            inst.PctShrinkage.N1R1Flag = item.SKN1R1Flag.HasValue ? item.SKN1R1Flag.Value : false;
                            inst.PctShrinkage.N1R2Flag = item.SKN1R2Flag.HasValue ? item.SKN1R2Flag.Value : false;
                            inst.PctShrinkage.N2R1Flag = item.SKN2R1Flag.HasValue ? item.SKN2R1Flag.Value : false;
                            inst.PctShrinkage.N2R2Flag = item.SKN2R2Flag.HasValue ? item.SKN2R2Flag.Value : false;
                            inst.PctShrinkage.N3R1Flag = item.SKN3R1Flag.HasValue ? item.SKN3R1Flag.Value : false;
                            inst.PctShrinkage.N3R2Flag = item.SKN3R2Flag.HasValue ? item.SKN3R2Flag.Value : false;
                        }

                        inst.SampleType = item.SampleType;
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
        public static NDbResult<CordShrinkagePct> Save(CordShrinkagePct value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordShrinkagePct> ret = new NDbResult<CordShrinkagePct>();

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

            p.Add("@lbhn1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N1 : new decimal?());
            p.Add("@lbhn2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N2 : new decimal?());
            p.Add("@lbhn3", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N3 : new decimal?());

            p.Add("@lbhn1r1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N1R1 : new decimal?());
            p.Add("@lbhn1r2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N1R2 : new decimal?());
            p.Add("@lbhn2r1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N2R1 : new decimal?());
            p.Add("@lbhn2r2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N2R2 : new decimal?());
            p.Add("@lbhn3r1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N3R1 : new decimal?());
            p.Add("@lbhn3r2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N3R2 : new decimal?());

            p.Add("@lbhn1r1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N1R1 : new decimal?());
            p.Add("@lbhn1r2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N1R2 : new decimal?());
            p.Add("@lbhn2r1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N2R1 : new decimal?());
            p.Add("@lbhn2r2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N2R2 : new decimal?());
            p.Add("@lbhn3r1", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N3R1 : new decimal?());
            p.Add("@lbhn3r2", (null != value.LengthBeforeHeat) ? value.LengthBeforeHeat.N3R2 : new decimal?());

            p.Add("@lahn1", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N1 : new decimal?());
            p.Add("@lahn2", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N2 : new decimal?());
            p.Add("@lahn3", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N3 : new decimal?());

            p.Add("@lahn1r1", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N1R1 : new decimal?());
            p.Add("@lahn1r2", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N1R2 : new decimal?());
            p.Add("@lahn2r1", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N2R1 : new decimal?());
            p.Add("@lahn2r2", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N2R2 : new decimal?());
            p.Add("@lahn3r1", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N3R1 : new decimal?());
            p.Add("@lahn3r2", (null != value.LengthAfterHeat) ? value.LengthAfterHeat.N3R2 : new decimal?());

            p.Add("@skn1", (null != value.PctShrinkage) ? value.PctShrinkage.N1 : new decimal?());
            p.Add("@skn2", (null != value.PctShrinkage) ? value.PctShrinkage.N2 : new decimal?());
            p.Add("@skn3", (null != value.PctShrinkage) ? value.PctShrinkage.N3 : new decimal?());
            
            p.Add("@skn1r1", (null != value.PctShrinkage) ? value.PctShrinkage.N1R1 : new decimal?());
            p.Add("@skn1r2", (null != value.PctShrinkage) ? value.PctShrinkage.N1R2 : new decimal?());
            p.Add("@skn2r1", (null != value.PctShrinkage) ? value.PctShrinkage.N2R1 : new decimal?());
            p.Add("@skn2r2", (null != value.PctShrinkage) ? value.PctShrinkage.N2R2 : new decimal?());
            p.Add("@skn3r1", (null != value.PctShrinkage) ? value.PctShrinkage.N3R1 : new decimal?());
            p.Add("@skn3r2", (null != value.PctShrinkage) ? value.PctShrinkage.N3R2 : new decimal?());

            p.Add("@skn1r1flag", (null != value.PctShrinkage) ? (value.PctShrinkage.N1R1Flag) ? true : new bool?() : new bool?());
            p.Add("@skn1r2flag", (null != value.PctShrinkage) ? (value.PctShrinkage.N1R2Flag) ? true : new bool?() : new bool?());
            p.Add("@skn2r1flag", (null != value.PctShrinkage) ? (value.PctShrinkage.N2R1Flag) ? true : new bool?() : new bool?());
            p.Add("@skn2r2flag", (null != value.PctShrinkage) ? (value.PctShrinkage.N2R2Flag) ? true : new bool?() : new bool?());
            p.Add("@skn3r1flag", (null != value.PctShrinkage) ? (value.PctShrinkage.N3R1Flag) ? true : new bool?() : new bool?());
            p.Add("@skn3r2flag", (null != value.PctShrinkage) ? (value.PctShrinkage.N3R2Flag) ? true : new bool?() : new bool?());

            p.Add("@sampletype", (string.IsNullOrWhiteSpace(value.SampleType) || value.SampleType != "F")
                ? "S" : value.SampleType);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveShrinkage", p, commandType: CommandType.StoredProcedure);
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
