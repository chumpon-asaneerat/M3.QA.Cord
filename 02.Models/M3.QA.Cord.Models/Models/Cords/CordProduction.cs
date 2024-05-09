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
    /// <summary>
    /// The Cord Production class.
    /// </summary>
    public class CordProduction
    {
        #region Public Methods

        private CordTestSpec GetSpec(Utils.M_GetReportTestSpecByMasterid value)
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

            return spec;
        }

        public void LoadProperties()
        {
            Properties = new List<CordProductionTest>();

            var rpts = Utils.M_GetReportTestSpecByMasterid.Gets(this.MasterId).Value();
            if (null == rpts) 
                return;
            foreach (var rpt in rpts) 
            {
                var inst = new CordProductionTest();
                inst.ItemCode = rpt.ItemCode;
                inst.MasterId = rpt.MasterId;

                inst.PropertyNo = rpt.PropertyNo;
                inst.PropertyName = rpt.PropertName;

                inst.NoOfSample = rpt.NoSample;

                inst.Spec = GetSpec(rpt); // Setup Spec.

                Properties.Add(inst);
            }
        }

        #endregion


        #region Public Properties

        public int? MasterId { get; set; }
        public string LotNo { get; set; }
        public string ItemCode { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string YarnCode { get; set; }
        public decimal PiNoSL { get; set; }
        public string SpindleNo { get; set; }
        public DateTime? InputDate { get; set; }
        public string InputTestBy { get; set; }

        public int? CoaNo { get; set; }

        public string sInputDate
        {
            get 
            {
                return (InputDate.HasValue) ? 
                    InputDate.Value.ToString("dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) : string.Empty;
            }
            set { }
        }

        public List<CordProductionTest> Properties { get; set; }

        #endregion

        #region Static Methods

        public static NDbResult<List<CordProduction>> Gets(
            string lotNo, DateTime? dateform, DateTime? dateto)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordProduction>> ret = new NDbResult<List<CordProduction>>();

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
                var items = cnn.Query<CordProduction>("P_SearchTestCordProduction", p, commandType: CommandType.StoredProcedure);
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
