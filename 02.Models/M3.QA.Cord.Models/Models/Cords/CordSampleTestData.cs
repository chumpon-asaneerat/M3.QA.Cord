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
    #region CordSampleTestData

    /// <summary>
    /// The CordSampleTestData class.
    /// </summary>
    public class CordSampleTestData : NInpc
    {
        #region Public Properties

        public string LotNo { get; set; }

        public string ItemCode { get; set; }

        public int? MasterId { get; set; }

        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }

        public int? TotalSP { get; set; }
        public DateTime? StartTestDate { get; set; }

        public string Spindle { get; set; }
        public string ELongLoadN { get; set; }

        public bool CanEditStartDate { get; set; }

        /// <summary>The Tensile Strengths Items.</summary>
        public List<CordTensileStrengthProperty> TensileStrengths { get; set; }
        /// <summary>The Elongations Items.</summary>
        public List<CordElongationProperty> Elongations { get; set; }

        #endregion

        #region Private Methods

        private void InitTensileStrengths()
        {
            TensileStrengths = CordTensileStrengthProperty.Create(this);
        }

        private void InitElongations()
        {
            /*
            int noOfSample = 0;
            Utils.M_GetPropertyTotalNByItem total;

            // For Elongation Break Proepty No = 2
            total = Utils.M_GetPropertyTotalNByItem.GetByItem(masterId, 2).Value();
            noOfSample = (null != total) ? total.NoSample : 0;

            var eBreaks = CordElongationBreakProperty.Create(lotNo, totalSP, noOfSample,
                this.SP1, this.SP2, this.SP3, this.SP4, this.SP5, this.SP6, this.SP7);

            var existBreaks = CordElongationBreakProperty.GetsByLotNo(
                lotNo, masterId).Value();

            if (null != existBreaks && null != eBreaks)
            {
                int idx = -1;
                foreach (var item in existBreaks)
                {
                    item.NoOfSample = noOfSample; // need to set because not return from db.

                    idx = eBreaks.FindIndex((x) => { return x.SPNo == item.SPNo; });
                    if (idx != -1)
                    {
                        CordElongationBreakProperty.Clone(item, eBreaks[idx]);
                    }
                    idx++;
                }
            }

            // For Elongation Load Proepty No = 3
            total = Utils.M_GetPropertyTotalNByItem.GetByItem(masterId, 3).Value();
            noOfSample = (null != total) ? total.NoSample : 0;

            var eLoads = CordElongationLoadProperty.Create(lotNo, totalSP, noOfSample,
                this.SP1, this.SP2, this.SP3, this.SP4, this.SP5, this.SP6, this.SP7, this.ELongLoadN);

            var existLoads = CordElongationLoadProperty.GetsByLotNo(
                lotNo, masterId).Value();

            if (null != existLoads && null != eLoads)
            {
                int idx = -1;
                foreach (var item in existLoads)
                {
                    item.NoOfSample = noOfSample; // need to set because not return from db.

                    idx = eLoads.FindIndex((x) => { return x.SPNo == item.SPNo; });
                    if (idx != -1)
                    {
                        CordElongationLoadProperty.Clone(item, eLoads[idx]);
                    }
                    idx++;
                }
            }
            */

            //var results = new List<CordElongationProperty>();
            // Join 2 collections
            /*
            if (null != eBreaks) results.AddRange(eBreaks);
            if (null != eLoads) results.AddRange(eLoads);
            */
            // Sort by SP No.
            //Elongations = results.OrderBy(o => o.SPNo).ThenBy(o => o.PropertyNo).ToList();

            Elongations = CordElongationProperty.Create(this);
        }

        private void InitTestProperties()
        {
            if (TotalSP.HasValue && MasterId.HasValue)
            {
                InitTensileStrengths();
                InitElongations();
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets CordSampleTestData by Lot No.
        /// </summary>
        /// <param name="value">The CordSampleTestData item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordSampleTestData> GetByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordSampleTestData> ret = new NDbResult<CordSampleTestData>();

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

            var p = new DynamicParameters();

            p.Add("@LotNo", lotNo);

            try
            {
                var items = cnn.Query<CordSampleTestData>("M_CheckLotReceive", p, commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;

                if (null != data)
                {
                    // already has date so cannot edit.
                    data.CanEditStartDate = (data.StartTestDate.HasValue) ? false : true;
                    data.InitTestProperties();
                }

                ret.Success(data);
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

        public static NDbResult<CordSampleTestData> Save(CordSampleTestData value, 
            UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordSampleTestData> ret = new NDbResult<CordSampleTestData>();

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

            try
            {
                //cnn.Execute("M_CheckLotReceive", p, commandType: CommandType.StoredProcedure);
                value.TensileStrengths.ForEach(x => 
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    CordTensileStrengthProperty.Save(x);
                });

                ret.Success(value);
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
    }

    #endregion
}