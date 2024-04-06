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
using NLib.Reflection;

#endregion

namespace M3.QA.Models
{
    #region CordAdhesionForceSubProperty

    /// <summary>
    /// The CordAdhesionForceSubProperty class.
    /// </summary>
    public class CordAdhesionForceSubProperty : NInpc
    {
        #region Internal Variables

        private List<Func<decimal?>> _GetNs;
        private List<Action<decimal?>> _SetNs;
        private List<Func<decimal?>> _GetRs;
        private List<Action<decimal?>> _SetRs;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordAdhesionForceSubProperty() : base()
        {
            #region Init Get/Set link methods

            // Get N
            _GetNs = new List<Func<decimal?>>()
            {
                () => { return this.N1; },
                () => { return this.N2; }
            };
            // Set N
            _SetNs = new List<Action<decimal?>>()
            {
                (value) => { this.N1 = value; },
                (value) => { this.N2 = value; }
            };
            // Get R
            _GetRs = new List<Func<decimal?>>()
            {
                () => { return this.R1; },
                () => { return this.R2; }
            };
            // Set R
            _SetRs = new List<Action<decimal?>>()
            {
                (value) => { this.R1 = value; },
                (value) => { this.R2 = value; }
            };

            #endregion

            BuildItems(0); // create empty items.
        }

        #endregion

        #region Private Methods

        private void CalcAvg()
        {
            decimal total = decimal.Zero;

            int iCnt = 0;
            if (N1.HasValue && !R1.HasValue)
            {
                total += N1.Value;
                ++iCnt;
            }
            if (N2.HasValue && !R2.HasValue)
            {
                total += N2.Value;
                ++iCnt;
            }

            if (R1.HasValue)
            {
                total += R1.Value;
                ++iCnt;
            }
            if (R2.HasValue)
            {
                total += R2.Value;
                ++iCnt;
            }

            decimal avg = (iCnt > 0) ? (total / iCnt) : 0;
            this.Avg = avg;
            // Raise events
            this.Raise(() => this.Avg);

            if (null != ValueChanges) ValueChanges(); // call parent object to calucate related formula
        }

        private void NotifyItemEvents(int idx)
        {
            if (idx < 0 || idx >= this.Items.Count) 
                return;

            try
            {
                this.Items[idx].RaisePropertyChanges();
            }
            catch { }
        }

        protected internal void BuildItems(int noOfSample)
        {
            Items = new List<CordAdhesionForceSubProperyItem>();

            for (int i = 1; i <= 2; i++)
            {
                if (i > noOfSample) continue; // skip if more than allow no of sample.

                var item = new CordAdhesionForceSubProperyItem();
                item.No = i;
                item.SPNo = SPNo; // assign SPNo
                item.NeedSP = NeedSP;

                // Link get/set methods.
                item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                item.SetN = (null != _SetNs) ? _SetNs[i - 1] : null;
                item.GetR = (null != _GetRs) ? _GetRs[i - 1] : null;
                item.SetR = (null != _SetRs) ? _SetRs[i - 1] : null;

                Items.Add(item);
            }
        }

        #endregion

        #region Internal Properties

        internal Action ValueChanges { get; set; }

        #endregion

        #region Public Properties

