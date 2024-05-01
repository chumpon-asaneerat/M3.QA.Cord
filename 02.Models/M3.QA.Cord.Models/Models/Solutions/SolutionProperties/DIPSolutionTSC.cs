#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region DIPSolutionTSC

    /// <summary>
    /// The DIP Solution TSC class.
    /// </summary>
    public class DIPSolutionTSC : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPSolutionTSC() : base()
        {
            BreakerWeight = new NRTestProperty();
            // Set calc formula
            BreakerWeight.ValueChanges = CalculateFormula;

            BreakerWeightBeforeHeat = new NRTestProperty();
            // Set calc formula
            BreakerWeight.ValueChanges = CalculateFormula;

            BreakerWeightAfterHeat = new NRTestProperty();
            // Set calc formula
            BreakerWeight.ValueChanges = CalculateFormula;

            RPU = new NRTestProperty();
        }

        #endregion

        #region Private Methods

        private void CalculateFormula()
        {

        }

        private void UpdateProperties()
        {
            if (null == BreakerWeight) BreakerWeight = new NRTestProperty();

            BreakerWeight.LotNo = LotNo;
            BreakerWeight.PropertyNo = PropertyNo;
            BreakerWeight.NoOfSample = NoOfSample;
            BreakerWeight.NeedSP = NeedSP;
            // Check calculate action
            if (null == BreakerWeight.ValueChanges)
            {
                BreakerWeight.ValueChanges = CalculateFormula;
            }

            if (null == BreakerWeightBeforeHeat) BreakerWeightBeforeHeat = new NRTestProperty();
            BreakerWeightBeforeHeat.LotNo = LotNo;
            BreakerWeightBeforeHeat.PropertyNo = PropertyNo;
            BreakerWeightBeforeHeat.NoOfSample = NoOfSample;
            BreakerWeightBeforeHeat.NeedSP = NeedSP;
            // Check calculate action
            if (null == BreakerWeightBeforeHeat.ValueChanges)
            {
                BreakerWeightBeforeHeat.ValueChanges = CalculateFormula;
            }

            if (null == BreakerWeightAfterHeat) BreakerWeightAfterHeat = new NRTestProperty();
            BreakerWeightAfterHeat.LotNo = LotNo;
            BreakerWeightAfterHeat.PropertyNo = PropertyNo;
            BreakerWeightAfterHeat.NoOfSample = NoOfSample;
            BreakerWeightAfterHeat.NeedSP = NeedSP;
            // Check calculate action
            if (null == BreakerWeightAfterHeat.ValueChanges)
            {
                BreakerWeightAfterHeat.ValueChanges = CalculateFormula;
            }

            if (null == RPU) RPU = new NRTestProperty();
            RPU.LotNo = LotNo;
            RPU.PropertyNo = PropertyNo;
            RPU.NoOfSample = NoOfSample;
            RPU.NeedSP = NeedSP;

            CalculateFormula(); // calculate

            this.Raise(() => this.EnableTest);
        }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/NoOfSample

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

        #endregion

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return true; }
            set { }
        }

        #endregion

        #region BreakerWeight/BreakerWeightBeforeHeat/BreakerWeightAfterHeat/RPU

        /// <summary>Gets or sets BreakerWeight.</summary>
        public NRTestProperty BreakerWeight { get; set; }
        /// <summary>Gets or sets BreakerWeightBeforeHeat.</summary>
        public NRTestProperty BreakerWeightBeforeHeat { get; set; }
        /// <summary>Gets or sets BreakerWeightAfterHeat.</summary>
        public NRTestProperty BreakerWeightAfterHeat { get; set; }
        /// <summary>Gets or sets RPU.</summary>
        public NRTestProperty RPU { get; set; }

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
        public static void Clone(DIPSolutionTSC src, DIPSolutionTSC dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.NoOfSample = src.NoOfSample;
            dst.NeedSP = src.NeedSP;

            dst.Spec = src.Spec;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            NRTestProperty.Clone(src.BreakerWeight, dst.BreakerWeight);
            NRTestProperty.Clone(src.BreakerWeightBeforeHeat, dst.BreakerWeightBeforeHeat);
            NRTestProperty.Clone(src.BreakerWeightAfterHeat, dst.BreakerWeightAfterHeat);
            NRTestProperty.Clone(src.RPU, dst.RPU);
        }

        #endregion

        #region Create

        /*
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="totalN"></param>
        /// <returns></returns>
        internal static List<CordAdhesionForce> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<CordAdhesionForce> results = new List<CordAdhesionForce>();
            if (null == value)
                return results;

            // For Adhesion Force Proepty No = 4
            int noOfSample = (null != totalN) ? totalN.NoSample : 0;
            int alllowSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            // Adhesion Force Proepty No = 4
            var spec = value.Specs.FindByPropertyNo(4);

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

                var inst = new CordAdhesionForce()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 4, // Adhesion Force = 4
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
                        existItems[idx].Spec = spec; // assign spec
                        // Clone anther properties
                        Clone(existItems[idx], item);
                    }
                }
            }

            return results;
        }
        */
        #endregion

        #endregion
    }

    #endregion
}
