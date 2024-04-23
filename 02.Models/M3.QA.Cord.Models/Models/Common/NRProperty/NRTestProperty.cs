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
    #region NRTestProperty

    /// <summary>
    /// The NR Test Property class
    /// </summary>
    public class NRTestProperty : NInpc
    {
        #region Internal Variables

        private Func<int?> _GetSPNo;
        private Func<bool> _GetNeedSP;
        private Func<string> _GetYarnType;
        private List<Func<decimal?>> _GetNs;
        private List<Action<decimal?>> _SetNs;
        private List<Func<decimal?>> _GetRs;
        private List<Action<decimal?>> _SetRs;
        private List<Func<bool>> _GetNOuts;
        private List<Action<bool>> _SetNOuts;
        private List<Func<bool>> _GetROuts;
        private List<Action<bool>> _SetROuts;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRTestProperty() : base()
        {
            #region Init Get/Set link methods

            // Get SPNo
            _GetSPNo = () => { return this.SPNo; };
            // Get NeedSP
            _GetNeedSP = () => { return this.NeedSP; };
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
            // Get N Out
            _GetNOuts = new List<Func<bool>>()
            {
                () => { return this.NOut1; },
                () => { return this.NOut2; },
                () => { return this.NOut3; },
                () => { return this.NOut4; },
                () => { return this.NOut5; },
                () => { return this.NOut6; },
                () => { return this.NOut7; }
            };
            // Set N Out
            _SetNOuts = new List<Action<bool>>()
            {
                (value) => { this.NOut1 = value; },
                (value) => { this.NOut2 = value; },
                (value) => { this.NOut3 = value; },
                (value) => { this.NOut4 = value; },
                (value) => { this.NOut5 = value; },
                (value) => { this.NOut6 = value; },
                (value) => { this.NOut7 = value; }
            };
            // Get R Out
            _GetROuts = new List<Func<bool>>()
            {
                () => { return this.ROut1; },
                () => { return this.ROut2; },
                () => { return this.ROut3; },
                () => { return this.ROut4; },
                () => { return this.ROut5; },
                () => { return this.ROut6; },
                () => { return this.ROut7; }
            };
            // Set R Out
            _SetROuts = new List<Action<bool>>()
            {
                (value) => { this.ROut1 = value; },
                (value) => { this.ROut2 = value; },
                (value) => { this.ROut3 = value; },
                (value) => { this.ROut4 = value; },
                (value) => { this.ROut5 = value; },
                (value) => { this.ROut6 = value; },
                (value) => { this.ROut7 = value; }
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
                Items = new List<NRTestPropertyItem>();
                NRTestPropertyItem item;
                for (int i = 1; i <= 7; i++)
                {
                    if (i > noOfSample) continue; // skip if more than allow no of sample.

                    item = new NRTestPropertyItem();
                    // set Sample No.
                    item.No = i;
                    // assign method pointer to Get SPNo/Need SP
                    item.GetSPNo = (null != _GetSPNo) ? _GetSPNo : null;
                    item.GetNeedSP = (null != _GetNeedSP) ? _GetNeedSP : null;
                    item.GetYarnType = (null != _GetYarnType) ? _GetYarnType : null;
                    // assign method pointer to Get/Set N
                    item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                    item.SetN = (null != _SetNs) ? _SetNs[i - 1] : null;
                    // assign method pointer to Get/Set R
                    item.GetR = (null != _GetRs) ? _GetRs[i - 1] : null;
                    item.SetR = (null != _SetRs) ? _SetRs[i - 1] : null;
                    // assign method pointer to Get/Set O
                    item.GetNOut = (null != _GetNOuts) ? _GetNOuts[i - 1] : null;
                    item.SetNOut = (null != _SetNOuts) ? _SetNOuts[i - 1] : null;

                    item.GetROut = (null != _GetROuts) ? _GetROuts[i - 1] : null;
                    item.SetROut = (null != _SetROuts) ? _SetROuts[i - 1] : null;

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
                    CheckSpec();
                    CalcAvg();
                }
            }
            else if (propertyName.StartsWith("R")) 
            {
                string sIdx = propertyName.Replace("R", string.Empty);
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    // Note: R1 -> index must be 0, R2  -> index must be 1 so need decrease index by 1.
                    idx--; // remove by 1 for zero based

                    if (idx < 0 || idx >= this.Items.Count) return;
                    this.Items[idx].RaiseRChanges();
                    CheckSpec();
                    CalcAvg();
                }
            }
            else if (propertyName.StartsWith("NOut"))
            {
                string sIdx = propertyName.Replace("NOut", string.Empty);
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    // Note: O1 -> index must be 0, O2  -> index must be 1 so need decrease index by 1.
                    idx--; // remove by 1 for zero based

                    if (idx < 0 || idx >= this.Items.Count) return;
                    this.Items[idx].RaiseNOutChanges();
                }
            }
            else if (propertyName.StartsWith("ROut"))
            {
                string sIdx = propertyName.Replace("ROut", string.Empty);
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    // Note: O1 -> index must be 0, O2  -> index must be 1 so need decrease index by 1.
                    idx--; // remove by 1 for zero based

                    if (idx < 0 || idx >= this.Items.Count) return;
                    this.Items[idx].RaiseROutChanges();
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
            else if (propertyName.StartsWith("NeedSP"))
            {
                this.Raise(() => this.EnableTest);
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseNeedSPChanges();
                    }
                }
            }
            else if (propertyName.StartsWith("YarnType"))
            {
                this.Raise(() => this.EnableTest);
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
                        if (item.N.HasValue && !item.R.HasValue)
                        {
                            // Has N value and no R value so use N to calc avg
                            total += item.N.Value;
                            ++iCnt;
                        }
                        if (item.R.HasValue)
                        {
                            // Either N has value or not but when R value exists so use R to calc avg
                            total += item.R.Value;
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

        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected virtual void CheckSpec()
        {

        }

        #endregion

        #region Callback Actions

        internal Action ValueChanges { get; set; }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo { get; set; }
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
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
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
                    ValueChange();
                });
            }
        }

        #endregion

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        /// <summary>Gets or sets is enable to enter test data.</summary>
        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
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

        #region Re Test (1-7)

        /// <summary>Gets or sets R1 value.</summary>
        public decimal? R1
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
        /// <summary>Gets or sets R2 value.</summary>
        public decimal? R2
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
        /// <summary>Gets or sets R3 value.</summary>
        public decimal? R3
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
        /// <summary>Gets or sets R4 value.</summary>
        public decimal? R4
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
        /// <summary>Gets or sets R5 value.</summary>
        public decimal? R5
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
        /// <summary>Gets or sets R6 value.</summary>
        public decimal? R6
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
        /// <summary>Gets or sets R7 value.</summary>
        public decimal? R7
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

        #region Normal OutSpec (1-7)

        /// <summary>Gets or sets Normal OutSpec1 value.</summary>
        public bool NOut1
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec2 value.</summary>
        public bool NOut2
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec3 value.</summary>
        public bool NOut3
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec4 value.</summary>
        public bool NOut4
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec5 value.</summary>
        public bool NOut5
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec6 value.</summary>
        public bool NOut6
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec7 value.</summary>
        public bool NOut7
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Re-Test OutSpec (1-7)

        /// <summary>Gets or sets Re-Test OutSpec1 value.</summary>
        public bool ROut1
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test OutSpec2 value.</summary>
        public bool ROut2
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test OutSpec3 value.</summary>
        public bool ROut3
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test OutSpec4 value.</summary>
        public bool ROut4
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test OutSpec5 value.</summary>
        public bool ROut5
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test OutSpec6 value.</summary>
        public bool ROut6
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test OutSpec7 value.</summary>
        public bool ROut7
        {
            get { return Get<bool>(); }
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
        /// Gets NR Items.
        /// </summary>
        public List<NRTestPropertyItem> Items { get; set; }

        #endregion

        #endregion

        #region Static Methods

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(NRTestProperty src, NRTestProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;

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
        }

        #endregion
    }

    #endregion
}
