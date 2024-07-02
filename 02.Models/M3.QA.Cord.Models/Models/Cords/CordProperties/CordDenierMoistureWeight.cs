#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
            YarnWeightBeforeDrying.AllowReTest = AllowRetest;
            YarnWeightBeforeDrying.EnableMultiPropertyTest = true;
            YarnWeightBeforeDrying.GetNMultiOut = IsMultiout;
            // BeforeHeat change need to calc formula for Yarn Weight
            YarnWeightBeforeDrying.ValueChanges = CalculateFormula;

            ContentWeight = new NRTestProperty();
            ContentWeight.AllowReTest = AllowRetest;
            ContentWeight.EnableMultiPropertyTest = true;
            ContentWeight.GetNMultiOut = IsMultiout;
            // AfterHeat change need to calc formula for Yarn Weight
            ContentWeight.ValueChanges = CalculateFormula;

            YarnAndContentWeightAfterDrying = new NRTestProperty();
            YarnAndContentWeightAfterDrying.AllowReTest = AllowRetest;
            YarnAndContentWeightAfterDrying.EnableMultiPropertyTest = true;
            YarnAndContentWeightAfterDrying.GetNMultiOut = IsMultiout;
            // AfterHeat change need to calc formula for Yarn Weight
            YarnAndContentWeightAfterDrying.ValueChanges = CalculateFormula;

            YarnWeightAfterDrying = new NRTestProperty();
            YarnWeightAfterDrying.AllowReTest = AllowRetest;
            YarnWeightAfterDrying.EnableMultiPropertyTest = true;
            YarnWeightAfterDrying.GetNMultiOut = IsMultiout;
            // AfterHeat change need to calc formula for Yarn Type
            YarnWeightAfterDrying.ValueChanges = CalculateDenierFormula;

            StandardDenierD = new NRTestProperty();
            StandardDenierD.AllowReTest = AllowRetest;
            StandardDenierD.EnableMultiPropertyTest = true;
            StandardDenierD.GetNMultiOut = IsMultiout;

            StandardDenierDtex = new NRTestProperty();
            StandardDenierDtex.AllowReTest = AllowRetest;
            StandardDenierDtex.EnableMultiPropertyTest = true;
            StandardDenierDtex.GetNMultiOut = IsMultiout;

            EquilibriumMoistureContent = new NRTestProperty();
            EquilibriumMoistureContent.AllowReTest = AllowRetest;
            EquilibriumMoistureContent.EnableMultiPropertyTest = true;
            EquilibriumMoistureContent.GetNMultiOut = IsMultiout;

            Weight = new NRTestProperty();
            Weight.AllowReTest = AllowRetest;
            Weight.EnableMultiPropertyTest = true;
            Weight.GetNMultiOut = IsMultiout;
        }

        #endregion

        #region Private Methods

        private bool AllowRetest()
        {
            return StandardDenierDtex.N1Out || EquilibriumMoistureContent.N1Out || Weight.N1Out;
        }

        private bool IsMultiout()
        {
            // Case one of property is out of spec so need to allow to enter retest in related properties
            return StandardDenierDtex.N1Out || EquilibriumMoistureContent.N1Out || Weight.N1Out;
        }

        private void CheckDenierSpec()
        {
            if (null != SpecDenier && null != YarnWeightAfterDrying && null != StandardDenierD && null != StandardDenierDtex)
            {
                if (!string.IsNullOrEmpty(SpecDenier.UnitId) && SpecDenier.UnitId.Trim().ToLower() == "dtex")
                {
                    StandardDenierDtex.N1Out = (StandardDenierDtex.N1.HasValue) ? SpecDenier.IsOutOfSpec(StandardDenierDtex.N1.Value) : false;
                    StandardDenierDtex.N1R1Out = (StandardDenierDtex.N1R1.HasValue) ? SpecDenier.IsOutOfSpec(StandardDenierDtex.N1R1.Value) : false;
                    StandardDenierDtex.N1R2Out = (StandardDenierDtex.N1R2.HasValue) ? SpecDenier.IsOutOfSpec(StandardDenierDtex.N1R2.Value) : false;

                    StandardDenierD.N1Out = StandardDenierDtex.N1Out;
                    StandardDenierD.N1R1Out = StandardDenierDtex.N1R1Out;
                    StandardDenierD.N1R2Out = StandardDenierDtex.N1R2Out;

                    // Raise items events
                    StandardDenierD.RaiseNOutChanges();
                    StandardDenierD.RaiseR1OutChanges();
                    StandardDenierD.RaiseR2OutChanges();

                    StandardDenierDtex.RaiseNOutChanges();
                    StandardDenierDtex.RaiseR1OutChanges();
                    StandardDenierDtex.RaiseR2OutChanges();
                }
                else if (!string.IsNullOrEmpty(SpecDenier.UnitId) && SpecDenier.UnitId.Trim().ToLower() == "D")
                {
                    StandardDenierD.N1Out = (StandardDenierD.N1.HasValue) ? SpecDenier.IsOutOfSpec(StandardDenierD.N1.Value) : false;
                    StandardDenierD.N1R1Out = (StandardDenierD.N1R1.HasValue) ? SpecDenier.IsOutOfSpec(StandardDenierD.N1R1.Value) : false;
                    StandardDenierD.N1R2Out = (StandardDenierD.N1R2.HasValue) ? SpecDenier.IsOutOfSpec(StandardDenierD.N1R2.Value) : false;

                    StandardDenierDtex.N1Out = StandardDenierD.N1Out;
                    StandardDenierDtex.N1R1Out = StandardDenierD.N1R1Out;
                    StandardDenierDtex.N1R2Out = StandardDenierD.N1R2Out;

                    // Raise items events
                    StandardDenierD.RaiseNOutChanges();
                    StandardDenierD.RaiseR1OutChanges();
                    StandardDenierD.RaiseR2OutChanges();

                    StandardDenierDtex.RaiseNOutChanges();
                    StandardDenierDtex.RaiseR1OutChanges();
                    StandardDenierDtex.RaiseR2OutChanges();
                }
            }
        }

        private void CheckMoistureSpec()
        {
            if (null != SpecMoisture && null != YarnWeightBeforeDrying && null != YarnWeightAfterDrying && null != EquilibriumMoistureContent)
            {
                EquilibriumMoistureContent.N1Out = (EquilibriumMoistureContent.N1.HasValue) ? SpecMoisture.IsOutOfSpec(EquilibriumMoistureContent.N1.Value) : false;
                EquilibriumMoistureContent.N1R1Out = (EquilibriumMoistureContent.N1R1.HasValue) ? SpecMoisture.IsOutOfSpec(EquilibriumMoistureContent.N1R1.Value) : false;
                EquilibriumMoistureContent.N1R2Out = (EquilibriumMoistureContent.N1R2.HasValue) ? SpecMoisture.IsOutOfSpec(EquilibriumMoistureContent.N1R2.Value) : false;

                // Raise items events
                EquilibriumMoistureContent.RaiseNOutChanges();
                EquilibriumMoistureContent.RaiseR1OutChanges();
                EquilibriumMoistureContent.RaiseR2OutChanges();
            }
        }

        private void CheckWeightSpec()
        {
            if (null != SpecWeight && null != YarnWeightAfterDrying && null != Weight)
            {
                Weight.N1Out = (Weight.N1.HasValue) ? SpecWeight.IsOutOfSpec(Weight.N1.Value) : false;
                Weight.N1R1Out = (Weight.N1R1.HasValue) ? SpecWeight.IsOutOfSpec(Weight.N1R1.Value) : false;
                Weight.N1R2Out = (Weight.N1R2.HasValue) ? SpecWeight.IsOutOfSpec(Weight.N1R2.Value) : false;

                // Raise items events
                Weight.RaiseNOutChanges();
                Weight.RaiseR1OutChanges();
                Weight.RaiseR2OutChanges();
            }
        }

        private void CheckCommonSpec()
        {
            if (null == StandardDenierDtex || null == EquilibriumMoistureContent || null == Weight)
                return;

            if (null != YarnWeightBeforeDrying)
            {
                YarnWeightBeforeDrying.N1Out = EquilibriumMoistureContent.N1Out;
                YarnWeightBeforeDrying.N1R1Out = EquilibriumMoistureContent.N1R1Out;
                YarnWeightBeforeDrying.N1R2Out = EquilibriumMoistureContent.N1R2Out;

                YarnWeightBeforeDrying.RaiseNOutChanges();
                YarnWeightBeforeDrying.RaiseR1OutChanges();
                YarnWeightBeforeDrying.RaiseR2OutChanges();
            }
            if (null != ContentWeight)
            {
                ContentWeight.RaiseNOutChanges();
                ContentWeight.RaiseR1OutChanges();
                ContentWeight.RaiseR2OutChanges();
            }
            if (null != YarnAndContentWeightAfterDrying)
            {
                YarnAndContentWeightAfterDrying.RaiseNOutChanges();
                YarnAndContentWeightAfterDrying.RaiseR1OutChanges();
                YarnAndContentWeightAfterDrying.RaiseR2OutChanges();
            }

            if (null != YarnWeightAfterDrying)
            {
                YarnWeightAfterDrying.RaiseNOutChanges();
                YarnWeightAfterDrying.RaiseR1OutChanges();
                YarnWeightAfterDrying.RaiseR2OutChanges();
            }

            if (null != StandardDenierD)
            {
                StandardDenierD.RaiseNOutChanges();
                StandardDenierD.RaiseR1OutChanges();
                StandardDenierD.RaiseR2OutChanges();
            }

            if (null != StandardDenierDtex)
            {
                StandardDenierDtex.RaiseNOutChanges();
                StandardDenierDtex.RaiseR1OutChanges();
                StandardDenierDtex.RaiseR2OutChanges();
            }

            if (null != EquilibriumMoistureContent)
            {
                EquilibriumMoistureContent.RaiseNOutChanges();
                EquilibriumMoistureContent.RaiseR1OutChanges();
                EquilibriumMoistureContent.RaiseR2OutChanges();
            }

            if (null != Weight)
            {
                Weight.RaiseNOutChanges();
                Weight.RaiseR1OutChanges();
                Weight.RaiseR2OutChanges();
            }
        }

        private void CalculateFormula()
        {
            if (null != YarnAndContentWeightAfterDrying && null != ContentWeight && null != YarnWeightAfterDrying)
            {
                // YarnWeightAfterDrying = YarnAndContentWeightAfterDrying – ContentWeight
                YarnWeightAfterDrying.N1 = (YarnAndContentWeightAfterDrying.N1.HasValue ?
                    YarnAndContentWeightAfterDrying.N1.Value - (ContentWeight.N1.HasValue ? ContentWeight.N1.Value : decimal.Zero) :
                    new decimal?());

                YarnWeightAfterDrying.N1R1 = (YarnAndContentWeightAfterDrying.N1R1.HasValue ?
                    YarnAndContentWeightAfterDrying.N1R1.Value - (ContentWeight.N1R1.HasValue ? ContentWeight.N1R1.Value : decimal.Zero) :
                    new decimal?());
                YarnWeightAfterDrying.N1R2 = (YarnAndContentWeightAfterDrying.N1R2.HasValue ?
                    YarnAndContentWeightAfterDrying.N1R2.Value - (ContentWeight.N1R2.HasValue ? ContentWeight.N1R2.Value : decimal.Zero) :
                    new decimal?());

                Raise(() => this.YarnWeightAfterDrying);
            }

            CalculateDenierFormula();
            CalculateMoistureFormula();
            CalculateWeightFormula();

            // Check Common Spec.
            CheckCommonSpec();
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
                    StandardDenierD.N1R1 = (YarnWeightAfterDrying.N1R1.HasValue) ?
                        YarnWeightAfterDrying.N1R1.Value * dFactor : new decimal?();
                    StandardDenierD.N1R2 = (YarnWeightAfterDrying.N1R2.HasValue) ?
                        YarnWeightAfterDrying.N1R2.Value * dFactor : new decimal?();

                    // Polyester: StandardDenierDtex = YarnWeightAfterDrying * 444.18
                    decimal dtexFactor = (decimal)444.18;
                    StandardDenierDtex.N1 = (YarnWeightAfterDrying.N1.HasValue) ?
                        YarnWeightAfterDrying.N1.Value * dtexFactor : new decimal?();
                    StandardDenierDtex.N1R1 = (YarnWeightAfterDrying.N1R1.HasValue) ?
                        YarnWeightAfterDrying.N1R1.Value * dtexFactor : new decimal?();
                    StandardDenierDtex.N1R2 = (YarnWeightAfterDrying.N1R2.HasValue) ?
                        YarnWeightAfterDrying.N1R2.Value * dtexFactor : new decimal?();
                    // Raise events
                    Raise(() => this.StandardDenierD);
                    Raise(() => this.StandardDenierDtex);

                    CheckDenierSpec(); // Check Denier Spec
                }
                else if (!string.IsNullOrWhiteSpace(YarnType) && string.Compare(YarnType, "Nylon", true) == 0)
                {
                    // Nylon: StandardDenierD = YarnWeightAfterDrying * 418
                    decimal dFactor = (decimal)418;
                    StandardDenierD.N1 = (YarnWeightAfterDrying.N1.HasValue) ?
                        YarnWeightAfterDrying.N1.Value * dFactor : new decimal?();
                    StandardDenierD.N1R1 = (YarnWeightAfterDrying.N1R1.HasValue) ?
                        YarnWeightAfterDrying.N1R1.Value * dFactor : new decimal?();
                    StandardDenierD.N1R2 = (YarnWeightAfterDrying.N1R2.HasValue) ?
                        YarnWeightAfterDrying.N1R2.Value * dFactor : new decimal?();

                    // Nylon: StandardDenierDtex = YarnWeightAfterDrying * 463.98
                    decimal dtexFactor = (decimal)463.98;
                    StandardDenierDtex.N1 = (YarnWeightAfterDrying.N1.HasValue) ?
                        YarnWeightAfterDrying.N1.Value * dtexFactor : new decimal?();
                    StandardDenierDtex.N1R1 = (YarnWeightAfterDrying.N1R1.HasValue) ?
                        YarnWeightAfterDrying.N1R1.Value * dtexFactor : new decimal?();
                    StandardDenierDtex.N1R2 = (YarnWeightAfterDrying.N1R2.HasValue) ?
                        YarnWeightAfterDrying.N1R2.Value * dtexFactor : new decimal?();

                    // Raise events
                    Raise(() => this.StandardDenierD);
                    Raise(() => this.StandardDenierDtex);

                    CheckDenierSpec(); // Check Denier Spec
                }
            }
        }

        private void CalculateMoistureFormula()
        {
            if (null != YarnWeightBeforeDrying && null != YarnWeightAfterDrying && null != EquilibriumMoistureContent)
            {
                // Moisture = ((YarnWeightBeforeDrying - YarnWeightAfterDrying) / YarnWeightAfterDrying) * 100
                var eN1 = (YarnWeightBeforeDrying.N1.HasValue) ?
                    YarnWeightBeforeDrying.N1.Value - ((YarnWeightAfterDrying.N1.HasValue) ? YarnWeightAfterDrying.N1.Value : decimal.Zero) : new decimal?();
                var eR1 = (YarnWeightBeforeDrying.N1R1.HasValue) ?
                    YarnWeightBeforeDrying.N1R1.Value - ((YarnWeightAfterDrying.N1R1.HasValue) ? YarnWeightAfterDrying.N1R1.Value : decimal.Zero) : new decimal?();
                var eR2 = (YarnWeightBeforeDrying.N1R2.HasValue) ?
                    YarnWeightBeforeDrying.N1R2.Value - ((YarnWeightAfterDrying.N1R2.HasValue) ? YarnWeightAfterDrying.N1R2.Value : decimal.Zero) : new decimal?();

                var aN1 = (YarnWeightAfterDrying.N1.HasValue) ? YarnWeightAfterDrying.N1.Value : new decimal?();
                var aR1 = (YarnWeightAfterDrying.N1R1.HasValue) ? YarnWeightAfterDrying.N1R1.Value : new decimal?();
                var aR2 = (YarnWeightAfterDrying.N1R2.HasValue) ? YarnWeightAfterDrying.N1R2.Value : new decimal?();

                // No Round degits
                EquilibriumMoistureContent.N1 = (aN1.HasValue) ? 
                    (eN1.HasValue ? (eN1 / aN1.Value) * 100 : new decimal?()) : new decimal?();
                EquilibriumMoistureContent.N1R1 = (aR1.HasValue) ? 
                    (eN1.HasValue ? (eR1 / aR1.Value) * 100 : new decimal?()) : new decimal?();
                EquilibriumMoistureContent.N1R2 = (aR2.HasValue) ?
                    (eN1.HasValue ? (eR2 / aR2.Value) * 100 : new decimal?()) : new decimal?();

                // Round to 2 digits
                /*
                EquilibriumMoistureContent.N1 = (aN1.HasValue) ?
                    (eN1.HasValue ? decimal.Round(((decimal)(eN1 / aN1.Value) * 100), 2) : new decimal?()) : new decimal?();
                EquilibriumMoistureContent.N1R1 = (aR1.HasValue) ?
                    (eN1.HasValue ? decimal.Round(((decimal)(eR1 / aR1.Value) * 100), 2) : new decimal?()) : new decimal?();
                EquilibriumMoistureContent.N1R2 = (aR2.HasValue) ?
                    (eN1.HasValue ? decimal.Round(((decimal)(eR2 / aR2.Value) * 100), 2) : new decimal?()) : new decimal?();
                */

                // Raise events
                Raise(() => this.EquilibriumMoistureContent);

                CheckMoistureSpec(); // Check Moisture Spec
            }
        }

        private void CalculateWeightFormula()
        {
            // Weight = YarnWeightAfterDrying  / 22.5
            if (null != YarnWeightAfterDrying && null != Weight)
            {
                decimal wFactor = (decimal)22.5;
                Weight.N1 = (YarnWeightAfterDrying.N1.HasValue) ? YarnWeightAfterDrying.N1.Value / wFactor : new decimal?();
                Weight.N1R1 = (YarnWeightAfterDrying.N1R1.HasValue) ? YarnWeightAfterDrying.N1R1.Value / wFactor : new decimal?();
                Weight.N1R2 = (YarnWeightAfterDrying.N1R2.HasValue) ? YarnWeightAfterDrying.N1R2.Value / wFactor : new decimal?();

                // Raise events
                Raise(() => this.Weight);

                CheckWeightSpec(); // Check Weight Spec
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
            if (null == YarnWeightBeforeDrying)
            {
                YarnWeightBeforeDrying = new NRTestProperty();
                YarnWeightBeforeDrying.AllowReTest = AllowRetest;
                YarnWeightBeforeDrying.EnableMultiPropertyTest = true;
                YarnWeightBeforeDrying.GetNMultiOut = IsMultiout;
            }
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
            YarnWeightBeforeDrying.SampleType = SampleType;

            if (null == ContentWeight)
            {
                ContentWeight = new NRTestProperty();
                ContentWeight.AllowReTest = AllowRetest;
                ContentWeight.EnableMultiPropertyTest = true;
                ContentWeight.GetNMultiOut = IsMultiout;
            }
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
            ContentWeight.SampleType = SampleType;

            if (null == YarnAndContentWeightAfterDrying)
            {
                YarnAndContentWeightAfterDrying = new NRTestProperty();
                YarnAndContentWeightAfterDrying.AllowReTest = AllowRetest;
                YarnAndContentWeightAfterDrying.EnableMultiPropertyTest = true;
                YarnAndContentWeightAfterDrying.GetNMultiOut = IsMultiout;
            }
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
            YarnAndContentWeightAfterDrying.SampleType = SampleType;

            if (null == YarnWeightAfterDrying)
            {
                YarnWeightAfterDrying = new NRTestProperty();
                YarnWeightAfterDrying.AllowReTest = AllowRetest;
                YarnWeightAfterDrying.EnableMultiPropertyTest = true;
                YarnWeightAfterDrying.GetNMultiOut = IsMultiout;
            }
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
            YarnWeightAfterDrying.SampleType = SampleType;

            if (null == StandardDenierD)
            {
                StandardDenierD = new NRTestProperty();
                StandardDenierD.AllowReTest = AllowRetest;
                StandardDenierD.EnableMultiPropertyTest = true;
                StandardDenierD.GetNMultiOut = IsMultiout;
            }
            StandardDenierD.SPNo = SPNo;
            StandardDenierD.LotNo = LotNo;
            StandardDenierD.PropertyNo = PropertyNo1; // Denier Property No = 10
            StandardDenierD.SPNo = SPNo;
            StandardDenierD.NoOfSample = NoOfSample;
            StandardDenierD.NeedSP = NeedSP;
            StandardDenierD.YarnType = YarnType;
            StandardDenierD.SampleType = SampleType;

            if (null == StandardDenierDtex)
            {
                StandardDenierDtex = new NRTestProperty();
                StandardDenierDtex.AllowReTest = AllowRetest;
                StandardDenierDtex.EnableMultiPropertyTest = true;
                StandardDenierDtex.GetNMultiOut = IsMultiout;
            }
            StandardDenierDtex.SPNo = SPNo;
            StandardDenierDtex.LotNo = LotNo;
            StandardDenierDtex.PropertyNo = PropertyNo1; // Denier Property No = 10
            StandardDenierDtex.SPNo = SPNo;
            StandardDenierDtex.NoOfSample = NoOfSample;
            StandardDenierDtex.NeedSP = NeedSP;
            StandardDenierDtex.YarnType = YarnType;
            StandardDenierDtex.SampleType = SampleType;
        }

        private void UpdateMoistureProperties()
        {
            if (null == EquilibriumMoistureContent)
            {
                EquilibriumMoistureContent = new NRTestProperty();
                EquilibriumMoistureContent.AllowReTest = AllowRetest;
                EquilibriumMoistureContent.EnableMultiPropertyTest = true;
                EquilibriumMoistureContent.GetNMultiOut = IsMultiout;
            }
            EquilibriumMoistureContent.SPNo = SPNo;
            EquilibriumMoistureContent.LotNo = LotNo;
            EquilibriumMoistureContent.PropertyNo = PropertyNo2; // Moisture Property No = 11
            EquilibriumMoistureContent.SPNo = SPNo;
            EquilibriumMoistureContent.NoOfSample = NoOfSample;
            EquilibriumMoistureContent.NeedSP = NeedSP;
            EquilibriumMoistureContent.YarnType = YarnType;
            EquilibriumMoistureContent.SampleType = SampleType;
        }

        private void UpdateWeightProperties()
        {
            if (null == Weight)
            {
                Weight = new NRTestProperty();
                Weight.AllowReTest = AllowRetest;
                Weight.EnableMultiPropertyTest = true;
                Weight.GetNMultiOut = IsMultiout;
            }
            Weight.SPNo = SPNo;
            Weight.LotNo = LotNo;
            Weight.PropertyNo = PropertyNo3; // Weight Property No = 14
            Weight.SPNo = SPNo;
            Weight.NoOfSample = NoOfSample;
            Weight.NeedSP = NeedSP;
            Weight.YarnType = YarnType;
            Weight.SampleType = SampleType;
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

        #endregion

        #region Spec

        /// <summary>Gets or sets CordTestSpec (Denier).</summary>
        public CordTestSpec SpecDenier { get; set; }
        /// <summary>Gets or sets CordTestSpec (Moisture).</summary>
        public CordTestSpec SpecMoisture { get; set; }
        /// <summary>Gets or sets CordTestSpec (Weight).</summary>
        public CordTestSpec SpecWeight { get; set; }

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

            // Denier (PropertyNo = 10)
            var spec1 = value.Specs.FindByPropertyNo(10);
            // Moisture regain (PropertyNo = 11)
            var spec2 = value.Specs.FindByPropertyNo(11);
            // Weight (PropertyNo = 14)
            var spec3 = value.Specs.FindByPropertyNo(14);

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
                    SpecDenier = spec1, // Denier
                    SpecMoisture = spec2, // Moisture
                    SpecWeight = spec3, // Weight
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
                        existItems[idx].SampleType = item.SampleType;
                        // Clone anther properties
                        Clone(existItems[idx], item);
                    }
                }
            }

            // re checking.
            if (null != results)
            {
                foreach (var item in results) item.CalculateFormula(); // Check spec to update UI
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
            dst.SampleType = src.SampleType;

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
                            inst.YarnWeightBeforeDrying.AllowReTest = inst.AllowRetest;
                            inst.YarnWeightBeforeDrying.EnableMultiPropertyTest = true;
                            inst.YarnWeightBeforeDrying.GetNMultiOut = inst.IsMultiout;

                            inst.YarnWeightBeforeDrying.N1 = item.YWBDN1;
                            inst.YarnWeightBeforeDrying.N1R1 = item.YWBDN1R1;
                            inst.YarnWeightBeforeDrying.N1R2 = item.YWBDN1R2;
                        }
                        if (null != inst.ContentWeight)
                        {
                            inst.ContentWeight.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.ContentWeight.AllowReTest = inst.AllowRetest;
                            inst.ContentWeight.EnableMultiPropertyTest = true;
                            inst.ContentWeight.GetNMultiOut = inst.IsMultiout;

                            inst.ContentWeight.N1 = item.CWN1;
                            inst.ContentWeight.N1R1 = item.CWN1R1;
                            inst.ContentWeight.N1R2 = item.CWN1R2;
                        }
                        if (null != inst.YarnAndContentWeightAfterDrying)
                        {
                            inst.YarnAndContentWeightAfterDrying.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.YarnAndContentWeightAfterDrying.AllowReTest = inst.AllowRetest;
                            inst.YarnAndContentWeightAfterDrying.EnableMultiPropertyTest = true;
                            inst.YarnAndContentWeightAfterDrying.GetNMultiOut = inst.IsMultiout;

                            inst.YarnAndContentWeightAfterDrying.N1 = item.YCWADN1;
                            inst.YarnAndContentWeightAfterDrying.N1R1 = item.YCWADN1R1;
                            inst.YarnAndContentWeightAfterDrying.N1R2 = item.YCWADN1R2;
                        }
                        if (null != inst.YarnWeightAfterDrying)
                        {
                            inst.YarnWeightAfterDrying.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.YarnWeightAfterDrying.AllowReTest = inst.AllowRetest;
                            inst.YarnWeightAfterDrying.EnableMultiPropertyTest = true;
                            inst.YarnWeightAfterDrying.GetNMultiOut = inst.IsMultiout;

                            inst.YarnWeightAfterDrying.N1 = item.YWADN1;
                            inst.YarnWeightAfterDrying.N1R1 = item.YWADN1R1;
                            inst.YarnWeightAfterDrying.N1R2 = item.YWADN1R2;
                        }
                        if (null != inst.StandardDenierD)
                        {
                            inst.StandardDenierD.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.StandardDenierD.AllowReTest = inst.AllowRetest;
                            inst.StandardDenierD.EnableMultiPropertyTest = true;
                            inst.StandardDenierD.GetNMultiOut = inst.IsMultiout;

                            inst.StandardDenierD.N1 = item.DENIER_D_N1;
                            inst.StandardDenierD.N1R1 = item.DENIER_D_N1R1;
                            inst.StandardDenierD.N1R2 = item.DENIER_D_N1R2;
                            inst.StandardDenierD.N1R1Flag = item.DENIER_D_N1R1Flag.HasValue ? item.DENIER_D_N1R1Flag.Value : false;
                            inst.StandardDenierD.N1R2Flag = item.DENIER_D_N1R2Flag.HasValue ? item.DENIER_D_N1R2Flag.Value : false;
                        }
                        if (null != inst.StandardDenierDtex)
                        {
                            inst.StandardDenierD.PropertyNo = 10; // Denier PropertyNo = 10
                            inst.StandardDenierDtex.AllowReTest = inst.AllowRetest;
                            inst.StandardDenierDtex.EnableMultiPropertyTest = true;
                            inst.StandardDenierDtex.GetNMultiOut = inst.IsMultiout;

                            inst.StandardDenierDtex.N1 = item.DENIER_Dtex_N1;
                            inst.StandardDenierDtex.N1R1 = item.DENIER_Dtex_N1R1;
                            inst.StandardDenierDtex.N1R2 = item.DENIER_Dtex_N1R2;
                            inst.StandardDenierDtex.N1R1Flag = item.DENIER_Dtex_N1R1Flag.HasValue ? item.DENIER_Dtex_N1R1Flag.Value : false;
                            inst.StandardDenierDtex.N1R2Flag = item.DENIER_Dtex_N1R2Flag.HasValue ? item.DENIER_Dtex_N1R2Flag.Value : false;
                        }
                        if (null != inst.EquilibriumMoistureContent)
                        {
                            inst.EquilibriumMoistureContent.PropertyNo = 11; // Moisture regain PropertyNo = 11
                            inst.EquilibriumMoistureContent.AllowReTest = inst.AllowRetest;
                            inst.EquilibriumMoistureContent.EnableMultiPropertyTest = true;
                            inst.EquilibriumMoistureContent.GetNMultiOut = inst.IsMultiout;

                            inst.EquilibriumMoistureContent.N1 = item.MOISTURE_N1;
                            inst.EquilibriumMoistureContent.N1R1 = item.MOISTURE_N1R1;
                            inst.EquilibriumMoistureContent.N1R2 = item.MOISTURE_N1R2;
                            inst.EquilibriumMoistureContent.N1R1Flag = item.MOISTURE_N1R1Flag.HasValue ? item.MOISTURE_N1R1Flag.Value : false;
                            inst.EquilibriumMoistureContent.N1R2Flag = item.MOISTURE_N1R2Flag.HasValue ? item.MOISTURE_N1R2Flag.Value : false;
                        }
                        if (null != inst.Weight)
                        {
                            inst.Weight.PropertyNo = 14; // Weight PropertyNo = 14
                            inst.Weight.AllowReTest = inst.AllowRetest;
                            inst.Weight.EnableMultiPropertyTest = true;
                            inst.Weight.GetNMultiOut = inst.IsMultiout;

                            inst.Weight.N1 = item.WEIGHT_N1;
                            inst.Weight.N1R1 = item.WEIGHT_N1R1;
                            inst.Weight.N1R2 = item.WEIGHT_N1R2;
                            inst.Weight.N1R1Flag = item.WEIGHT_N1R1Flag.HasValue ? item.WEIGHT_N1R1Flag.Value : false;
                            inst.Weight.N1R2Flag = item.WEIGHT_N1R2Flag.HasValue ? item.WEIGHT_N1R1Flag.Value : false;
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
            p.Add("@ywbdn1r1", (null != value.YarnWeightBeforeDrying) ? value.YarnWeightBeforeDrying.N1R1 : new decimal?());
            p.Add("@ywbdn1r2", (null != value.YarnWeightBeforeDrying) ? value.YarnWeightBeforeDrying.N1R2 : new decimal?());

            p.Add("@cwn1", (null != value.ContentWeight) ? value.ContentWeight.N1 : new decimal?());
            p.Add("@cwn1r1", (null != value.ContentWeight) ? value.ContentWeight.N1R1 : new decimal?());
            p.Add("@cwn1r2", (null != value.ContentWeight) ? value.ContentWeight.N1R2 : new decimal?());

            p.Add("@ycwadn1", (null != value.YarnAndContentWeightAfterDrying) ? value.YarnAndContentWeightAfterDrying.N1 : new decimal?());
            p.Add("@ycwadn1r1", (null != value.YarnAndContentWeightAfterDrying) ? value.YarnAndContentWeightAfterDrying.N1R1 : new decimal?());
            p.Add("@ycwadn1r2", (null != value.YarnAndContentWeightAfterDrying) ? value.YarnAndContentWeightAfterDrying.N1R2 : new decimal?());

            p.Add("@ywadn1", (null != value.YarnWeightAfterDrying) ? value.YarnWeightAfterDrying.N1 : new decimal?());
            p.Add("@ywadn1r1", (null != value.YarnWeightAfterDrying) ? value.YarnWeightAfterDrying.N1R1 : new decimal?());
            p.Add("@ywadn1r2", (null != value.YarnWeightAfterDrying) ? value.YarnWeightAfterDrying.N1R2 : new decimal?());

            p.Add("@denierdn1", (null != value.StandardDenierD) ? value.StandardDenierD.N1 : new decimal?());
            p.Add("@denierdn1r1", (null != value.StandardDenierD) ? value.StandardDenierD.N1R1 : new decimal?());
            p.Add("@denierdn1r2", (null != value.StandardDenierD) ? value.StandardDenierD.N1R2 : new decimal?());
            p.Add("@denierdn1r1flag", (null != value.StandardDenierD) ? (value.StandardDenierD.N1R1Flag) ? true : new bool?() : new bool?());
            p.Add("@denierdn1r2flag", (null != value.StandardDenierD) ? (value.StandardDenierD.N1R2Flag) ? true : new bool?()  : new bool?());

            p.Add("@denierdtexn1", (null != value.StandardDenierDtex) ? value.StandardDenierDtex.N1 : new decimal?());
            p.Add("@denierdtexn1r1", (null != value.StandardDenierDtex) ? value.StandardDenierDtex.N1R1 : new decimal?());
            p.Add("@denierdtexn1r2", (null != value.StandardDenierDtex) ? value.StandardDenierDtex.N1R2 : new decimal?());
            p.Add("@denierdtexn1r1flag", (null != value.StandardDenierDtex) ? (value.StandardDenierDtex.N1R1Flag) ? true : new bool?() : new bool?());
            p.Add("@denierdtexn1r2flag", (null != value.StandardDenierDtex) ? (value.StandardDenierDtex.N1R2Flag) ? true : new bool?() : new bool?());

            p.Add("@moisturen1", (null != value.EquilibriumMoistureContent) ? value.EquilibriumMoistureContent.N1 : new decimal?());
            p.Add("@moisturen1r1", (null != value.EquilibriumMoistureContent) ? value.EquilibriumMoistureContent.N1R1 : new decimal?());
            p.Add("@moisturen1r2", (null != value.EquilibriumMoistureContent) ? value.EquilibriumMoistureContent.N1R2 : new decimal?());
            p.Add("@moisturen1r1flag", (null != value.EquilibriumMoistureContent) ? (value.EquilibriumMoistureContent.N1R1Flag) ? true : new bool?() : new bool?());
            p.Add("@moisturen1r2flag", (null != value.EquilibriumMoistureContent) ? (value.EquilibriumMoistureContent.N1R2Flag) ? true : new bool?() : new bool?());

            p.Add("@weightn1", (null != value.Weight) ? value.Weight.N1 : new decimal?());
            p.Add("@weightn1r1", (null != value.Weight) ? value.Weight.N1R1 : new decimal?());
            p.Add("@weightn1r2", (null != value.Weight) ? value.Weight.N1R2 : new decimal?());
            p.Add("@weightn1r1flag", (null != value.Weight) ? (value.Weight.N1R1Flag) ? true : new bool?() : new bool?());
            p.Add("@weightn1r2flag", (null != value.Weight) ? (value.Weight.N1R2Flag) ? true : new bool?() : new bool?());

            p.Add("@sampletype", (string.IsNullOrWhiteSpace(value.SampleType) || value.SampleType != "F")
                ? "S" : value.SampleType);

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
