#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;
using M3.QA.Models.Solutions;

#endregion

namespace M3.QA.Models
{
    public class DIPSolutionProduction : NInpc
    {
        #region Public Methods

        public void LoadProperties()
        {
            var rpts = Utils.M_GetReportTestSpecByMasterid.Gets(MasterId).Value();
            if (null != rpts)
            {
                // PH
                PHSpec = GetSpec(rpts.FindByPropertyNo(16));
                // Temp
                TempturatureSpec = GetSpec(rpts.FindByPropertyNo(17));
                // Viscosity
                ViscositySpec = GetSpec(rpts.FindByPropertyNo(18));
                // TSC
                TSCSpec = GetSpec(rpts.FindByPropertyNo(13));
            }
        }

        public void CalcStat()
        {
            decimal total = decimal.Zero;
            int iCnt = 0;

            #region Calc Avg

            lock (this)
            {
                if (TSCN1.HasValue)
                {
                    // use N to calc avg
                    total += TSCN1.Value;
                    ++iCnt;
                }
                if (TSCN2.HasValue)
                {
                    // use N to calc avg
                    total += TSCN2.Value;
                    ++iCnt;
                }
            }

            #endregion

            // Calc average value.
            this.TSCAvg = (iCnt > 0) ? (total / iCnt) : new decimal?();
        }

        #endregion

        #region Public Properties

        public int? MasterId { get; set; }
        public string LotNo { get; set; }
        public string ItemCode { get; set; }
        public string UserName { get; set; }

        public DateTime? ValidDate { get; set; }
        public DateTime? ManufacturingDate { get; set; }
        public DateTime? InputDate { get; set; }
        public string SaveBy { get; set; }
        public int? CoaNo { get; set; }

        public CordTestSpec PHSpec { get; set; }
        public decimal? PH { get; set; }

        public CordTestSpec TempturatureSpec { get; set; }
        public decimal? Tempturature { get; set; }

        public CordTestSpec ViscositySpec { get; set; }
        public decimal? Viscosity { get; set; }

        public CordTestSpec TSCSpec { get; set; }
        public decimal? TSCN1 { get; set; }
        public decimal? TSCN2 { get; set; }
        /// <summary>Gets or sets Avg (or mean) value.</summary>
        public decimal? TSCAvg
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }

        public string sInputDate
        {
            get
            {
                return (InputDate.HasValue) ?
                    InputDate.Value.ToString("dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) : string.Empty;
            }
            set { }
        }

        #endregion

        #region Static Methods

        private static CordTestSpec GetSpec(Utils.M_GetReportTestSpecByMasterid value)
        {
            CordTestSpec spec = null;
            if (null == value)
                return spec;

            // Setup Spec
            spec = new CordTestSpec();
            spec.ItemCode = value.ItemCode;
            spec.MasterId = value.MasterId;

            spec.PropertyNo = value.PropertyNo;
            spec.PropertyName = value.PropertName;
            spec.NoSample = value.NoSample;

            spec.SpecId = value.SpecId;
            spec.SpecDesc = value.SpecDesc;

            spec.UnitId = value.UnitId;
            spec.UnitDesc = value.UnitDesc;

            spec.OptionId = value.OptionId;
            spec.OptionDesc = value.OptionDesc;

            spec.VCenter = value.VCenter;
            spec.VMin = value.VMin;
            spec.VMax = value.VMax;

            spec.TestMethod = value.TestMethod;

            spec.UnitReport = value.UnitReport;

            return spec;
        }

        public static NDbResult<List<DIPSolutionProduction>> Gets(
            string lotNo, DateTime? dateform, DateTime? dateto)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<DIPSolutionProduction>> ret = new NDbResult<List<DIPSolutionProduction>>();

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

            p.Add("@lotno", string.IsNullOrWhiteSpace(lotNo) ? null : lotNo);
            p.Add("@dateform", dateform.HasValue ? dateform.Value.Date : new DateTime?());
            p.Add("@dateto", dateto.HasValue ? dateto.Value.Date : new DateTime?());

            try
            {
                var items = cnn.Query<DIPSolutionProduction>("P_SearchTestSolutionProduction", p, commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

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

        #endregion
    }
}
