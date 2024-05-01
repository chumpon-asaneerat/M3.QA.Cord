#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region SolutionSampleTestData

    /// <summary>
    /// The Solution Sample Test Data class.
    /// </summary>
    public class SolutionSampleTestData
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

        #endregion

        #region Static Methods

        #region GetByLotNo

        /// <summary>
        /// Gets CordSampleTestData by Lot No.
        /// </summary>
        /// <param name="value">The CordSampleTestData item to save.</param>
        /// <returns></returns>
        public static SolutionSampleTestData GetByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            SolutionSampleTestData ret = null;

            if (string.IsNullOrWhiteSpace(lotNo))
            {
                return ret;
            }

            try
            {
                var items = Utils.M_CheckSolutionLotReceive.Gets(lotNo).Value();

                if (null == items || items.Count <= 0)
                    return ret;
                
                ret = new SolutionSampleTestData();
                foreach (var item in items)
                {
                    ret.MasterId = item.MasterId;
                    ret.MasterId = item.MasterId;
                    ret.LotNo = item.LotNo;

                    ret.Compounds += (string.IsNullOrEmpty(ret.Compounds)) ? item.Compound : ", " + item.Compound;

                    ret.SendDate = item.SendDate;
                    ret.SendBy = item.SendBy;

                    ret.ForecastFinishDate = item.ForecastFinishDate;
                    ret.ValidDate = item.ValidDate;

                    ret.InputBy = item.InputBy;
                    ret.InputDate = item.InputDate;
                    ret.EditBy = item.EditBy;
                    ret.EditDate = item.EditDate;
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
