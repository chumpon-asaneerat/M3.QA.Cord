#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.Reflection;
using OfficeOpenXml;
using static NLib.IO.Folders;

#endregion

namespace M3.QA.Models
{
    #region ImportNTestPropertyItem

    /// <summary>
    /// The ImportNTestPropertyItem class.
    /// </summary>
    public class ImportNTestPropertyItem : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImportNTestPropertyItem() : base() { }

        #endregion

        #region virtual methods

        protected internal void RaiseSPNoChanges()
        {
            // Raise relelated events
            Raise(() => this.SPNo);
        }

        protected internal void RaiseYarnTypeChanges()
        {
            // Raise relelated events
            Raise(() => this.YarnType);
        }

        protected internal void RaiseNChanges()
        {
            // Raise relelated events
            Raise(() => this.N);
        }

        #endregion

        #region Protected Properties

        // SP Gets
        protected internal Func<int?> GetSPNo { get; set; }
        // Need SP Gets
        protected internal Func<bool> GetNeedSP { get; set; }
        // YarnType Gets
        protected internal Func<string> GetYarnType { get; set; }
        // N Gets/Sets
        protected internal Func<decimal?> GetN { get; set; }
        protected internal Action<decimal?> SetN { get; set; }

        #endregion

        #region Public Properties

        #region SP/YarnType/No

        /// <summary>
        /// Gets or sets SP No.
        /// </summary>
        public int? SPNo
        {
            get { return (null != GetSPNo) ? GetSPNo() : new int?(); }
            set { }
        }
        /// <summary>Gets or sets Yarn Type.</summary>
        public string YarnType
        {
            get { return (null != GetYarnType) ? GetYarnType() : null; }
            set { }
        }
        /// <summary>Gets or sets Test No. (N1, N2, N3)</summary>
        public int No { get; set; }

        #endregion

        #region N

        /// <summary>Gets or sets Test Value.</summary>
        public decimal? N
        {
            get { return (null != GetN) ? GetN() : new decimal?(); }
            set
            {
                if (null != SetN)
                {
                    SetN(value);
                    Raise(() => this.N);
                }
            }
        }

        #endregion

        #region CaptionN (For Runtime binding)

        /// <summary>Gets N Display Caption.</summary>
        public string CaptionN
        {
            get { return "N" + No.ToString(); }
            set { }
        }

        #endregion

