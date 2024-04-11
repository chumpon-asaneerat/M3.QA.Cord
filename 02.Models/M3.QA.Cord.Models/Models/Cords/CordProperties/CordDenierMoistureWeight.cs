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

            if (null == AfterHeat) AfterHeat = new NRTestProperty();
            AfterHeat.SPNo = SPNo;
            AfterHeat.LotNo = LotNo;
            AfterHeat.PropertyNo = PropertyNo;
            AfterHeat.SPNo = SPNo;
            AfterHeat.NoOfSample = NoOfSample;
            AfterHeat.NeedSP = NeedSP;

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

        #endregion

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
