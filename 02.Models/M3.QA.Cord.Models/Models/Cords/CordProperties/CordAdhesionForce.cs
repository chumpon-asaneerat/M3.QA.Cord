#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordAdhesionForce

    /// <summary>
    /// The Cord Adhesion Force
    /// </summary>
    public class CordAdhesionForce : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordAdhesionForce() : base()
        {
            PeakPoint = new NRTestProperty();
            // only PeakPoint change need to calc formula for AdhesionForce
            PeakPoint.ValueChanges = CalculateFormula;

            AdhesionForce = new NRTestProperty();
        }

        #endregion

        #region Private Methods

        private void CheckSpec()
        {
            if (null != Spec && null != PeakPoint && null != AdhesionForce)
            {
                // Check AdhesionForce Range.
                AdhesionForce.NOut1 = (AdhesionForce.N1.HasValue) ? Spec.IsOutOfSpec(AdhesionForce.N1.Value) : false;
                AdhesionForce.NOut2 = (AdhesionForce.N2.HasValue) ? Spec.IsOutOfSpec(AdhesionForce.N2.Value) : false;

                AdhesionForce.ROut1 = (AdhesionForce.R1.HasValue) ? Spec.IsOutOfSpec(AdhesionForce.R1.Value) : false;
                AdhesionForce.ROut2 = (AdhesionForce.R2.HasValue) ? Spec.IsOutOfSpec(AdhesionForce.R2.Value) : false;
                // set out of range flag to PeakPoint object
                PeakPoint.NOut1 = AdhesionForce.NOut1;
                PeakPoint.NOut2 = AdhesionForce.NOut2;

                PeakPoint.ROut1 = AdhesionForce.ROut1;
                PeakPoint.ROut2 = AdhesionForce.ROut2;

                // Raise items events
                AdhesionForce.RaiseNOutChanges();
                AdhesionForce.RaiseROutChanges();

                PeakPoint.RaiseNOutChanges();
                PeakPoint.RaiseROutChanges();
            }
        }

        private void CalculateFormula()
        {
            if (null != PeakPoint && null != AdhesionForce)
            {
                AdhesionForce.N1 = (PeakPoint.N1.HasValue) ? PeakPoint.N1.Value / 5 : new decimal?();
                AdhesionForce.N2 = (PeakPoint.N2.HasValue) ? PeakPoint.N2.Value / 5 : new decimal?();
                AdhesionForce.R1 = (PeakPoint.R1.HasValue) ? PeakPoint.R1.Value / 5 : new decimal?();
                AdhesionForce.R2 = (PeakPoint.R2.HasValue) ? PeakPoint.R2.Value / 5 : new decimal?();
                // Raise events
                Raise(() => this.AdhesionForce);

                CheckSpec(); // Check Spec
            }
        }

        private void UpdateProperties()
        {
            if (null == PeakPoint) PeakPoint = new NRTestProperty();

            PeakPoint.LotNo = LotNo;
            PeakPoint.PropertyNo = PropertyNo;
            PeakPoint.SPNo = SPNo;
            PeakPoint.NoOfSample = NoOfSample;
            PeakPoint.NeedSP = NeedSP;

            if (null == AdhesionForce) AdhesionForce = new NRTestProperty();
            AdhesionForce.SPNo = SPNo;
            AdhesionForce.LotNo = LotNo;
            AdhesionForce.PropertyNo = PropertyNo;
            AdhesionForce.SPNo = SPNo;
            AdhesionForce.NoOfSample = NoOfSample;
            AdhesionForce.NeedSP = NeedSP;

            // Check calculate action
            if (null == PeakPoint.ValueChanges)
            {
                PeakPoint.ValueChanges = CalculateFormula;
            }

            CalculateFormula(); // calculate

            this.Raise(() => this.EnableTest);
        }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample/YarnType

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

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }

        #endregion

        #region PeakPoint/AdhesionForce

        /// <summary>Gets or sets PeakPoint.</summary>
        public NRTestProperty PeakPoint { get; set; }
        /// <summary>Gets or sets AdhesionForce.</summary>
        public NRTestProperty AdhesionForce { get; set; }

        #endregion

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

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
        internal static List<CordAdhesionForce> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<CordAdhesionForce> results = new List<CordAdhesionForce>();
            if (null == value)
                return results;

            // For Adhesion Force Proepty No = 4
            int noOfSample = (null != totalN) ? totalN.NoSample : 0;
            int alllowSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            // Adhesion Force Proepty No = 4
            var spec = value.Specs.FindByPropertyNo(4);

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

                var inst = new CordAdhesionForce()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 4, // Adhesion Force = 4
                    SPNo = SP,
                    NeedSP = true,
                    Spec = spec,
                    YarnType = value.YarnType,
                    NoOfSample = noOfSample
                };

                results.Add(inst);

                i++; // increase index
            }

            var existItems = (value.MasterId.HasValue) ? GetsByLotNo(value.LotNo).Value() : null;

            // In case No items need to check has some import data
            if (null == existItems || existItems.Count == 0)
            {
                var imports = Utils.Ex_GetAdhesionDataByLot.Gets(value.LotNo).Value();
                if (null != imports && imports.Count > 0)
                {
                    existItems = new List<CordAdhesionForce>();
                    foreach (var item in imports)
                    {
                        var imp = new CordAdhesionForce()
                        {
                            LotNo = item.LotNo,
                            PropertyNo = 4, // Adhesion Force = 4
                            SPNo = item.SPNo,
                            NeedSP = true,
                            Spec = spec,
                            YarnType = value.YarnType,
                            NoOfSample = noOfSample
                        };

                        if (null == imp.PeakPoint) imp.PeakPoint = new NRTestProperty();
                        imp.PeakPoint.N1 = item.PeakN1;
                        imp.PeakPoint.N2 = item.PeakN2;
                        imp.PeakPoint.R1 = item.PeakR1;
                        imp.PeakPoint.R2 = item.PeakR2;

                        if (null == imp.AdhesionForce) imp.AdhesionForce = new NRTestProperty();
                        imp.AdhesionForce.N1 = item.AdhesionN1;
                        imp.AdhesionForce.N2 = item.AdhesionN2;
                        imp.AdhesionForce.R1 = item.AdhesionR1;
                        imp.AdhesionForce.R2 = item.AdhesionR2;

                        existItems.Add(imp);
                    }
                }
            }

            if (null != existItems && null != results)
            {
                int idx = -1;
                // loop trought all initial results and fill data with the exists on database
                foreach (var item in results)
                {
                    idx = existItems.FindIndex((x) => { return x.SPNo == item.SPNo; });
                    if (idx != -1)
                    {
                        // need to set because not return from db.
                        existItems[idx].NoOfSample = item.NoOfSample;
                        existItems[idx].YarnType = item.YarnType;
                        existItems[idx].Spec = spec; // assign spec
                        // Clone anther properties
                        Clone(existItems[idx], item);
                    }
                }
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
        public static void Clone(CordAdhesionForce src, CordAdhesionForce dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            NRTestProperty.Clone(src.PeakPoint, dst.PeakPoint);
            NRTestProperty.Clone(src.AdhesionForce, dst.AdhesionForce);
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets CordAdhesionForce by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordAdhesionForce>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordAdhesionForce>> ret = new NDbResult<List<CordAdhesionForce>>();

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
                List<CordAdhesionForce> results = new List<CordAdhesionForce>();

                var items = Utils.P_GetAdhesionForceByLot.GetByLot(lotNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new CordAdhesionForce();
                        inst.LotNo = item.LotNo;
                        inst.PropertyNo = 4;
                        inst.SPNo = item.SPNo;

                        inst.NeedSP = true;
                        //inst.NoOfSample = 2; // ???

                        if (null != inst.PeakPoint)
                        {
                            inst.PeakPoint.N1 = item.PeakN1;
                            inst.PeakPoint.N2 = item.PeakN2;
                            inst.PeakPoint.R1 = item.PeakR1;
                            inst.PeakPoint.R2 = item.PeakR2;
                        }
                        if (null != inst.AdhesionForce)
                        {
                            inst.AdhesionForce.N1 = item.AdhesionN1;
                            inst.AdhesionForce.N2 = item.AdhesionN2;
                            inst.AdhesionForce.R1 = item.AdhesionR1;
                            inst.AdhesionForce.R2 = item.AdhesionR2;
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
        public static NDbResult<CordAdhesionForce> Save(CordAdhesionForce value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordAdhesionForce> ret = new NDbResult<CordAdhesionForce>();

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

            p.Add("@peakn1", (null != value.PeakPoint) ? value.PeakPoint.N1 : new decimal?());
            p.Add("@peakn2", (null != value.PeakPoint) ? value.PeakPoint.N2 : new decimal?());
            p.Add("@peakr1", (null != value.PeakPoint) ? value.PeakPoint.R1 : new decimal?());
            p.Add("@peakr2", (null != value.PeakPoint) ? value.PeakPoint.R2 : new decimal?());
            p.Add("@adforcen1", (null != value.AdhesionForce) ? value.AdhesionForce.N1 : new decimal?());
            p.Add("@adforcen2", (null != value.AdhesionForce) ? value.AdhesionForce.N2 : new decimal?());
            p.Add("@adforcer1", (null != value.AdhesionForce) ? value.AdhesionForce.R1 : new decimal?());
            p.Add("@adforcer2", (null != value.AdhesionForce) ? value.AdhesionForce.R2 : new decimal?());

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveAdhesionForce", p, commandType: CommandType.StoredProcedure);
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
