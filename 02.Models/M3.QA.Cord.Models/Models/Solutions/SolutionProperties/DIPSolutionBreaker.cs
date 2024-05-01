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
    #region DIPSolutionBreaker

    /// <summary>
    /// The DIP Solution Breaker class.
    /// </summary>
    public class DIPSolutionBreaker : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPSolutionBreaker() : base()
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
    }

    #endregion
}
