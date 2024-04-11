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

        #endregion
    }

    #endregion
}