        #endregion
    }

    #endregion

    #region ImportNTestProperty

    /// <summary>
    /// The ImportNTestProperty class.
    /// </summary>
    public class ImportNTestProperty : NInpc
    {
        #region Internal Variables

        private Func<int?> _GetSPNo;
        private Func<string> _GetYarnType;
        private List<Func<decimal?>> _GetNs;
        private List<Action<decimal?>> _SetNs;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImportNTestProperty() : base() 
        {
            #region Init Get/Set link methods

            // Get SPNo
            _GetSPNo = () => { return this.SPNo; };
            // Get Yarn Type
            _GetYarnType = () => { return this.YarnType; };
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

            #endregion

            BuildItems(0); // create empty items.
        }

        #endregion

        #region Private Methods

        private void BuildItems(int noOfSample)
        {
            lock (this)
            {
                Items = new List<ImportNTestPropertyItem>();
                ImportNTestPropertyItem item;
                for (int i = 1; i <= 7; i++)
                {
                    if (i > noOfSample) continue; // skip if more than allow no of sample.

                    item = new ImportNTestPropertyItem();
                    // set Sample No.
                    item.No = i;
                    // assign method pointer to Get SPNo/Need SP
                    item.GetSPNo = (null != _GetSPNo) ? _GetSPNo : null;
                    item.GetYarnType = (null != _GetYarnType) ? _GetYarnType : null;
                    // assign method pointer to Get/Set N
                    item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                    item.SetN = (null != _SetNs) ? _SetNs[i - 1] : null;

                    Items.Add(item);
                }
            }
        }

        private void ValueChange([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return;
            if (null == this.Items)
                return; // No items.

            if (propertyName.StartsWith("N"))
            {
                string sIdx = propertyName.Replace("N", string.Empty);
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    // Note: N1 -> index must be 0, N2  -> index must be 1 so need decrease index by 1.
                    idx--; // remove by 1 for zero based

                    if (idx < 0 || idx >= this.Items.Count) return;
                    this.Items[idx].RaiseNChanges();

                    CalcAvg();
                }
            }
            else if (propertyName.StartsWith("SPNo"))
            {
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseSPNoChanges();
                    }
                }
            }
            else if (propertyName.StartsWith("YarnType"))
            {
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseYarnTypeChanges();
                    }
                }
            }
        }

        private void CalcAvg()
        {
            decimal total = decimal.Zero;
            int iCnt = 0;
            lock (this)
            {
                if (null != this.Items)
                {
                    foreach (var item in this.Items)
                    {
                        if (item.N.HasValue)
                        {
                            // Has N value and no R value so use N to calc avg
                            total += item.N.Value;
                            ++iCnt;
                        }
                    }
                }
            }
            // Calc average value.
            this.Avg = (iCnt > 0) ? (total / iCnt) : new decimal?();

            if (null != ValueChanges) ValueChanges();
        }

        #endregion

        #region Callback Actions

        internal Action ValueChanges { get; set; }

        #endregion

        #region Public Properties

        #region LotNo/SPNo/NoOfSample/YarnType

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
        /// <summary>Gets or sets SP No.</summary>
        public int? SPNo
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample
        {
            get { return (null != Items) ? Items.Count : 0; }
            set
            {
                BuildItems(value);
                // Raise events
                this.Raise(() => this.NoOfSample);
                this.Raise(() => this.Items);
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
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets ElongN.</summary>
        public string ElongN
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Normal Test (1-7)

        /// <summary>Gets or sets N1 value.</summary>
        public decimal? N1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N2 value.</summary>
        public decimal? N2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N3 value.</summary>
        public decimal? N3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N4 value.</summary>
        public decimal? N4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N5 value.</summary>
        public decimal? N5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N6 value.</summary>
        public decimal? N6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N7 value.</summary>
        public decimal? N7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Avg

        /// <summary>Gets or sets Avg value.</summary>
        public decimal? Avg
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }

        #endregion

        #region Items

        /// <summary>
        /// Gets N Items.
        /// </summary>
        public List<ImportNTestPropertyItem> Items { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region UniTestTensileElongation

    public class UniTestTensileElongation : NInpc
    {
        #region Private Methods

        private void ParseComment1()
        {
            string[] sps = Comment1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            NoOfSample = (null != sps) ? sps.Length : 0;
            int sp;
            for (int i = 0; i < sps.Length; i++) 
            { 
                switch (i)
                {
                    case 0: 
                        {
                            SP1 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 1:
                        {
                            SP2 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 2:
                        {
                            SP3 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 3:
                        {
                            SP4 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 4:
                        {
                            SP5 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 5:
                        {
                            SP6 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 6:
                        {
                            SP7 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Test Date.</summary>
        public DateTime? TestDate { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        /// <summary>Gets or sets Sample Name (aka. Product Name).</summary>
        public string SampleName { get; set; }
        public string LotNo { get; set; }

        public decimal? Preparation { get; set; }

        public string Operator { get; set; }

        public string Comment1 
        { 
            get { return Get<string>(); }
            set 
            {
                Set(value, () => 
                {
                    ParseComment1();
                });
            }
        }

        public string TestType { get; set; }

        public int NoOfSample { get; set; }

        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }

        #endregion

        #region Static Methods

        public static UniTestTensileElongation Import(string fileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            UniTestTensileElongation inst = null;

            var cfg = new ExcelConfig();
            cfg.DataSource.FileName = fileName;
            cfg.DataSource.HeaderInFirstRow = false;
            cfg.DataSource.Driver = ExcelDriver.Jet;
            cfg.DataSource.IMexMode = IMex.ImportMode;

            var conn = new NDbConnection();
            conn.Config = cfg;
            if (conn.Connect())
            {
                try
                {
                    #region UniTest.TensileCond

                    string sheetName1 = "UniTest.TensileCond";
                    var table1 = conn.Query("Select * from [" + sheetName1 + "$]").Result;
                    if (null != table1)
                    {
                        inst = new UniTestTensileElongation();
                        foreach (DataRow row in table1.Rows)
                        {
                            if (null == row) continue;
                            string pName = null != row["F2"] ? row["F2"].ToString() : null;
                            object pValue = null != row["F3"] ? row["F3"] : null;
                            if (string.IsNullOrEmpty(pName)) continue;

                            if (pName == "Test date")
                            {
                                inst.TestDate = (null != pValue) ? 
                                    DateTime.ParseExact(pValue.ToString(), "yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo) : new DateTime?();
                            }
                            else if (pName == "Temperature")
                            {
                                inst.Temperature = (null != pValue) ?
                                    decimal.Parse(pValue.ToString()) : new decimal?();
                            }
                            else if (pName == "Humidity")
                            {
                                inst.Humidity = (null != pValue) ?
                                    decimal.Parse(pValue.ToString()) : new decimal?();
                            }
                            else if (pName == "Sample name")
                            {
                                inst.SampleName = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Lot No.")
                            {
                                inst.LotNo = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Preparation")
                            {
                                inst.Preparation = (null != pValue) ?
                                    decimal.Parse(pValue.ToString()) : new decimal?();
                            }
                            else if (pName == "Operator")
                            {
                                inst.Operator = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Comment 1")
                            {
                                inst.Comment1 = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Test type")
                            {
                                inst.TestType = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                        }
                        table1.Dispose();
                    }
                    table1 = null;

                    #endregion

                    // Check can load data
                    if (null == inst)
                    {
                        return inst;
                    }


                    #region Data

                    string sheetName2 = "Data";
                    var table2 = conn.Query("Select * from [" + sheetName2 + "$]").Result;
                    if (null != table2)
                    {

                    }
                    table2 = null;

                    #endregion
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                }
                finally
                {

                }
            }
            conn.Disconnect();
            conn.Dispose();
            conn = null;

            return inst;
        }

        #endregion
    }

    #endregion
}