        #region LotNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo { get; set; }
        /// <summary>Gets or sets SP No.</summary>
        public int? SPNo { get; set; }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample
        {
            get
            {
                return (null != Items) ? Items.Count : 0;
            }
            set
            {
                BuildItems(value);
                // Raise events
                this.Raise(() => this.NoOfSample);
                this.Raise(() => this.Items);
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
                    this.Raise(() => this.EnableTest);
                    if (null != Items)
                    {
                        foreach (var item in Items)
                        {
                            item.NeedSP = value;
                        }
                    }
                });
            }
        }

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }

        #endregion

        #region Normal Test (1-2)

        public decimal? N1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                    NotifyItemEvents(0);
                });
            }
        }
        public decimal? N2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                    NotifyItemEvents(1);
                });
            }
        }

        #endregion

        #region Re Test (1-2)

        public decimal? R1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                    NotifyItemEvents(0);
                });
            }
        }
        public decimal? R2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                    NotifyItemEvents(1);
                });
            }
        }

        #endregion

        #region Avg

        public decimal? Avg
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {

                });
            }
        }

        #endregion

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #region Items

        /// <summary>
        /// Gets Items.
        /// </summary>
        public List<CordAdhesionForceSubProperyItem> Items { get; set; }

        #endregion

        #endregion

        #region Static Methods

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordAdhesionForceSubProperty src, CordAdhesionForceSubProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            dst.N1 = src.N1;
            dst.N2 = src.N2;

            dst.R1 = src.R1;
            dst.R2 = src.R2;

            /*
            dst.AllowN1 = src.AllowN1;
            dst.AllowN2 = src.AllowN2;

            dst.AllowR1 = src.AllowR1;
            dst.AllowR2 = src.AllowR2;

            dst.Avg = src.Avg;
            */
        }

        #endregion
    }

    #endregion

    #region CordAdhesionForceSubProperyItem

    /// <summary>
    /// The CordAdhesionForceSubProperyItem class.
    /// </summary>
    public class CordAdhesionForceSubProperyItem : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordAdhesionForceSubProperyItem() : base() { }

        #endregion

        #region virtual methods

        protected virtual void CheckRange() { }

        protected internal void RaisePropertyChanges()
        {
            Raise(() => this.N);
            Raise(() => this.R);
        }

        #endregion

        #region Protected Properties

        protected internal Func<decimal?> GetN { get; set; }
        protected internal Action<decimal?> SetN { get; set; }

        protected internal Func<decimal?> GetR { get; set; }
        protected internal Action<decimal?> SetR { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets SP No.
        /// </summary>
        public int? SPNo
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    // Raise events
                    Raise(() => this.EnableNormalTest);
                    Raise(() => this.EnableReTest);
                });
            }
        }

        /// <summary>
        /// Gets or sets Test No. (N1, N2, N3)
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Gets or sets Test Value.
        /// </summary>
        public decimal? N
        {
            get
            {
                var val = (null != GetN) ? GetN() : new decimal?();
                return val;
            }
            set
            {
                if (null != SetN)
                {
                    SetN(value);
                    // Raise events
                    Raise(() => this.EnableNormalTest);
                    Raise(() => this.EnableReTest);
                    Raise(() => this.N);
                }
            }
        }
        /// <summary>
        /// Gets or sets Re Test Value.
        /// </summary>
        public decimal? R
        {
            get
            {
                return (null != GetR) ? GetR() : new decimal?();
            }
            set
            {
                if (null != SetR)
                {
                    SetR(value);
                    // Raise events
                    Raise(() => this.R);
                }
            }
        }
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP { get; set; }
        /// <summary>
        /// Check is Enable Normal Test. 
        /// </summary>
        public bool EnableNormalTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }
        /// <summary>
        /// Check is Enable Re Test. 
        /// </summary>
        public bool EnableReTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }
        /// <summary>Gets N Display Caption.</summary>
        public string NCaption { get { return "N" + No.ToString(); } set { } }
        /// <summary>Gets R Display Caption.</summary>
        public string RCaption { get { return "R" + No.ToString(); } set { } }

        #endregion
    }

    #endregion

    #region CordAdhesionForceProperty

    /// <summary>
    /// The CordAdhesionForceProperty class.
    /// </summary>
    public class CordAdhesionForceProperty : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordAdhesionForceProperty() : base() 
        {
            PeakPoint = new CordAdhesionForceSubProperty();
            // only PeakPoint change need to calc formula for AdhesionForce
            PeakPoint.ValueChanges = CalculateFormula;

            AdhesionForce = new CordAdhesionForceSubProperty();
        }

        #endregion

        #region Private Methods

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
            }
        }

        private void UpdateProperties()
        {
            if (null == PeakPoint) PeakPoint = new CordAdhesionForceSubProperty();

            PeakPoint.LotNo = LotNo;
            PeakPoint.PropertyNo = PropertyNo;
            PeakPoint.SPNo = SPNo;
            PeakPoint.NoOfSample = NoOfSample;
            PeakPoint.NeedSP = NeedSP;
            if (null != PeakPoint.Items)
            {
                foreach (var item in PeakPoint.Items)
                {
                    item.NeedSP = NeedSP;
                }
            }

            if (null == AdhesionForce) AdhesionForce = new CordAdhesionForceSubProperty();
            AdhesionForce.SPNo = SPNo;
            AdhesionForce.LotNo = LotNo;
            AdhesionForce.PropertyNo = PropertyNo;
            AdhesionForce.SPNo = SPNo;
            AdhesionForce.NoOfSample = NoOfSample;
            AdhesionForce.NeedSP = NeedSP;
            if (null != AdhesionForce.Items)
            {
                foreach (var item in AdhesionForce.Items)
                {
                    item.NeedSP = NeedSP;
                }
            }

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
        public CordAdhesionForceSubProperty PeakPoint { get; set; }
        /// <summary>Gets or sets AdhesionForce.</summary>
        public CordAdhesionForceSubProperty AdhesionForce { get; set; }

        #endregion

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #endregion

        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static List<CordAdhesionForceProperty> Create(CordSampleTestData value)
        {
            List<CordAdhesionForceProperty> results = new List<CordAdhesionForceProperty>();
            if (null == value)
                return results;

            // For Adhesion Force Proepty No = 4
            var total = (value.MasterId.HasValue) ?
                Utils.M_GetPropertyTotalNByItem.GetByItem(value.MasterId.Value, 4).Value() : null;
            int noOfSample = (null != total) ? total.NoSample : 0;
            int maxSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            for (int i = 1; i <= maxSP; i++)
            {
                if (i > 7) continue;

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
                var inst = new CordAdhesionForceProperty()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 4, // Adhesion Force = 4
                    SPNo = SP,
                    NeedSP = true,
                    NoOfSample = noOfSample
                };

                results.Add(inst);
            }

            var existItems = (value.MasterId.HasValue) ? GetsByLotNo(value.LotNo).Value() : null;
            if (null != existItems && null != results)
            {
                int idx = -1;
                foreach (var item in existItems)
                {
                    idx = results.FindIndex((x) => { return x.SPNo == item.SPNo; });
                    if (idx != -1)
                    {
                        // need to set because not return from db.
                        item.NoOfSample = results[idx].NoOfSample;
                        // Clone anther properties
                        Clone(item, results[idx]);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordAdhesionForceProperty src, CordAdhesionForceProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            CordAdhesionForceSubProperty.Clone(src.PeakPoint, dst.PeakPoint);
            CordAdhesionForceSubProperty.Clone(src.AdhesionForce, dst.AdhesionForce);
        }

        /// <summary>
        /// Gets CordAdhesionForceProperty by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <returns></returns>
        public static NDbResult<List<CordAdhesionForceProperty>> GetsByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordAdhesionForceProperty>> ret = new NDbResult<List<CordAdhesionForceProperty>>();

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
                List<CordAdhesionForceProperty> results = new List<CordAdhesionForceProperty>();

                var items = Utils.P_GetAdhesionForceByLot.GetByLot(lotNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new CordAdhesionForceProperty();
                        inst.LotNo = item.LotNo;
                        inst.PropertyNo = 4;
                        inst.SPNo = item.SPNo;

                        inst.NeedSP = true;
                        //inst.NoOfSample = 2; // ???

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
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NDbResult<CordAdhesionForceProperty> Save(CordAdhesionForceProperty value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordAdhesionForceProperty> ret = new NDbResult<CordAdhesionForceProperty>();

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
    }

    #endregion
}
