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

        #region Test Properties

        public DIPSolutionPH Ph { get; set; }
        public DIPSolutionTempurature Temperature { get; set; }
        public DIPSolutionViscosity Viscosity { get; set; }

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
                    ret.Temperature = DIPSolutionTempurature.Create(ret, TempN, TempR);
                    ret.Viscosity = DIPSolutionViscosity.Create(ret, ViscosityN, ViscosityR);
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
