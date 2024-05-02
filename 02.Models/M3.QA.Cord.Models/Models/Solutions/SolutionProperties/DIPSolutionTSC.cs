#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

        private void CheckSpec()
        {

        }

        private void CalculateFormula()
        {
            // A = BreakerWeight
            // B = BreakerWeightBeforeHeat
            // C = BreakerWeightAfterHeat
            if (null != BreakerWeight && null != BreakerWeightBeforeHeat && 
                null != BreakerWeightAfterHeat && null != RPU)
            {
                var A = BreakerWeight;
                var B = BreakerWeightBeforeHeat;
                var C = BreakerWeightAfterHeat;
                // X = C - A
                decimal? XN1 = (C.N1.HasValue) ? C.N1.Value - ((A.N1.HasValue) ? A.N1.Value : new decimal?()) : new decimal?();
                decimal? XN2 = (C.N2.HasValue) ? C.N2.Value - ((A.N2.HasValue) ? A.N2.Value : new decimal?()) : new decimal?();
                decimal? XR1 = (C.R1.HasValue) ? C.R1.Value - ((A.R1.HasValue) ? A.R1.Value : new decimal?()) : new decimal?();
                decimal? XR2 = (C.R2.HasValue) ? C.R2.Value - ((A.R2.HasValue) ? A.R2.Value : new decimal?()) : new decimal?();
                // Y = B - A
                decimal? YN1 = (B.N1.HasValue) ? B.N1.Value - ((A.N1.HasValue) ? A.N1.Value : new decimal?()) : new decimal?();
                decimal? YN2 = (B.N2.HasValue) ? B.N2.Value - ((A.N2.HasValue) ? A.N2.Value : new decimal?()) : new decimal?();
                decimal? YR1 = (B.R1.HasValue) ? B.R1.Value - ((A.R1.HasValue) ? A.R1.Value : new decimal?()) : new decimal?();
                decimal? YR2 = (B.R2.HasValue) ? B.R2.Value - ((A.R2.HasValue) ? A.R2.Value : new decimal?()) : new decimal?();
                // Z = X/Y
                decimal? ZN1 = new decimal?();
                decimal? ZN2 = new decimal?();
                decimal? ZR1 = new decimal?();
                decimal? ZR2 = new decimal?();
                if (YN1.HasValue && YN1.Value > 0)
                {
                    try
                    {
                        ZN1 = (XN1.HasValue) ? XN1.Value / ((YN1.HasValue) ? YN1.Value : new decimal?()) : new decimal?();
                    }
                    catch 
                    { 
                        // devide by zero
                        ZN1 = new decimal?(); 
                    }
                }
                if (YN2.HasValue && YN2.Value > 0)
                {
                    try
                    {
                        ZN2 = (XN2.HasValue) ? XN2.Value / ((YN2.HasValue) ? YN2.Value : new decimal?()) : new decimal?();
                    }
                    catch
                    {
                        // devide by zero
                        ZN2 = new decimal?();
                    }
                }
                if (YR1.HasValue && YR1.Value > 0)
                {
                    try
                    {
                        ZR1 = (XN1.HasValue) ? XR1.Value / ((YR1.HasValue) ? YR1.Value : new decimal?()) : new decimal?();
                    }
                    catch
                    {
                        // devide by zero
                        ZR1 = new decimal?();
                    }
                }
                if (YR2.HasValue && YR2.Value > 0)
                {
                    try
                    {
                        ZR2 = (XR2.HasValue) ? XR2.Value / ((YR2.HasValue) ? YR2.Value : new decimal?()) : new decimal?();
                    }
                    catch
                    {
                        // devide by zero
                        ZR2 = new decimal?();
                    }
                }

                RPU.N1 = (ZN1.HasValue) ? ZN1.Value / 100 : new decimal?();
                RPU.N2 = (ZN2.HasValue) ? ZN2.Value / 100 : new decimal?();
                RPU.R1 = (ZR1.HasValue) ? ZR1.Value / 100 : new decimal?();
                RPU.R2 = (ZR2.HasValue) ? ZR2.Value / 100 : new decimal?();

                // Raise events
                Raise(() => this.RPU);

                CheckSpec(); // Check Spec
            }
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

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="breakWN1"></param>
        /// <param name="breakWN2"></param>
        /// <param name="breakWR1"></param>
        /// <param name="breakWR2"></param>
        /// <param name="breakWBHN1"></param>
        /// <param name="breakWBHN2"></param>
        /// <param name="breakWBHR1"></param>
        /// <param name="breakWBHR2"></param>
        /// <param name="breakWAHN1"></param>
        /// <param name="breakWAHN2"></param>
        /// <param name="breakWAHR1"></param>
        /// <param name="breakWAHR2"></param>
        /// <returns></returns>
        internal static DIPSolutionTSC Create(DIPSolutionSampleTestData value,
            decimal? breakWN1, decimal? breakWN2,
            decimal? breakWR1, decimal? breakWR2,
            decimal? breakWBHN1, decimal? breakWBHN2,
            decimal? breakWBHR1, decimal? breakWBHR2,
            decimal? breakWAHN1, decimal? breakWAHN2,
            decimal? breakWAHR1, decimal? breakWAHR2,
            Func<bool> allowReTest)
        {
            DIPSolutionTSC result = null;
            if (null == value)
                return result;


            // TSC Proepty No = 13
            var spec = value.Specs.FindByPropertyNo(13);
            int noOfSample = (null != spec) ? spec.NoSample : 0;

            result = new DIPSolutionTSC();
            result.LotNo = value.LotNo;
            result.PropertyNo = 13; // TSC = 13
            result.NeedSP = false;
            result.Spec = spec;
            result.NoOfSample = noOfSample;

            // Break Weight
            if (null == result.BreakerWeight) result.BreakerWeight = new NRTestProperty();
            // Set Common properties
            result.BreakerWeight.LotNo = result.LotNo;
            result.BreakerWeight.PropertyNo = result.PropertyNo;
            result.BreakerWeight.NeedSP = result.NeedSP;
            result.BreakerWeight.NoOfSample = result.NoOfSample;
            result.BreakerWeight.AllowReTest = allowReTest;
            result.BreakerWeight.Spec = result.Spec;
            // Set calc formula
            if (null == result.BreakerWeight.ValueChanges)
            {
                result.BreakerWeight.ValueChanges = result.CalculateFormula;
            }
            // Set N/R
            result.BreakerWeight.N1 = breakWN1;
            result.BreakerWeight.N2 = breakWN2;
            result.BreakerWeight.R1 = breakWR1;
            result.BreakerWeight.R2 = breakWR2;

            // Break Weight Before Heat
            if (null == result.BreakerWeightBeforeHeat) result.BreakerWeightBeforeHeat = new NRTestProperty();
            // Set Common properties
            result.BreakerWeightBeforeHeat.LotNo = result.LotNo;
            result.BreakerWeightBeforeHeat.PropertyNo = result.PropertyNo;
            result.BreakerWeightBeforeHeat.NeedSP = result.NeedSP;
            result.BreakerWeightBeforeHeat.NoOfSample = result.NoOfSample;
            result.BreakerWeightBeforeHeat.AllowReTest = allowReTest;
            result.BreakerWeightBeforeHeat.Spec = result.Spec;
            // Set calc formula
            if (null == result.BreakerWeightBeforeHeat.ValueChanges)
            {
                result.BreakerWeightBeforeHeat.ValueChanges = result.CalculateFormula;
            }
            // Set N/R
            result.BreakerWeightBeforeHeat.N1 = breakWBHN1;
            result.BreakerWeightBeforeHeat.N2 = breakWBHN2;
            result.BreakerWeightBeforeHeat.R1 = breakWBHR1;
            result.BreakerWeightBeforeHeat.R2 = breakWBHR2;

            // Break Weight After Heat
            if (null == result.BreakerWeightAfterHeat) result.BreakerWeightAfterHeat = new NRTestProperty();
            // Set Common properties
            result.BreakerWeightAfterHeat.LotNo = result.LotNo;
            result.BreakerWeightAfterHeat.PropertyNo = result.PropertyNo;
            result.BreakerWeightAfterHeat.NeedSP = result.NeedSP;
            result.BreakerWeightAfterHeat.NoOfSample = result.NoOfSample;
            result.BreakerWeightAfterHeat.AllowReTest = allowReTest;
            result.BreakerWeightAfterHeat.Spec = result.Spec;
            // Set calc formula
            if (null == result.BreakerWeightAfterHeat.ValueChanges)
            {
                result.BreakerWeightAfterHeat.ValueChanges = result.CalculateFormula;
            }
            // Set N/R
            result.BreakerWeightAfterHeat.N1 = breakWAHN1;
            result.BreakerWeightAfterHeat.N2 = breakWAHN2;
            result.BreakerWeightAfterHeat.R1 = breakWAHR1;
            result.BreakerWeightAfterHeat.R2 = breakWAHR2;

            // RPU
            if (null == result.RPU) result.RPU = new NRTestProperty();
            // Set Common properties
            result.RPU.LotNo = result.LotNo;
            result.RPU.PropertyNo = result.PropertyNo;
            result.RPU.NeedSP = result.NeedSP;
            result.RPU.NoOfSample = result.NoOfSample;
            result.RPU.AllowReTest = allowReTest;
            result.RPU.Spec = result.Spec;

            // Calculate Formula
            result.CalculateFormula();

            return result;
        }

        #endregion

        #endregion
    }

    #endregion
}
