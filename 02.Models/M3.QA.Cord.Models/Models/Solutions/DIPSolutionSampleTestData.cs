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
            bool isFinal = (string.IsNullOrEmpty(Compounds) ? false : Compounds.Trim() != "RF");

            bool ret = false;
            if (isFinal)
            {
                ret = IsMultiout();
            }
            return ret;
        }

        #endregion

        #region IsMultiout

        private bool IsMultiout()
        {
            // Case one of property is out of spec so need to allow to enter retest in related properties
            bool ret = false;
            if (null != Ph)
            {
                ret |= Ph.N1Out;
            }
            if (null != Temperature)
            {
                ret |= Temperature.N1Out;
            }
            return ret;
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

        private void AfterCalcAvg(string caller)
        {
            if (caller == "PH")
            {
                if (null != Temperature)
                {
                    Temperature.RaiseNOutChanges();
                }
            }
            else if (caller == "TEMP")
            {
                if (null != Ph)
                {
                    Ph.RaiseNOutChanges();
                }
            }
        }

        #endregion

        #region Static Methods

        #region GetByLotNo

        /// <summary>
        /// Gets DIPSolutionSampleTestData by Lot No.
        /// </summary>
        /// <param name="lotNo"></param>
        /// <param name="compound"></param>
        /// <returns></returns>
        public static DIPSolutionSampleTestData GetByLotNo(string lotNo, string compound)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            DIPSolutionSampleTestData ret = null;

            if (string.IsNullOrWhiteSpace(lotNo))
            {
                return ret;
            }

            try
            {
                var items = Utils.M_CheckSolutionLotReceive.Gets(lotNo, compound).Value();

                if (null == items || items.Count <= 0)
                    return ret;

                ret = new DIPSolutionSampleTestData();

                decimal? PhN = new decimal?();
                decimal? PhR = new decimal?();
                decimal? TempN = new decimal?();
                decimal? TempR = new decimal?();
                decimal? ViscosityN = new decimal?();
                decimal? ViscosityR = new decimal?();

                decimal? beakWN1 = new decimal?(); 
                decimal? beakWN2 = new decimal?();
                decimal? beakWR1 = new decimal?(); 
                decimal? beakWR2 = new decimal?();
                decimal? beakWBHN1 = new decimal?();
                decimal? beakWBHN2 = new decimal?();
                decimal? beakWBHR1 = new decimal?();
                decimal? beakWBHR2 = new decimal?();
                decimal? beakWAHN1 = new decimal?();
                decimal? beakWAHN2 = new decimal?();
                decimal? beakWAHR1 = new decimal?(); 
                decimal? beakWAHR2 = new decimal?();


                var item = items.FirstOrDefault();
                if (null == item)
                    return ret;

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

                beakWN1 = item.BeakerWN1;
                beakWN2 = item.BeakerWN2;
                beakWR1 = item.BeakerWR1;
                beakWR2 = item.BeakerWR2;
                beakWBHN1 = item.BeakerW_BHN1;
                beakWBHN2 = item.BeakerW_BHN2;
                beakWBHR1 = item.BeakerW_BHR1;
                beakWBHR2 = item.BeakerW_BHR2;
                beakWAHN1 = item.BeakerW_AHN1;
                beakWAHN2 = item.BeakerW_AHN2;
                beakWAHR1 = item.BeakerW_AHR1;
                beakWAHR2 = item.BeakerW_AHR2;


                // Read from PLC
                var plcs = Models.Utils.Plc_GetPhTemperatureByLot.Gets(lotNo, compound).Value();
                if (null != plcs && plcs.Count > 0)
                {
                    foreach (var plc in plcs) 
                    {
                        if (plc.TestType == "NORMAL")
                        {
                            if (plc.Ph.HasValue) PhN = plc.Ph.Value;
                            if (plc.Tempturature.HasValue) TempN = plc.Tempturature.Value;
                        }
                        else if (plc.TestType == "RETEST")
                        {
                            if (plc.Ph.HasValue) PhR = plc.Ph.Value;
                            if (plc.Tempturature.HasValue) TempR = plc.Tempturature.Value;
                        }
                    }
                }

                if (null != ret && ret.MasterId.HasValue)
                {
                    ret.InitSpecs();
                    ret.Ph = DIPSolutionPH.Create(ret, PhN, PhR);
                    ret.Ph.AllowReTest = ret.AloowRetest;
                    ret.Ph.EnableMultiPropertyTest = true;
                    ret.Ph.GetNMultiOut = ret.IsMultiout;
                    ret.Ph.CalcAvgCallback = ret.AfterCalcAvg;

                    ret.Temperature = DIPSolutionTempurature.Create(ret, TempN, TempR);
                    ret.Temperature.AllowReTest = ret.AloowRetest;
                    ret.Temperature.EnableMultiPropertyTest = true;
                    ret.Temperature.GetNMultiOut = ret.IsMultiout;
                    ret.Temperature.CalcAvgCallback = ret.AfterCalcAvg;

                    ret.Viscosity = DIPSolutionViscosity.Create(ret, ViscosityN, ViscosityR);
                    ret.Viscosity.AllowReTest = ret.AloowRetest;

                    var spec = ret.Specs.FindByPropertyNo(13);
                    ret.TSC = DIPSolutionTSC.Create(ret,
                        beakWN1, beakWN2, beakWR1, beakWR2,
                        beakWBHN1, beakWBHN2, beakWBHR1, beakWBHR2,
                        beakWAHN1, beakWAHN2, beakWAHR1, beakWAHR2,
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

        #region Save

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static NDbResult<DIPSolutionSampleTestData> Save(DIPSolutionSampleTestData value,
            QA.Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<DIPSolutionSampleTestData> ret = new NDbResult<DIPSolutionSampleTestData>();

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

            // Ph
            p.Add("@phn", (null != value.Ph) ? value.Ph.N1 : new decimal?());
            p.Add("@phr", (null != value.Ph) ? value.Ph.N1R1 : new decimal?());
            // Temperature
            p.Add("@temperaturen", (null != value.Temperature) ? value.Temperature.N1 : new decimal?());
            p.Add("@temperaturer", (null != value.Temperature) ? value.Temperature.N1R1 : new decimal?());
            // Viscosity
            p.Add("@viscosityn", (null != value.Viscosity) ? value.Viscosity.N1 : new decimal?());
            p.Add("@viscosityr", (null != value.Viscosity) ? value.Viscosity.N1R1 : new decimal?());

            // BreakerWeight
            p.Add("@beakerwn1", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.N1 : new decimal?());
            p.Add("@beakerwn2", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.N2 : new decimal?());
            p.Add("@beakerwr1", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.N1R1 : new decimal?());
            p.Add("@beakerwr2", (null != value.TSC && null != value.TSC.BeakerWeight) ? value.TSC.BeakerWeight.N2R1 : new decimal?());

            // BreakerWeightBeforeHeat
            p.Add("@beakerw_bhn1", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.N1 : new decimal?());
            p.Add("@beakerw_bhn2", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.N2 : new decimal?());
            p.Add("@beakerw_bhr1", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.N1R1 : new decimal?());
            p.Add("@beakerw_bhr2", (null != value.TSC && null != value.TSC.BeakerWeightBeforeHeat) ? value.TSC.BeakerWeightBeforeHeat.N2R1 : new decimal?());

            // BreakerWeightAfterHeat
            p.Add("@beakerw_ahn1", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.N1 : new decimal?());
            p.Add("@beakerw_ahn2", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.N2 : new decimal?());
            p.Add("@beakerw_ahr1", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.N1R1 : new decimal?());
            p.Add("@beakerw_ahr2", (null != value.TSC && null != value.TSC.BeakerWeightAfterHeat) ? value.TSC.BeakerWeightAfterHeat.N2R1 : new decimal?());

            // RPU
            p.Add("@tscn1", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.N1 : new decimal?());
            p.Add("@tscn2", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.N2 : new decimal?());
            p.Add("@tscr1", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.N1R1 : new decimal?());
            p.Add("@tscr2", (null != value.TSC && null != value.TSC.RPU) ? value.TSC.RPU.N2R1 : new decimal?());

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

    #endregion
}
