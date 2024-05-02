#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;

using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region DIPSolutionSampleTestData

    /// <summary>
    /// The DIP Solution Sample Test Data class.
    /// </summary>
    public class DIPSolutionSampleTestData
    {
        #region Public Properties

        #region Common

        public string ItemCode { get; set; }
        public string LotNo { get; set; }
        public int? MasterId { get; set; }
        public string Compounds { get; set; }

        public string SendBy { get; set; }
        public DateTime? SendDate { get; set; }
        
        public DateTime? ForecastFinishDate { get; set; }
        public DateTime? ValidDate { get; set; }

        public DateTime? InputDate { get; set; }
        public string InputBy { get; set; }

        public DateTime? EditDate { get; set; }
        public string EditBy { get; set; }

        #endregion

        #region Spec

        /// <summary>The Spec.</summary>
        public List<CordTestSpec> Specs { get; set; }

        #endregion

        #region Custom Allow R

        private bool AloowRetest()
        {
            // Case RF: not allow retest
            return (string.IsNullOrEmpty(Compounds) ? false : Compounds.Trim() != "RF");
        }

        #endregion

        #region Test Properties

        public DIPSolutionPH Ph { get; set; }
        public DIPSolutionTempurature Temperature { get; set; }
        public DIPSolutionViscosity Viscosity { get; set; }

        public DIPSolutionTSC TSC { get; set; }

        #endregion

        #endregion

        #region Private Methods

        private void InitSpecs()
        {
            var specs = CordTestSpec.GetsByMasterId(this.MasterId).Value();
            Specs = (null != specs) ? specs : new List<CordTestSpec>();
        }

        #endregion

        #region Static Methods

        #region GetByLotNo

        /// <summary>
        /// Gets DIPSolutionSampleTestData by Lot No.
        /// </summary>
        /// <param name="value">The CordSampleTestData item to save.</param>
        /// <returns></returns>
        public static DIPSolutionSampleTestData GetByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            DIPSolutionSampleTestData ret = null;

            if (string.IsNullOrWhiteSpace(lotNo))
            {
                return ret;
            }

            try
            {
                var items = Utils.M_CheckSolutionLotReceive.Gets(lotNo).Value();

                if (null == items || items.Count <= 0)
                    return ret;

                int iCnt = 0;
                ret = new DIPSolutionSampleTestData();

                decimal? PhN = new decimal?();
                decimal? PhR = new decimal?();
                decimal? TempN = new decimal?();
                decimal? TempR = new decimal?();
                decimal? ViscosityN = new decimal?();
                decimal? ViscosityR = new decimal?();

                decimal? breakWN1 = new decimal?(); 
                decimal? breakWN2 = new decimal?();
                decimal? breakWR1 = new decimal?(); 
                decimal? breakWR2 = new decimal?();
                decimal? breakWBHN1 = new decimal?();
                decimal? breakWBHN2 = new decimal?();
                decimal? breakWBHR1 = new decimal?();
                decimal? breakWBHR2 = new decimal?();
                decimal? breakWAHN1 = new decimal?();
                decimal? breakWAHN2 = new decimal?();
                decimal? breakWAHR1 = new decimal?(); 
                decimal? breakWAHR2 = new decimal?();

                foreach (var item in items)
                {
                    if (iCnt == 0)
                    {
                        ret.MasterId = item.MasterId;
                        ret.ItemCode = item.ItemCode;
                        ret.LotNo = item.LotNo;

                        ret.Compounds = item.Compound;

                        ret.SendDate = item.SendDate;
                        ret.SendBy = item.SendBy;

                        ret.ForecastFinishDate = item.ForecastFinishDate;
                        ret.ValidDate = item.ValidDate;

                        ret.InputBy = item.InputBy;
                        ret.InputDate = item.InputDate;
                        ret.EditBy = item.EditBy;
                        ret.EditDate = item.EditDate;

                        // Store variables.
                        PhN = item.PhN;
                        PhR = item.PhR;
                        TempN = item.TempturatureN;
                        TempR = item.TempturatureR;
                        ViscosityN = item.ViscosityN;
                        ViscosityR = item.ViscosityR;

                        breakWN1 = item.BreakerWN1;
                        breakWN2 = item.BreakerWN2;
                        breakWR1 = item.BreakerWR1;
                        breakWR2 = item.BreakerWR2;
                        breakWBHN1 = item.BreakerW_BHN1;
                        breakWBHN2 = item.BreakerW_BHN2;
                        breakWBHR1 = item.BreakerW_BHR1;
                        breakWBHR2 = item.BreakerW_BHR2;
                        breakWAHN1 = item.BreakerW_AHN1;
                        breakWAHN2 = item.BreakerW_AHN2;
                        breakWAHR1 = item.BreakerW_AHR1;
                        breakWAHR2 = item.BreakerW_AHR2;
                    }
                    else
                    {
                        ret.Compounds += (string.IsNullOrEmpty(ret.Compounds)) ? item.Compound : ", " + item.Compound;
                    }
                    iCnt++;
                }

                if (null != ret && ret.MasterId.HasValue)
                {
                    ret.InitSpecs();
                    ret.Ph = DIPSolutionPH.Create(ret, PhN, PhR);
                    ret.Ph.AllowReTest = ret.AloowRetest;

                    ret.Temperature = DIPSolutionTempurature.Create(ret, TempN, TempR);
                    ret.Temperature.AllowReTest = ret.AloowRetest;

                    ret.Viscosity = DIPSolutionViscosity.Create(ret, ViscosityN, ViscosityR);
                    ret.Viscosity.AllowReTest = ret.AloowRetest;

                    var spec = ret.Specs.FindByPropertyNo(13);
                    ret.TSC = DIPSolutionTSC.Create(ret, 2,
                        breakWN1, breakWN2, breakWR1, breakWR2,
                        breakWBHN1, breakWBHN2, breakWBHR1, breakWBHR2,
                        breakWAHN1, breakWAHN2, breakWAHR1, breakWAHR2,
                        ret.AloowRetest);
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return ret;
        }

        #endregion

        #endregion
    }

    #endregion
}
