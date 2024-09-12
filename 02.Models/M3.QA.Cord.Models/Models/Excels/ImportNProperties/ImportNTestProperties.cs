#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.Models;
using NLib.Reflection;
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

        protected internal void RaiseR1Changes()
        {
            // Raise relelated events
            Raise(() => this.R1);
        }

        protected internal void RaiseR2Changes()
        {
            // Raise relelated events
            Raise(() => this.R2);
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
        // R1 Gets/Sets
        protected internal Func<decimal?> GetR1 { get; set; }
        protected internal Action<decimal?> SetR1 { get; set; }
        // R2 Gets/Sets
        protected internal Func<decimal?> GetR2 { get; set; }
        protected internal Action<decimal?> SetR2 { get; set; }

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

        #region R1/R2

        /// <summary>Gets or sets Re Test 1 Value.</summary>
        public decimal? R1
        {
            get { return (null != GetR1) ? GetR1() : new decimal?(); }
            set
            {
                if (null != SetR1)
                {
                    SetR1(value);
                    // Raise events
                    Raise(() => this.R1);
                    Raise(() => this.VisibleR1);
                }
            }
        }
        /// <summary>Gets or sets Re Test 2 Value.</summary>
        public decimal? R2
        {
            get { return (null != GetR2) ? GetR2() : new decimal?(); }
            set
            {
                if (null != SetR2)
                {
                    SetR2(value);
                    // Raise events
                    Raise(() => this.R2);
                    Raise(() => this.VisibleR2);
                }
            }
        }

        #endregion

        #region CaptionN/R (For Runtime binding)

        /// <summary>Gets N Display Caption.</summary>
        public string CaptionN
        {
            get { return "N" + No.ToString(); }
            set { }
        }
        /// <summary>Gets R Display Caption.</summary>
        public string CaptionR
        {
            get { return "R" + No.ToString(); }
            set { }
        }

        #endregion

        #region VisibleR1/VisibleR2

        /// <summary>Gets Visibility of Re Test 1.</summary>
        public Visibility VisibleR1
        {
            get
            {
                // Note: In some case some row has only R data so need to display it.
                return (R1.HasValue) ? Visibility.Visible : Visibility.Collapsed;
            }
            set { }
        }
        /// <summary>Gets Visibility of Re Test 2.</summary>
        public Visibility VisibleR2
        {
            get
            {
                // Note: In some case some row has only R data so need to display it.
                return (R2.HasValue) ? Visibility.Visible : Visibility.Collapsed;
            }
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
        private List<Func<decimal?>> _GetR1s;
        private List<Action<decimal?>> _SetR1s;
        private List<Func<decimal?>> _GetR2s;
        private List<Action<decimal?>> _SetR2s;

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
            // Get R1
            _GetR1s = new List<Func<decimal?>>()
            {
                () => { return this.N1R1; },
                () => { return this.N2R1; },
                () => { return this.N3R1; },
                () => { return this.N4R1; },
                () => { return this.N5R1; },
                () => { return this.N6R1; },
                () => { return this.N7R1; }
            };
            // Set R1
            _SetR1s = new List<Action<decimal?>>()
            {
                (value) => { this.N1R1 = value; },
                (value) => { this.N2R1 = value; },
                (value) => { this.N3R1 = value; },
                (value) => { this.N4R1 = value; },
                (value) => { this.N5R1 = value; },
                (value) => { this.N6R1 = value; },
                (value) => { this.N7R1 = value; }
            };
            // Get R2
            _GetR2s = new List<Func<decimal?>>()
            {
                () => { return this.N1R2; },
                () => { return this.N2R2; },
                () => { return this.N3R2; },
                () => { return this.N4R2; },
                () => { return this.N5R2; },
                () => { return this.N6R2; },
                () => { return this.N7R2; }
            };
            // Set R2
            _SetR2s = new List<Action<decimal?>>()
            {
                (value) => { this.N1R2 = value; },
                (value) => { this.N2R2 = value; },
                (value) => { this.N3R2 = value; },
                (value) => { this.N4R2 = value; },
                (value) => { this.N5R2 = value; },
                (value) => { this.N6R2 = value; },
                (value) => { this.N7R2 = value; }
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
                    // assign method pointer to Get/Set R
                    item.GetR1 = (null != _GetR1s) ? _GetR1s[i - 1] : null;
                    item.SetR1 = (null != _SetR1s) ? _SetR1s[i - 1] : null;
                    item.GetR2 = (null != _GetR2s) ? _GetR2s[i - 1] : null;
                    item.SetR2 = (null != _SetR2s) ? _SetR2s[i - 1] : null;

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

            if (propertyName.StartsWith("N") && propertyName.Length > 1)
            {
                string sIdx = propertyName.Substring(1, 1);

                // Note: N1 -> index must be 0, N2  -> index must be 1 so need decrease index by 1.
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    idx--; // Make zero index

                    if (idx < 0 || idx >= this.Items.Count) return;
                    if (propertyName.Contains("R1"))
                    {
                        // R1 only
                        this.Items[idx].RaiseR1Changes();
                        CalcAvg();
                    }
                    else if (propertyName.Contains("R2"))
                    {
                        // R2 only
                        this.Items[idx].RaiseR2Changes();
                        CalcAvg();
                    }
                    else
                    {
                        // N only
                        this.Items[idx].RaiseNChanges();
                        CalcAvg();
                    }
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
                        if (item.R1.HasValue || item.R2.HasValue)
                        {
                            if (item.R1.HasValue)
                            {
                                total += item.R1.Value;
                                ++iCnt;
                            }
                            if (item.R2.HasValue)
                            {
                                total += item.R2.Value;
                                ++iCnt;
                            }
                        }
                        else
                        {
                            // R has no value so use N
                            if (item.N.HasValue)
                            {
                                // Has N value and no R value so use N to calc avg
                                total += item.N.Value;
                                ++iCnt;
                            }
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
        /// <summary>Gets or sets Sample Type.</summary>
        public string SampleType
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    //ValueChange();
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

        #region Re Test 1 (1-7)

        /// <summary>Gets or sets N1R1 value.</summary>
        public decimal? N1R1
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
        /// <summary>Gets or sets N2R1 value.</summary>
        public decimal? N2R1
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
        /// <summary>Gets or sets N3R1 value.</summary>
        public decimal? N3R1
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
        /// <summary>Gets or sets N4R1 value.</summary>
        public decimal? N4R1
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
        /// <summary>Gets or sets N5R1 value.</summary>
        public decimal? N5R1
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
        /// <summary>Gets or sets N6R1 value.</summary>
        public decimal? N6R1
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
        /// <summary>Gets or sets N7R1 value.</summary>
        public decimal? N7R1
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

        #region Re Test 2 (1-7)

        /// <summary>Gets or sets N1R2 value.</summary>
        public decimal? N1R2
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
        /// <summary>Gets or sets N2R2 value.</summary>
        public decimal? N2R2
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
        /// <summary>Gets or sets N3R2 value.</summary>
        public decimal? N3R2
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
        /// <summary>Gets or sets N4R2 value.</summary>
        public decimal? N4R2
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
        /// <summary>Gets or sets N5R2 value.</summary>
        public decimal? N5R2
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
        /// <summary>Gets or sets N6R2 value.</summary>
        public decimal? N6R2
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
        /// <summary>Gets or sets N7R2 value.</summary>
        public decimal? N7R2
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
}
