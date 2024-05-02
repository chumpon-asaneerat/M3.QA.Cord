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
            BeakerWeight = new NRTestProperty();
            // Set calc formula
            BeakerWeight.ValueChanges = CalculateFormula;

            BeakerWeightBeforeHeat = new NRTestProperty();
            // Set calc formula
            BeakerWeight.ValueChanges = CalculateFormula;

            BeakerWeightAfterHeat = new NRTestProperty();
            // Set calc formula
            BeakerWeight.ValueChanges = CalculateFormula;

            RPU = new NRTestProperty();
        }

        #endregion

        #region Private Methods

        private void CheckSpec()
        {
            if (null != Spec && 
                null != BeakerWeight && null != BeakerWeightBeforeHeat &&
                null != BeakerWeightAfterHeat && null != RPU)
            {
                // Check AdhesionForce Range.
                RPU.NOut1 = (RPU.N1.HasValue) ? Spec.IsOutOfSpec(RPU.N1.Value) : false;
                RPU.NOut2 = (RPU.N2.HasValue) ? Spec.IsOutOfSpec(RPU.N2.Value) : false;

                RPU.ROut1 = (RPU.R1.HasValue) ? Spec.IsOutOfSpec(RPU.R1.Value) : false;
                RPU.ROut2 = (RPU.R2.HasValue) ? Spec.IsOutOfSpec(RPU.R2.Value) : false;

                // Raise items events
                RPU.RaiseNOutChanges();
                RPU.RaiseROutChanges();

                // set out of range flag to BeakerWeight object
                BeakerWeight.NOut1 = RPU.NOut1;
                BeakerWeight.NOut2 = RPU.NOut2;

                BeakerWeight.ROut1 = RPU.ROut1;
                BeakerWeight.ROut2 = RPU.ROut2;
                
                // Raise items events
                BeakerWeight.RaiseNOutChanges();
                BeakerWeight.RaiseROutChanges();

                // set out of range flag to BeakerWeightBeforeHeat object
                BeakerWeightBeforeHeat.NOut1 = RPU.NOut1;
                BeakerWeightBeforeHeat.NOut2 = RPU.NOut2;

                BeakerWeightBeforeHeat.ROut1 = RPU.ROut1;
                BeakerWeightBeforeHeat.ROut2 = RPU.ROut2;

                // Raise items events
                BeakerWeightBeforeHeat.RaiseNOutChanges();
                BeakerWeightBeforeHeat.RaiseROutChanges();

                // set out of range flag to BeakerWeightAfterHeat object
                BeakerWeightAfterHeat.NOut1 = RPU.NOut1;
                BeakerWeightAfterHeat.NOut2 = RPU.NOut2;

                BeakerWeightAfterHeat.ROut1 = RPU.ROut1;
                BeakerWeightAfterHeat.ROut2 = RPU.ROut2;

                // Raise items events
                BeakerWeightAfterHeat.RaiseNOutChanges();
                BeakerWeightAfterHeat.RaiseROutChanges();
            }
        }

        private void CalculateFormula()
        {
            // A = BreakerWeight
            // B = BreakerWeightBeforeHeat
            // C = BreakerWeightAfterHeat
            if (null != BeakerWeight && null != BeakerWeightBeforeHeat && 
                null != BeakerWeightAfterHeat && null != RPU)
            {
                var A = BeakerWeight;
                var B = BeakerWeightBeforeHeat;
                var C = BeakerWeightAfterHeat;
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
            if (null == BeakerWeight) BeakerWeight = new NRTestProperty();

            BeakerWeight.LotNo = LotNo;
            BeakerWeight.PropertyNo = PropertyNo;
            BeakerWeight.NoOfSample = NoOfSample;
            BeakerWeight.NeedSP = NeedSP;
            // Check calculate action
            if (null == BeakerWeight.ValueChanges)
            {
                BeakerWeight.ValueChanges = CalculateFormula;
            }

            if (null == BeakerWeightBeforeHeat) BeakerWeightBeforeHeat = new NRTestProperty();
            BeakerWeightBeforeHeat.LotNo = LotNo;
            BeakerWeightBeforeHeat.PropertyNo = PropertyNo;
            BeakerWeightBeforeHeat.NoOfSample = NoOfSample;
            BeakerWeightBeforeHeat.NeedSP = NeedSP;
            // Check calculate action
            if (null == BeakerWeightBeforeHeat.ValueChanges)
            {
                BeakerWeightBeforeHeat.ValueChanges = CalculateFormula;
            }

            if (null == BeakerWeightAfterHeat) BeakerWeightAfterHeat = new NRTestProperty();
            BeakerWeightAfterHeat.LotNo = LotNo;
            BeakerWeightAfterHeat.PropertyNo = PropertyNo;
            BeakerWeightAfterHeat.NoOfSample = NoOfSample;
            BeakerWeightAfterHeat.NeedSP = NeedSP;
            // Check calculate action
            if (null == BeakerWeightAfterHeat.ValueChanges)
            {
                BeakerWeightAfterHeat.ValueChanges = CalculateFormula;
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

        #region BeakerWeight/BeakerWeightBeforeHeat/BeakerWeightAfterHeat/RPU

        /// <summary>Gets or sets BeakerWeight.</summary>
        public NRTestProperty BeakerWeight { get; set; }
        /// <summary>Gets or sets BeakerWeightBeforeHeat.</summary>
        public NRTestProperty BeakerWeightBeforeHeat { get; set; }
        /// <summary>Gets or sets BeakerWeightAfterHeat.</summary>
        public NRTestProperty BeakerWeightAfterHeat { get; set; }
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

            NRTestProperty.Clone(src.BeakerWeight, dst.BeakerWeight);
            NRTestProperty.Clone(src.BeakerWeightBeforeHeat, dst.BeakerWeightBeforeHeat);
            NRTestProperty.Clone(src.BeakerWeightAfterHeat, dst.BeakerWeightAfterHeat);
            NRTestProperty.Clone(src.RPU, dst.RPU);
        }

        #endregion

        #region Create

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="beakWN1"></param>
        /// <param name="beakWN2"></param>
        /// <param name="beakWR1"></param>
        /// <param name="beakWR2"></param>
        /// <param name="beakWBHN1"></param>
        /// <param name="beakWBHN2"></param>
        /// <param name="beakWBHR1"></param>
        /// <param name="beakWBHR2"></param>
        /// <param name="beakWAHN1"></param>
        /// <param name="beakWAHN2"></param>
        /// <param name="beakWAHR1"></param>
        /// <param name="beakWAHR2"></param>
        /// <returns></returns>
        internal static DIPSolutionTSC Create(DIPSolutionSampleTestData value,
            decimal? beakWN1, decimal? beakWN2,
            decimal? beakWR1, decimal? beakWR2,
            decimal? beakWBHN1, decimal? beakWBHN2,
            decimal? beakWBHR1, decimal? beakWBHR2,
            decimal? beakWAHN1, decimal? beakWAHN2,
            decimal? beakWAHR1, decimal? beakWAHR2,
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

            // Beaker Weight
            if (null == result.BeakerWeight) result.BeakerWeight = new NRTestProperty();
            // Set Common properties
            result.BeakerWeight.LotNo = result.LotNo;
            result.BeakerWeight.PropertyNo = result.PropertyNo;
            result.BeakerWeight.NeedSP = result.NeedSP;
            result.BeakerWeight.NoOfSample = result.NoOfSample;
            result.BeakerWeight.AllowReTest = allowReTest;
            result.BeakerWeight.Spec = result.Spec;
            // Set calc formula
            if (null == result.BeakerWeight.ValueChanges)
            {
                result.BeakerWeight.ValueChanges = result.CalculateFormula;
            }
            // Set N/R
            result.BeakerWeight.N1 = beakWN1;
            result.BeakerWeight.N2 = beakWN2;
            result.BeakerWeight.R1 = beakWR1;
            result.BeakerWeight.R2 = beakWR2;

            // Beaker Weight Before Heat
            if (null == result.BeakerWeightBeforeHeat) result.BeakerWeightBeforeHeat = new NRTestProperty();
            // Set Common properties
            result.BeakerWeightBeforeHeat.LotNo = result.LotNo;
            result.BeakerWeightBeforeHeat.PropertyNo = result.PropertyNo;
            result.BeakerWeightBeforeHeat.NeedSP = result.NeedSP;
            result.BeakerWeightBeforeHeat.NoOfSample = result.NoOfSample;
            result.BeakerWeightBeforeHeat.AllowReTest = allowReTest;
            result.BeakerWeightBeforeHeat.Spec = result.Spec;
            // Set calc formula
            if (null == result.BeakerWeightBeforeHeat.ValueChanges)
            {
                result.BeakerWeightBeforeHeat.ValueChanges = result.CalculateFormula;
            }
            // Set N/R
            result.BeakerWeightBeforeHeat.N1 = beakWBHN1;
            result.BeakerWeightBeforeHeat.N2 = beakWBHN2;
            result.BeakerWeightBeforeHeat.R1 = beakWBHR1;
            result.BeakerWeightBeforeHeat.R2 = beakWBHR2;

            // Beaker Weight After Heat
            if (null == result.BeakerWeightAfterHeat) result.BeakerWeightAfterHeat = new NRTestProperty();
            // Set Common properties
            result.BeakerWeightAfterHeat.LotNo = result.LotNo;
            result.BeakerWeightAfterHeat.PropertyNo = result.PropertyNo;
            result.BeakerWeightAfterHeat.NeedSP = result.NeedSP;
            result.BeakerWeightAfterHeat.NoOfSample = result.NoOfSample;
            result.BeakerWeightAfterHeat.AllowReTest = allowReTest;
            result.BeakerWeightAfterHeat.Spec = result.Spec;
            // Set calc formula
            if (null == result.BeakerWeightAfterHeat.ValueChanges)
            {
                result.BeakerWeightAfterHeat.ValueChanges = result.CalculateFormula;
            }
            // Set N/R
            result.BeakerWeightAfterHeat.N1 = beakWAHN1;
            result.BeakerWeightAfterHeat.N2 = beakWAHN2;
            result.BeakerWeightAfterHeat.R1 = beakWAHR1;
            result.BeakerWeightAfterHeat.R2 = beakWAHR2;

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
