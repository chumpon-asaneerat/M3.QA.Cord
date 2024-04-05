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
    #region Utils class

    public static class Utils
    {
        #region M_GetPropertyTotalNByItem

        public class M_GetPropertyTotalNByItem
        {
            #region Public Properties

            public int MasterId { get; set; }
            public int PropertyNo { get; set; }
            public int NoSample { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<M_GetPropertyTotalNByItem> GetByItem(int masterId, int propertyNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<M_GetPropertyTotalNByItem> ret = new NDbResult<M_GetPropertyTotalNByItem>();

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

                p.Add("@MasterId", masterId);
                p.Add("@PropertyNo", propertyNo);

                try
                {
                    var items = cnn.Query<M_GetPropertyTotalNByItem>("M_GetPropertyTotalNByItem", p, commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList().FirstOrDefault() : null;

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

        #endregion
    }

    #endregion

    #region CordTestProperty

    /// <summary>
    /// The CordTestProperty class.
    /// </summary>
    public class CordTestProperty : NInpc
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
        public CordTestProperty() : base()
        {
            #region Init Get/Set link methods

            // Get N
            _GetNs = new List<Func<decimal?>>()
            {
                () => { return this.N1; },
                () => { return this.N2; },
                () => { return this.N3; },
                () => { return this.N4; },
                () => { return this.N5; },
                () => { return this.N6; },
                () => { return this.N7; }
            };
            // Set N
            _SetNs = new List<Action<decimal?>>()
            {
                (value) => { this.N1 = value; },
                (value) => { this.N2 = value; },
                (value) => { this.N3 = value; },
                (value) => { this.N4 = value; },
                (value) => { this.N5 = value; },
                (value) => { this.N6 = value; },
                (value) => { this.N7 = value; }
            };
            // Get R
            _GetRs = new List<Func<decimal?>>()
            {
                () => { return this.R1; },
                () => { return this.R2; },
                () => { return this.R3; },
                () => { return this.R4; },
                () => { return this.R5; },
                () => { return this.R6; },
                () => { return this.R7; }
            };
            // Set R
            _SetRs = new List<Action<decimal?>>()
            {
                (value) => { this.R1 = value; },
                (value) => { this.R2 = value; },
                (value) => { this.R3 = value; },
                (value) => { this.R4 = value; },
                (value) => { this.R5 = value; },
                (value) => { this.R6 = value; },
                (value) => { this.R7 = value; }
            };

            #endregion

            BuildItems(0); // create empty items.
        }

        #endregion

        #region Private Methods

        private void CalcAvg()
        {

        }

        protected internal void BuildItems(int noOfSample)
        {
            Items = new List<CordTestPropertyItem>();
            CordTestPropertyItem item;
            for (int i = 1; i < 7; i++) 
            {
                if (i > noOfSample) continue; // skip if more than allow no of sample.

                item = new CordTestPropertyItem();
                item.No = i;
                item.SPNo = SPNo; // assign SPNo

                // Link get/set methods.
                item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                item.SetN = (null != _SetNs) ? _SetNs[i - 1] : null;
                item.GetR = (null != _GetRs) ? _GetRs[i - 1] : null;
                item.SetR = (null != _SetRs) ? _SetRs[i - 1] : null;

                Items.Add(item);
            }
        }

        #endregion

        #region Public Properties

        #region LotNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
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

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return SPNo.HasValue; }
            set { }
        }

        #endregion

        #region Normal Test (1-7)

        public decimal? N1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
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
                });
            }
        }
        public decimal? N3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }

        #endregion

        #region Re Test (1-7)

        public decimal? R1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
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
                });
            }
        }
        public decimal? R3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
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

        /// <summary>Gets is N1 visible on UI.</summary>
        public bool AllowN1 { get; set; }

        /// <summary>Gets is R1 visible on UI.</summary>
        public bool AllowR1 { get; set; }

        #region Items

        /// <summary>
        /// Gets Items.
        /// </summary>
        public List<CordTestPropertyItem> Items { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region CordTestPropertyItem

    public class CordTestPropertyItem : NInpc 
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordTestPropertyItem() : base() { }

        #endregion

        #region virtual methods

        protected virtual void CheckRange() { }

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

        public decimal? N
        {
            get 
            { 
                return (null != GetN) ? GetN() : new decimal?();
            }
            set
            {
                if (null != SetN)
                {
                    SetN(value);
                    // Raise events
                    Raise(() => this.EnableNormalTest);
                    Raise(() => this.EnableReTest);
                }
            }
        }
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
                }
            }
        }

        public bool EnableNormalTest
        {
            get { return SPNo.HasValue; }
            set { }
        }
        public bool EnableReTest
        {
            get { return SPNo.HasValue && N.HasValue; } 
            set { } 
        }

        public string NCaption { get { return "N" + No.ToString(); } set { } }
        public string RCaption { get { return "R" + No.ToString(); } set { } }

        #endregion
    }

    #endregion

    #region CordTensileStrengthProperty

    /// <summary>
    /// The CordTensileStrengthProperty class.
    /// </summary>
    public class CordTensileStrengthProperty : CordTestProperty
    {
        #region Public Properties

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="maxSP"></param>
        /// <returns></returns>
        internal static List<CordTensileStrengthProperty> Create(string lotNo, int maxSP, int noOfSample,
            int? sp1,
            int? sp2,
            int? sp3,
            int? sp4,
            int? sp5,
            int? sp6,
            int? sp7)
        {
            List<CordTensileStrengthProperty> results = new List<CordTensileStrengthProperty>();
            for (int i = 1; i <= maxSP; i++)
            {
                if (i > 7) continue;

                int? SP;

                switch (i)
                {
                    case 1: SP = sp1; break;
                    case 2: SP = sp2; break;
                    case 3: SP = sp3; break;
                    case 4: SP = sp4; break;
                    case 5: SP = sp5; break;
                    case 6: SP = sp6; break;
                    case 7: SP = sp7; break;
                    default: SP = new int?(); break;
                }
                var inst = new CordTensileStrengthProperty() 
                { 
                    LotNo = lotNo, 
                    SPNo = SP,
                    NoOfSample = noOfSample 
                };

                results.Add(inst);
            }
            return results;
        }
        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordTensileStrengthProperty src, CordTensileStrengthProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            dst.N1 = src.N1;
            dst.N2 = src.N2;
            dst.N3 = src.N3;
            dst.N4 = src.N4;
            dst.N5 = src.N5;
            dst.N6 = src.N6;
            dst.N7 = src.N7;

            dst.R1 = src.R1;
            dst.R2 = src.R2;
            dst.R3 = src.R3;
            dst.R4 = src.R4;
            dst.R5 = src.R5;
            dst.R6 = src.R6;
            dst.R7 = src.R7;

            /*
            dst.AllowN1 = src.AllowN1;
            dst.AllowN2 = src.AllowN2;
            dst.AllowN3 = src.AllowN3;
            dst.AllowN4 = src.AllowN4;
            dst.AllowN5 = src.AllowN5;
            dst.AllowN6 = src.AllowN6;
            dst.AllowN7 = src.AllowN7;

            dst.AllowR1 = src.AllowR1;
            dst.AllowR2 = src.AllowR2;
            dst.AllowR3 = src.AllowR3;
            dst.AllowR4 = src.AllowR4;
            dst.AllowR5 = src.AllowR5;
            dst.AllowR6 = src.AllowR6;
            dst.AllowR7 = src.AllowR7;

            dst.Avg = src.Avg;
            */
        }
        /// <summary>
        /// Gets CordTensileStrengthProperty by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <param name="masterId">The MasterId.</param>
        /// <returns></returns>
        public static NDbResult<List<CordTensileStrengthProperty>> GetsByLotNo(string lotNo, int masterId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordTensileStrengthProperty>> ret = new NDbResult<List<CordTensileStrengthProperty>>();

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
                var items = cnn.Query<CordTensileStrengthProperty>("P_GetTensileDataByLot",
                    p, commandType: CommandType.StoredProcedure).ToList();
                ret.Success(items);
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
        public static NDbResult<CordTensileStrengthProperty> Save(CordTensileStrengthProperty value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTensileStrengthProperty> ret = new NDbResult<CordTensileStrengthProperty>();

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

            p.Add("@n1", value.N1);
            p.Add("@n2", value.N2);
            p.Add("@n3", value.N3);
            p.Add("@r1", value.R1);
            p.Add("@r2", value.R2);
            p.Add("@r3", value.R3);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveTensile", p, commandType: CommandType.StoredProcedure);
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

        // Test Properties
        public List<CordTensileStrengthProperty> TensileStrengths { get; set; }

        #endregion

        #region Private Methods

        private void InitTensileStrengths(string lotNo, int masterId, int totalSP)
        {
            // For Tensile Strength Proepty No = 1
            var total = Utils.M_GetPropertyTotalNByItem.GetByItem(masterId, 1).Value();
            int noOfSample = (null != total) ? total.NoSample : 0;

            TensileStrengths = CordTensileStrengthProperty.Create(lotNo, totalSP, noOfSample,
                this.SP1, this.SP2, this.SP3, this.SP4, this.SP5, this.SP6, this.SP7);

            var existItems = CordTensileStrengthProperty.GetsByLotNo(
                lotNo, masterId).Value();
            if (null != existItems && null != TensileStrengths)
            {
                int idx = -1;
                foreach (var item in existItems)
                {
                    item.NoOfSample = noOfSample; // need to set because not return from db.

                    idx = TensileStrengths.FindIndex((x) => { return x.SPNo == item.SPNo; });
                    if (idx != -1)
                    {
                        CordTensileStrengthProperty.Clone(item, TensileStrengths[idx]);
                    }
                    idx++;
                }
            }
        }

        private void InitTestProperties()
        {
            if (TotalSP.HasValue && MasterId.HasValue)
            {
                InitTensileStrengths(LotNo, MasterId.Value, TotalSP.Value);
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