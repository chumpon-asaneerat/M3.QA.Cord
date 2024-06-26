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
    public class DIPSolitionPHTestData
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

        #region Test Properties

        public decimal Ph { get; set; }
        public decimal Temperature { get; set; }

        #endregion

        #endregion

        #region Static Methods

        #region Save

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static NDbResult<DIPSolitionPHTestData> Save(DIPSolitionPHTestData value,
            QA.Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPSolitionPHTestData> ret = new NDbResult<DIPSolitionPHTestData>();

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

            string compound;
            if (string.IsNullOrEmpty(value.Compounds))
            {
                compound = null;
            }
            else
            {
                compound = value.Compounds.Contains("Final") ? "Final" : "RF";
            }

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@compound", compound);
            /*
            // Ph
            p.Add("@phn", (null != value.Ph) ? value.Ph.N1 : new decimal?());
            p.Add("@phr", (null != value.Ph) ? value.Ph.R1 : new decimal?());
            // Temperature
            p.Add("@temperaturen", (null != value.Temperature) ? value.Temperature.N1 : new decimal?());
            p.Add("@temperaturer", (null != value.Temperature) ? value.Temperature.R1 : new decimal?());
            // Viscosity
            p.Add("@viscosityn", (null != value.Viscosity) ? value.Viscosity.N1 : new decimal?());
            p.Add("@viscosityr", (null != value.Viscosity) ? value.Viscosity.R1 : new decimal?());

            // BreakerWeight
            p.Add("@beakerwn1", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.N1 : new decimal?());
            p.Add("@beakerwn2", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.N2 : new decimal?());
            p.Add("@beakerwr1", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.R1 : new decimal?());
            p.Add("@beakerwr2", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.R2 : new decimal?());

            // BreakerWeightBeforeHeat
            p.Add("@beakerw_bhn1", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.N1 : new decimal?());
            p.Add("@beakerw_bhn2", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.N2 : new decimal?());
            p.Add("@beakerw_bhr1", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.R1 : new decimal?());
            p.Add("@beakerw_bhr2", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.R2 : new decimal?());

            // BreakerWeightAfterHeat
            p.Add("@beakerw_ahn1", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.N1 : new decimal?());
            p.Add("@beakerw_ahn2", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.N2 : new decimal?());
            p.Add("@beakerw_ahr1", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.R1 : new decimal?());
            p.Add("@beakerw_ahr2", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.R2 : new decimal?());

            // RPU
            p.Add("@tscn1", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.N1 : new decimal?());
            p.Add("@tscn2", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.N2 : new decimal?());
            p.Add("@tscr1", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.R1 : new decimal?());
            p.Add("@tscr2", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.R2 : new decimal?());
            */
            p.Add("@user", (null != user) ? user.FullName : null);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveDipSolutionResult", p, commandType: CommandType.StoredProcedure);
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
}
